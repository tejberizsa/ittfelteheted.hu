import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styleUrls: ['./answer.component.css']
})
export class AnswerComponent implements OnInit {
  @Input() answer: Answer;

  constructor(private authService: AuthService, private alertify: AlertifyService, private postService: PostService) { }

  ngOnInit() {
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  mustLogin() {
    this.alertify.error('Előbb be kell jelentkezned');
  }

  sendLike() {
    if (!this.authService.loggedIn()) {
      this.alertify.error('Szavazat küldéshez be kell jelentkezned');
    }
    this.postService.sendLike(this.authService.decodedToken.nameid, this.answer.id).subscribe(data => {
      this.alertify.success('Sikeres szavazat');
      this.answer.like++;
    }, error => {
      this.alertify.error(error);
    });
  }

  sendDislike() {
    if (!this.authService.loggedIn()) {
      this.alertify.error('Szavazat küldéshez be kell jelentkezned');
    }
    this.postService.sendDislike(this.authService.decodedToken.nameid, this.answer.id).subscribe(data => {
      this.alertify.success('Sikeres szavazat');
      this.answer.like--;
    }, error => {
      this.alertify.error(error);
    });
  }

}
