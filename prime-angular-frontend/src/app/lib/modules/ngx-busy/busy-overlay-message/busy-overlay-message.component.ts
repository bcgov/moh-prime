import { Component } from '@angular/core';

import { BusyService } from '../busy.service';

@Component({
  selector: 'app-busy-overlay-message',
  templateUrl: './busy-overlay-message.component.html',
  styleUrls: ['./busy-overlay-message.component.scss']
})
export class BusyOverlayMessageComponent {
  constructor(
    private busyService: BusyService
  ) { }

  public get message() {
    return this.busyService.message
  }
}
