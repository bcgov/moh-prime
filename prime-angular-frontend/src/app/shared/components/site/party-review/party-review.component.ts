import { Component, Input } from '@angular/core';

import { Party } from '@lib/models/party.model';

// TODO refactor this component it doesn't match a consistent pattern with enrolments for reuse
@Component({
  selector: 'app-party-review',
  templateUrl: './party-review.component.html',
  styleUrls: ['./party-review.component.scss']
})
export class PartyReviewComponent {
  // TODO set this to be base class of Person and hide keys that don't exist
  @Input() public party: Party;
  // TODO temporary fix for signing authority being different then the other parties
  @Input() public showName: boolean;

  constructor() {
    this.showName = true;
  }
}
