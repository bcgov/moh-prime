import { Component, Inject } from '@angular/core';

import { InstanceConfigHolderService } from 'ng-busy';

import { NgBusyService } from '../ng-busy.service';
@Component({
  selector: 'app-busy-overlay-message',
  templateUrl: './busy-overlay-message.component.html',
  styleUrls: ['./busy-overlay-message.component.scss']
})
export class BusyOverlayMessageComponent {
  constructor(
    @Inject('instanceConfigHolder') private instanceConfigHolder: InstanceConfigHolderService,
    private ngBusyService: NgBusyService
  ) { }

  public get message() {
    if (this.showMessage) {
      return this.ngBusyService.message ?? this.instanceConfigHolder.config.message;
    }
  }

  public get showMessage() {
    return this.ngBusyService.showMessage;
  }

  public get showSpinner() {
    return this.ngBusyService.showSpinner;
  }
}
