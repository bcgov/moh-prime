import { Component, OnInit, OnDestroy } from '@angular/core';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

@Component({
  selector: 'app-remote-access',
  templateUrl: './remote-access.component.html',
  styleUrls: ['./remote-access.component.scss']
})
export class RemoteAccessComponent extends BaseEnrolmentProfilePage implements OnInit {

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentStateService: EnrolmentStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentStateService, toastService, logger, utilService);
  }

  public onClick() {

  }

  ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.enrolmentStateService.careSettingsForm;
  }

  protected initForm() {

  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.CARE_SETTING;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  public routeBackTo() {
    const routePath = (this.enrolmentStateService.enrolment.certifications.length)
      ? EnrolmentRoutes.REGULATORY
      : EnrolmentRoutes.JOB;

    this.routeTo(routePath);
  }


}
