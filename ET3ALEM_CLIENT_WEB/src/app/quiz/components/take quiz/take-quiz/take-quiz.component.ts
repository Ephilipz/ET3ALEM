import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { plainToClass } from 'class-transformer';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { Quiz } from 'src/app/quiz/Model/quiz';
import { QuizService } from 'src/app/quiz/services/quiz.service';

@Component({
  selector: 'app-take-quiz',
  templateUrl: './take-quiz.component.html',
  styleUrls: ['./take-quiz.component.css']
})

export class TakeQuizComponent implements OnInit {

  quiz: Quiz = null;
  isLoaded: boolean = false;


  constructor(private quizService: QuizService, private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let code = params['code'];
      this.quizService.getFullQuizFromCode(code).subscribe(
        (quiz) => {
          this.quiz = plainToClass(Quiz, quiz);
          this.isLoaded = true;
        },
        (err) => {
          this.toastr.error('unable to load this quiz');
          this.isLoaded = true;
        }
      )
    });
  }

}
