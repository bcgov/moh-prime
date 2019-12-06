import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-progress-indicator',
  templateUrl: './progress-indicator.component.html',
  styleUrls: ['./progress-indicator.component.scss']
})
export class ProgressIndicatorComponent implements OnInit {
  @Input() public currentPage: number;

  public totalPages: number;

  constructor() {
    this.currentPage = 0;
    this.totalPages = 5;
  }

  public get percentComplete() {
    const page = this.currentPage - 1;
    return Math.trunc(page / this.totalPages * 100);
  }

  public ngOnInit() { }
}
