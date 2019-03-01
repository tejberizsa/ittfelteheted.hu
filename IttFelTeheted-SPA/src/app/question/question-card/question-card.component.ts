import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/_models/post';
import { AuthService } from 'src/app/_services/auth.service';
import { PostService } from 'src/app/_services/post.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Answer } from 'src/app/_models/answer';

@Component({
  selector: 'app-question-card',
  templateUrl: './question-card.component.html',
  styleUrls: ['./question-card.component.css']
})
export class QuestionCardComponent implements OnInit {
  @Input() post: Post;
  // answer: Answer;

  constructor(private authService: AuthService, private userService: PostService,
    private alertify: AlertifyService, private postService: PostService) { }

  ngOnInit() {
  }

  selectFirstAnswer() {
    // this.answer = this.post.answers.;
  }

  sendFollow() {
    if (!this.authService.loggedIn()) {
      this.alertify.error('Kérdés követéséhez be kell jelentkezned');
      return;
    }
    this.postService.sendFollow(this.authService.decodedToken.nameid, this.post.id).subscribe(data => {
      this.post.isFollowedByCurrentUser = true;
      this.alertify.success('Sikeres feliratkozás');
    }, error => {
      this.alertify.error(error);
    });
  }

  sendDisfollow() {
    if (!this.authService.loggedIn()) {
      this.alertify.error('Kérdés követéséhez be kell jelentkezned');
      return;
    }
    this.postService.sendDisfollow(this.authService.decodedToken.nameid, this.post.id).subscribe(data => {
      this.post.isFollowedByCurrentUser = false;
      this.alertify.success('Sikeres leiratkozás');
    }, error => {
      this.alertify.error(error);
    });
  }

}
