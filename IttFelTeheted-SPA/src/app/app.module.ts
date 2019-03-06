import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, CollapseModule, TabsModule, PaginationModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { TimeagoModule, TimeagoFormatter, TimeagoIntl, TimeagoCustomFormatter } from 'ngx-timeago';
import { FileUploadModule } from 'ng2-file-upload';
import { NgxEditorModule } from 'ngx-editor';

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
import { MemberEditComponent } from './member/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { MemberMessagesComponent } from './member/member-messages/member-messages.component';
import { MemberPhotoEditorComponent } from './member/member-photo-editor/member-photo-editor.component';
import { PostDetailResolver } from './_resolvers/post-detail.resolver';
import { PostTrendingResolver } from './_resolvers/post-trending.resolver';
import { TrendingCardComponent } from './question/trending-card/trending-card.component';
import { QuestionAddComponent } from './question/question-add/question-add.component';

export function tokenGetter() {
   return localStorage.getItem('token');
}
export class MyIntl extends TimeagoIntl {
   // do extra stuff here...
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
      MemberDetailComponent,
      MemberEditComponent,
      MemberMessagesComponent,
      MemberPhotoEditorComponent,
      TrendingCardComponent,
      QuestionAddComponent
   ],
   imports: [
      [ BrowserModule, CollapseModule.forRoot()],
      HttpClientModule,
      ReactiveFormsModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      FileUploadModule,
      NgxEditorModule,
      PaginationModule.forRoot(),
      TimeagoModule.forRoot({
         intl: { provide: TimeagoIntl, useClass: MyIntl },
         formatter: { provide: TimeagoFormatter, useClass: TimeagoCustomFormatter },
       }),
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
      TopicListResolver,
      MemberEditResolver,
      MessagesResolver,
      PostDetailResolver,
      PostTrendingResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
