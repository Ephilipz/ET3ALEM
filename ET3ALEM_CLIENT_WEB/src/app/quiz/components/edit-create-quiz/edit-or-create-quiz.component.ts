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
import { EditOrCreateQuestionComponent } from 'src/app/question/create-question/edit-or-create-question.component.js';
import { QuizQuestion } from '../../Model/quizQuestion.js';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper.js';

@Component({
  selector: 'app-create-quiz',
  templateUrl: './edit-or-create-quiz.component.html',
  styleUrls: ['./edit-or-create-quiz.component.css']
})


export class EditOrCreateQuizComponent extends ExtraFormOptions implements OnInit {

  @ViewChild('RichTextEditorComponent') private richTextComponent: RichTextEditorComponent;

  @ViewChildren('CreateQuestionComponent') createQuestionComponents: QueryList<EditOrCreateQuestionComponent>;

  //manage create and edit modes
  mode: mode = mode.create;
  currentQuiz: Quiz;

  questions: Array<any> = [];

  isLoaded: boolean = false;

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
          this.currentQuiz = res;
          this.questions = this.currentQuiz.QuizQuestions.map(x => Helper.getSpecificQuestion(x.Question));
          this.setFormControls();
          this.isLoaded = true;
          console.log('current quiz retrieved ', this.currentQuiz);
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
    this.quizInstructions.setValue(this.currentQuiz.instructions);
    this.durationHours.setValue(Math.floor(this.currentQuiz.DurationSeconds / 3600));
    this.durationMinutes.setValue(this.currentQuiz.DurationSeconds % 3600 / 60);
    this.unlimitedTime.setValue(this.currentQuiz.UnlimitedTime);
    this.dueStart.setValue(this.currentQuiz.StartDate);
    this.dueEnd.setValue(this.currentQuiz.EndDate);
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
    let index = this.questions.findIndex(q => q.Id == question.Id);
    if (index > -1)
      this.questions.splice(index, 1);
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

    await this.createQuestionComponents.forEach(async component => {
      component.saveQuestion(this.mode).then(
        question => { quizQuestions.push(new QuizQuestion(question)) }
      );
    });

    this.currentQuiz = new Quiz(0, '', this.quizTitle.value, this.quizInstructions.value, (this.durationHours.value * 3600 + this.durationMinutes.value * 60), this.unlimitedTime.value, this.dueStart.value, this.dueEnd.value, this.noDueDate.value, quizQuestions);

    console.log('current quiz', this.currentQuiz);

    this.quizService.createQuiz(this.currentQuiz).subscribe(
      (quiz: Quiz) => {
        this.toastr.success('Quiz Created');
        this.router.navigate(['../../manage'],{relativeTo: this.route})
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

    let questions = [];

    await this.createQuestionComponents.forEach(async (component, i) => {
      let question = await component.saveQuestion(this.questions[i].Id > 0 ? mode.edit : mode.create);
      questions.push(Object.assign({}, question));
      this.questions[i] = Object.assign({}, question);
    });

    this.questions.forEach(question => {
      //check if question already existed in quiz questions
      let questionIndex = this.currentQuiz.QuizQuestions.map(x => x.Question.Id).indexOf(question.Id);
      if (questionIndex > -1) {
        this.currentQuiz.QuizQuestions[questionIndex].Question = question;
      }

      //if not, insert it
      else {
        this.currentQuiz.QuizQuestions.push(new QuizQuestion(question));
      }
    });

    this.currentQuiz = Quiz.quizFromExisting(this.currentQuiz, this.quizTitle.value, this.quizInstructions.value, (this.durationHours.value * 3600 + this.durationMinutes.value * 60), this.unlimitedTime.value, this.dueStart.value, this.dueEnd.value, this.noDueDate.value, this.currentQuiz.QuizQuestions);

    console.log('current quiz', this.currentQuiz);

    this.quizService.updateQuiz(this.currentQuiz).subscribe(
      () => {
        this.toastr.success('Quiz Updated');
        this.router.navigate(['../../manage'], {relativeTo: this.route});
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
