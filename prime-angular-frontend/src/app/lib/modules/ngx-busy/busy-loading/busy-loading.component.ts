import { Component, Input } from '@angular/core';

import { Subscription } from 'rxjs';

@Component({
  selector: 'app-busy-loading',
  templateUrl: './busy-loading.component.html',
  styleUrls: ['./busy-loading.component.scss']
})
export class BusyLoadingComponent {
  @Input() busy: Subscription;
  @Input() align: 'left' | 'center';
  @Input() loadingMessage: string;

  constructor() {
    this.align = 'left';
    this.loadingMessage = 'Loading...';
  }

  /**
   * @description
   * Get the text alignment CSS class.
   */
  public getTextAlignment() {
    return `text-${this.align}`;
  }
}
