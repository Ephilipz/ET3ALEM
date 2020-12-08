import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { QuizService } from '../../services/quiz.service';

@Component({
  selector: 'app-access-quiz',
  templateUrl: './access-quiz.component.html',
  styleUrls: ['./access-quiz.component.css']
})
export class AccessQuizComponent implements OnInit {

  quizTitle: string = '';
  quizCode: string = '';

  constructor(private quizService: QuizService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  getQuizFromCode(e) {
    if (this.quizCode.length == 5) {
      this.quizService.getQuizTitleFromCode(this.quizCode).subscribe(
        obj => {
          this.quizTitle = obj["title"];
        },
        err => {
          this.toastr.info('No quiz found with the entered code');
          this.quizTitle = '';
        });
    }
    else {
      this.quizTitle = '';
    }
  }

  routeToQuiz() {

  }

}
