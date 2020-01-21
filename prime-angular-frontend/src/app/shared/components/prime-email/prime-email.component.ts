import { Component, Inject } from '@angular/core';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-prime-email',
  templateUrl: './prime-email.component.html',
  styleUrls: ['./prime-email.component.scss']
})
export class PrimeEmailComponent {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public get primeEmail() {
    return this.config.prime.email;
  }

  public get primeEmailHref() {
    return `mailto:${this.primeEmail}`;
  }
}
