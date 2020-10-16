import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { EnrolleeAgreement } from '@shared/models/agreement.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-access-agreement-history',
  templateUrl: './access-agreement-history.component.html',
  styleUrls: ['./access-agreement-history.component.scss']
})
export class AccessAgreementHistoryComponent extends BaseEnrolmentPage implements OnInit {
  public busy: Subscription;
  public accessTerm: EnrolleeAgreement;

  constructor(
    protected router: Router,
    protected route: ActivatedRoute,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private logger: LoggerService,
    private toastService: ToastService,
  ) {
    super(route, router);
  }

  public routeTo() {
    super.routeTo(EnrolmentRoutes.routePath(EnrolmentRoutes.ACCESS_TERMS));
  }

  public ngOnInit(): void {
    this.getAccessTerm();
  }

  private getAccessTerm() {
    const enrolleeId = this.enrolmentService.enrolment.id;
    const accessTermId = this.route.snapshot.params.id;
    this.busy = this.enrolmentResource.getAccessTerm(enrolleeId, accessTermId)
      .subscribe(
        (accessTerm: EnrolleeAgreement) => {
          this.logger.info('ENROLLEE_AGREEMENT', accessTerm);
          this.accessTerm = accessTerm;
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrollee agreement could not be retrieved');
          this.logger.error('[Enrolments] AccessAgreementHistory::getAccessTerm error has occurred: ', error);
        }
      );
  }
}
