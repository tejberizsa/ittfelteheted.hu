import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Topic } from '../_models/topic';
import { Pagination, PaginatedResult } from '../_models/pagination';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean;
  posts: Post[];
  topics: Topic[];
  pagination: Pagination;
  queryString: string;

  constructor(private authService: AuthService, private postService: PostService,
    private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
      this.route.data.subscribe(data => {
      this.posts = data['posts'].result;
      this.pagination = data['posts'].pagination;
      this.topics = data['topics'];
    });
  }

  loadPosts() {
    this.postService.getPosts(this.pagination.currentPage, this.pagination.itemsPerPage, this.queryString)
    .subscribe((res: PaginatedResult<Post[]>) => {
        this.posts = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

  getTopicIcon(topicIcon: string) {
    return topicIcon + ' mr-4';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadPosts();
    window.scrollTo(0, 0);
  }

}
