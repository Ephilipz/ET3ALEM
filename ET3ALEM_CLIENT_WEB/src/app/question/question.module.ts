import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditOrCreateQuestionComponent } from './create-question/edit-or-create-question.component';
import { ViewQuestionComponent } from './view-question/view-question.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedComponentsModule } from '../Shared/modules/shared-components/shared-components.module';




@NgModule({
  declarations: [EditOrCreateQuestionComponent, ViewQuestionComponent],
  imports: [
    CommonModule,
    AngularMaterialModule,
    SharedComponentsModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    EditOrCreateQuestionComponent,
  ]
})
export class QuestionModule { }
