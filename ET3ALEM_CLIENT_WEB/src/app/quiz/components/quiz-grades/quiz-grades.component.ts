import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { plainToClass } from 'class-transformer';
import { ToastrService } from 'ngx-toastr';
import { Quiz } from '../../Model/quiz';
import { QuizAttempt } from '../../Model/quiz-attempt';
import { QuizAttemptService } from '../../services/quiz-attempt.service';

@Component({
  selector: 'app-quiz-grades',
  templateUrl: './quiz-grades.component.html',
  styleUrls: ['./quiz-grades.component.css']
})
export class QuizGradesComponent implements OnInit {

  displayedColumns: string[] = ['User.FullName', 'Grade', 'StartTime', 'GradeButton'];
  @ViewChild(MatSort, {static: false}) sort: MatSort;
  
  quiz: Quiz = null;
  quizAttemptListDS = new MatTableDataSource<QuizAttempt>();
  isLoaded: boolean = false;

  constructor(private route: ActivatedRoute, private quizAttemptService: QuizAttemptService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.quizAttemptService.getAllQuizAttemptsForQuiz(id).subscribe(
        (quizAttempts) => {
          this.quizAttemptListDS.data = plainToClass(QuizAttempt, quizAttempts);
          this.quizAttemptListDS.sortingDataAccessor = (obj, property) => this.getProperty(obj, property);
          this.quizAttemptListDS.sort = this.sort;
          this.quiz = quizAttempts.length > 0 ? this.quizAttemptListDS.data[0].Quiz : null;
          this.isLoaded = true;
        },
        (err) => {
          this.toastr.error('unable to load quiz grades');
          this.isLoaded = true;
        }
      )
    });
  }

  getAverage(): number {
    if (this.isLoaded) {
      let totalGrades = 0;
      this.quizAttemptListDS.data.forEach(qa => totalGrades += qa.Grade);
      return +(totalGrades / this.quizAttemptListDS.data.length).toFixed(2);;
    }
  }

  getHighestScore(): number {
    if (this.isLoaded) {
      let maxGrade = 0;
      this.quizAttemptListDS.data.forEach(qa => maxGrade = Math.max(qa.Grade, maxGrade))
      return maxGrade;
    }
  }

  getStudentsCount(): number {
    if (this.isLoaded)
      return [...new Set(this.quizAttemptListDS.data.map(qa => qa.UserId))].length;
  }

  getProperty = (obj, path) => (
    path.split('.').reduce((o, p) => o && o[p], obj)
  )

}
