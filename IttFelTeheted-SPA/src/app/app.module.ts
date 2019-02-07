import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, CollapseModule, TabsModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AlertifyService } from './_services/alertify.service';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { QuestionDetailComponent } from './question/questionDetail/questionDetail.component';
import { AnswerComponent } from './question/answer/answer.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterGuard } from './_guards/register.guard';
import { PostService } from './_services/post.service';
import { QuestionCardComponent } from './question/question-card/question-card.component';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { UserService } from './_services/user.service';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { PostListResolver } from './_resolvers/post-list.resolver';
import { TopicListResolver } from './_resolvers/topic-list.resolver';

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      ListsComponent,
      MessagesComponent,
      QuestionDetailComponent,
      AnswerComponent,
      QuestionCardComponent,
      MemberDetailComponent
   ],
   imports: [
      [ BrowserModule, CollapseModule.forRoot()],
      HttpClientModule,
      ReactiveFormsModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      RegisterGuard,
      PostService,
      UserService,
      MemberDetailResolver,
      PostListResolver,
      TopicListResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
