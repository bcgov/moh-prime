import { Component, OnInit, Input } from '@angular/core';

import { Registrant } from '@shared/models/registrant';

@Component({
  selector: 'app-registrant-profile-review',
  templateUrl: './registrant-profile-review.component.html',
  styleUrls: ['./registrant-profile-review.component.scss']
})
export class RegistrantProfileReviewComponent implements OnInit {
  @Input() registrant: Registrant;

  constructor() { }

  public ngOnInit() { }
}
