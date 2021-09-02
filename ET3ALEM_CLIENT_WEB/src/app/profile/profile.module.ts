import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularMaterialModule } from '../Shared/modules/material.module';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [ChangePasswordComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    ProfileRoutingModule,
    ToastrModule.forRoot(),
  ]
})
export class ProfileModule { }
