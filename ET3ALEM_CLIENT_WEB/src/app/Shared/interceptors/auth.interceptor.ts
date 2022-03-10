import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import {Observable, throwError, BehaviorSubject} from 'rxjs';
import {catchError, filter, take, switchMap} from 'rxjs/operators';
import {Tokens} from 'src/app/auth/Model/Tokens';
import {AuthService} from 'src/app/auth/services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(public authService: AuthService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (this.authService.getJWT() && !req.url.toLowerCase().includes('imgur')) {
      req = AuthInterceptor.addToken(req, this.authService.getJWT());
    }

    return next.handle(req).pipe(catchError(error => {
      if (AuthInterceptor.isUnathenticated(error)) {
        return this.handle401Error(req, next);
      }
      // if unable to refresh token, logout
      else if (AuthInterceptor.isRequestToRefresh(req)) {
        this.authService.logout().subscribe(_=>{});
        this.authService.redirectToLogin();
        return;
      } else {
        return throwError(error);
      }
    }));

  }

  private static isRequestToRefresh(req: HttpRequest<any>) {
    return req.url.toLowerCase().includes('refresh');
  }

  private static isUnathenticated(error) {
    return error instanceof HttpErrorResponse && error.status === 401;
  }

  private static addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
    if (!this.isRefreshing) {
      return this.getNewRefreshToken(next, request);
    } else {
      return this.passExistingToken(next, request);
    }
  }

  private passExistingToken(next: HttpHandler, request: HttpRequest<any>) {
    return this.refreshTokenSubject.pipe(
      filter(token => token != null),
      take(1),
      switchMap(jwt => {
        return next.handle(AuthInterceptor.addToken(request, jwt));
      }));
  }

  private getNewRefreshToken(next: HttpHandler, request: HttpRequest<any>) {
    this.notifyNewRefreshTokenNeeded();

    return this.authService.refresh().pipe(
      switchMap((token: Tokens) => {
        this.notifyRefreshTokenReceived(token);
        return next.handle(AuthInterceptor.addToken(request, token.JWT));
      }));
  }

  private notifyRefreshTokenReceived(token: Tokens) {
    this.isRefreshing = false;
    this.refreshTokenSubject.next(token.JWT);
  }

  private notifyNewRefreshTokenNeeded() {
    this.isRefreshing = true;
    this.refreshTokenSubject.next(null);
  }
}
