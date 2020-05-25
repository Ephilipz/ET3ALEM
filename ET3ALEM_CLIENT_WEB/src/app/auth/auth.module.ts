import { NgModule } from '@angular/core';import { LoginComponent } from './components/login/login.component';import { RegisterComponent } from './components/register/register.component';import { PasswordRecoverComponent } from './components/password-recover/password-recover.component';import { AuthRoutingModule } from './auth-routing.module';import { ReactiveFormsModule, FormsModule } from '@angular/forms';import { AngularMaterialModule } from '../Shared/modules/material.module';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../Shared/services/auth.interceptor';


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    PasswordRecoverComponent],
  imports: [
    AuthRoutingModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    FormsModule,
    RouterModule
  ],
  providers: [
    // { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ]
})
export class AuthModule { }
