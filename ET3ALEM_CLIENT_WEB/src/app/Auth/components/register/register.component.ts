import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions';
import { confirmPasswordErrorStateMatcher } from 'src/app/Shared/Classes/forms/confirmPasswordErrorStateMatcher';
import { AuthService } from '../../services/auth.service';
import { RegisterUser } from '../../Model/User';
import { Role } from '../../Model/UserEnums';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent extends ExtraFormOptions implements OnInit {

  studentForm = new FormGroup({
    name: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    confirmPassword: new FormControl(''),
  }, { validators: this.checkMatch });

  matcher = new confirmPasswordErrorStateMatcher();

  constructor(private AuthService: AuthService) {
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
      this.studentForm.get('name').value,
      this.studentForm.get('email').value,
      this.studentForm.get('password').value,
      Role.Student
    );
      this.AuthService.register(registerUserObject)


  }



}
