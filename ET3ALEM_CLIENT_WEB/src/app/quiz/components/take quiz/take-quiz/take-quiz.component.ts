import {Component, OnInit, QueryList, ViewChildren} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {plainToClass} from 'class-transformer';
import * as moment from 'moment';
import {ToastrService} from 'ngx-toastr';
import {map} from 'rxjs/operators';
import {AnswerQuestionHeaderComponent} from 'src/app/question/answer-question/answer-question-header/answer-question-header.component';
import {Quiz} from 'src/app/quiz/Model/quiz';
import {QuizAttempt} from 'src/app/quiz/Model/quiz-attempt';
import {QuizAttemptService} from 'src/app/quiz/services/quiz-attempt.service';
import {QuizService} from 'src/app/quiz/services/quiz.service';

@Component({
  selector: 'take-quiz',
  templateUrl: './take-quiz.component.html',
  styleUrls: ['./take-quiz.component.css']
})

export class TakeQuizComponent implements OnInit {

  @ViewChildren('AnswerQuestion') private AnswerQuestionComponents: QueryList<AnswerQuestionHeaderComponent>;

  quiz: Quiz = null;
  quizAttempt: QuizAttempt;
  isLoaded: boolean = false;
  endDate: Date;
  id: number = -1;

  constructor(private quizService: QuizService, private route: ActivatedRoute,
              private toastr: ToastrService, private router: Router,
              private quizAttemptService: QuizAttemptService) {

  }

  ngOnInit(): void {
    this.route.paramMap
      .pipe(map(() => window.history.state)).subscribe(
      (state) => {
        if (state.hasOwnProperty('quizAttemptId'))
          this.id = state.quizAttemptId;
      }
    );

    if (this.id == -1) {
      this.router.navigate(['../'], {relativeTo: this.route});
      return;
    }

    //new quiz attempt
    if (!this.id) {
      this.route.params.subscribe(params => {
        let code = params['code'];
        this.quizService.getBasicQuizFromCode(code).subscribe(
          (quiz) => {
            this.quiz = plainToClass(Quiz, quiz);
            this.quizAttempt = new QuizAttempt(0, 0, this.quiz.Id, moment.utc());
            this.quizAttemptService.createQuizAttempt(this.quizAttempt).subscribe(
              (quizAttempt) => {
                this.quizAttempt = plainToClass(QuizAttempt, quizAttempt);
                this.endDate = moment.utc().add(this.quiz.DurationSeconds, 'seconds').toDate();
                this.isLoaded = true;
              },
              () => this.errorLoadingQuiz()
            )
          },
          () => this.errorLoadingQuiz()
        )
      });
    }

    //resume quiz attempt
    else {
      this.quizAttemptService.getQuizAttemptWithQuizLight(this.id).subscribe(
        (quizAttempt) => {
          this.quizAttempt = plainToClass(QuizAttempt, quizAttempt);
          this.quiz = plainToClass(Quiz, quizAttempt.Quiz);
          this.endDate = moment.utc(this.quizAttempt.StartTime.toString() + 'Z').add(this.quiz.DurationSeconds, 'seconds').toDate();
          this.isLoaded = true;
        },
        () => this.errorLoadingQuiz()
      );
    }
  }

  errorLoadingQuiz() {
    this.toastr.error('unable to load this quiz');
    this.router.navigate(['../'], {relativeTo: this.route});
  }

  quizFinished() {
    this.toastr.info('Time Up!');
    this.submitQuiz();
  }

  submitQuiz() {
    //in create quiz the quiz is not retrieved with the attempt so it must be set
    if (!this.quizAttempt.Quiz)
      this.quizAttempt.Quiz = this.quiz;
    this.getQuestionAttempts();
    this.quizAttempt.SubmitTime = moment.utc().toDate();
    this.quizAttempt.UpdateQuestionTypes();
    this.quizAttemptService.updateQuizAttempt(this.quizAttempt).subscribe(
      () => {
        this.router.navigate(['../../../viewAttempt', this.quizAttempt.Id], {relativeTo: this.route});
      },
      () => {
        this.toastr.error('unable to submit quiz');
      }
    )
  }

  private getQuestionAttempts() {
    this.AnswerQuestionComponents.forEach((component, i) => {
      this.quizAttempt.QuestionsAttempts[i] = component.getQuestionAttempt();
    });
  }

}
