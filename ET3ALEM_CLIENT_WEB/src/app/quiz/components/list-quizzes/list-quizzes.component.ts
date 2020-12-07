import { Component, OnInit, ViewChild } from '@angular/core';
import { Quiz } from '../../Model/quiz';
import { QuizService } from '../../services/quiz.service';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-list-quizzes',
  templateUrl: './list-quizzes.component.html',
  styleUrls: ['./list-quizzes.component.css']
})
export class ListQuizzesComponent implements OnInit {

  quizListDS = new MatTableDataSource();
  displayedColumns: string[] = ['index', 'Name', 'code', 'StartDate', 'actions'];
  isLoaded = false;

  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private quizService: QuizService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadQuizzes();
  }

  loadQuizzes() {
    this.quizService.getQuizzes().subscribe(
      res => {
        this.quizListDS.data = res.map((x, i) => ({ ...x, 'index': i + 1 }));
        this.isLoaded = true;
        this.quizListDS.sort = this.sort;
      },
      err => {
        this.toastr.error('Unable to load quizzes');
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
        this.toastr.success('Quiz Deleted Successfully');
        this.loadQuizzes();
      },
      err => {
        this.toastr.error('Unable to delete quiz');
        console.error(err);
      }
    )
  }

}
