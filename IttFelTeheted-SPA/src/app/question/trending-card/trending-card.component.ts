import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/_models/post';

@Component({
  selector: 'app-trending-card',
  templateUrl: './trending-card.component.html',
  styleUrls: ['./trending-card.component.css']
})
export class TrendingCardComponent implements OnInit {
  @Input() trending: Post;

  constructor() { }

  ngOnInit() {
  }

}
