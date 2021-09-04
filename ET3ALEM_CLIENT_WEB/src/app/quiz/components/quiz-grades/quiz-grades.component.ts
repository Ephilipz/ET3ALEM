import {Component, OnInit, ViewChild} from '@angular/core';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {ActivatedRoute} from '@angular/router';
import {plainToClass} from 'class-transformer';
import {ToastrService} from 'ngx-toastr';
import {GeneralHelper} from 'src/app/Shared/Classes/helpers/GeneralHelper';
import {Quiz} from '../../Model/quiz';
import {QuizAttempt} from '../../Model/quiz-attempt';
import {QuizAttemptService} from '../../services/quiz-attempt.service';
import {QuizGradingHelper} from "../../../Shared/Classes/helpers/quiz-grading-helper";

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

  constructor(private route: ActivatedRoute, private quizAttemptService: QuizAttemptService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.getQuizAttemptsUsingId(id);
      }
    });
  }

  private getQuizAttemptsUsingId(id) {
    this.quizAttemptService.getAllQuizAttemptsForQuiz(id).subscribe(
      (quizAttempts) => {
        this.setQuizAttemptGridDataSource(quizAttempts);
        this.quiz = quizAttempts.length > 0 ? this.quizAttemptListDS.data[0].Quiz : null;
        this.isLoaded = true;
      },
      () => {
        this.toastr.error('unable to load quiz grades');
        this.isLoaded = true;
      }
    )
  }

  private setQuizAttemptGridDataSource(quizAttempts: Array<QuizAttempt>) {
    this.quizAttemptListDS.data = plainToClass(QuizAttempt, quizAttempts);
    this.quizAttemptListDS.sortingDataAccessor = (obj, property) => GeneralHelper.getProperty(obj, property);
    this.quizAttemptListDS.sort = this.sort;
  }

  getAverage(): string {
    if (this.isLoaded) {
      let {length, totalGrades} = this.getGradesSum();
      const averageGrade = totalGrades / length;
      const averageGradeAsPercentage = averageGrade / this.quiz.TotalGrade * 100;
      return averageGradeAsPercentage.toFixed(2) + '%';
    }
  }

  private getGradesSum(): { length: number; totalGrades: number } {
    const gradedQuizAttempts = this.quizAttemptListDS.data.filter(attempt => attempt.IsGraded);
    const grades = gradedQuizAttempts.map(attempt => attempt.Grade);
    let totalGrades = 0;
    grades.forEach(grade => totalGrades += grade);
    const length = grades.length;
    return {length, totalGrades};
  }

  getHighestScore(): string {
    if (this.isLoaded) {
      let maxGrade = 0;
      this.quizAttemptListDS.data.forEach(qa => maxGrade = Math.max(qa.Grade, maxGrade))
      return (maxGrade / this.quiz.TotalGrade * 100).toFixed(2) + '%';
    }
  }

  getStudentsCount(): number {
    if (this.isLoaded) {
      const userIds = this.quizAttemptListDS.data.map(qa => qa.UserId);
      const uniqueUserIds = [...new Set(userIds)];
      return uniqueUserIds.length;
    }
  }

  getGrade(attempt: QuizAttempt): string {
    return QuizGradingHelper.getGradeAsPercentage(attempt);
  }
}
