import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditOrCreateQuestionCollectionComponent } from './components/edit-or-create-question-collection/edit-or-create-question-collection.component';
import { ListQuestionCollectionsComponent } from './components/list-question-collections/list-question-collections.component';


const routes: Routes = [
    { path: '', component: ListQuestionCollectionsComponent },
    { path: 'create', component: EditOrCreateQuestionCollectionComponent },
    { path: 'edit/:id', component: EditOrCreateQuestionCollectionComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class QuestionCollectionRoutingModule { }