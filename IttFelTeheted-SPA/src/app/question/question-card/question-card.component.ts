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

  constructor(private authService: AuthService, private userService: PostService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  selectFirstAnswer() {
    // this.answer = this.post.answers.;
  }

}
