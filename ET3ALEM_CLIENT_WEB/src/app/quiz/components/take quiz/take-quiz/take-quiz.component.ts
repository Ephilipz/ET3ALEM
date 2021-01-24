import { AfterViewInit, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { plainToClass } from 'class-transformer';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { AnswerQuestionHeaderComponent } from 'src/app/question/answer-question/answer-question-header/answer-question-header.component';
import { Quiz } from 'src/app/quiz/Model/quiz';
import { QuizAttempt } from 'src/app/quiz/Model/quiz-attempt';
import { QuizService } from 'src/app/quiz/services/quiz.service';

@Component({
  selector: 'app-take-quiz',
  templateUrl: './take-quiz.component.html',
  styleUrls: ['./take-quiz.component.css']
})

export class TakeQuizComponent implements OnInit {

  @ViewChildren('AnswerQuestion') private AnswerQuestionComponents: QueryList<AnswerQuestionHeaderComponent>;

  quiz: Quiz = null;
  quizAttempt: QuizAttempt;
  isLoaded: boolean = false;
  endDate: Date;

  constructor(private quizService: QuizService, private route: ActivatedRoute, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let code = params['code'];
      this.quizService.getFullQuizFromCode(code).subscribe(
        (quiz) => {
          this.quiz = plainToClass(Quiz, quiz);
          this.endDate = moment().utc().add(this.quiz.DurationSeconds, 'seconds').toDate();
          this.isLoaded = true;
        },
        (err) => {
          this.toastr.error('unable to load this quiz');
          this.isLoaded = true;
          this.router.navigate(['../'], { relativeTo: this.route });
        }
      )
    });
  }

  quizFinished() {
    this.toastr.info('Time Up!');
    this.quizAttempt = new QuizAttempt(0, 0, this.quiz.Id);
    this.getQuestionAttempts();
  }

  private getQuestionAttempts(){
    this.AnswerQuestionComponents.forEach((component) => {
      const attempt = component.getQuestionAttempt();
      this.quizAttempt.QuestionAttempts.push(attempt);
    });
  }

}
