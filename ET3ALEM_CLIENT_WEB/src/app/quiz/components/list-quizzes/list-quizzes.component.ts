import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Quiz } from '../../Model/quiz';
import { QuizService } from '../../services/quiz.service';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import * as moment from 'moment';
import { Location, LocationStrategy } from '@angular/common';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-list-quizzes',
  templateUrl: './list-quizzes.component.html',
  styleUrls: ['./list-quizzes.component.css']
})
export class ListQuizzesComponent implements OnInit, AfterViewInit {

  quizListDS = new MatTableDataSource();
  displayedColumns: string[] = ['Name', 'Code', 'CreatedDate', 'Status', 'Link', 'actions'];
  isLoaded = false;

  @ViewChild(MatSort) matSort: MatSort;

  constructor(private quizService: QuizService, 
    private toastr: ToastrService, 
    private locationStrategy: LocationStrategy, 
    private clipboard: Clipboard, 
    private Location: Location) { }

  ngOnInit(): void {
    this.loadQuizzes();
  }

  ngAfterViewInit() {
    this.quizListDS.sort = this.matSort;
  }

  loadQuizzes() {
    this.quizService.getQuizzes().subscribe(
      res => {
        this.quizListDS.data = res;
        this.isLoaded = true;

      },
      err => {
        this.toastr.error('Unable to load quizzes');
        this.isLoaded = true;
        console.error(err);
      }
    );
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.quizListDS.filter = filterValue.trim().toLowerCase();
  }

  delete(id: Number) {
    this.quizService.delete(id).subscribe(
      res => {
        this.toastr.info('Quiz Deleted');
        this.loadQuizzes();
      },
      err => {
        this.toastr.error('Unable to delete quiz');
        console.error(err);
      }
    )
  }

  getQuizStatus(quiz: Quiz) {
    const isExpired = moment(quiz.EndDate).isBefore(moment.utc()) && !quiz.NoDueDate;
    const isNotStarted = moment(quiz.StartDate).isAfter(moment.utc());
    if (isExpired)
      return 'Expired'
    if (isNotStarted)
      return 'Not Started'
    return 'Active';
  }

  getQuizLink(quiz: Quiz){
    const baseURL = window.location.origin;
    const fullURL = baseURL + '/quiz/take/' + quiz.Code;
    this.clipboard.copy(fullURL);
    this.toastr.info('Quiz link was copied');
  }

}
