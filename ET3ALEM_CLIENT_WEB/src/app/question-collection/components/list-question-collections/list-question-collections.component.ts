import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { QuestionCollectionService } from '../../question-collection.service';

@Component({
  selector: 'app-list-question-collections',
  templateUrl: './list-question-collections.component.html',
  styleUrls: ['./list-question-collections.component.css']
})
export class ListQuestionCollectionsComponent implements OnInit, AfterViewInit {

  collectionListDS = new MatTableDataSource();
  displayedColumns: string[] = ['Name', 'CreatedDate', 'actions'];
  isLoaded = false;

  @ViewChild(MatSort) matSort: MatSort;

  constructor(private questionCollectionService: QuestionCollectionService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadCollections();
  }

  ngAfterViewInit() {
    this.collectionListDS.sort = this.matSort;
  }

  loadCollections() {
    this.questionCollectionService.getCollections().subscribe(
      res => {
        this.collectionListDS.data = res;
        this.isLoaded = true;

      },
      err => {
        this.toastr.error('Unable to load collections');
        this.isLoaded = true;
        console.error(err);
      }
    );
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.collectionListDS.filter = filterValue.trim().toLowerCase();
  }

  delete(id: number) {
    this.questionCollectionService.delete(id).subscribe(
      res => {
        this.toastr.info('Collection Deleted');
        this.loadCollections();
      },
      err => {
        this.toastr.error('Unable to delete collection');
        console.error(err);
      }
    );
  }
}

