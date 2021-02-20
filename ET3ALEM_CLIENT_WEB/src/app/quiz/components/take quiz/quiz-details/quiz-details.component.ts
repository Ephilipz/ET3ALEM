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
              this.checkQuizAttempts();
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

  checkQuizAttempts() {
    if (!this.quizAttempts || this.quizAttempts.length == 0)
      return;
    this.latestQuizAttempt = this.quizAttempts[0];
    const endTime = moment.utc(this.latestQuizAttempt.StartTime.toString()+'Z').add(this.quiz.DurationSeconds, 'seconds');
    const currentTime = moment.utc();
    this.inProgress = endTime.isAfter(currentTime) && this.latestQuizAttempt.SubmitTime == null;
    this.quizAttemptLimitReached = this.quizAttempts.length == this.quiz.AllowedAttempts && !this.quiz.UnlimitedAttempts;
  }

}
