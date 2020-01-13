import { Component, OnInit, Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-prime-contact',
  templateUrl: './prime-contact.component.html',
  styleUrls: ['./prime-contact.component.scss']
})
export class PrimeContactComponent implements OnInit {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public get primeContact() {
    return this.config.prime;
  }

  public get primePhoneHref() {
    return `tel:+${this.primeContact.phone}`;
  }

  public get primeEmailHref() {
    return `mailto:${this.primeContact.email}`;
  }

  public ngOnInit() { }
}
