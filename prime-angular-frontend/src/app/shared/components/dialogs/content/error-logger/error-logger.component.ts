import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

import moment, { Moment } from 'moment';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { DialogOptions } from '../../dialog-options.model';

@Component({
  selector: 'app-error-logger',
  templateUrl: './error-logger.component.html',
  styleUrls: ['./error-logger.component.scss']
})
export class ErrorLoggerComponent implements OnInit {
  public errorId: number;
  public currentDate: Moment;
  public primePhone: string;
  public primeSupportEmail: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) private options: DialogOptions,
    @Inject(APP_CONFIG) public config: AppConfig
  ) {
    this.errorId = options.data.errorId;
    this.currentDate = moment();
    this.primePhone = config.prime.phone;
    this.primeSupportEmail = config.prime.supportEmail;
  }

  public ngOnInit(): void {}
}
