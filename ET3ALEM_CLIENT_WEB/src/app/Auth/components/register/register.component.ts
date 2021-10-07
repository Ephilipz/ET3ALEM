import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions';
import { confirmPasswordErrorStateMatcher } from 'src/app/Shared/Classes/forms/confirmPasswordErrorStateMatcher';
import { AuthService } from '../../services/auth.service';
import { RegisterUser } from '../../Model/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent extends ExtraFormOptions implements OnInit {

  registerForm = new FormGroup({
    name: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    confirmPassword: new FormControl(''),
  }, { validators: this.checkMatch });

  matcher = new confirmPasswordErrorStateMatcher();

  constructor(private AuthService: AuthService, private router: Router) {
    super();
  }

  ngOnInit(): void {
  }

  checkMatch(formGrp: FormGroup) {
    let _original = formGrp.get('password');
    let _confirm = formGrp.get('confirmPassword');
    if (!_original || !_confirm) return null;
    return _original.value === _confirm.value ? null : { notSame: true };
  }

  register() {
    let registerUserObject: RegisterUser = new RegisterUser(
      this.registerForm.get('name').value,
      this.registerForm.get('email').value,
      this.registerForm.get('password').value,
    );
    this.AuthService.register(registerUserObject).subscribe(
      res => {
        this.router.navigateByUrl('/quiz');
      }
    )
  }



}
