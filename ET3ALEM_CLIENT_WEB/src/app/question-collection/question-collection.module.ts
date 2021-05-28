import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListQuestionCollectionsComponent } from './components/list-question-collections/list-question-collections.component';
import { EditOrCreateQuestionCollectionComponent } from './components/edit-or-create-question-collection/edit-or-create-question-collection.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { QuestionCollectionRoutingModule } from './question-collection-routing.module';



@NgModule({
  declarations: [
    ListQuestionCollectionsComponent,
    EditOrCreateQuestionCollectionComponent
  ],
  imports: [
    CommonModule,
    AngularMaterialModule,
    QuestionCollectionRoutingModule
  ]
})
export class QuestionCollectionModule { }
