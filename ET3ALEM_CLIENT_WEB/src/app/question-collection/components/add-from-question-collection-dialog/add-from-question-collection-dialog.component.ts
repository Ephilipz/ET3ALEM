import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { plainToClass } from 'class-transformer';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { QuestionCollection } from '../../models/question-collection';
import { QuestionCollectionService } from '../../question-collection.service';

@Component({
  selector: 'app-add-from-question-collection-dialog',
  templateUrl: './add-from-question-collection-dialog.component.html',
  styleUrls: ['./add-from-question-collection-dialog.component.css']
})
export class AddFromQuestionCollectionDialogComponent implements OnInit {

  constructor(private toastr: ToastrService, private questionCollectionService: QuestionCollectionService, public dialogRef: MatDialogRef<AddFromQuestionCollectionDialogComponent>, @Inject(MAT_DIALOG_DATA) public data) { }

  questionCollection: QuestionCollection = null;
  questionCollectionId: number = null;
  questionCollections: Array<QuestionCollection> = null;
  questionList = null;
  isLoadedInitial = false;
  isLoadedQuestions = false;

  ngOnInit(): void {
    this.questionCollectionService.getCollections().subscribe(
      (questionCollections) => {
        this.questionCollections = questionCollections;
        this.isLoadedInitial = true;
        //this.loadQuestions();
      },
      (err) => this.toastr.error('Unable to load Question Collections')
    )
  }

  loadQuestions() {
    if (!this.questionCollectionId)
      return;
    this.questionCollectionService.getCollection(this.questionCollectionId).subscribe(
      (collection) => {
        this.questionCollection = plainToClass(QuestionCollection, collection);
        console.log(this.questionCollection.Questions[0].duplicateQuestion());
        this.questionCollection.Questions = this.questionCollection.Questions.map(question => question.duplicateQuestion());
        this.isLoadedQuestions = true;
      }
    )
  }

  onNoClick() {
    this.dialogRef.close();
  }

  closeDialog() {
    console.log(this.questionList);
    this.dialogRef.close(this.questionList);
  }

}
