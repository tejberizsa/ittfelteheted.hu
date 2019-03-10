import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/_models/post';

@Component({
  selector: 'app-member-post-card',
  templateUrl: './member-post-card.component.html',
  styleUrls: ['./member-post-card.component.css']
})
export class MemberPostCardComponent implements OnInit {
  @Input() memberPost: Post;

  constructor() { }

  ngOnInit() {
  }

}
