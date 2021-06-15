import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { HttpEnrollee } from '@shared/models/enrolment.model';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-review-status',
  templateUrl: './enrollee-review-status.component.html',
  styleUrls: ['./enrollee-review-status.component.scss']
})
export class EnrolleeReviewStatusComponent implements OnInit {
  public enrollee: HttpEnrollee;
  public hasActions: boolean;

  constructor(
    private route: ActivatedRoute,
    private adjudicationResource: AdjudicationResource
  ) {
    this.hasActions = true;
  }

  public onAction() {
    this.getEnrolleeById(this.route.snapshot.params.id);
  }

  public ngOnInit(): void {
    this.route.params.subscribe((params) => this.getEnrolleeById(params.id));
  }

  private getEnrolleeById(enrolleeId: number) {
    this.adjudicationResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => this.enrollee = enrollee);
  }
}
