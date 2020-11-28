import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditOrCreateQuizComponent } from './components/create-quiz/edit-or-create-quiz.component';
import { QuizHomeComponent } from './components/quiz-home/quiz-home.component';
import { ListQuizzesComponent } from './components/list-quizzes/list-quizzes.component';
import { AccessQuizComponent } from './components/access-quiz/access-quiz.component';


const routes: Routes = [
  { path: '', pathMatch: 'full', component: QuizHomeComponent },
  { path: 'create', component: EditOrCreateQuizComponent },
  { path: 'edit/:id', component: EditOrCreateQuizComponent },
  { path: 'manage', component: ListQuizzesComponent },
  { path: 'edit', component: EditOrCreateQuizComponent },
  { path: 'take', component: AccessQuizComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuizRoutingModule { }
