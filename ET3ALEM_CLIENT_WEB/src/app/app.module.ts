import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './General/components/home/home.component';
import { ContactComponent } from './General/components/contact/contact.component';
import { NotFoundComponent } from './General/components/not-found/not-found.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AngularMaterialModule } from './Shared/modules/material.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './Shared/services/auth.interceptor';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { MainMenuComponent } from './General/components/main-menu/main-menu.component';
import { HttpErrorInterceptor } from './Shared/services/error.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ContactComponent,
    NotFoundComponent,
    MainMenuComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    AngularMaterialModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      countDuplicates: true,
    })
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }, {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpErrorInterceptor,
    multi: true
  },],
  bootstrap: [AppComponent]
})
export class AppModule { }
