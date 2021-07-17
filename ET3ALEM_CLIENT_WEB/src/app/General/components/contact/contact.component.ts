import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private toastyService: ToastrService) { }
  @ViewChild('formDirective') private contactFormDirective: NgForm;
  contactForm = this.formBuilder.group({
    email: ['', Validators.required],
    subject: '',
    message: ['', Validators.required]
  });

  ngOnInit(): void {

  }

  onSubmit(contactForm: FormGroup, formDirective: FormGroupDirective) {
    this.http.post(environment.baseUrl + '/api/ContactUs', contactForm.value).subscribe({
      next: () => {
        contactForm.reset();
        formDirective.resetForm();
        this.toastyService.success('message sent successfully');
      }
    });
  }
}
