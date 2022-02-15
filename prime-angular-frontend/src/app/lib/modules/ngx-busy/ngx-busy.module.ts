import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgBusyModule } from 'ng-busy';

import { busyConfig } from './busy.config';
import { BusyOverlayComponent } from './busy-overlay/busy-overlay.component';
import { BusyOverlayMessageComponent } from './busy-overlay-message/busy-overlay-message.component';
import { BusyLoadingComponent } from './busy-loading/busy-loading.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  imports: [
    CommonModule,
    NgBusyModule.forRoot(busyConfig),
    MatProgressSpinnerModule
  ],
  declarations: [
    BusyOverlayComponent,
    BusyLoadingComponent,
    BusyOverlayMessageComponent
  ],
  exports: [
    NgBusyModule,
    BusyLoadingComponent,
    BusyOverlayComponent
  ]
})
export class NgxBusyModule { }
