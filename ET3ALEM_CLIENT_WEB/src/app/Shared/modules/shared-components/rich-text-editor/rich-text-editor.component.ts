import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { GeneralHelper } from 'src/app/Shared/Classes/helpers/GeneralHelper';
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
  readonly imageSizeLimit = 5;

  uploadedImages: Array<ImageUrls> = [];

  ngOnInit(): void {
  }

  tinyMCESettings = {
    height: 400,
    menubar: false,
    plugins: [
      'forecolor autolink lists link imagetools image',
      'preview code advlist tiny_mce_wiris',
    ],
    external_plugins: { tiny_mce_wiris: 'https://www.wiris.net/demo/plugins/tiny_mce/plugin.js' },
    toolbar:
      'undo redo | formatselect forecolor | bold italic | \
      alignleft aligncenter alignright alignjustify | \
      bullist numlist outdent indent | link preview | \ image tiny_mce_wiris_formulaEditor tiny_mce_wiris_formulaEditorChemistry',

    images_upload_handler:
      (blobInfo, success, failure, progress) => {
        let formData;
        let xhr: XMLHttpRequest;

        xhr = new XMLHttpRequest();

        xhr.open('POST', environment.baseUrl + '/api/Storage/UploadImage', true);

        xhr.upload.onprogress = function (e) {
          progress((e.loaded / e.total * 100).toFixed(0));
        };

        xhr.onload = () => {

          if (xhr.status < 200 || xhr.status >= 300) {
            failure('HTTP Error: ' + xhr.status);
            return;
          }

          this.uploadedImages.push(new ImageUrls(xhr.responseText, 'deleteHash'));

          success(xhr.responseText);
        };

        xhr.onerror = function () {
          failure('Image upload failed due to a XHR Transport error. Code: ' + xhr.status);
        };

        formData = new FormData();

        if (GeneralHelper.BtoMB(blobInfo.blob().size) > this.imageSizeLimit) {
          failure(`file cannot be larger than ${this.imageSizeLimit} mb`);
          return;
        }

        formData.append('File', blobInfo.blob(), blobInfo.blob().name);
        formData.append('FileName', blobInfo.blob().name);

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
    //TODO : delete unused images logic
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
