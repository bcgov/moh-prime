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
    return this.ngBusyService.message;
  }

  public get isShowSpinner() {
    return this.ngBusyService.isShowSpinner
  }
}
