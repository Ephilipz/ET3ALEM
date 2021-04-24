import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { plainToClass } from 'class-transformer';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { QuizAttempt } from 'src/app/quiz/Model/quiz-attempt';
import { QuizAttemptService } from 'src/app/quiz/services/quiz-attempt.service';
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
    // this.router.navigateByUrl('/user', { state: { orderId: 1234 } });
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
    const currentTime = moment.utc();

    //check due date
    if (!this.quiz.NoDueDate) {
      const endTime = moment.utc(this.quiz.EndDate.toString() + 'Z');
      this.dueDatePassed = endTime.isBefore(currentTime);
      if (this.dueDatePassed)
        return;
    }

    //check previous attempts
    if (!this.quizAttempts || this.quizAttempts.length == 0)
      return;
    this.latestQuizAttempt = this.quizAttempts[0];
    const latestQuizEndTime = moment.utc(this.latestQuizAttempt.StartTime.toString() + 'Z').add(this.quiz.DurationSeconds + this.secondsBuffer, 'seconds');
    this.inProgress = latestQuizEndTime.isAfter(currentTime) && this.latestQuizAttempt.SubmitTime == null;
    this.quizAttemptLimitReached = this.quizAttempts.length >= this.quiz.AllowedAttempts && !this.quiz.UnlimitedAttempts && !this.inProgress;
  }

}
