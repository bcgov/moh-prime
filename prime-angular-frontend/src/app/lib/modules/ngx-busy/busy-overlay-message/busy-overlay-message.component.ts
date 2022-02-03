import { Component, Inject, OnInit } from '@angular/core';

import { InstanceConfigHolderService } from 'ng-busy';
import { NgBusyService } from '../ng-busy.service';
@Component({
  selector: 'app-busy-overlay-message',
  templateUrl: './busy-overlay-message.component.html',
  styleUrls: ['./busy-overlay-message.component.scss']
})
export class BusyOverlayMessageComponent implements OnInit {
  public message: string;
  public isShowSpinner;

  constructor(
    @Inject('instanceConfigHolder') private instanceConfigHolder: InstanceConfigHolderService,
    private ngBusyService: NgBusyService
  ) { }

  ngOnInit(): void {
    this.ngBusyService.message$.subscribe((result: string) => {
      this.message = result;
    });
    this.ngBusyService.isShowSpinner$.subscribe((result: boolean) => {
      this.isShowSpinner = result;
    })
  }
}
