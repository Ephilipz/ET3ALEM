import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as moment from "moment"
import { LoginUser, RegisterUser } from '../Model/User';
import { pluck, share, shareReplay, tap, catchError, mapTo } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Tokens } from '../Model/Tokens';
import { Observable, of, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ErrorStateMatcher } from '@angular/material/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  JWT = 'id_token';
  Refresh = 'refresh';

  constructor(private http: HttpClient, private router: Router, private toastyService: ToastrService) { }

  login(email: string, password: string): Observable<boolean> {
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/login', { email, password }).pipe(
      tap(
        tokens => {
          this.setSession(tokens);
          this.toastyService.success('Welcome');
        }),
      mapTo(true),
      catchError(err => {
        if (err.error[Object.keys(err.error)[0]][0] != undefined)
          this.toastyService.error(err.error[Object.keys(err.error)[0]][0])
        else
          this.toastyService.error(err);
        return of(false);
      }));

  }

  register(registerUserObject: RegisterUser) {
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/Register', {
      ...registerUserObject,
      'ConfirmPassword': registerUserObject.Password
    }).pipe(
      tap(
        token => {
          this.setSession(token);
          this.toastyService.success('Welcome');
        }),
      mapTo(true),
      catchError(err => {
        if (err.error[Object.keys(err.error)[0]][0] != undefined) {
          this.toastyService.clear();
          this.toastyService.error(err.error[Object.keys(err.error)[0]][0], 'Error');
        }
        else
          this.toastyService.error('Unable to register', 'Unknown Error');
        return throwError(err);
      }));
  }

  refresh() {
    console.log('refreshing');
    let oldTokens: Tokens = new Tokens(this.getJWT(), this.getRefreshToken());
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/Refresh', oldTokens).pipe(
      tap((tokens: Tokens) => {
        this.setSession(tokens);
      }));
  }

  setSession(tokens: Tokens) {
    localStorage.setItem(this.JWT, tokens.JWT);
    localStorage.setItem(this.Refresh, tokens.RefreshToken);
  }

  logout() {
    localStorage.removeItem(this.JWT);
    localStorage.removeItem(this.Refresh);
    return this.http.get(environment.baseUrl + '/api/Account/Logout');
  }

  public isLoggedIn() {
    return !!this.getJWT();
  }

  public isLoggedOut() {
    return !this.isLoggedIn();
  }

  getJWT() {
    return localStorage.getItem(this.JWT);
  }

  getRefreshToken() {
    return localStorage.getItem(this.Refresh);
  }

  sendRecoveryMail(email: string) {
    return this.http.post(environment.baseUrl + '/api/Account/sendRecoveryMail', { email }).pipe(
      tap(() => this.toastyService.success('email is sent, check you inbox', 'success')),
      mapTo(true),
      catchError(error => {
        if (error.status == 404) {
          this.toastyService.error('email does not exist', 'error');
        } else {
          this.toastyService.error('error sending email', 'error');
        }
        return of(false);
      }));
  }
  resetPassword(resetPasswordVM: { recoveryToken: string, password: string, confirmPassword: string }) {
    return this.http.post(environment.baseUrl + '/api/Account/ResetPassword', resetPasswordVM).pipe(
      tap(() => this.toastyService.success('password is reset', 'success')),
      mapTo(true),
      catchError(error => {
        if (error.status == 404) {
          this.toastyService.error('invalid reset link', 'error');
        } else {
          this.toastyService.error('error resetting password', 'error');
        }
        return of(false);
      })
    );
  }


}
