import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Route, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {QuizService} from '../../../services/quiz.service';

@Component({
  selector: 'app-access-quiz',
  templateUrl: './access-quiz.component.html',
  styleUrls: ['./access-quiz.component.css']
})
export class AccessQuizComponent implements OnInit {

  quizTitle: string = '';
  quizCode: string = '';
  isLoaded = true;

  constructor(private quizService: QuizService, private toastr: ToastrService, private route: Router, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
  }

  getQuizFromCode(e) {
    if (this.quizCode.length == 5) {
      this.isLoaded = false;
      this.quizService.getQuizTitleFromCode(this.quizCode).subscribe(
        obj => {
          this.quizTitle = obj["title"];
        }, _ => {
        },
        () => {
          this.isLoaded = true;
        });
    } else {
      this.quizTitle = '';
    }
  }

  quizClick() {
    this.route.navigate(['../take', this.quizCode], {relativeTo: this.activatedRoute});
  }

}
