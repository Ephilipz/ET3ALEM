import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor() { 
  }

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    debugger;
    //get jwt from local storage
    const idToken = localStorage.getItem('id_token');

    //valid token is present
    if (idToken) {
      req = req.clone(
        {
          headers: req.headers.set("Authorization", "Bearer" + idToken)
        }
      );

    }

    //if no content type is present set to json
    if (!req.headers.has('Content-Type')) {
      req = req.clone({ headers: req.headers.set('Content-Type', 'application/json') });
    }

    //accept the incoming request in json format
    req = req.clone({ headers: req.headers.set('Accept', 'application/json') });

    return next.handle(req);
  }
}
