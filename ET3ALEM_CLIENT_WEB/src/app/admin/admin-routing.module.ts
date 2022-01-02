import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminContactUsComponent } from './admin-contact-us/admin-contact-us.component';

const routes: Routes = [
  { path: 'contact', component: AdminContactUsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
