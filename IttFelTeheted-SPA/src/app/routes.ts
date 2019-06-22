import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { QuestionDetailComponent } from './question/questionDetail/questionDetail.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { RegisterGuard } from './_guards/register.guard';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { PostListResolver } from './_resolvers/post-list.resolver';
import { TopicListResolver } from './_resolvers/topic-list.resolver';
import { MemberEditComponent } from './member/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { PostDetailResolver } from './_resolvers/post-detail.resolver';
import { PostTrendingResolver } from './_resolvers/post-trending.resolver';
import { QuestionAddComponent } from './question/question-add/question-add.component';
import { MemberPostResolver } from './_resolvers/member-post.resolver';
import { MemberFollowedPostResolver } from './_resolvers/member-followed-post.resolver';
import { MemberFollowedUserResolver } from './_resolvers/member-followed-user.resolver';
import { PolicyComponent } from './policy/policy.component';
import { MemberConfirmComponent } from './member/member-confirm/member-confirm.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent, resolve: {posts: PostListResolver, topics: TopicListResolver} },
    { path: 'detail/:id', component: QuestionDetailComponent, resolve: { post: PostDetailResolver, trendings: PostTrendingResolver } },
    { path: 'lists', component: ListsComponent },
    { path: 'policy', component: PolicyComponent },
    { path: 'confirm/:id/:ckey', component: MemberConfirmComponent },
    { path: 'register', component: RegisterComponent, canActivate: [RegisterGuard] },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'messages', component: MessagesComponent, resolve: { messages: MessagesResolver } },
            { path: 'addpost', component: QuestionAddComponent, resolve: { topics: TopicListResolver } },
            { path: 'member/edit', component: MemberEditComponent, resolve: { user: MemberEditResolver } },
            { path: 'member/:id', component: MemberDetailComponent, resolve: { user: MemberDetailResolver,
                posts: MemberPostResolver, followedPosts: MemberFollowedPostResolver, followedUsers: MemberFollowedUserResolver } }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
