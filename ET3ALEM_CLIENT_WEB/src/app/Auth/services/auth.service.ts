import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as moment from "moment"
import { LoginUser, RegisterUser } from '../Model/User';
import { pluck, share, shareReplay, tap, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  constructor(private http: HttpClient, private router: Router) { }

  login(email: string, password: string) {
    return this.http.post<LoginUser>(environment.baseUrl + '/api/login', { email, password }).pipe(
      tap(this.setSession),
      shareReplay()
    )
  }

  register(registerUserObject: RegisterUser) {
    return this.http.post(environment.baseUrl + '/api/Account/Register', {
      'Email': registerUserObject.email,
      'Password': registerUserObject.password,
      'ConfirmPassword': registerUserObject.password
    }).subscribe(token => {
      this.setSession(token);
      this.router.navigateByUrl('/');
    }, err => console.error(err.error[Object.keys(err.error)[0]][0]));
  }

  setSession(authResult) {
    console.log('jwt', authResult);
    return;
    const expiresAt = moment().add(authResult.expiry, 'second');
    localStorage.setItem('id_token', authResult);
    localStorage.setItem('expires_at', JSON.stringify(expiresAt.valueOf()));
  }

  logout() {
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
  }

  public isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  public isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at");
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }


}
