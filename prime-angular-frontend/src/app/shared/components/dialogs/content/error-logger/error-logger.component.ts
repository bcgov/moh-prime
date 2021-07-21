import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';

import { environment } from '@env/environment';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { DialogOptions } from '../../dialog-options.model';

@Component({
  selector: 'app-error-logger',
  templateUrl: './error-logger.component.html',
  styleUrls: ['./error-logger.component.scss']
})
export class ErrorLoggerComponent implements OnInit {
  public error: any;
  public enviornment = environment;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
    @Inject(APP_CONFIG) public config: AppConfig,
    public router: Router
  ) {
    this.error = data.data;
  }

  public ngOnInit(): void {
  }
}
