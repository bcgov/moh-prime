import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-identity-access-code',
  templateUrl: './identity-access-code.component.html',
  styleUrls: ['./identity-access-code.component.scss']
})
export class IdentityAccessCodeComponent extends BaseEnrolmentProfilePage implements OnInit {
  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    private formUtilsService: FormUtilsService
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentFormStateService, toastService, logger, utilService);
  }

  public ngOnInit() {
    this.createFormInstance();
    // this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    // this.form = this.enrolmentFormStateService.identityForm;
  }

  protected initForm() { }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.IDENTITY_SUBMISSION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }
}
