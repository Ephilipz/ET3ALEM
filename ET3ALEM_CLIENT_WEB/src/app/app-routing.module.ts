import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './General/components/home/home.component';
import { ContactComponent } from './General/components/contact/contact.component';
import { NotFoundComponent } from './General/components/not-found/not-found.component';
import { AuthModule } from './auth/auth.module';
import { QuizModule } from './quiz/quiz.module';
import { AuthGuardService } from './auth/services/auth-guard.service';


const routes: Routes = [
  { path: '', redirectTo: 'quiz', pathMatch: 'full' },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'auth', loadChildren: './auth/auth.module#AuthModule', canActivate: [AuthGuardService] },
  { path: 'quiz', loadChildren: './quiz/quiz.module#QuizModule', canActivate: [AuthGuardService] },
  { path: 'contact', component: ContactComponent },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
