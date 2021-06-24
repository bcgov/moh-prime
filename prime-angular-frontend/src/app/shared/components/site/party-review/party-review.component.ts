import { Component, Input } from '@angular/core';

import { Party } from '@lib/models/party.model';

// TODO refactor this component it doesn't match a consistent pattern with enrolments for reuse
@Component({
  selector: 'app-party-review',
  templateUrl: './party-review.component.html',
  styleUrls: ['./party-review.component.scss']
})
export class PartyReviewComponent {
  @Input() public party: Party;
  /**
   * @description
   * List of fields that should be excluded.
   */
  @Input() public excludeList: ('jobRoleTitle' | 'fax' | 'smsPhone' | 'mailingAddress')[];

  constructor() {
    this.excludeList = [];
  }
}
