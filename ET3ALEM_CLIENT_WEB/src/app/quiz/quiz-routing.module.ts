import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuizHomeComponent } from './components/quiz-home/quiz-home.component';
import { ListQuizzesComponent } from './components/list-quizzes/list-quizzes.component';
import { AccessQuizComponent } from './components/access-quiz/access-quiz.component';
import { EditOrCreateQuizComponent } from './components/edit-create-quiz/edit-or-create-quiz.component';
import { QuizDetailsComponent } from './components/quiz-details/quiz-details.component';


const routes: Routes = [
  { path: '', pathMatch: 'full', component: QuizHomeComponent },
  { path: 'create', component: EditOrCreateQuizComponent },
  { path: 'edit/:id', component: EditOrCreateQuizComponent },
  { path: 'manage', component: ListQuizzesComponent },
  { path: 'edit', component: EditOrCreateQuizComponent },
  { path: 'take', component: AccessQuizComponent },
  { path: 'take/:code', component: QuizDetailsComponent },
  // { path: 'start', component: QuizDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuizRoutingModule { }
