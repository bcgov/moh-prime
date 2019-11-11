import { Component, Inject } from '@angular/core';

import { InstanceConfigHolderService } from 'ng-busy';

@Component({
  selector: 'app-busy-overlay-message',
  templateUrl: './busy-overlay-message.component.html',
  styleUrls: ['./busy-overlay-message.component.scss']
})
export class BusyOverlayMessageComponent {
  constructor(
    @Inject('instanceConfigHolder') private instanceConfigHolder: InstanceConfigHolderService
  ) { }

  get message() {
    return this.instanceConfigHolder.config.message;
  }
}
