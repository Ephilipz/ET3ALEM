import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import {ExtraFormOptions} from 'src/app/Shared/Classes/forms/ExtraFormOptions';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent extends ExtraFormOptions implements OnInit {

  isLoading = false;

  constructor(private authService: AuthService, private router: Router) {
    super();
  }

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.email, Validators.required]),
    password: new FormControl('', [Validators.minLength(8), Validators.maxLength(20), Validators.required]),
  });

  ngOnInit(): void {
  }

  login() {
    const email: string = this.loginForm.get('email').value;
    const password: string = this.loginForm.get('password').value;
    this.isLoading = true;
    this.authService.login(email, password).subscribe({
      next: res => {
        this.isLoading = false;
        if (res) {
          if (this.authService.nextUrlPath.length > 0) {
            this.router.navigate(['/' + this.authService.nextUrlPath]);
            return;
          }
          this.router.navigate(['/quiz']);
        }
      },
      complete: () => this.isLoading = false
    });
  }
}
