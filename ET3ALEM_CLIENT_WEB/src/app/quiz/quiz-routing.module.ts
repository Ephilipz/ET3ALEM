import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateQuizComponent } from './components/create-quiz/create-quiz.component';
import { TakeQuizComponent } from './components/take-quiz/take-quiz.component';
import { ViewQuizComponent } from './components/view-quiz/view-quiz.component';


const routes: Routes = [
  {path: '', pathMatch: 'full', component: TakeQuizComponent},
  {path: 'create', component: CreateQuizComponent},
  {path: 'edit', component: CreateQuizComponent},
  {path: 'view', component: ViewQuizComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuizRoutingModule { }
