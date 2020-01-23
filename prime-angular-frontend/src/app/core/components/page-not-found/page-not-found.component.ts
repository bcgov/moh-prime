import { Component, OnInit, Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrls: ['./page-not-found.component.scss']
})
export class PageNotFoundComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public get email(): string {
    return this.config.prime.email;
  }

  public ngOnInit() { }
}
