import { Component, Inject, Input } from '@angular/core';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-prime-phone',
  templateUrl: './prime-phone.component.html',
  styleUrls: ['./prime-phone.component.scss']
})
export class PrimePhoneComponent {
  @Input() public mode: 'vanity' | 'normal';

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.mode = 'vanity';
  }

  public get primePhoneHref() {
    const phone = this.config.prime.phone.replace(/[^0-9.]/g, '');
    return `tel:+${phone}`;
  }

  public get primePhoneDisplay() {
    return (this.mode === 'vanity')
      ? this.primePhoneVanity
      : this.primePhone;
  }

  private get primePhone() {
    return this.config.prime.phone;
  }

  private get primePhoneVanity() {
    return this.config.prime.displayPhone;
  }
}
