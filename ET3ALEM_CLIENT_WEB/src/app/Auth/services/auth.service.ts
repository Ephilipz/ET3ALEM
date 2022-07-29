import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { RegisterUser } from '../Model/User';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Tokens } from '../Model/Tokens';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { LocalStorageService } from 'src/app/Shared/services/local-storage.service';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { ExternalAuthDto } from '../model/ExternalAuthDto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private extAuthChangeSub = new Subject<SocialUser>();
  public extAuthChanged = this.extAuthChangeSub.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastyService: ToastrService,
    private localStorageService: LocalStorageService,
    private socialAuthService: SocialAuthService) {
    this.socialAuthService.authState.subscribe((user) => {
      console.log(user);
      this.extAuthChangeSub.next(user);
    });

  }

  private _nextUrlPath: string;
  loginStateChanged: Subject<boolean> = new BehaviorSubject<boolean>(this.isLoggedIn());

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
    return this.http.post<Tokens>(environment.baseUrl + '/api/Account/login', { email, password }).pipe(
      tap(
        tokens => {
          this.setSession(tokens);
          this.toastyService.success('Welcome');
          this.loginStateChanged.next(true);
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
    this.localStorageService.JWT = tokens.JWT;
    this.localStorageService.RefreshToken = tokens.RefreshToken;
    this.localStorageService.UserId = tokens.UserId;
  }

  logout() {
    this.localStorageService.clear();
    this.loginStateChanged.next(false);
    return this.http.get(environment.baseUrl + '/api/Account/Logout');
  }

  public isLoggedIn() {
    return !!this.getJWT();
  }

  public isLoggedOut() {
    return !this.isLoggedIn();
  }

  getJWT() {
    return this.localStorageService.JWT;
  }

  getRefreshToken() {
    return this.localStorageService.RefreshToken;
  }

  sendRecoveryMail(email: string) {
    return this.http.post(environment.baseUrl + '/api/Account/sendRecoveryMail', { email }).pipe(
      tap(() => this.toastyService.success('email is sent, check you inbox', 'success')));
  }

  resetPassword(resetPasswordVM: { recoveryToken: string, password: string, confirmPassword: string }) {
    return this.http.post(environment.baseUrl + '/api/Account/ResetPassword', resetPasswordVM);
  }

  redirectToLogin() {
    this.router.navigate(['auth/login']);
  }

  public signInWithGoogle() {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
    this.extAuthChanged.subscribe(user => {
      const externalAuth: ExternalAuthDto = {
        provider: user.provider,
        idToken: user.idToken
      };
      this.validateExternalAuth(externalAuth);
    });
  }
  public signOutExternal() {
    this.socialAuthService.signOut();
  }



  private validateExternalAuth(externalAuth: ExternalAuthDto) {
    this.http.post(environment.baseUrl + '/api/account/externalLogin', externalAuth).subscribe({
      next: (tokens: any) => {
        this.setSession(tokens);
        this.toastyService.success('Welcome');
        this.loginStateChanged.next(true);
        this.router.navigate(['/quiz']);
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
        this.signOutExternal();
      }
    });
  }
}
