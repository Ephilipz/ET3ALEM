import {
  Component,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChildren
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { plainToClass } from 'class-transformer';
import { ToastrService } from 'ngx-toastr';
import { map, mergeMap, takeWhile } from 'rxjs/operators';
import { AnswerQuestionHeaderComponent } from 'src/app/question/answer-question/answer-question-header/answer-question-header.component';
import { Quiz } from 'src/app/quiz/Model/quiz';
import { QuizAttempt } from 'src/app/quiz/Model/quiz-attempt';
import { QuizAttemptService } from 'src/app/quiz/services/quiz-attempt.service';
import { QuizService } from 'src/app/quiz/services/quiz.service';
import { interval } from "rxjs";
import { Subscription } from "rxjs/internal/Subscription";
import DateHelper from 'src/app/Shared/Classes/helpers/date.helper';

@Component({
  selector: 'take-quiz',
  templateUrl: './take-quiz.component.html',
  styleUrls: ['./take-quiz.component.css']
})

export class TakeQuizComponent implements OnInit, OnDestroy {

  @ViewChildren('AnswerQuestion') private AnswerQuestionComponents: QueryList<AnswerQuestionHeaderComponent>;

  quiz: Quiz = null;
  quizAttempt: QuizAttempt;
  isLoaded: boolean = false;
  endDate: Date;
  id: number = -1;
  readonly AutoSaveMinutes = 0.5;
  private autoSaveObservable: Subscription;

  constructor(private quizService: QuizService, private route: ActivatedRoute,
    private toastr: ToastrService, private router: Router,
    private quizAttemptService: QuizAttemptService) {

  }

  ngOnInit(): void {
    this.setIdFromRouteParams();

    if (this.id == -1) {
      this.router.navigate(['../'], { relativeTo: this.route });
      return;
    }

    this.getQuizAttempt();

    // this.setAutoSaveObservable();
  }

  private getQuizAttempt() {
    if (!this.id) {
      this.getNewQuiz();
    } else {
      this.getInProgressQuiz();
    }
  }

  private setAutoSaveObservable() {
    this.autoSaveObservable = interval(this.AutoSaveMinutes * 60 * 1000)
      .pipe(
        takeWhile(() => this.quiz.UnlimitedTime || this.getMinutesRemaining() > this.AutoSaveMinutes),
        mergeMap(() => {
          this.prepareQuizAttemptForSubmission();
          return this.quizAttemptService.updateQuizAttempt(this.quizAttempt);
        })
      )
      .subscribe((quizAttempt) => {
        this.toastr.info('quiz auto saved');
        console.log('quiz attempt after auto save : ', quizAttempt);
      });

  }

  private getMinutesRemaining() {
    const timeDifference = DateHelper.difference(this.endDate, DateHelper.utcNow);
    return DateHelper.asMinutes(timeDifference);
  }

  private setIdFromRouteParams() {
    this.route.paramMap
      .pipe(map(() => window.history.state)).subscribe(
        (state) => {
          if (state.hasOwnProperty('quizAttemptId'))
            this.id = state.quizAttemptId;
        }
      );
  }

  private getInProgressQuiz() {
    this.quizAttemptService.getQuizAttemptWithQuizLight(this.id).subscribe(
      (quizAttempt) => {
        console.log(quizAttempt);
        this.quizAttempt = plainToClass(QuizAttempt, quizAttempt);
        this.quiz = plainToClass(Quiz, quizAttempt.Quiz);
        let startDateLocalTime = DateHelper.utc(this.quizAttempt.StartTime.toString() + 'Z');
        this.endDate = DateHelper.addSeconds(startDateLocalTime, this.quiz.DurationSeconds);
        this.isLoaded = true;
        console.log(startDateLocalTime);
        console.log(this.endDate);
      },
      () => this.errorLoadingQuiz()
    );
  }

  private getNewQuiz() {
    this.route.params.subscribe(params => {
      let code = params['code'];
      this.quizService.getBasicQuizFromCode(code).subscribe(
        (quiz) => {
          this.getNewQuizWithAttempt(quiz);
        },
        () => this.errorLoadingQuiz()
      )
    });
  }

  private getNewQuizWithAttempt(quiz: Quiz) {
    this.quiz = plainToClass(Quiz, quiz);
    this.quizAttempt = new QuizAttempt(0, 0, this.quiz.Id, DateHelper.utcNow);
    this.quizAttemptService.createQuizAttempt(this.quizAttempt).subscribe(
      (quizAttempt) => {
        this.quizAttempt = plainToClass(QuizAttempt, quizAttempt);
        this.endDate = DateHelper.addSeconds(DateHelper.utcNow, this.quiz.DurationSeconds);
        this.isLoaded = true;
      },
      () => this.errorLoadingQuiz()
    );
  }

  errorLoadingQuiz() {
    this.toastr.error('unable to load this quiz');
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  quizFinished() {
    this.toastr.info('Time Up!');
    this.submitQuiz();
  }

  submitQuiz() {
    this.prepareQuizAttemptForSubmission();
    this.quizAttempt.IsSubmitted = true;
    this.quizAttemptService.updateQuizAttempt(this.quizAttempt).subscribe(
      () => {
        this.router.navigate(['../../../viewAttempt', this.quizAttempt.Id], { relativeTo: this.route });
      }
    )
  }

  private prepareQuizAttemptForSubmission() {
    //in create quiz the quiz is not retrieved with the attempt so it must be set
    if (!this.quizAttempt.Quiz)
      this.quizAttempt.Quiz = this.quiz;
    this.getQuestionAttempts();
    this.quizAttempt.SubmitTime = DateHelper.utcNow;
    this.quizAttempt.UpdateQuestionTypes();
  }

  private getQuestionAttempts() {
    this.AnswerQuestionComponents.forEach((component, i) => {
      this.quizAttempt.QuestionsAttempts[i] = component.getQuestionAttempt();
    });
  }

  ngOnDestroy(): void {
    this.autoSaveObservable?.unsubscribe();
  }

}
