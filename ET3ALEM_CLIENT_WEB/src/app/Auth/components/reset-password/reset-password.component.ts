import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { confirmPasswordErrorStateMatcher } from 'src/app/Shared/Classes/forms/confirmPasswordErrorStateMatcher';
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent extends ExtraFormOptions implements OnInit {

  form = new FormGroup({
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    confirmPassword: new FormControl(''),
  }, { validators: this.checkMatch });
  token = '';
  matcher = new confirmPasswordErrorStateMatcher();
  isLoading = false;

  constructor(private AuthService: AuthService, private route: ActivatedRoute, private router: Router) {
    super();
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.token = params['token'];
    });
  }

  checkMatch(formGrp: FormGroup) {
    let _original = formGrp.get('password');
    let _confirm = formGrp.get('confirmPassword');
    if (!_original || !_confirm) return null;
    return _original.value === _confirm.value ? null : { notSame: true };
  }

  resetPassword() {
    this.isLoading = true;
    let resetPasswordVM = {
      recoveryToken: this.token,
      password: this.form.get('password').value,
      confirmPassword: this.form.get('confirmPassword').value,
    };
    this.AuthService.resetPassword(resetPasswordVM).pipe(filter(Boolean)).subscribe(
      () => this.router.navigateByUrl('/auth/login')
    )
  }

}
