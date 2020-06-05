import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent extends ExtraFormOptions implements OnInit {

  constructor() {
    super();
  }

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.email, Validators.required]),
    password: new FormControl('', [Validators.minLength(8), Validators.maxLength(20), Validators.required]),
  });

  ngOnInit(): void {
  }

  login() {
    let email = this.loginForm.get('email').value;
    let password = this.loginForm.get('password').value;
  }

}