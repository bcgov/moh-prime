import { Component, Input } from '@angular/core';

import { Party } from '@registration/shared/models/party.model';

@Component({
  selector: 'app-party-review',
  templateUrl: './party-review.component.html',
  styleUrls: ['./party-review.component.scss']
})
export class PartyReviewComponent {
  @Input() party: Party;
}
