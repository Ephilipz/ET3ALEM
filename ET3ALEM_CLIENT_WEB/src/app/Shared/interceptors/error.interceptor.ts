import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, of, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(private toastyService: ToastrService) {
  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(1),
        catchError((error: HttpErrorResponse) => {
          if ((error instanceof HttpErrorResponse && error.status === 401) || request.url.toLowerCase().includes('refresh')) {
            return throwError(error);
          }
          const errorMessage = error?.error?.message ?? 'General Error';
          this.toastyService.error(errorMessage);
          return of(null);
        })
      );
  }
}
