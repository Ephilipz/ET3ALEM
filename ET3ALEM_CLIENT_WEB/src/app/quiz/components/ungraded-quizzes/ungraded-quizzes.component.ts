import { Component, OnInit } from '@angular/core';
import {ToastrService} from "ngx-toastr";
import {QuizService} from "../../services/quiz.service";
import {Quiz} from "../../Model/quiz";
import {UngradedQuizTableVM} from "../../Model/ungraded-quiz-table-vm";

@Component({
  selector: 'app-ungraded-quizzes',
  templateUrl: './ungraded-quizzes.component.html',
  styleUrls: ['./ungraded-quizzes.component.css']
})
export class UngradedQuizzesComponent implements OnInit {

  quizList: Array<UngradedQuizTableVM> = [];

  constructor(private toast: ToastrService, private quizService: QuizService) { }

  ngOnInit(): void {
    this.getUngradedQuizzes();
  }

  private getUngradedQuizzes(){
    this.quizService.getUngradedQuizzes().subscribe(
      (result) => {
        this.quizList = result;
      },
    (error) => {
        this.toast.error('Unable to load ungraded quizzes')
    }
    )
  }

}
