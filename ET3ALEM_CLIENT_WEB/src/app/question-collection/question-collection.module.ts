import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListQuestionCollectionsComponent } from './components/list-question-collections/list-question-collections.component';
import { EditOrCreateQuestionCollectionComponent } from './components/edit-or-create-question-collection/edit-or-create-question-collection.component';



@NgModule({
  declarations: [
    ListQuestionCollectionsComponent,
    EditOrCreateQuestionCollectionComponent
  ],
  imports: [
    CommonModule
  ]
})
export class QuestionCollectionModule { }
