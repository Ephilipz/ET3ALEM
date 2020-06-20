import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateQuestionComponent } from './create-question/create-question.component';
import { ViewQuestionComponent } from './view-question/view-question.component';



@NgModule({
  declarations: [CreateQuestionComponent, ViewQuestionComponent],
  imports: [
    CommonModule
  ]
})
export class QuestionModule { }
