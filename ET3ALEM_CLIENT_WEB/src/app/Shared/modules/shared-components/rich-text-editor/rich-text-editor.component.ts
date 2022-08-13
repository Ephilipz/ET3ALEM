import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {FormControl} from '@angular/forms';
import {environment} from 'src/environments/environment';
import {GeneralHelper} from 'src/app/Shared/Classes/helpers/GeneralHelper';
import {HttpClient} from '@angular/common/http';
import {Path} from "../../../Classes/helpers/path";

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
    external_plugins: {tiny_mce_wiris: 'https://www.wiris.net/demo/plugins/tiny_mce/plugin.js'},
    toolbar:
      'undo redo | formatselect forecolor | bold italic | \
      alignleft aligncenter alignright alignjustify | \
      bullist numlist outdent indent | link preview | \ image tiny_mce_wiris_formulaEditor tiny_mce_wiris_formulaEditorChemistry',

    images_upload_handler:
      (blobInfo, success, failure, progress) => {
        let formData = new FormData();
        formData.append('File', blobInfo.blob(), blobInfo.blob().name);
        formData.append('FileName', blobInfo.blob().name);
        this.http.post(Path.join(environment.baseUrl, 'api/Storage/UploadImage'), formData)
          .subscribe(
            (res) => {
              console.log(res);
              success(res['StorageLink']);
            },
            (err) =>
            {
              failure(err);
            }
          )
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
