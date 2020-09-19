import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RichTextEditorComponent } from './rich-text-editor/rich-text-editor.component';
import { EditorModule } from '@tinymce/tinymce-angular';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [RichTextEditorComponent],
  imports: [
    CommonModule,
    EditorModule,
    ReactiveFormsModule
  ],
  exports: [
    RichTextEditorComponent
  ]
})
export class SharedComponentsModule { }
