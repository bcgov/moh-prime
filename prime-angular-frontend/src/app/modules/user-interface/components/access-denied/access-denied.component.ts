import { Component, OnInit, Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-access-denied',
  templateUrl: './access-denied.component.html',
  styleUrls: ['./access-denied.component.scss']
})
export class AccessDeniedComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public get email(): string {
    return this.config.prime.email;
  }

  public ngOnInit() { }
}
