import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { Party } from '@registration/shared/models/party.model';
import { SiteService } from '@registration/shared/services/site.service';

@Component({
  selector: 'app-same-as',
  templateUrl: './same-as.component.html',
  styleUrls: ['./same-as.component.scss']
})
export class SameAsComponent implements OnInit {
  // When not provided selections are not displayed
  @Input() public selectFor: string;
  @Output() public selected: EventEmitter<Party>;
  public parties: { key: string, display: string, data: Party }[];

  constructor(
    private siteService: SiteService
  ) {
    this.selected = new EventEmitter<Party>();
  }

  public onSelect(party: Party) {
    this.selected.emit(party);
  }

  public ngOnInit(): void {
    const site = this.siteService.site;
    this.parties = [
      {
        key: 'signingAuthority',
        display: 'Signing Authority',
        data: site?.provisioner
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

    // Order of parties is dependent on the order of routing
    const index = this.parties.findIndex(p => p.key === this.selectFor);
    this.parties = this.parties.slice(0, index);
  }
}
