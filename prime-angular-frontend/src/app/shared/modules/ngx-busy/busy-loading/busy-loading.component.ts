import { Component, OnInit, Input } from '@angular/core';

import { Subscription } from 'rxjs';

@Component({
  selector: 'app-busy-loading',
  templateUrl: './busy-loading.component.html',
  styleUrls: ['./busy-loading.component.scss']
})
export class BusyLoadingComponent implements OnInit {
  @Input() busy: Subscription;
  @Input() align: 'left' | 'center';
  @Input() loadingMessage: string;

  constructor() {
    this.align = 'left';
    this.loadingMessage = 'Loading...';
  }

  /**
   * Get the text alignment class.
   *
   * @returns
   * @memberof BusyLoadingComponent
   */
  public getTextAlignment() {
    return `text-${this.align}`;
  }

  /**
   * OnInit lifecycle hook.
   *
   * @memberof BusyLoadingComponent
   */
  public ngOnInit() { }
}
