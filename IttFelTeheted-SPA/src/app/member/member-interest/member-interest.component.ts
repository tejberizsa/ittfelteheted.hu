import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/_models/post';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { PostService } from 'src/app/_services/post.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-member-interest',
  templateUrl: './member-interest.component.html',
  styleUrls: ['./member-interest.component.css']
})
export class MemberInterestComponent implements OnInit {
  followedPosts: Post[];
  followPagination: Pagination;
  detailUserId: any;

  constructor(private postService: PostService, private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.followedPosts = data['followedPosts'].result;
      this.followPagination = data['followedPosts'].pagination;
    });
    this.route.params.subscribe(params => {
      this.detailUserId = +params['id'];
  });
  }

  loadPosts() {
    this.postService.getFollowedPosts(this.detailUserId, true, this.followPagination.currentPage, this.followPagination.itemsPerPage)
    .subscribe((res: PaginatedResult<Post[]>) => {
        this.followedPosts = res.result;
        this.followPagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

  pageChanged(event: any): void {
    this.followPagination.currentPage = event.page;
    this.loadPosts();
    window.scrollTo(0, 0);
  }

}
