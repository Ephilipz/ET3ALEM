import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListQuestionCollectionsComponent } from './components/list-question-collections/list-question-collections.component';
import { EditOrCreateQuestionCollectionComponent } from './components/edit-or-create-question-collection/edit-or-create-question-collection.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { QuestionCollectionRoutingModule } from './question-collection-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { QuestionModule } from '../question/question.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { AddFromQuestionCollectionDialogComponent } from './components/add-from-question-collection-dialog/add-from-question-collection-dialog.component';



@NgModule({
  declarations: [
    ListQuestionCollectionsComponent,
    EditOrCreateQuestionCollectionComponent,
    AddFromQuestionCollectionDialogComponent
  ],
  imports: [
    CommonModule,
    AngularMaterialModule,
    QuestionCollectionRoutingModule,
    ReactiveFormsModule,
    QuestionModule,
    DragDropModule,
    FormsModule
  ]
})
export class QuestionCollectionModule { }
