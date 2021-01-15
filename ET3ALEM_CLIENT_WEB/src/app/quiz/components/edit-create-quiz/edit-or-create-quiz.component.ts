import { Component, OnInit, ViewChild, ViewChildren, QueryList, AfterViewInit } from '@angular/core';

// import * as Editor from '../../../../assets/js/ck-editor-custom-build/ckeditor.js'
import { FormControl, Validators } from '@angular/forms';
import * as moment from 'moment'
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions.js';
import { ToastrService } from 'ngx-toastr';
import { Quiz } from '../../Model/quiz.js';
import { QuizService } from '../../services/quiz.service.js';
import { ActivatedRoute, Router } from '@angular/router';
import { RichTextEditorComponent } from 'src/app/Shared/modules/shared-components/rich-text-editor/rich-text-editor.component.js';
import { Question } from 'src/app/question/Models/question.js';
import { MultipleChoiceQuestion } from 'src/app/question/Models/mcq.js';
import { EditOrCreateQuestionHeaderComponent } from 'src/app/question/edit-create-question/Edit-Create-QuestionHeader/edit-or-create-questionHeader.component.js';
import { QuizQuestion } from '../../Model/quizQuestion.js';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper.js';
import { plainToClass } from 'class-transformer';

@Component({
  selector: 'app-create-quiz',
  templateUrl: './edit-or-create-quiz.component.html',
  styleUrls: ['./edit-or-create-quiz.component.css']
})


export class EditOrCreateQuizComponent extends ExtraFormOptions implements OnInit {

  @ViewChild('RichTextEditorComponent') private richTextComponent: RichTextEditorComponent;

  @ViewChildren('CreateQuestionComponent') createQuestionComponents: QueryList<EditOrCreateQuestionHeaderComponent>;

  //manage create and edit modes
  mode: mode = mode.create;
  currentQuiz: Quiz;

  questions: Array<any> = [];
  deletedQuizQuestions: Array<any> = [];

  isLoaded: boolean = false;
  richTextLoaded: boolean = false;

  today: Date = moment().toDate();

  quizTitle = new FormControl('', [Validators.required]);
  quizInstructions = new FormControl();
  durationHours = new FormControl(1, [Validators.max(5), Validators.min(0)]);
  durationMinutes = new FormControl(0, [Validators.max(59), Validators.min(0)]);
  unlimitedTime = new FormControl(false);
  dueStart = new FormControl(moment().toDate());
  dueEnd = new FormControl(moment().add(3, 'days').toDate());
  noDueDate = new FormControl(false);

  constructor(private toastr: ToastrService, private quizService: QuizService, private route: ActivatedRoute, private router: Router) {
    super();
  }

  ngOnInit(): void {
    let id: Number = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.mode = mode.edit;
      this.quizService.getQuiz(id).subscribe(
        res => {
          this.currentQuiz = plainToClass(Quiz, res);
          this.questions = this.currentQuiz.QuizQuestions.map(x => x.Question);
          this.setFormControls();
          this.isLoaded = true;
        },
        err => {
          this.toastr.error('unable to open quiz');
          console.error(err);
        }
      )
    }
    else {
      this.isLoaded = true;
    }
  }

  setFormControls() {
    this.quizTitle.setValue(this.currentQuiz.Name)
    this.quizInstructions.setValue(this.currentQuiz.Instructions);
    this.durationHours.setValue(Math.floor(this.currentQuiz.DurationSeconds / 3600));
    this.durationMinutes.setValue(this.currentQuiz.DurationSeconds % 3600 / 60);
    this.unlimitedTime.setValue(this.currentQuiz.UnlimitedTime);
    this.dueStart.setValue(Helper.getLocalDateFromUTC(this.currentQuiz.StartDate));
    this.dueEnd.setValue(Helper.getLocalDateFromUTC(this.currentQuiz.EndDate));
    this.noDueDate.setValue(this.currentQuiz.NoDueDate);

    if (this.currentQuiz.UnlimitedTime) {
      this.durationHours.disable();
      this.durationMinutes.disable();
    }

    if (this.currentQuiz.NoDueDate) {
      this.dueEnd.disable();
    }
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
    //check if question existed in original quz questions
    let quizQuestionToDelete: QuizQuestion = this.currentQuiz.QuizQuestions.find(qQ => qQ.QuestionId == question.Id);
    if (quizQuestionToDelete) {
      quizQuestionToDelete.Id = quizQuestionToDelete.Id * -1;
    }

    let index = this.questions.findIndex(q => q.Id == question.Id);
    if (index > -1)
      this.questions.splice(index, 1);
  }

  getGradeFromQuestion(question: Question): number{
    const grade = this.currentQuiz.QuizQuestions.find(qq => qq.QuestionId == question.Id)?.Grade;
    return grade ?? 1;
  }

  validate(): boolean {
    //check duration
    if ((this.durationHours.value ?? 0) * 60 + (this.durationMinutes.value ?? 0) < 5 && !this.unlimitedTime.value) {
      this.toastr.error('duration must be at least 5 minutes or unlimited time');
      return false;
    }
    return true;
  }

  async createQuiz() {
    if (!this.validate())
      return;

    await this.richTextComponent.removeUnusedImages();

    let quizQuestions: Array<QuizQuestion> = [];

    await Promise.all(this.createQuestionComponents.map(async (component) => {
      let question = await component.saveQuestion(this.mode);
      let grade = component.getGrade();
      quizQuestions.push(new QuizQuestion(question, grade));
    }));

    this.currentQuiz = new Quiz(0, '', this.quizTitle.value, this.quizInstructions.value, (this.durationHours.value * 3600 + this.durationMinutes.value * 60), this.unlimitedTime.value, Helper.getUTCFromLocal(this.dueStart.value), Helper.getUTCFromLocal(this.dueEnd.value), this.noDueDate.value, quizQuestions);

    console.log('current quiz', this.currentQuiz);

    this.quizService.createQuiz(this.currentQuiz).subscribe(
      (quiz: Quiz) => {
        this.toastr.success('Quiz Created');
        this.router.navigate(['../manage'], { relativeTo: this.route })
      },
      () => {
        this.toastr.error('Quiz not created');
      }
    )
  }

  async updateQuiz() {
    if (!this.validate())
      return;

    await this.richTextComponent.removeUnusedImages();


    await Promise.all(this.createQuestionComponents.map(async (component, i) => {
      let question = await component.saveQuestion(this.questions[i].Id > 0 ? mode.edit : mode.create);
      this.questions[i] = Helper.deepCopy(question);
      let grade = component.getGrade();

      //check if question already existed in quiz questions
      let questionIndex = this.currentQuiz.QuizQuestions.map(x => x.Question.Id).indexOf(question.Id);
      if (questionIndex > -1) {
        let currentQuizQuestion = this.currentQuiz.QuizQuestions[questionIndex]
        currentQuizQuestion.Question = question;
        currentQuizQuestion.Grade = component.getGrade();
      }

      //if not, insert it
      else {
        this.currentQuiz.QuizQuestions.push(new QuizQuestion(question, grade));
      }
    }));

    this.currentQuiz.updateQuiz(this.quizTitle.value, this.quizInstructions.value, (this.durationHours.value * 3600 + this.durationMinutes.value * 60), this.unlimitedTime.value, Helper.getUTCFromLocal(this.dueStart.value), Helper.getUTCFromLocal(this.dueEnd.value), this.noDueDate.value, this.currentQuiz.QuizQuestions);

    this.quizService.updateQuiz(this.currentQuiz).subscribe(
      () => {
        this.toastr.success('Quiz Updated');
        this.router.navigate(['../../manage'], { relativeTo: this.route });
      },
      () => {
        this.toastr.error('Quiz not updated');
      }
    )
  }

}

export enum mode {
  edit,
  create
}
