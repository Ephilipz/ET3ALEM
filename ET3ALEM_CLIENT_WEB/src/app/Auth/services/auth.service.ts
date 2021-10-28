import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RegisterUser} from '../Model/User';
import {tap, catchError, mapTo} from 'rxjs/operators';
import {environment} from 'src/environments/environment';
import {Router} from '@angular/router';
import {Tokens} from '../Model/Tokens';
import {Observable, of, throwError} from 'rxjs';
import {ToastrService} from 'ngx-toastr';
import {LocalstorgeService} from 'src/app/Shared/services/localstorge.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastyService: ToastrService,
    private localstorgeService: LocalstorgeService) {

  }

  private _nextUrlPath: string;

  get nextUrlPath() {
    if (this._nextUrlPath) {
      return this._nextUrlPath;
    }
    return '';
  }

  set nextUrlPath(val) {
    this._nextUrlPath = val;
  }

  login(email: string, password: string): Observable<any> {
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/login', {email, password}).pipe(
      tap(
        tokens => {
          this.setSession(tokens);
          this.toastyService.success('Welcome');
        })
    );
  }

  register(registerUserObject: RegisterUser) {
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/Register', {
      ...registerUserObject,
      ConfirmPassword: registerUserObject.Password
    }).pipe(
      tap(
        token => {
          this.setSession(token);
          this.toastyService.success('Welcome');
        })
    );
  }

  refresh() {
    const oldTokens: Tokens = new Tokens(this.getJWT(), this.getRefreshToken());
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/Refresh', oldTokens).pipe(
      tap((tokens: Tokens) => {
        this.setSession(tokens);
      }));
  }

  setSession(tokens: Tokens) {
    this.localstorgeService.JWT = tokens.JWT;
    this.localstorgeService.RefreshToken = tokens.RefreshToken;
    this.localstorgeService.UserId = tokens.UserId;
  }

  logout() {
    this.localstorgeService.clear();
    return this.http.get(environment.baseUrl + '/api/Account/Logout');
  }

  public isLoggedIn() {
    return !!this.getJWT();
  }

  public isLoggedOut() {
    return !this.isLoggedIn();
  }

  getJWT() {
    return this.localstorgeService.JWT;
  }

  getRefreshToken() {
    return this.localstorgeService.RefreshToken;
  }

  sendRecoveryMail(email: string) {
    return this.http.post(environment.baseUrl + '/api/Account/sendRecoveryMail', {email}).pipe(
      tap(() => this.toastyService.success('email is sent, check you inbox', 'success')));
  }

  resetPassword(resetPasswordVM: { recoveryToken: string, password: string, confirmPassword: string }) {
    return this.http.post(environment.baseUrl + '/api/Account/ResetPassword', resetPasswordVM);
  }


}
