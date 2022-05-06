import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { plainToClass } from 'class-transformer';
import { ToastrService } from 'ngx-toastr';
import { QuizAttempt } from 'src/app/quiz/Model/quiz-attempt';
import { QuizAttemptService } from 'src/app/quiz/services/quiz-attempt.service';
import { DateHelper } from 'src/app/Shared/helper/date.helper';
import { Quiz } from '../../../Model/quiz';
import { QuizService } from '../../../services/quiz.service';
// import { documentToHtmlString } from '@contentful/rich-text-html-renderer';

@Component({
  selector: 'app-quiz-details',
  templateUrl: './quiz-details.component.html',
  styleUrls: ['./quiz-details.component.css']
})
export class QuizDetailsComponent implements OnInit {

  quiz: Quiz = null;
  quizAttempts: Array<QuizAttempt> = [];
  isLoaded = false;
  latestQuizAttempt: QuizAttempt = null;
  inProgress = false;
  quizAttemptLimitReached = false;
  dueDatePassed = false;
  readonly secondsBuffer = 10;

  constructor(private route: ActivatedRoute, private quizService: QuizService, private quizAttemptService: QuizAttemptService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let code = params['code'];
      this.quizService.getBasicQuizFromCode(code).subscribe(
        (quiz) => {
          this.quiz = plainToClass(Quiz, quiz);
          this.quizAttemptService.getQuizAttemptsForQuiz(quiz.Id).subscribe(
            (quizAttemptList) => {
              this.quizAttempts = plainToClass(QuizAttempt, quizAttemptList)?.sort(qA => qA.StartTime.getMilliseconds())?.reverse();
              this.checkQuizValidityAndProgress();
              this.isLoaded = true;
            },
            (err) => {
              console.error(err)
            }
          )
        },
        (err) => {
          this.toastr.error('unable to load this quiz');
          this.isLoaded = true;
        }
      )
    });
  }

  resumeQuiz() {
    this.checkQuizValidityAndProgress();
    if (!this.inProgress) {
      this.toastr.info('Unable to resume. The time finished for your previous quiz attempt.');
      return;
    }
    this.router.navigate(['./start'], { relativeTo: this.route, state: { quizAttemptId: this.latestQuizAttempt.Id } })
  }

  startQuiz() {
    this.checkQuizValidityAndProgress();
    if (this.inProgress) {
      this.toastr.info('Unable to start a new quiz. This quiz is now in progress.');
      return;
    }
    this.router.navigate(['./start'], { relativeTo: this.route, state: { quizAttemptId: null } })
  }

  checkQuizValidityAndProgress() {
    const currentTime = DateHelper.utcNow;

    //check due date
    if (!this.quiz.NoDueDate) {
      const endTime = DateHelper.utc(this.quiz.EndDate.toString() + 'Z');
      this.dueDatePassed = DateHelper.isBefore(endTime, currentTime);;
      if (this.dueDatePassed)
        return;
    }

    if (this.noPreviousAttempts())
      return;

    this.quizAttemptLimitReached = this.quizAttempts.length >= this.quiz.AllowedAttempts && !this.quiz.UnlimitedAttempts;

    this.latestQuizAttempt = this.quizAttempts[0];
    if (this.quiz.NoDueDate) {
      this.inProgress = !this.latestQuizAttempt.IsSubmitted;
      return;
    }

    const latestQuizEndTime = DateHelper.addSeconds(
      DateHelper.utc(this.latestQuizAttempt.StartTime.toString() + 'Z'), this.quiz.DurationSeconds + this.secondsBuffer);
    this.inProgress = DateHelper.isAfter(latestQuizEndTime, currentTime) && !this.latestQuizAttempt.IsSubmitted;
    this.quizAttemptLimitReached &&= !this.inProgress;
  }

  private noPreviousAttempts() {
    return !this.quizAttempts || this.quizAttempts.length == 0;
  }
}
