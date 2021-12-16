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
  public enrolleeReviewStatus: EnrolleeReviewStatus;
  public hasActions: boolean;

  constructor(
    protected route: ActivatedRoute,
    private adjudicationResource: AdjudicationResource
  ) {
    this.hasActions = true;
  }

  public onAction(): void {
    this.getEnrolleeReviewStatusByEnrolleeId(this.route.snapshot.params.id);
  }

  public ngOnInit(): void {
    this.route.params.subscribe((params) => this.getEnrolleeReviewStatusByEnrolleeId(params.id));
  }

  private getEnrolleeReviewStatusByEnrolleeId(enrolleeId: number): void {
    this.adjudicationResource.getEnrolleeReviewStatus(enrolleeId)
      .subscribe((reviewStatus: EnrolleeReviewStatus) => this.enrolleeReviewStatus = reviewStatus);
  }
}
