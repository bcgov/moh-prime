import { Component, Input } from '@angular/core';

import { Party } from '@lib/models/party.model';
import { HealthAuthorityTechnicalSupport } from '@shared/models/health-authority-technical-support';

@Component({
  selector: 'app-party-review',
  templateUrl: './party-review.component.html',
  styleUrls: ['./party-review.component.scss']
})
export class PartyReviewComponent {
  @Input() public party: Party;
  /**
   * @description
   * `party` field is not sufficient as `party instanceof HealthAuthorityTechnicalSupport` test fails and we want to avoid use of `$any()`
   */
  @Input() public healthAuthorityTechnicalSupport: HealthAuthorityTechnicalSupport;
  /**
   * @description
   * List of fields that should be excluded.
   */
  @Input() public excludeList: ('name' | 'jobRoleTitle' | 'fax' | 'smsPhone' | 'physicalAddress' | 'mailingAddress')[];

  constructor() {
    this.excludeList = [];
  }
}
