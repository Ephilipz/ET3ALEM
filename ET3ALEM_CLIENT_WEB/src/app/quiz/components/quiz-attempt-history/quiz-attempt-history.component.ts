import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper';
import { QuizAttempt } from '../../Model/quiz-attempt';
import { QuizAttemptService } from '../../services/quiz-attempt.service';

@Component({
  selector: 'app-quiz-attempt-history',
  templateUrl: './quiz-attempt-history.component.html',
  styleUrls: ['./quiz-attempt-history.component.css']
})

export class QuizAttemptHistoryComponent implements OnInit {

  isLoaded = false;
  quizAttemptListDS = new MatTableDataSource<QuizAttempt>();
  displayedColumns = ['Quiz.Name', 'SubmitTime', 'Grade', 'ViewButton'];
  @ViewChild(MatSort) matSort: MatSort;

  constructor(private quizAttemptService: QuizAttemptService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.quizAttemptService.getQuizAttemptsForUser().subscribe(
      (res: any) => {
        this.quizAttemptListDS.data = res;
        this.quizAttemptListDS.sortingDataAccessor = Helper.getProperty;
        this.quizAttemptListDS.sort = this.matSort;
        this.isLoaded = true;
      },
      (err) => {
        this.toastr.error('Unable to load quiz history');
        this.isLoaded = true;
      }
    );
  }

  getGrade(attempt: QuizAttempt) {
    return attempt.IsGraded ? (attempt.Grade / attempt.Quiz.TotalGrade * 100).toFixed(2) + '%' : 'Not Graded Yet';
  }

}
