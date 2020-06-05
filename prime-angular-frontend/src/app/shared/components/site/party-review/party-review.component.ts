import { Component, Input } from '@angular/core';

import { Party } from '@registration/shared/models/party.model';

@Component({
  selector: 'app-party-review',
  templateUrl: './party-review.component.html',
  styleUrls: ['./party-review.component.scss']
})
export class PartyReviewComponent {
  @Input() party: Party;
  // TODO temporary fix for signing authority being different then the other parties
  @Input() showName: boolean;

  constructor() {
    this.showName = true;
  }
}
