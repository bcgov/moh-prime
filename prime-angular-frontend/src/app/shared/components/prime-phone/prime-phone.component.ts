import { Component, Inject, Input } from '@angular/core';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-prime-phone',
  templateUrl: './prime-phone.component.html',
  styleUrls: ['./prime-phone.component.scss']
})
export class PrimePhoneComponent {
  @Input() public mode: 'vanity' | 'normal';
  @Input() public phoneNumber: 'prime' | 'director';

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.mode = 'normal';
    this.phoneNumber = 'director';
  }

  public get primePhoneHref(): string {
    const phone = this.primePhone.replace(/[^0-9.]/g, '');
    return `tel:+${phone}`;
  }

  public get primePhoneDisplay(): string {
    return (this.mode === 'vanity')
      ? this.primePhoneVanity
      : this.primePhone;
  }

  private get primePhone(): string {
    return (this.phoneNumber === 'prime')
      ? this.config.prime.phone
      : this.config.phoneNumbers.director;
  }

  private get primePhoneVanity(): string {
    return this.config.prime.displayPhone;
  }
}
