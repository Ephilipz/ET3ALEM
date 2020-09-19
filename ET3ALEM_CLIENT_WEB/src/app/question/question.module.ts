import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateQuestionComponent } from './create-question/create-question.component';
import { ViewQuestionComponent } from './view-question/view-question.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedComponentsModule } from '../Shared/modules/shared-components/shared-components.module';




@NgModule({
  declarations: [CreateQuestionComponent, ViewQuestionComponent],
  imports: [
    CommonModule,
    AngularMaterialModule,
    SharedComponentsModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    CreateQuestionComponent,
  ]
})
export class QuestionModule { }
