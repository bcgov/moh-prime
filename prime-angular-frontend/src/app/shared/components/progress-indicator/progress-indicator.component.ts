import { Component, OnInit, Input } from '@angular/core';

export interface IProgressIndicator {
  currentRoute: string;
  inProgress: boolean;
  routes: string[];
}

@Component({
  selector: 'app-progress-indicator',
  templateUrl: './progress-indicator.component.html',
  styleUrls: ['./progress-indicator.component.scss']
})
export class ProgressIndicatorComponent implements OnInit, IProgressIndicator {
  @Input() public currentRoute: string;
  @Input() public inProgress: boolean;
  @Input() public routes: string[];
  @Input() public prefix: string;
  @Input() public message: string;

  public percentComplete: number;

  constructor() {
    this.routes = [];
    this.prefix = '';
  }

  public ngOnInit() {
    const currentRoute = this.routes.findIndex(r => r === this.currentRoute);
    const currentPage = (currentRoute > -1) ? currentRoute : 0;
    const totalPages = this.routes.length - 1;

    const percentComplete = Math.trunc(currentPage / totalPages * 100);

    this.percentComplete = (this.inProgress)
      ? percentComplete
      : 100;
  }
}
