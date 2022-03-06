import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { GeneralHelper } from 'src/app/Shared/Classes/helpers/GeneralHelper';
import { QuizAttempt } from '../../Model/quiz-attempt';
import { QuizAttemptService } from '../../services/quiz-attempt.service';
import {QuizGradingHelper} from "../../../Shared/Classes/helpers/quiz-grading-helper";

@Component({
  selector: 'app-quiz-attempt-history',
  templateUrl: './quiz-attempt-history.component.html',
  styleUrls: ['./quiz-attempt-history.component.css']
})

export class QuizAttemptHistoryComponent implements OnInit {

  isLoaded = false;
  quizAttemptListDS = new MatTableDataSource<QuizAttempt>();
  displayedColumns = ['Quiz.Name', 'SubmitTime', 'Grade', 'Actions'];
  @ViewChild(MatSort) matSort: MatSort;

  constructor(private quizAttemptService: QuizAttemptService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.quizAttemptService.getQuizAttemptsForUser().subscribe(
      (res: any) => {
        this.quizAttemptListDS.data = res;
        this.quizAttemptListDS.sortingDataAccessor = GeneralHelper.getProperty;
        this.quizAttemptListDS.sort = this.matSort;
        this.isLoaded = true;
      },
      (err) => {
        this.toastr.error('Unable to load quiz history');
        this.isLoaded = true;
      }
    );
  }

  getGrade(attempt:QuizAttempt){
    return QuizGradingHelper.getGradeAsPercentage(attempt);
  }

  canRetakeQuiz(Quiz: any) {
    return true
  }
}
