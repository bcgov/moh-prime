import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { EnrolmentProfileVersion } from '@adjudication/shared/models/enrollee-profile-history.model';
import { ActivatedRoute, Router } from '@angular/router';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';

@Component({
  selector: 'app-access-agreement-history-enrolment',
  templateUrl: './access-agreement-history-enrolment.component.html',
  styleUrls: ['./access-agreement-history-enrolment.component.scss']
})
export class AccessAgreementHistoryEnrolmentComponent extends BaseEnrolmentPage implements OnInit {
  public busy: Subscription;
  public enrolmentProfileHistory: EnrolmentProfileVersion;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private logger: LoggerService,
    private enrolmentService: EnrolmentService
  ) {
    super(route, router);
  }

  public routeTo() {
    super.routeTo(EnrolmentRoutes.routePath(EnrolmentRoutes.ACCESS_TERMS));
  }

  public ngOnInit() {
    const enrolleeId = this.enrolmentService.enrolment.id;
    const accessTermId = this.route.snapshot.params.id;
    this.busy = this.enrolmentResource
      .getEnrolmentProfileForAccessTerm(enrolleeId, accessTermId)
      .subscribe(
        (enrolmentProfileVersion: EnrolmentProfileVersion) => this.enrolmentProfileHistory = enrolmentProfileVersion,
        (error: any) => {
          this.toastService.openErrorToast('Enrollee history could not be retrieved');
          this.logger.error('[Enrolment] AccessAgreementHistoryEnrolmentComponent::ngOnInit error has occurred: ', error);
        }
      );
  }
}
