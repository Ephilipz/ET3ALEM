import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

// import * as Editor from '../../../../assets/js/ck-editor-custom-build/ckeditor.js'
import { environment } from 'src/environments/environment';
import { CustomImageUploadAdapter } from 'src/app/Shared/Classes/forms/CustomImageUploadAdapter.js';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import * as moment from 'moment'
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions.js';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { QuizModule } from '../../quiz.module.js';
import { Quiz } from '../../Model/quiz.js';
import { QuizService } from '../../services/quiz.service.js';
import { Converter } from 'src/app/Shared/Classes/helpers/Coverter.js';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-create-quiz',
  templateUrl: './edit-or-create-quiz.component.html',
  styleUrls: ['./edit-or-create-quiz.component.css']
})

export class EditOrCreateQuizComponent extends ExtraFormOptions implements OnInit {

  //manage create and edit modes
  mode: mode = mode.create;
  currentQuiz: Quiz;

  isLoaded: boolean = false;

  today: Date = moment().toDate();

  uploadedImages: Array<ImageUrls> = [];

  quizTitle = new FormControl('', [Validators.required]);
  quizInstructions = new FormControl();
  durationHours = new FormControl(1, [Validators.max(5), Validators.min(0)]);
  durationMinutes = new FormControl(0, [Validators.max(59), Validators.min(0)]);
  unlimitedTime = new FormControl(false);
  dueStart = new FormControl(moment().toDate());
  dueEnd = new FormControl(moment().add(3, 'days').format());
  noDueDate = new FormControl(false);

  constructor(private toastr: ToastrService, private quizService: QuizService, private route: ActivatedRoute, private http: HttpClient) {
    super();
  }

  ngOnInit(): void {
    let id: Number = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.mode = mode.edit;
      this.quizService.getQuiz(id).subscribe(
        res => {
          this.currentQuiz = res;
          this.setFormControls();
          this.isLoaded = true;
        },
        err => {
          this.toastr.error('unable to open quiz');
          console.error(err);
        }
      )
    }
    else {
      this.isLoaded = true;
    }
  }

  setFormControls() {
    this.quizTitle.setValue(this.currentQuiz.Name)
    this.quizInstructions.setValue(this.currentQuiz.instructions);
    this.durationHours.setValue(this.currentQuiz.durationHours);
    this.durationMinutes.setValue(this.currentQuiz.durationMinutes);
    this.unlimitedTime.setValue(this.currentQuiz.UnlimitedTime);
    this.dueStart.setValue(this.currentQuiz.StartDate);
    this.dueEnd.setValue(this.currentQuiz.EndDate);
    this.noDueDate.setValue(this.currentQuiz.NoDueDate);

    if (this.currentQuiz.UnlimitedTime) {
      this.durationHours.disable();
      this.durationMinutes.disable();
    }

    if (this.currentQuiz.NoDueDate) {
      this.dueEnd.disable();
    }
  }

  tinyMCESettings = {
    height: 400,
    menubar: false,
    plugins: [
      'forecolor autolink lists link imagetools image',
      'preview code advlist',
    ],
    toolbar:
      'undo redo | formatselect forecolor | bold italic | \
      alignleft aligncenter alignright alignjustify | \
      bullist numlist outdent indent | link image preview',

    images_upload_handler:
      (blobInfo, success, failure, progress) => {
        let formData;
        let xhr: XMLHttpRequest;

        console.log(blobInfo.blob());

        xhr = new XMLHttpRequest();

        xhr.open('POST', environment.postUploadImgurImage, true);
        xhr.setRequestHeader('Authorization', 'Client-ID ' + environment.imgurClientId)

        xhr.upload.onprogress = function (e) {
          progress((e.loaded / e.total * 100).toFixed(0));
        };

        xhr.onload = () => {

          var json;

          if (xhr.status < 200 || xhr.status >= 300) {
            failure('HTTP Error: ' + xhr.status);
            return;
          }

          json = JSON.parse(xhr.responseText);

          if (!json || typeof json.data.link != 'string') {
            failure('Invalid JSON: ' + xhr.responseText);
            return;
          }

          this.uploadedImages.push(new ImageUrls(json.data.link, json.data.deletehash));
          
          success(json.data.link);
        };

        xhr.onerror = function () {
          failure('Image upload failed due to a XHR Transport error. Code: ' + xhr.status);
        };

        formData = new FormData();

        if (Converter.BtoMB(blobInfo.blob().size) > 2) {
          failure('file cannot be larger than 2 mb');
          return;
        }

        formData.append('image', blobInfo.blob(), blobInfo.filename());

        xhr.send(formData);
      }

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

  async removeUnusedImages() {
    let imagesToBeDeleted: Array<string> = [];

    //check if images are being used. Add unused images to the delete array
    this.uploadedImages.forEach(element => {
      if (!(<string>this.quizInstructions.value ?? '').includes(element.imageURL)) {
        imagesToBeDeleted.push(element.deleteURL);
      }
    });

    //iterate over the delete array
    imagesToBeDeleted.forEach(deletHash => {
      this.http.delete(environment.postUploadImgurImage + '/' + deletHash, {
        headers: {
          'Authorization': 'Client-ID ' + environment.imgurClientId
        }
      }).subscribe(
        res => console.log('success ', res),
        err => console.log('error ', err)
      )
    });
  }

  validate(): boolean {
    //check duration
    if ((this.durationHours.value ?? 0) * 60 + (this.durationMinutes.value ?? 0) < 5 && !this.unlimitedTime.value) {
      this.toastr.error('duration must be at least 5 minutes or unlimited time');
      return false;
    }
    return true;
  }

  async createQuiz() {
    if (!this.validate())
      return;

    await this.removeUnusedImages();

    this.currentQuiz = new Quiz(0, this.quizTitle.value, this.quizInstructions.value, this.durationHours.value, this.durationMinutes.value, this.unlimitedTime.value, this.dueStart.value, this.dueEnd.value, this.noDueDate.value);

    this.quizService.createQuiz(this.currentQuiz).subscribe(
      result => {
        this.toastr.success('Quiz Created');
      },
      error => {
        this.toastr.error('Quiz not created');
      }
    )
  }

  updateQuiz() {
    if (!this.validate())
      return;

    this.currentQuiz = new Quiz(this.currentQuiz.Id, this.quizTitle.value, this.quizInstructions.value, this.durationHours.value, this.durationMinutes.value, this.unlimitedTime.value, this.dueStart.value, this.dueEnd.value, this.noDueDate.value);

    this.quizService.updateQuiz(this.currentQuiz).subscribe(
      result => {
        this.toastr.success('Quiz Updated');
      },
      error => {
        this.toastr.error('Quiz not updated');
      }
    )
  }

}

enum mode {
  edit,
  create
}

class ImageUrls {
  imageURL: string;
  deleteURL: string;
  constructor(imageUrl, deleteUrl) {
    this.imageURL = imageUrl;
    this.deleteURL = deleteUrl;
  }
}
