import { Component, OnInit } from '@angular/core';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { AuthRoutes } from '@auth/auth.routes';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccessTerm } from '@enrolment/shared/models/access-term.model';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

@Component({
  selector: 'app-access-agreement-history',
  templateUrl: './access-agreement-history.component.html',
  styleUrls: ['./access-agreement-history.component.scss']
})
export class AccessAgreementHistoryComponent extends BaseEnrolmentPage implements OnInit {
  public busy: Subscription;
  public accessTerm: AccessTerm;

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

  public ngOnInit() {
    this.getAccessTerm();
  }

  private getAccessTerm() {
    const enrolleeId = this.enrolmentService.enrolment.id;
    const accessTermId = this.route.snapshot.params.id;
    this.busy = this.enrolmentResource.getAccessTerm(enrolleeId, accessTermId)
      .subscribe(
        (accessTerm: AccessTerm) => {
          this.logger.info('ACCESS TERM', accessTerm);
          this.accessTerm = accessTerm;
        },
        (error: any) => {
          this.toastService.openErrorToast('Access Term could not be retrieved');
          this.logger.error('[Enrolments] AccessAgreementHistory::getAccessTerm error has occurred: ', error);
        }
      );
  }
}
