import { AfterViewInit, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { plainToClass } from 'class-transformer';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { AnswerQuestionHeaderComponent } from 'src/app/question/answer-question/answer-question-header/answer-question-header.component';
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

  constructor(private quizService: QuizService, private route: ActivatedRoute, private toastr: ToastrService, private router: Router, private quizAttemptService: QuizAttemptService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let code = params['code'];
      this.quizService.getFullQuizFromCode(code).subscribe(
        (quiz) => {
          this.quiz = plainToClass(Quiz, quiz);
          this.endDate = moment.utc().add(this.quiz.DurationSeconds, 'seconds').toDate();
          this.quizAttempt = new QuizAttempt(0, 0, this.quiz.Id, moment.utc());
          this.quizAttemptService.createQuizAttempt(this.quizAttempt).subscribe(
            (quizAttempt) => {
              this.quiz.QuizQuestions.sort(Qq => Qq.Sequence);
              this.quizAttempt.Id = quizAttempt.Id;
              this.isLoaded = true;
            },
            (err) => this.errorLoadingQuiz(err)
          )
        },
        (err) => this.errorLoadingQuiz(err)
      )
    });
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
    this.quizAttempt.SubmitTime = moment.utc().toDate();
    this.quizAttempt.UpdateQuestionTypes();
    this.quizAttemptService.updateQuizAttempt(this.quizAttempt).subscribe(
      () => { },
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
