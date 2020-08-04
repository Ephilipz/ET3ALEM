import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QuizRoutingModule } from './quiz-routing.module';
import { EditOrCreateQuizComponent } from './components/create-quiz/edit-or-create-quiz.component';
import { TakeQuizComponent } from './components/take-quiz/take-quiz.component';
import { ViewQuizComponent } from './components/view-quiz/view-quiz.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { EditorModule } from '@tinymce/tinymce-angular';

import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { AuthInterceptor } from '../Shared/services/auth.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { QuizHomeComponent } from './components/quiz-home/quiz-home.component';
import { ListQuizzesComponent } from './components/list-quizzes/list-quizzes.component';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [EditOrCreateQuizComponent, TakeQuizComponent, ViewQuizComponent, QuizHomeComponent, ListQuizzesComponent],
  imports: [
    CommonModule,
    QuizRoutingModule,
    AngularMaterialModule,
    CKEditorModule,
    AngularMaterialModule,
    EditorModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
  ]
})
export class QuizModule { }
