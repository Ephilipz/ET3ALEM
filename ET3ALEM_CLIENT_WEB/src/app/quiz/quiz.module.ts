import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizRoutingModule } from './quiz-routing.module';
import { TakeQuizComponent } from './components/take quiz/take-quiz/take-quiz.component';
import { ViewQuizComponent } from './components/take quiz/view-quiz/view-quiz.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { EditorModule } from '@tinymce/tinymce-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { QuizHomeComponent } from './components/quiz-home/quiz-home.component';
import { ListQuizzesComponent } from './components/list-quizzes/list-quizzes.component';
import { SharedComponentsModule } from '../Shared/modules/shared-components/shared-components.module';
import { QuestionModule } from '../question/question.module';
import { AccessQuizComponent } from './components/take quiz/access-quiz/access-quiz.component';
import { EditOrCreateQuizComponent } from './components/edit-create-quiz/edit-or-create-quiz.component';
import { QuizDetailsComponent } from './components/take quiz/quiz-details/quiz-details.component';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { ViewQuizResultComponent } from './components/quiz-results/view-quiz-result/view-quiz-result.component';
import { GradeQuizComponent } from './components/grade-quiz/grade-quiz.component';
import { QuizAttemptHistoryComponent } from './components/quiz-attempt-history/quiz-attempt-history.component';
import { QuizGradesComponent } from './components/quiz-grades/quiz-grades.component';
import { ClipboardModule } from '@angular/cdk/clipboard';

@NgModule({
  declarations: [
    EditOrCreateQuizComponent,
    TakeQuizComponent,
    ViewQuizComponent,
    QuizHomeComponent,
    ListQuizzesComponent,
    AccessQuizComponent,
    QuizDetailsComponent,
    ViewQuizResultComponent,
    GradeQuizComponent,
    QuizAttemptHistoryComponent,
    QuizGradesComponent
  ],
  imports: [
    CommonModule,
    QuizRoutingModule,
    AngularMaterialModule,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,
    NgxMatTimepickerModule,
    EditorModule,
    ReactiveFormsModule,
    FormsModule,
    ToastrModule.forRoot(),
    SharedComponentsModule,
    DragDropModule,
    ClipboardModule,
    QuestionModule]
})
export class QuizModule { }
