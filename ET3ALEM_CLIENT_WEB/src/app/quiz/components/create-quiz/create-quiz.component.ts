import { Component, OnInit, ViewChild } from '@angular/core';

import * as Editor from '../../../../assets/js/ck-editor-custom-build/ckeditor.js'
import { environment } from 'src/environments/environment';
import { CustomImageUploadAdapter } from 'src/app/Shared/Classes/forms/CustomImageUploadAdapter.js';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import * as moment from 'moment'
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions.js';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { QuizModule } from '../../quiz.module.js';
import { Quiz } from '../../Model/quiz.js';
import { QuizService } from '../../services/quiz.service.js';

@Component({
  selector: 'app-create-quiz',
  templateUrl: './create-quiz.component.html',
  styleUrls: ['./create-quiz.component.css']
})


export class CreateQuizComponent extends ExtraFormOptions implements OnInit {

  public Editor = Editor;
  public editText;

  public editorConfig;

  public quizInstructions: string = '';

  today: Date = moment().toDate();

  quizTitle = new FormControl('', [Validators.required]);
  durationHours = new FormControl(1, [Validators.max(5), Validators.min(0)]);
  durationMinutes = new FormControl(0, [Validators.max(59), Validators.min(0)]);
  unlimitedTime = new FormControl(false);
  dueStart = new FormControl(moment().toDate());
  dueEnd = new FormControl(moment().add(3, 'days').format());
  noDueDate = new FormControl(false);

  constructor(private toastr: ToastrService, private quizService: QuizService) {
    super();
  }

  ngOnInit(): void {
  }

  CustomImageUploadAdapterPlugin(editor) {
    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
      // Configure the URL to the upload script in your back-end here!
      return new CustomImageUploadAdapter(loader, editor.config);
    };
  }
  public onChange(event) {
    console.log(this.editText.getData());
  }

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

  toggleDisable(checked: boolean, list: Array<string>) {
    list.forEach((x) => {
      if (checked) {
        this[x].disable();
      }
      else {
        this[x].enable();
      }
    });
  }

  subtractDays(date: Date, days = 1) {
    return moment(date).subtract(days, 'days').toDate();
  }

  createQuiz() {
    //check duration
    if (this.durationHours.value * 60 + this.durationMinutes.value < 5 && !this.unlimitedTime.value) {
      this.toastr.error('duration must be at least 5 minutes');
      return;
    }

    let quiz: Quiz = new Quiz(this.quizTitle.value, this.quizInstructions, this.durationHours.value, this.durationMinutes.value, this.unlimitedTime.value, this.dueStart.value, this.dueEnd.value, this.noDueDate.value);

    this.quizService.createQuiz(quiz).subscribe(
      result => {
        this.toastr.success('Quiz Created');
        console.log(result);
      },
      error => {
        this.toastr.error('Quiz not created');
        console.error(error);
      }
    )
  }

}
