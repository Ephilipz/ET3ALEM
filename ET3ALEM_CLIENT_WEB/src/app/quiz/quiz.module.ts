import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizRoutingModule } from './quiz-routing.module';
import { TakeQuizComponent } from './components/take-quiz/take-quiz.component';
import { ViewQuizComponent } from './components/view-quiz/view-quiz.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { EditorModule } from '@tinymce/tinymce-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { AuthInterceptor } from '../Shared/services/auth.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { QuizHomeComponent } from './components/quiz-home/quiz-home.component';
import { ListQuizzesComponent } from './components/list-quizzes/list-quizzes.component';
import { BrowserModule } from '@angular/platform-browser';
import { SharedComponentsModule } from '../Shared/modules/shared-components/shared-components.module';
import { QuestionModule } from '../question/question.module';
import { AccessQuizComponent } from './components/access-quiz/access-quiz.component';
import { EditOrCreateQuizComponent } from './components/edit-create-quiz/edit-or-create-quiz.component';
import { QuizDetailsComponent } from './components/quiz-details/quiz-details.component';

@NgModule({
  declarations: [EditOrCreateQuizComponent, TakeQuizComponent, ViewQuizComponent, QuizHomeComponent, ListQuizzesComponent, AccessQuizComponent, QuizDetailsComponent],
  imports: [
    CommonModule,
    QuizRoutingModule,
    AngularMaterialModule,
    EditorModule,
    ReactiveFormsModule,
    FormsModule,
    ToastrModule.forRoot(),
    SharedComponentsModule,
    QuestionModule]
})
export class QuizModule { }
