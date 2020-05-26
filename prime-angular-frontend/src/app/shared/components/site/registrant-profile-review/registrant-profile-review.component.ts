import { Component, OnInit, Input } from '@angular/core';

import { Party } from '@registration/shared/models/party.model';

// TODO rename and make it party instead of registrant
@Component({
  selector: 'app-registrant-profile-review',
  templateUrl: './registrant-profile-review.component.html',
  styleUrls: ['./registrant-profile-review.component.scss']
})
export class RegistrantProfileReviewComponent implements OnInit {
  @Input() party: Party;

  constructor() { }

  public ngOnInit() { }
}
