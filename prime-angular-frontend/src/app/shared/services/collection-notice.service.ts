import { Inject, Injectable } from '@angular/core';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';

@Injectable({
  providedIn: 'root'
})
export class CollectionNoticeService {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public get Title(): string {
    return 'Collection of Personal Information Notice';
  }

  public get ContentToRender(): string {
    return `<p>
        The personal information you provide to the PRIME application is collected by the British Columbia Ministry of
        Health under the authority of s. 26(a) and 26(c) of the Freedom of Information and Protection of Privacy Act
        (FOIPPA) and s. 22(1)(b) of the Pharmaceutical Services Act. If you have any questions about the collection or use
        of this information, call
      </p>
      <p>
        Director, Information and PharmaNet Innovation at <a href="tel:+${this.config.phoneNumbers.director}">${this.config.phoneNumbers.director}</a>
        or <a href="mailto:${this.config.prime.supportEmail}">${this.config.prime.supportEmail}</a>.
      </p>`;
  }
}
