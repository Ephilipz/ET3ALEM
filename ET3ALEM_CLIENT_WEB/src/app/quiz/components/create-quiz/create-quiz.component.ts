import { Component, OnInit, ViewChild } from '@angular/core';

import * as Editor from '../../../../assets/js/ck-editor-custom-build/ckeditor.js'
// import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic',
import { environment } from 'src/environments/environment';
import { CustomImageUploadAdapter } from 'src/app/Shared/Classes/forms/CustomImageUploadAdapter.js';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import * as moment from 'moment'

@Component({
  selector: 'app-create-quiz',
  templateUrl: './create-quiz.component.html',
  styleUrls: ['./create-quiz.component.css']
})


export class CreateQuizComponent implements OnInit {

  public Editor = Editor;
  public editText;

  public editorConfig;

  public quizInstructions: string = '';

  today: Date = moment().toDate();

  // quizBasicDataForm : new FormGroup({
  quizTitle = new FormControl('', [Validators.required]);
  durationHours = new FormControl(1, [Validators.max(5), Validators.min(0)]);
  durationMinutes = new FormControl(0, [Validators.max(59), Validators.min(0)]);
  noDuration = new FormControl(false);
  dueStart = new FormControl(moment().toDate());
  dueEnd = new FormControl(moment().add(10, 'days').format());
  noDueDate = new FormControl(false);
  // });

  constructor() { }

  ngOnInit(): void {
  }

  //#region CKEditor
  CustomImageUploadAdapterPlugin(editor) {
    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
      // Configure the URL to the upload script in your back-end here!
      return new CustomImageUploadAdapter(loader, editor.config);
    };
  }
  public onChange(event) {
    // console.log(this.quizInstructions);
    console.log(this.editText.getData());
  }


  onReady(editor) {
    console.log(editor.config);
  }

  //#endregion

  //#region TINYMCE
  tinyMCESettings = {
    height: 400,
    menubar: false,
    plugins: [
      'advlist autolink lists link image charmap print',
      'preview anchor searchreplace visualblocks code',
      'fullscreen insertdatetime media table paste',
      'help wordcount'
    ],
    toolbar:
      'undo redo | formatselect | bold italic | \
      alignleft aligncenter alignright alignjustify | \
      bullist numlist outdent indent | help'
  }

  tinyMCEEditor: '';
  //#endregion

  toggleDisable(checked: boolean, list: Array<string>) {
    list.forEach((x) => {
      if(checked){
        this[x].disable();
      }
      else{
        this[x].enable();
      }
    });
  }

}
