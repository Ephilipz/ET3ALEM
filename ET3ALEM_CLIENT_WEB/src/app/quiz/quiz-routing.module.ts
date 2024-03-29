import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuizHomeComponent } from './components/quiz-home/quiz-home.component';
import { ListQuizzesComponent } from './components/list-quizzes/list-quizzes.component';
import { AccessQuizComponent } from './components/take quiz/access-quiz/access-quiz.component';
import { EditOrCreateQuizComponent } from './components/edit-create-quiz/edit-or-create-quiz.component';
import { QuizDetailsComponent } from './components/take quiz/quiz-details/quiz-details.component';
import { TakeQuizComponent } from './components/take quiz/take-quiz/take-quiz.component';
import { ViewQuizResultComponent } from './components/quiz-results/view-quiz-result/view-quiz-result.component';
import { GradeQuizComponent } from './components/grade-quiz/grade-quiz.component';
import { QuizGradesComponent } from './components/quiz-grades/quiz-grades.component';
import { QuizAttemptHistoryComponent } from './components/quiz-attempt-history/quiz-attempt-history.component';
import {UngradedQuizzesComponent} from "./components/ungraded-quizzes/ungraded-quizzes.component";


const routes: Routes = [
  { path: '', pathMatch: 'full', component: QuizHomeComponent },
  { path: 'create', component: EditOrCreateQuizComponent },
  { path: 'edit/:id', component: EditOrCreateQuizComponent },
  { path: 'grades/:id', component: QuizGradesComponent },
  { path: 'manage', component: ListQuizzesComponent },
  { path: 'edit', component: EditOrCreateQuizComponent },
  { path: 'take', component: AccessQuizComponent },
  { path: 'take/:code', component: QuizDetailsComponent },
  { path: 'take/:code/start', component: TakeQuizComponent },
  { path: 'viewAttempt/:id', component: ViewQuizResultComponent },
  { path: 'gradeAttempt/:id', component: GradeQuizComponent },
  { path: 'history', component: QuizAttemptHistoryComponent },
  { path: 'grade', component: UngradedQuizzesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuizRoutingModule { }
