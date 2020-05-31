import { Component, OnInit, Input } from '@angular/core';

import { Party } from '@registration/shared/models/party.model';
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state-service.service';
import { SiteService } from '@registration/shared/services/site.service';

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
    // TODO setup guard to pull organization on each route in the loop
    // private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
  ) { }

  public onSelect(party: Party) {
    // TODO need to get site, but need to set up service first
    // const site = this.siteRegistrationService.site;
    // site.location[this.selectFor] = party;
    // this.siteRegistrationStateService.setSite(site, true);
  }

  public ngOnInit(): void {
    // TODO need to get site, but need to set up service first
    // const site = this.siteRegistrationService.site;
    this.parties = [
      // {
      //   key: 'signingAuthority',
      //   display: 'Signing Authority',
      //   data: site?.location?.organization?.signingAuthority
      // },
      // {
      //   key: 'administratorPharmaNet',
      //   display: 'Administrator of PharmaNet Onboarding',
      //   data: site?.location?.administratorPharmaNet
      // },
      // {
      //   key: 'privacyOfficer',
      //   display: 'Privacy Officer',
      //   data: site?.location?.privacyOfficer
      // },
      // {
      //   key: 'technicalSupport',
      //   display: 'Technical Support',
      //   data: site?.location?.technicalSupport
      // }
    ];

    const index = this.parties.findIndex(p => p.key === this.selectFor);
    this.parties = this.parties.slice(0, index);
  }
}
