import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QuizRoutingModule } from './quiz-routing.module';
import { CreateQuizComponent } from './components/create-quiz/create-quiz.component';
import { TakeQuizComponent } from './components/take-quiz/take-quiz.component';
import { ViewQuizComponent } from './components/view-quiz/view-quiz.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { EditorModule } from '@tinymce/tinymce-angular';

// import { RichTextEditorModule, ToolbarService, LinkService, ImageService, HtmlEditorService } from '@syncfusion/ej2-angular-richtexteditor';

import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [CreateQuizComponent, TakeQuizComponent, ViewQuizComponent],
  imports: [
    CommonModule,
    QuizRoutingModule,
    AngularMaterialModule,
    // RichTextEditorModule,
    CKEditorModule,
    EditorModule,
    ReactiveFormsModule,
    // FormsModule
  ],
  // providers: [ToolbarService, LinkService, ImageService, HtmlEditorService]
})
export class QuizModule { }
