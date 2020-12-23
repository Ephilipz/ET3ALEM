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

@Injectable({
  providedIn: 'root'
})
export class AuthService {

   JWT = 'id_token';
   Refresh = 'refresh';
  
  constructor(private http: HttpClient, private router: Router, private toastr: ToastrService) { }

  login(email: string, password: string): Observable<boolean> {
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/login', { email, password }).pipe(
      tap(
        token => {
          this.setSession(token);
          this.toastr.success('Welcome');
        }),
      mapTo(true),
      catchError(err => {
        if (err.error[Object.keys(err.error)[0]][0] != undefined)
          this.toastr.error(err.error[Object.keys(err.error)[0]][0])
        else
          this.toastr.error(err);
        return of(false);
      }));

  }

  register(registerUserObject: RegisterUser) {
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/Register', {
      'Email': registerUserObject.email,
      'Password': registerUserObject.password,
      'ConfirmPassword': registerUserObject.password
    }).pipe(
      tap(
        token => {
          this.setSession(token);
          this.toastr.success('Welcome');
        }),
      mapTo(true),
      catchError(err => {
        if (err.error[Object.keys(err.error)[0]][0] != undefined) {
          this.toastr.clear();
          this.toastr.error(err.error[Object.keys(err.error)[0]][0], 'Error');
        }
        else
          this.toastr.error('Unable to register', 'Unknown Error');
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
    console.log(tokens);
    localStorage.setItem(this.JWT, tokens.JWT);
    localStorage.setItem(this.Refresh, tokens.RefreshToken);
  }

  logout() {
    localStorage.removeItem(this.JWT);
    localStorage.removeItem(this.Refresh);
  }

  public isLoggedIn() {
    return !!this.getJWT();
  }

  public isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at");
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }

  getJWT() {
    return localStorage.getItem(this.JWT);
  }

  getRefreshToken() {
    return localStorage.getItem(this.Refresh);
  }


}
