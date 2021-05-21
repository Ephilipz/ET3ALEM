import { AfterViewInit, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ActivatedRoute, NavigationStart, Router } from '@angular/router';
import { plainToClass } from 'class-transformer';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { filter, map } from 'rxjs/operators';
import { AnswerQuestionHeaderComponent } from 'src/app/question/answer-question/answer-question-header/answer-question-header.component';
import { QuestionAttempt } from 'src/app/question/Models/question-attempt';
import { Quiz } from 'src/app/quiz/Model/quiz';
import { QuizAttempt } from 'src/app/quiz/Model/quiz-attempt';
import { QuizAttemptService } from 'src/app/quiz/services/quiz-attempt.service';
import { QuizService } from 'src/app/quiz/services/quiz.service';

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

  constructor(private quizService: QuizService, private route: ActivatedRoute, private toastr: ToastrService, private router: Router, private quizAttemptService: QuizAttemptService) {

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
      this.router.navigate(['../'], { relativeTo: this.route });
      return;
    }

    //new quiz attempt
    if (!this.id) {
      this.route.params.subscribe(params => {
        let code = params['code'];
        this.quizService.getFullQuizFromCode(code).subscribe(
          (quiz) => {
            this.quiz = plainToClass(Quiz, quiz);
            this.endDate = moment.utc().add(this.quiz.DurationSeconds, 'seconds').toDate();
            this.quizAttempt = new QuizAttempt(0, 0, this.quiz.Id, moment.utc());
            this.quizAttemptService.createQuizAttempt(this.quizAttempt).subscribe(
              (quizAttempt) => {
                this.quizAttempt = plainToClass(QuizAttempt, quizAttempt);
                this.quiz.QuizQuestions.sort(Qq => Qq.Sequence);
                this.isLoaded = true;
              },
              (err) => this.errorLoadingQuiz(err)
            )
          },
          (err) => this.errorLoadingQuiz(err)
        )
      });
    }

    //resume quiz attempt
    else {
      this.quizAttemptService.getQuizAttemptWithQuiz(this.id).subscribe(
        (quizAttempt) => {
          this.quizAttempt = plainToClass(QuizAttempt, quizAttempt);
          this.quizAttempt.Quiz.QuizQuestions = this.quizAttempt.QuestionsAttempts.map(qA => qA.QuizQuestion);
          this.quiz = plainToClass(Quiz, quizAttempt.Quiz);
          this.endDate = moment.utc(this.quizAttempt.StartTime.toString() + 'Z').add(this.quiz.DurationSeconds, 'seconds').toDate();
          if (!this.quizAttempt.QuestionsAttempts)
            this.quizAttempt.QuestionsAttempts = new Array<QuestionAttempt>();
          this.isLoaded = true;
        },
        (err) => this.errorLoadingQuiz(err)
      );
    }
  }

  errorLoadingQuiz(error) {
    this.toastr.error('unable to load this quiz');
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  quizFinished() {
    this.toastr.info('Time Up!');
    this.submitQuiz();
  }

  submitQuiz() {
    this.getQuestionAttempts();
    this.quizAttempt.Quiz = this.quiz;
    this.quizAttempt.SubmitTime = moment.utc().toDate();
    this.quizAttempt.UpdateQuestionTypes();
    this.quizAttemptService.updateQuizAttempt(this.quizAttempt).subscribe(
      (success) => {
        this.router.navigate(['../../../viewAttempt', this.quizAttempt.Id], { relativeTo: this.route });
      },
      (err) => {
        this.toastr.error('unable to submit quiz');
      }
    )
  }

  private getQuestionAttempts() {
    this.AnswerQuestionComponents.forEach((component) => {
      const attempt = component.getQuestionAttempt();
      this.quizAttempt.QuestionsAttempts.push(attempt);
    });
  }

}
