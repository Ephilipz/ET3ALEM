import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminContactUsComponent } from './admin-contact-us/admin-contact-us.component';
import { AngularMaterialModule } from '../Shared/modules/material.module';


@NgModule({
  declarations: [AdminContactUsComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    AngularMaterialModule
  ]
})
export class AdminModule { }
