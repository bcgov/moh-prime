import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { EnrolmentSubmission } from '@shared/models/enrollee-submission.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-access-agreement-history-enrolment',
  templateUrl: './access-agreement-history-enrolment.component.html',
  styleUrls: ['./access-agreement-history-enrolment.component.scss']
})
export class AccessAgreementHistoryEnrolmentComponent extends BaseEnrolmentPage implements OnInit {
  public busy: Subscription;
  public enrolmentSubmission: EnrolmentSubmission;
  public expiryDate: string;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService
  ) {
    super(route, router);
  }

  public routeTo() {
    super.routeTo(EnrolmentRoutes.routePath(EnrolmentRoutes.ACCESS_TERMS));
  }

  public ngOnInit(): void {
    const enrolleeId = this.enrolmentService.enrolment.id;
    const accessTermId = this.route.snapshot.params.id;
    this.expiryDate = this.enrolmentService.enrolment.expiryDate;

    this.busy = this.enrolmentResource
      .getEnrolmentSubmissionForAccessTerm(enrolleeId, accessTermId)
      .subscribe((enrolmentSubmission: EnrolmentSubmission) => this.enrolmentSubmission = enrolmentSubmission);
  }
}

