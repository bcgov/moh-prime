import { Component, OnInit, Inject } from '@angular/core';
import { AppConfig, APP_CONFIG } from 'app/app-config.module';

@Component({
  selector: 'app-prime-support-email',
  templateUrl: './prime-support-email.component.html',
  styleUrls: ['./prime-support-email.component.scss']
})
export class PrimeSupportEmailComponent {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public get primeEmail() {
    return this.config.prime.supportEmail;
  }

  public get primeEmailHref() {
    return `mailto:${this.primeEmail}`;
  }
}
