import { Component, OnInit, Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-maintenance',
  templateUrl: './maintenance.component.html',
  styleUrls: ['./maintenance.component.scss']
})
export class MaintenanceComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public get email(): string {
    return this.config.prime.email;
  }

  public ngOnInit() { }
}
