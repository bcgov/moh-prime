import { Component, Input } from '@angular/core';

import { Party } from '@lib/models/party.model';

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
  @Input() public excludeList: ('name' | 'jobRoleTitle' | 'fax' | 'smsPhone' | 'mailingAddress')[];

  constructor() {
    this.excludeList = [];
  }
}
