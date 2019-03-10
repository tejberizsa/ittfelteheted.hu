import { Component, OnInit } from '@angular/core';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-member-follow',
  templateUrl: './member-follow.component.html',
  styleUrls: ['./member-follow.component.css']
})
export class MemberFollowComponent implements OnInit {
  followedUsers: User[];
  userPagination: Pagination;
  detailUserId: any;

  constructor(private userService: UserService, private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.followedUsers = data['followedUsers'].result;
      this.userPagination = data['followedUsers'].pagination;
    });
    this.route.params.subscribe(params => {
      this.detailUserId = +params['id'];
  });
  }

  loadPosts() {
    this.userService.getFollowedUsers(this.detailUserId, this.userPagination.currentPage, this.userPagination.itemsPerPage)
    .subscribe((res: PaginatedResult<User[]>) => {
        this.followedUsers = res.result;
        this.userPagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

  pageChanged(event: any): void {
    this.userPagination.currentPage = event.page;
    this.loadPosts();
    window.scrollTo(0, 0);
  }

}
