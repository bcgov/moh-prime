import { Component, Input, Output, EventEmitter } from '@angular/core';

import { Subscription } from 'rxjs';

@Component({
  selector: 'app-busy-overlay',
  templateUrl: './busy-overlay.component.html',
  styleUrls: ['./busy-overlay.component.scss']
})
export class BusyOverlayComponent {
  @Input() public busy: Subscription;
  @Output() public started: EventEmitter<boolean>;
  @Output() public stopped: EventEmitter<boolean>;

  constructor() {
    this.started = new EventEmitter();
    this.stopped = new EventEmitter();
  }

  /**
   * Indicate the busy overlay is displayed.
   *
   * @param {boolean} event
   * @memberof BusyOverlayComponent
   */
  public onBusyStart(event: boolean) {
    this.started.emit(event);
  }

  /**
   * Indicate the busy overlay is hidden.
   *
   * @param {boolean} event
   * @memberof BusyOverlayComponent
   */
  public onBusyStop(event: boolean) {
    this.stopped.emit(event);
  }
}
