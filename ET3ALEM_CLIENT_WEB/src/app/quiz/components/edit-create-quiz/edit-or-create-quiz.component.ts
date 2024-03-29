import {
  Component,
  OnInit,
  ViewChild,
  ViewChildren,
  QueryList,
  AfterViewInit
} from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Quiz} from '../../Model/quiz';
import {QuizService} from '../../services/quiz.service';
import {ActivatedRoute, Router} from '@angular/router';
import {
  RichTextEditorComponent
} from 'src/app/Shared/modules/shared-components/rich-text-editor/rich-text-editor.component';
import {Question} from 'src/app/question/Models/question';
import {MultipleChoiceQuestion} from 'src/app/question/Models/mcq';
import {
  EditOrCreateQuestionHeaderComponent
} from 'src/app/question/edit-create-question/Edit-Create-QuestionHeader/edit-or-create-questionHeader.component';
import {QuizQuestion} from '../../Model/quizQuestion';
import {GeneralHelper} from 'src/app/Shared/Classes/helpers/GeneralHelper';
import {plainToClass} from 'class-transformer';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import {MatDialog} from '@angular/material/dialog';
import {
  AddFromQuestionCollectionDialogComponent
} from 'src/app/question-collection/components/add-from-question-collection-dialog/add-from-question-collection-dialog.component';
import DateHelper from 'src/app/Shared/Classes/helpers/date.helper';
import {ExtraFormOptions} from 'src/app/Shared/Classes/forms/ExtraFormOptions';

@Component({
  selector: 'app-create-quiz',
  templateUrl: './edit-or-create-quiz.component.html',
  styleUrls: ['./edit-or-create-quiz.component.css']
})


export class EditOrCreateQuizComponent extends ExtraFormOptions implements OnInit {

  @ViewChild('RichTextEditorComponent') private richTextComponent: RichTextEditorComponent;

  @ViewChildren('CreateQuestionComponent') createQuestionComponents: QueryList<EditOrCreateQuestionHeaderComponent>;

  mode: mode = mode.create;
  currentQuiz: Quiz;

  questions: Array<any> = [];

  isLoaded: boolean = false;
  richTextLoaded: boolean = false;

  today: Date = DateHelper.now;

  questionLimit = 50;

  quizTitle = new FormControl('', [Validators.required]);
  quizInstructions = new FormControl();
  durationHours = new FormControl(1, [Validators.max(5), Validators.min(0)]);
  durationMinutes = new FormControl(0, [Validators.min(0)]);
  unlimitedTime = new FormControl(false);
  dueStart = new FormControl(DateHelper.now);
  dueEnd = new FormControl(DateHelper.addDays(DateHelper.now, 3));
  noDueDate = new FormControl(false);
  allowedAttempts = new FormControl(1, [Validators.max(10), Validators.min(1)]);
  unlimitedAttempts = new FormControl(false);
  showGrade = new FormControl(true);
  autoGrade = new FormControl(true);
  randomOrderQuestions = new FormControl(false);
  includeAllQuestions = new FormControl(true);
  includedQuestionsCount = new FormControl(1, [Validators.min(1)]);

  constructor(private toastr: ToastrService, private quizService: QuizService, private route: ActivatedRoute, private router: Router, public dialog: MatDialog) {
    super();
  }

  ngOnInit(): void {
    let id: number = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadQuiz(id);
      return;
    }
    this.isLoaded = true;
  }

  private loadQuiz(id: number) {
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
      }
    )
  }

  setFormControls() {
    this.quizTitle.setValue(this.currentQuiz.Name)
    this.quizInstructions.setValue(this.currentQuiz.Instructions);
    this.durationHours.setValue(Math.floor(this.currentQuiz.DurationSeconds / 3600));
    this.durationMinutes.setValue(this.currentQuiz.DurationSeconds % 3600 / 60);
    this.unlimitedTime.setValue(this.currentQuiz.UnlimitedTime);
    this.dueStart.setValue(DateHelper.getLocalDateFromUTC(this.currentQuiz.StartDate));
    this.dueEnd.setValue(DateHelper.getLocalDateFromUTC(this.currentQuiz.EndDate));
    this.noDueDate.setValue(this.currentQuiz.NoDueDate);
    this.randomOrderQuestions.setValue(this.currentQuiz.ShuffleQuestions);
    this.allowedAttempts.setValue(this.currentQuiz.AllowedAttempts);
    this.unlimitedAttempts.setValue(this.currentQuiz.UnlimitedAttempts);
    this.showGrade.setValue(this.currentQuiz.ShowGrade);
    this.includeAllQuestions.setValue(this.currentQuiz.IncludeAllQuestions);
    this.includedQuestionsCount.setValue(this.currentQuiz.IncludedQuestionsCount);
    this.includedQuestionsCount.setValidators([Validators.min(1), Validators.max(this.questions.length)]);
    this.includedQuestionsCount.updateValueAndValidity();

    this.setDisabledFields();
  }

  private setDisabledFields() {
    if (this.currentQuiz.UnlimitedTime) {
      this.durationHours.disable();
      this.durationMinutes.disable();
    }

    if (this.currentQuiz.NoDueDate) {
      this.dueEnd.disable();
    }

    if (this.currentQuiz.UnlimitedAttempts) {
      this.allowedAttempts.disable();
    }
  }

  toggleDisable(checked: boolean, list: Array<string>) {
    list.forEach((x) => {
      if (checked) {
        this[x]?.disable();
      } else {
        this[x]?.enable();
      }
    });
  }

  subtractDays(date: Date, days = 1) {
    return DateHelper.subtractDays(date, days);
  }

  addQuestion() {
    this.pushQuestion(new MultipleChoiceQuestion(GeneralHelper.randomInteger(0, 100) * -1));
    this.updateQuestionCountValidators();
  }

  pushQuestion(...questionsToAdd: any[]) {
    if (this.questions.length + questionsToAdd.length > this.questionLimit) {
      this.toastr.clear();
      this.toastr.error(`You cannot add more than ${this.questionLimit} questions`);
    }
    const numberOfQuestionsAvailable = this.questionLimit - this.questions.length;
    this.questions.push(...questionsToAdd.slice(0, numberOfQuestionsAvailable));
  }

  addFromCollection() {
    this.dialog.open(AddFromQuestionCollectionDialogComponent).afterClosed().subscribe(
      result => {
        if (result)
          this.pushQuestion(...result);
      }
    )
  }

  deleteQuestion(question: Question) {
    let quizQuestionToDelete: QuizQuestion = this.currentQuiz?.QuizQuestions.find(qQ => qQ.QuestionId == question.Id);
    if (quizQuestionToDelete) {
      quizQuestionToDelete.Id = quizQuestionToDelete.Id * -1;
      quizQuestionToDelete.QuestionId *= -1;
      quizQuestionToDelete.Question.Id *= -1;
    }

    let index = this.questions.findIndex(q => q.Id == question.Id);
    if (index > -1)
      this.questions.splice(index, 1);
    this.updateQuestionCountValidators();
  }

  duplicateQuestion(i: number) {
    let oldQuestion: Question = this.createQuestionComponents.find((item, index) => index == i)?.getQuestion();
    if (!oldQuestion)
      return;
    const newQuestion = oldQuestion.duplicateQuestion();
    this.questions.push(newQuestion);
    this.updateQuestionCountValidators();
  }

  private updateQuestionCountValidators() {
    this.includedQuestionsCount.setValidators([Validators.min(1), Validators.max(this.questions.length)]);
    this.includedQuestionsCount.updateValueAndValidity();
  }

  /**
   * For drag and drop event in reordering questions
   * @param event : drag or drop event
   */
  drop(event: CdkDragDrop<Question[]>) {
    const questionComponentsArray = this.createQuestionComponents.toArray();
    this.questions[event.previousIndex] = questionComponentsArray[event.previousIndex].getQuestion();
    this.questions[event.currentIndex] = questionComponentsArray[event.currentIndex].getQuestion();
    moveItemInArray(this.questions, event.previousIndex, event.currentIndex);
    const temp = GeneralHelper.deepCopy(this.questions);
    this.questions = null;
    this.questions = temp;
  }

  getGradeFromQuestion(question: Question): number {
    const grade = this.currentQuiz?.QuizQuestions?.find(qq => qq.QuestionId == question.Id)?.Grade;
    return grade ?? 1;
  }

  getIncludeQuestionsCountText() {
    const questionCount = this.includedQuestionsCount.value;
    if (questionCount)
      return Math.min(this.includedQuestionsCount.value, this.questions.length);
  }

  validate(): boolean {
    if ((this.durationHours.value ?? 0) * 60 + (this.durationMinutes.value ?? 0) < 1 && !this.unlimitedTime.value) {
      this.toastr.error('duration must be at least 1 minute or unlimited time');
      return false;
    }
    if (!this.unlimitedTime.value && DateHelper.isSameOrBefore(this.dueEnd.value, this.dueStart.value)) {
      this.toastr.error('The due date cannot be the same as the start date');
      return false;
    }

    this.createQuestionComponents.forEach(
      (component, i) => {
        try {
          component.validateQuestion();
        } catch (e) {
          this.toastr.error(e, `Question ${i + 1}`);
          return false;
        }
      });

    return true;
  }

  async createQuiz() {
    if (!this.validate())
      return;

    await this.richTextComponent.removeUnusedImages();

    let quizQuestions: Array<QuizQuestion> = [];

    await Promise.all(this.createQuestionComponents.map(async (component, i) => {
      let question = await component.saveQuestion();
      let grade = component.getGrade();
      question.Id = 0;
      quizQuestions.push(new QuizQuestion(question, grade, 0, i));
    }));

    this.createQuizWithQuizQuestions(quizQuestions);

    this.quizService.createQuiz(this.currentQuiz).subscribe(
      (_) => {
        this.toastr.success('Quiz Created');
        this.router.navigate(['../manage'], {relativeTo: this.route});
      }
    );
  }

  private createQuizWithQuizQuestions(quizQuestions: Array<QuizQuestion>) {
    const quizDuration = (this.durationHours.value * 3600 + this.durationMinutes.value * 60);
    this.currentQuiz = new Quiz(0, '',
      this.quizTitle.value, this.quizInstructions.value,
      quizDuration, this.unlimitedTime.value, DateHelper.getUTCFromLocal(this.dueStart.value),
      DateHelper.getUTCFromLocal(this.dueEnd.value), DateHelper.utcNow,
      this.noDueDate.value, quizQuestions, this.allowedAttempts.value,
      this.unlimitedAttempts.value, this.showGrade.value,
      this.autoGrade.value, this.randomOrderQuestions.value,
      this.includeAllQuestions.value, this.includedQuestionsCount.value);
  }

  async updateQuiz() {
    if (!this.validate())
      return;

    await this.richTextComponent.removeUnusedImages();
    await this.getQuizQuestionsFromChildComponents();

    this.currentQuiz.updateQuiz(this.quizTitle.value, this.quizInstructions.value, (this.durationHours.value * 3600 + this.durationMinutes.value * 60), this.unlimitedTime.value,
      DateHelper.getUTCFromLocal(this.dueStart.value), DateHelper.getUTCFromLocal(this.dueEnd.value), this.noDueDate.value, this.currentQuiz.QuizQuestions, this.allowedAttempts.value, this.unlimitedAttempts.value, this.showGrade.value, this.autoGrade.value, this.randomOrderQuestions.value, this.includeAllQuestions.value, this.includedQuestionsCount.value);

    this.quizService.updateQuiz(this.currentQuiz).subscribe(
      () => {
        this.toastr.success('Quiz Updated');
        this.router.navigate(['../../manage'], {relativeTo: this.route});
      }
    );
  }

  private async getQuizQuestionsFromChildComponents() {
    await Promise.all(this.createQuestionComponents.map(async (component, i) => {
      let question = await component.saveQuestion();
      question.Id = Math.max(0, question.Id);
      this.questions[i] = GeneralHelper.deepCopy(question);
      let grade = component.getGrade();

      const questionIndex = this.currentQuiz.QuizQuestions.map(x => x.Question.Id).indexOf(question.Id);
      const isQuestionExists = questionIndex > -1;
      if (isQuestionExists) {
        this.updateQuizQuestion(this.currentQuiz.QuizQuestions[questionIndex], question, grade, i);
      } else {
        this.insertQuizQuestion(question, grade, i);
      }
    }));
  }

  private updateQuizQuestion(quizQuestion: QuizQuestion, question: Question, grade: number, i: number) {
    quizQuestion.Question = question;
    quizQuestion.Grade = grade;
    quizQuestion.Sequence = i;
  }

  private insertQuizQuestion(question: Question, grade: number, i: number) {
    question.Id = 0;
    this.currentQuiz.QuizQuestions.push(new QuizQuestion(question, grade, 0, i));
  }
}

export enum mode {
  edit,
  create
}
