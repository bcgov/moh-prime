import { Component, Inject } from '@angular/core';

import { NgBusyService } from '../ng-busy.service';
@Component({
  selector: 'app-busy-overlay-message',
  templateUrl: './busy-overlay-message.component.html',
  styleUrls: ['./busy-overlay-message.component.scss']
})
export class BusyOverlayMessageComponent {
  constructor(
    private ngBusyService: NgBusyService
  ) { }

  public get message() {
    return this.ngBusyService.getMessage();
  }

  public get isShowSpinner() {
    return this.ngBusyService.getIsShowSpinner();
  }
}
