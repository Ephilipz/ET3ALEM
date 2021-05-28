import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './General/components/home/home.component';
import { ContactComponent } from './General/components/contact/contact.component';
import { NotFoundComponent } from './General/components/not-found/not-found.component';
import { AuthGuardService } from './auth/services/auth-guard.service';


const routes: Routes = [
  { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule), canActivate: [AuthGuardService] },
  { path: 'quiz', loadChildren: () => import('./quiz/quiz.module').then(m => m.QuizModule), canActivate: [AuthGuardService] },
  {
    path: 'questionCollection',
    loadChildren: () => import('./question-collection/question-collection.module').then(m => m.QuestionCollectionModule),
    canActivate: [AuthGuardService]
  },
  { path: 'contact', component: ContactComponent },
  { path: '', component: HomeComponent },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
