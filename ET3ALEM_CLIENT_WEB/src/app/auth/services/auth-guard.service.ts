import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (route.url.toString().includes('auth')) {
      if (this.authService.isLoggedOut()) {
        return true;
      }
      this.router.navigate(['']);
      return false;
    }
    if (this.authService.isLoggedOut()) {
      this.router.navigate(['auth']);
      return false;
    }
    return true;
  }
}


