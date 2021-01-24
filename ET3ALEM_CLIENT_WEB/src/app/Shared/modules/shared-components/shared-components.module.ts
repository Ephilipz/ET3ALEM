import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RichTextEditorComponent } from './rich-text-editor/rich-text-editor.component';
import { EditorModule } from '@tinymce/tinymce-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { CountDownTimerComponent } from './count-down-timer/count-down-timer.component';



@NgModule({
  declarations: [RichTextEditorComponent, CountDownTimerComponent],
  imports: [
    CommonModule,
    EditorModule,
    ReactiveFormsModule
  ],
  exports: [
    RichTextEditorComponent,
    CountDownTimerComponent
  ]
})
export class SharedComponentsModule { }
