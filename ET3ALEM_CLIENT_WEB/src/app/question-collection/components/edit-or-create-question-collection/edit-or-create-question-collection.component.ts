import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { QuestionCollectionService } from '../../question-collection.service';

@Component({
  selector: 'app-edit-or-create-question-collection',
  templateUrl: './edit-or-create-question-collection.component.html',
  styleUrls: ['./edit-or-create-question-collection.component.css']
})
export class EditOrCreateQuestionCollectionComponent  {

}

