import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditOrCreateQuestionHeaderComponent } from './edit-create-question/Edit-Create-QuestionHeader/edit-or-create-questionHeader.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedComponentsModule } from '../Shared/modules/shared-components/shared-components.module';
import { ConcreteEditQuestionMCQComponent } from './edit-create-question/ConcreteQuestions/concrete-edit-question-mcq/concrete-edit-quesiton-mcq.component';
import { ConcreteEditQuestionTrueFalseComponent } from './edit-create-question/ConcreteQuestions/concrete-edit-question-true-false/concrete-edit-question-true-false.component';
import { DynamicComponentHostDirective } from '../Shared/directives/dynamic-component-host.directive';
import { ConcreteAnswerQuestionMCQComponent } from './answer-question/ConcreteAnswerQuestions/concrete-answer-question-mcq/concrete-answer-question-mcq.component';
import { ConcreteAnswerQuestionTFComponent } from './answer-question/ConcreteAnswerQuestions/concrete-answer-question-tf/concrete-answer-question-tf.component';
import { AnswerQuestionHeaderComponent } from './answer-question/answer-question-header/answer-question-header.component';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { QuestionResultHeaderComponent } from './question result/question-result-header/question-result-header.component';
import { ConcreteQuestionResultMCQComponent } from './question result/concrete-question-result/concrete-question-result-mcq/concrete-question-result-mcq.component';
import { ConcreteQuestionResultTFComponent } from './question result/concrete-question-result/concrete-question-result-tf/concrete-question-result-tf.component';
import { ConcreteEditQuestionShortAnswerComponent } from './edit-create-question/ConcreteQuestions/concrete-edit-question-short-answer/concrete-edit-question-short-answer.component';
import { ConcreteAnswerQuestionShortAnswerComponent } from './answer-question/ConcreteAnswerQuestions/concrete-answer-question-short-answer/concrete-answer-question-short-answer.component';
import { ConcreteQuestionResultShortAnswerComponent } from './question result/concrete-question-result/concrete-question-result-short-answer/concrete-question-result-short-answer.component';

@NgModule({
  declarations: [EditOrCreateQuestionHeaderComponent, ConcreteEditQuestionMCQComponent, ConcreteEditQuestionTrueFalseComponent, DynamicComponentHostDirective, AnswerQuestionHeaderComponent, ConcreteAnswerQuestionMCQComponent, ConcreteAnswerQuestionTFComponent, QuestionResultHeaderComponent,
    ConcreteQuestionResultMCQComponent,
    ConcreteQuestionResultTFComponent,
    ConcreteEditQuestionShortAnswerComponent,
    ConcreteAnswerQuestionShortAnswerComponent,
    ConcreteQuestionResultShortAnswerComponent],
  imports: [
    CommonModule,
    AngularMaterialModule,
    SharedComponentsModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule
  ],
  exports: [
    EditOrCreateQuestionHeaderComponent,
    AnswerQuestionHeaderComponent,
    QuestionResultHeaderComponent]
})
export class QuestionModule { }
