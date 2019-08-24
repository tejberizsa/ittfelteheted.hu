import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {

  @Input() currentPage: number;
  @Input() totalItems: number;
  @Input() itemsPerPage: number;
  @Input() pagesToShow: number;
  @Input() loading: boolean;

  @Output() goPage = new EventEmitter<number>();
  baseUrl: string = environment.domain;

  constructor() { }

  ngOnInit() {
  }

  onPage(n: number): void {
    event.preventDefault();
    this.goPage.emit(n);
  }

  onPrev(): void {
    if (this.currentPage > 1) {
      this.onPage(--this.currentPage);
    }
  }

  onNext(): void {
    if (this.currentPage < this.totalPages()) {
      this.onPage(++this.currentPage);
    }
  }

  onFirst(): void {
    this.currentPage = 1;
    this.onPage(this.currentPage);
  }

  onLast(): void {
    if (!this.isLastPage()) {
      this.currentPage = this.totalPages();
      this.onPage(this.currentPage);
    }
  }

  totalPages(): number {
    return Math.ceil(this.totalItems / this.itemsPerPage) || 0;
  }

  isLastPage(): boolean {
    return this.itemsPerPage * this.currentPage >= this.totalItems;
  }

  getPages(): number[] {
    const totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
    const pages: number[] = [];
    pages.push(this.currentPage);

    for (let i = 0; i < this.pagesToShow - 1; i++) {
      if (pages.length < this.pagesToShow) {
        if (Math.min.apply(null, pages) > 1) {
          pages.push(Math.min.apply(null, pages) - 1);
        }
      }

      if (pages.length < this.pagesToShow) {
        if (Math.max.apply(null, pages) < totalPages) {
          pages.push(Math.max.apply(null, pages) + 1);
        }
      }
    }
    pages.sort((a, b) => a - b);
    return pages;
  }
}
