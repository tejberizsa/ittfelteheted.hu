import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Topic } from '../_models/topic';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean;
  posts: Post[];
  topics: Topic[];

  constructor(private authService: AuthService, private postService: PostService,
    private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
      this.route.data.subscribe(data => {
      this.posts = data['posts'];
      this.topics = data['topics'];
    });
      // this.topics.sort((leftSide, rightSide): number => {
      //   if (leftSide.topicName < rightSide.topicName) { return -1; }
      //   if (leftSide.topicName > rightSide.topicName) { return 1; }
      //   return 0;
      // });
  }

  getTopicIcon(topicIcon: string) {
    return topicIcon + ' mr-4';
  }

}
