import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean;
  posts: Post[];

  constructor(private authService: AuthService, private postService: PostService,
    private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
        // this.route.data.subscribe(data => {
    //   this.posts = data['posts'].result;
    // });
    this.loadPosts();
  }

  loadPosts() {
    this.postService.getPosts()
      .subscribe((res: Post[]) => {
      this.posts = res;
    }, error => {
      this.alertify.error(error);
    });
  }

}
