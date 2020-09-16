import { Component, OnInit, ViewChild, ViewChildren, QueryList, AfterViewInit } from '@angular/core';

// import * as Editor from '../../../../assets/js/ck-editor-custom-build/ckeditor.js'
import { FormControl, Validators } from '@angular/forms';
import * as moment from 'moment'
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions.js';
import { ToastrService } from 'ngx-toastr';
import { Quiz } from '../../Model/quiz.js';
import { QuizService } from '../../services/quiz.service.js';
import { ActivatedRoute } from '@angular/router';
import { RichTextEditorComponent } from 'src/app/Shared/modules/shared-components/rich-text-editor/rich-text-editor.component.js';
import { Question } from 'src/app/question/Models/question.js';
import { MultipleChoiceQuestion } from 'src/app/question/Models/mcq.js';
import { TrueFalseQuestion } from 'src/app/question/Models/true-false-question.js';
import { CreateQuestionComponent } from 'src/app/question/create-question/create-question.component.js';

@Component({
  selector: 'app-create-quiz',
  templateUrl: './edit-or-create-quiz.component.html',
  styleUrls: ['./edit-or-create-quiz.component.css']
})


export class EditOrCreateQuizComponent extends ExtraFormOptions implements OnInit {

  @ViewChild(RichTextEditorComponent) private richTextComponent: RichTextEditorComponent;

  @ViewChildren('CreateQuestionComponent') createQuestionComponents: QueryList<CreateQuestionComponent>;

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

  constructor(private toastr: ToastrService, private quizService: QuizService, private route: ActivatedRoute) {
    super();
  }

  ngOnInit(): void {
    let id: Number = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.mode = mode.edit;
      this.quizService.getQuiz(id).subscribe(
        res => {
          this.currentQuiz = res;
          this.questions = this.currentQuiz.QuizQuestions.map(x => x.)
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
    this.quizInstructions.setValue(this.currentQuiz.instructions);
    this.durationHours.setValue(this.currentQuiz.durationHours);
    this.durationMinutes.setValue(this.currentQuiz.durationMinutes);
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
    this.questions.push(new MultipleChoiceQuestion());
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
    this.createQuestionComponents.forEach(async (component,i) => {
      this.questions[i] = await component.saveQuestion();
    });

    if (!this.validate())
      return;

    await this.richTextComponent.removeUnusedImages();

    this.currentQuiz = new Quiz(0, this.quizTitle.value, this.quizInstructions.value, this.durationHours.value, this.durationMinutes.value, this.unlimitedTime.value, this.dueStart.value, this.dueEnd.value, this.noDueDate.value);

    this.quizService.createQuiz(this.currentQuiz).subscribe(
      () => {
        this.toastr.success('Quiz Created');
      },
      () => {
        this.toastr.error('Quiz not created');
      }
    )
  }

  updateQuiz() {
    if (!this.validate())
      return;

    this.currentQuiz = new Quiz(this.currentQuiz.Id, this.quizTitle.value, this.quizInstructions.value, this.durationHours.value, this.durationMinutes.value, this.unlimitedTime.value, this.dueStart.value, this.dueEnd.value, this.noDueDate.value);

    this.quizService.updateQuiz(this.currentQuiz).subscribe(
      () => {
        this.toastr.success('Quiz Updated');
      },
      () => {
        this.toastr.error('Quiz not updated');
      }
    )
  }

}

enum mode {
  edit,
  create
}
