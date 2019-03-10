import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-member-follow-card',
  templateUrl: './member-follow-card.component.html',
  styleUrls: ['./member-follow-card.component.css']
})
export class MemberFollowCardComponent implements OnInit {
  @Input() followedUser: User;

  constructor() { }

  ngOnInit() {
  }

}
