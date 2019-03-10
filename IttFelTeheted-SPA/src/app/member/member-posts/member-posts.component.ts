import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/_models/post';
import { PostService } from 'src/app/_services/post.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-member-posts',
  templateUrl: './member-posts.component.html',
  styleUrls: ['./member-posts.component.css']
})
export class MemberPostsComponent implements OnInit {
  posts: Post[];
  pagination: Pagination;
  detailUserId: any;

  constructor(private postService: PostService, private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.posts = data['posts'].result;
      this.pagination = data['posts'].pagination;
    });
    this.route.params.subscribe(params => {
      this.detailUserId = +params['id'];
  });
  }

  loadPosts() {
    this.postService.getUserPosts(this.detailUserId, this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((res: PaginatedResult<Post[]>) => {
        this.posts = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadPosts();
    window.scrollTo(0, 0);
  }

}
