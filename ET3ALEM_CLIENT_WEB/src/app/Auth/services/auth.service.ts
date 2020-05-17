import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as moment from "moment"
import { LoginUser, RegisterUser } from '../Model/User';
import { pluck, share, shareReplay, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  constructor(private http: HttpClient) { }

  login(email: string, password: string) {
    return this.http.post<LoginUser>('/api/login', { email, password }).pipe(
      tap(this.setSession),
      shareReplay()
    )
  }

  register(registerUserObject: RegisterUser) {
    return this.http.post<RegisterUser>('/api/register', registerUserObject).pipe(
      tap(this.setSession),
      shareReplay()
    )
  }

  setSession(authResult) {
    const expiresAt = moment().add(authResult.expiresIn, 'second');
    localStorage.setItem('id_token', authResult.id_token);
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
