import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';
import { EnrolmentStatusReason as EnrolmentStatusReasonEnum } from '@shared/enums/enrolment-status-reason.enum';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { ProgressStatus } from '@enrolment/shared/enums/progress-status.enum';

@Component({
  selector: 'app-submission-confirmation',
  templateUrl: './submission-confirmation.component.html',
  styleUrls: ['./submission-confirmation.component.scss']
})
export class SubmissionConfirmationComponent extends BaseEnrolmentPage implements OnInit {
  public isAutomatic: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolmentService: EnrolmentService
  ) {
    super(route, router);
  }

  public get primeContact() {
    return this.config.prime;
  }

  public ngOnInit() {
    this.enrolmentService.enrolment$
      .subscribe((enrolment: Enrolment) => {
        // Only automatic if the enrolment reason is `Automatic`
        this.isAutomatic = enrolment.currentStatus.enrolmentStatusReasons
          .every((reason: EnrolmentStatusReason) => reason.statusReasonCode === EnrolmentStatusReasonEnum.AUTOMATIC);
        this.isInitialEnrolment = enrolment.progressStatus !== ProgressStatus.FINISHED;
      });
  }
}
