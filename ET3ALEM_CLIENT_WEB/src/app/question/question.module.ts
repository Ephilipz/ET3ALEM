import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {EditOrCreateQuestionHeaderComponent} from './edit-create-question/Edit-Create-QuestionHeader/edit-or-create-questionHeader.component';
import {AngularMaterialModule} from '../Shared/modules/material.module';
import {ReactiveFormsModule, FormsModule} from '@angular/forms';
import {SharedComponentsModule} from '../Shared/modules/shared-components/shared-components.module';
import {
  ConcreteEditQuestionMCQComponent
} from './edit-create-question/ConcreteQuestions/concrete-edit-question-mcq/concrete-edit-quesiton-mcq.component';
import {
  ConcreteEditQuestionTrueFalseComponent
} from './edit-create-question/ConcreteQuestions/concrete-edit-question-true-false/concrete-edit-question-true-false.component';
import {DynamicComponentHostDirective} from '../Shared/directives/dynamic-component-host.directive';
import {
  ConcreteAnswerQuestionMCQComponent
} from './answer-question/ConcreteAnswerQuestions/concrete-answer-question-mcq/concrete-answer-question-mcq.component';
import {
  ConcreteAnswerQuestionTFComponent
} from './answer-question/ConcreteAnswerQuestions/concrete-answer-question-tf/concrete-answer-question-tf.component';
import {AnswerQuestionHeaderComponent} from './answer-question/answer-question-header/answer-question-header.component';
import {RouterModule} from '@angular/router';
import {QuestionResultHeaderComponent} from './question result/question-result-header/question-result-header.component';
import {
  ConcreteQuestionResultMCQComponent
} from './question result/concrete-question-result/concrete-question-result-mcq/concrete-question-result-mcq.component';
import {
  ConcreteQuestionResultTFComponent
} from './question result/concrete-question-result/concrete-question-result-tf/concrete-question-result-tf.component';
import {
  ConcreteEditQuestionShortAnswerComponent
} from './edit-create-question/ConcreteQuestions/concrete-edit-question-short-answer/concrete-edit-question-short-answer.component';
import {
  ConcreteAnswerQuestionShortAnswerComponent
} from './answer-question/ConcreteAnswerQuestions/concrete-answer-question-short-answer/concrete-answer-question-short-answer.component';
import {
  ConcreteQuestionResultShortAnswerComponent
} from './question result/concrete-question-result/concrete-question-result-short-answer/concrete-question-result-short-answer.component';
import {
  ConcreteEditQuestionLongAnswerComponent
} from './edit-create-question/ConcreteQuestions/concrete-edit-question-long-answer/concrete-edit-question-long-answer.component';
import {
  ConcreteQuestionResultLongAnswerComponent
} from './question result/concrete-question-result/concrete-question-result-long-answer/concrete-question-result-long-answer.component';
import {
  ConcreteAnswerQuestionLongAnswerComponent
} from './answer-question/ConcreteAnswerQuestions/concrete-answer-question-long-answer/concrete-answer-question-long-answer.component';
import {
  ConcreteAnswerQuestionOrderComponent
} from './answer-question/ConcreteAnswerQuestions/concrete-answer-question-order/concrete-answer-question-order.component';
import {DragDropModule} from "@angular/cdk/drag-drop";
import {
  ConcreteEditQuestionOrderComponent
} from './edit-create-question/ConcreteQuestions/concrete-edit-question-order/concrete-edit-question-order.component';
import {
  ConcreteQuestionResultOrderComponent
} from './question result/concrete-question-result/concrete-question-result-order/concrete-question-result-order.component';

@NgModule({
  declarations: [EditOrCreateQuestionHeaderComponent, ConcreteEditQuestionMCQComponent, ConcreteEditQuestionTrueFalseComponent, DynamicComponentHostDirective, AnswerQuestionHeaderComponent, ConcreteAnswerQuestionMCQComponent, ConcreteAnswerQuestionTFComponent, QuestionResultHeaderComponent,
    ConcreteQuestionResultMCQComponent,
    ConcreteQuestionResultTFComponent,
    ConcreteEditQuestionShortAnswerComponent,
    ConcreteAnswerQuestionShortAnswerComponent,
    ConcreteQuestionResultShortAnswerComponent,
    ConcreteEditQuestionLongAnswerComponent,
    ConcreteQuestionResultLongAnswerComponent,
    ConcreteAnswerQuestionLongAnswerComponent,
    ConcreteAnswerQuestionOrderComponent,
    ConcreteEditQuestionOrderComponent,
    ConcreteQuestionResultOrderComponent],
  imports: [
    CommonModule,
    AngularMaterialModule,
    SharedComponentsModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    DragDropModule
  ],
  exports: [
    EditOrCreateQuestionHeaderComponent,
    AnswerQuestionHeaderComponent,
    QuestionResultHeaderComponent]
})
export class QuestionModule {
}
