import { Component, Inject } from '@angular/core';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-prime-phone',
  templateUrl: './prime-phone.component.html',
  styleUrls: ['./prime-phone.component.scss']
})
export class PrimePhoneComponent {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public get primePhoneDisplay() {
    return this.config.prime.displayPhone;
  }

  public get primePhoneHref() {
    return `tel:+${this.config.prime.phone}`;
  }
}
