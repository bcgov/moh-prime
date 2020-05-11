import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Party } from '@registration/shared/models/party.model';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

@Component({
  selector: 'app-same-as',
  templateUrl: './same-as.component.html',
  styleUrls: ['./same-as.component.scss']
})
export class SameAsComponent implements OnInit {
  // When not provided selections are not displayed
  @Input() public selectFor: string;
  public parties: { key: string, display: string, data: Party }[];

  constructor(
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService
  ) { }

  public onSelect(party: Party) {
    const site = this.siteRegistrationService.site;
    site.location[this.selectFor] = party;
    this.siteRegistrationStateService.setSite(site, true);
  }

  public ngOnInit(): void {
    const site = this.siteRegistrationService.site;
    this.parties = [
      {
        key: 'signingAuthority',
        display: 'Signing Authority',
        data: site?.location?.organization?.signingAuthority
      },
      {
        key: 'administratorPharmaNet',
        display: 'Administrator of PharmaNet Onboarding',
        data: site?.location?.administratorPharmaNet
      },
      {
        key: 'privacyOfficer',
        display: 'Privacy Officer',
        data: site?.location?.privacyOfficer
      },
      {
        key: 'technicalSupport',
        display: 'Technical Support',
        data: site?.location?.technicalSupport
      }
    ];

    const index = this.parties.findIndex(p => p.key === this.selectFor);
    this.parties = this.parties.slice(0, index);
  }
}
