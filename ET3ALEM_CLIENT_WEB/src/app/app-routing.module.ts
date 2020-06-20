import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './General/components/home/home.component';
import { ContactComponent } from './General/components/contact/contact.component';
import { NotFoundComponent } from './General/components/not-found/not-found.component';
import { AuthModule } from './auth/auth.module';
import { QuizModule } from './quiz/quiz.module';


const routes: Routes = [
  { path: '', redirectTo: 'quiz/create', pathMatch: 'full' },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  {path: 'auth', loadChildren : './auth/auth.module#AuthModule'},
  {path: 'quiz', loadChildren : './quiz/quiz.module#QuizModule'},
  { path: 'contact', component: ContactComponent },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
