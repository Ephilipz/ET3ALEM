import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-rich-text-editor',
  templateUrl: './rich-text-editor.component.html',
  styleUrls: ['./rich-text-editor.component.css']
})
export class RichTextEditorComponent implements OnInit {

  constructor(private http: HttpClient) {
  }

  @Input('control') _formControl: FormControl;
  @Output() onLoad = new EventEmitter<void>();

  uploadedImages: Array<ImageUrls> = [];

  ngOnInit(): void {
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

        if (Helper.BtoMB(blobInfo.blob().size) > 2) {
          failure('file cannot be larger than 2 mb');
          return;
        }

        formData.append('image', blobInfo.blob(), blobInfo.filename());

        xhr.send(formData);
      }

  }

  Loaded() {
    this.onLoad.emit();
  }

  async removeUnusedImages() {
    let imagesToBeDeleted: Array<string> = [];

    //check if images are being used. Add unused images to the delete array
    this.uploadedImages.forEach(element => {
      if (!(<string>this._formControl.value ?? '').includes(element.imageURL)) {
        imagesToBeDeleted.push(element.deleteURL);
      }
    });

    //iterate over the delete array
    await imagesToBeDeleted.forEach(async deletHash => {
      await this.http.delete(environment.postUploadImgurImage + '/' + deletHash, {
        headers: {
          'Authorization': 'Client-ID ' + environment.imgurClientId
        }
      });
    });
  }
}

export class ImageUrls {

  imageURL: string;
  deleteURL: string;

  constructor(imageUrl, deleteUrl) {
    this.imageURL = imageUrl;
    this.deleteURL = deleteUrl;
  }
}