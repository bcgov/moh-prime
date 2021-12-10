import { Component, OnInit } from '@angular/core';

import { HttpEnrollee } from '@shared/models/enrolment.model';

@Component({
  selector: 'app-enrollee-review-status',
  templateUrl: './enrollee-review-status.component.html',
  styleUrls: ['./enrollee-review-status.component.scss']
})
export class EnrolleeReviewStatusComponent implements OnInit {
  public enrollee: HttpEnrollee;
  public hasActions: boolean;

  constructor(
  ) {
    this.hasActions = true;
  }

  public ngOnInit(): void { }
}
