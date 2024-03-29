import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { EnrolleeReviewStatus } from '@shared/models/enrollee-review-status.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-review-status',
  templateUrl: './enrollee-review-status.component.html',
  styleUrls: ['./enrollee-review-status.component.scss']
})
export class EnrolleeReviewStatusComponent implements OnInit {
  public hasActions: boolean;

  constructor(
    protected route: ActivatedRoute,
  ) {
    this.hasActions = true;
  }

  public onAction(): void { }

  public ngOnInit(): void { }
}
