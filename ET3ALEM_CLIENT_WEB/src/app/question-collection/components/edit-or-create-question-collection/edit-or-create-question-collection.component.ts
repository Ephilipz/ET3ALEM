import { Component, OnInit, ViewChild, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import * as moment from 'moment';
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions.js';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { RichTextEditorComponent } from 'src/app/Shared/modules/shared-components/rich-text-editor/rich-text-editor.component.js';
import { Question } from 'src/app/question/Models/question.js';
import { MultipleChoiceQuestion } from 'src/app/question/Models/mcq.js';
import { EditOrCreateQuestionHeaderComponent } from 'src/app/question/edit-create-question/Edit-Create-QuestionHeader/edit-or-create-questionHeader.component.js';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper.js';
import { plainToClass } from 'class-transformer';
import { isUnionTypeNode } from 'typescript';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { QuestionCollectionService } from '../../question-collection.service';
import { QuestionCollection } from '../../models/question-collection';

@Component({
  selector: 'app-edit-or-create-question-collection',
  templateUrl: './edit-or-create-question-collection.component.html',
  styleUrls: ['./edit-or-create-question-collection.component.css']
})
export class EditOrCreateQuestionCollectionComponent extends ExtraFormOptions implements OnInit {

  @ViewChildren('CreateQuestionComponent') createQuestionComponents: QueryList<EditOrCreateQuestionHeaderComponent>;

  // manage create and edit modes
  mode: mode = mode.create;
  currentCollection: QuestionCollection;
  questions: Array<any> = [];
  deletedQuestions: Array<any> = [];
  isLoaded: boolean = false;
  today: Date = moment().toDate();
  collectionName = new FormControl('', [Validators.required]);
  includedQuestions = new FormControl(null);

  constructor(
    private toastr: ToastrService,
    private questionCollectionService: QuestionCollectionService,
    private route: ActivatedRoute,
    private router: Router) {
    super();
  }

  ngOnInit(): void {
    const id: number = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.mode = mode.edit;
      this.questionCollectionService.getCollection(id).subscribe(
        res => {
          this.currentCollection = plainToClass(QuestionCollection, res);
          this.questions = this.currentCollection.Questions;
          this.setFormControls();
          this.isLoaded = true;
        },
        err => {
          this.toastr.error('unable to open collection');
          console.error(err);
        }
      );
    }
    else {
      this.isLoaded = true;
    }
  }

  setFormControls() {
    this.collectionName.setValue(this.currentCollection.Name);
  }

  toggleDisable(checked: boolean, list: Array<string>) {
    list.forEach((x) => {
      if (checked) {
        this[x].disable();
      }
      else {
        this[x].enable();
      }
    });
  }

  subtractDays(date: Date, days = 1) {
    return moment(date).subtract(days, 'days').toDate();
  }

  addQuestion() {
    this.questions.push(new MultipleChoiceQuestion(Helper.randomInteger(0, 100) * -1));
  }

  deleteQuestion(question: Question) {
    // check if question existed in original questions
    const questionToDelete: Question = this.currentCollection?.Questions.find(q => q.Id == question.Id);
    if (questionToDelete) {
      questionToDelete.Id = questionToDelete.Id * -1;
      questionToDelete.Id *= -1;
    }

    const index = this.questions.findIndex(q => q.Id == question.Id);
    if (index > -1) {
      this.questions.splice(index, 1);
    }
  }

  duplicateQuestion(question: Question, i: number) {
    const oldQuestion: Question = this.createQuestionComponents.find((_, index) => index == i)?.getQuestion();
    if (!oldQuestion)
      return;
    const newQuestion = oldQuestion.duplicateQuestion();
    this.questions.push(newQuestion);
  }

  /**
   * For drag and drop event in reordering questions
   * @param event
   */
  drop(event: CdkDragDrop<Question[]>) {
    const questionComponentsArray = this.createQuestionComponents.toArray();
    this.questions[event.previousIndex] = questionComponentsArray[event.previousIndex].getQuestion();
    this.questions[event.currentIndex] = questionComponentsArray[event.currentIndex].getQuestion();
    moveItemInArray(this.questions, event.previousIndex, event.currentIndex);
    const temp = Helper.deepCopy(this.questions);
    this.questions = null;
    this.questions = temp;
  }

  async createCollection() {
    if (!this.validate())
      return;
    const questions: Array<Question> = [];

    await Promise.all(this.createQuestionComponents.map(async (component, i) => {
      const question = await component.saveQuestion(this.mode);
      question.Id = 0;
      questions.push(question);
    }));

    this.currentCollection = new QuestionCollection(0, this.collectionName.value, questions);
    this.questionCollectionService.createCollection(this.currentCollection).subscribe(
      (collection: QuestionCollection) => {
        this.toastr.success('Question Collection Created');
        this.router.navigate(['../'], { relativeTo: this.route });
      },
      () => {
        this.toastr.error('Question Collection not created');
      }
    );
  }

  async updateCollection() {
    if (!this.validate())
      return;

    const questions: Array<Question> = [];

    await Promise.all(this.createQuestionComponents.map(async (component, i) => {
      const question = await component.saveQuestion(this.mode);
      question.Id = 0;
      questions.push(question);
    }));

    this.currentCollection.Name = this.collectionName.value;
    this.currentCollection.Questions = questions;

    this.questionCollectionService.updateCollection(this.currentCollection).subscribe(
      () => {
        this.toastr.success('Question Collection Updated');
        this.router.navigate(['../../'], { relativeTo: this.route });
      },
      () => {
        this.toastr.error('Question Collection not updated');
      }
    )
  }
  validate() {
    if (!this.collectionName.valid) {
      this.toastr.error('collection name must be unique');
      return false;
    }
    return true;
  }
}

export enum mode {
  edit,
  create
}

