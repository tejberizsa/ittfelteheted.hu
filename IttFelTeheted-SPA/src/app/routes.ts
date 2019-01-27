import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { QuestionDetailComponent } from './question/questionDetail/questionDetail.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'detail', component: QuestionDetailComponent },
    { path: 'lists', component: ListsComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'messages', component: MessagesComponent}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
