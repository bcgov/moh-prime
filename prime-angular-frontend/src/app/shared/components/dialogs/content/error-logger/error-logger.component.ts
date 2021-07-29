import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { DialogOptions } from '../../dialog-options.model';

@Component({
  selector: 'app-error-logger',
  templateUrl: './error-logger.component.html',
  styleUrls: ['./error-logger.component.scss']
})
export class ErrorLoggerComponent implements OnInit {
  public currentDate: string;
  public errorId: number;
  public primePhone: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) public options: DialogOptions,
    @Inject(APP_CONFIG) public config: AppConfig
  ) {
    this.errorId = options.data.errorId;
    this.currentDate = new Date().toUTCString();
    this.primePhone = config.prime.phone;
  }

  public ngOnInit(): void {
  }
}
