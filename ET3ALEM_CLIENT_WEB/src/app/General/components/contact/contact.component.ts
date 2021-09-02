import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {ProfileService} from "../../../profile/services/profile.service";
import {AuthService} from "../../../auth/services/auth.service";
import {ContactService} from "./contact.service";

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
              private toastyService: ToastrService,
              private profileService: ProfileService,
              private contactService: ContactService,
              private authService: AuthService) {
  }

  @ViewChild('formDirective') private contactFormDirective: NgForm;
  contactForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    subject: '',
    message: ['', Validators.required]
  });

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.getUserEmail();
    }
  }

  private getUserEmail() {
    this.profileService.getUserEmail().subscribe(
      (response) => {
        const emailControl = this.contactForm.controls['email'];
        emailControl.setValue(response.email);
        emailControl.disable();
      }
    )
  }

  onSubmit(contactForm: FormGroup, formDirective: FormGroupDirective) {
    this.contactService.submitContactForm(this.contactForm.value).subscribe({
      next: () => {
        contactForm.reset();
        formDirective.resetForm();
        this.toastyService.success('message sent successfully');
      }
    });
  }
}
