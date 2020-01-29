import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Enrolment } from '@shared/models/enrolment.model';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { ProgressStatus } from '@enrolment/shared/enums/progress-status.enum';

@Component({
  selector: 'app-declined-access-agreement',
  templateUrl: './declined-access-agreement.component.html',
  styleUrls: ['./declined-access-agreement.component.scss']
})
export class DeclinedAccessAgreementComponent extends BaseEnrolmentPage implements OnInit {
  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private enrolmentService: EnrolmentService
  ) {
    super(route, router);
  }

  public ngOnInit() {
    this.enrolmentService.enrolment$
      .subscribe((enrolment: Enrolment) =>
        this.isInitialEnrolment = enrolment
          ? enrolment.progressStatus !== ProgressStatus.FINISHED
          : null
      );
  }
}
