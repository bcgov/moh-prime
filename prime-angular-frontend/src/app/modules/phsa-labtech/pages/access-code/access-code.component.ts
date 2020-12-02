import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { PhsaLabtechFormStateService } from '@phsa/shared/services/phsa-labtech-form-state.service';
import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';

@Component({
  selector: 'app-access-code',
  templateUrl: './access-code.component.html',
  styleUrls: ['./access-code.component.scss']
})
export class AccessCodeComponent extends BaseEnrolmentProfilePage implements OnInit {
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
    protected formUtilsService: FormUtilsService,
    protected phsaLabtechFormStateService: PhsaLabtechFormStateService,
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService
    );
  }

  public get accessCode(): FormControl {
    return this.form.get('accessCode') as FormControl;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.phsaLabtechFormStateService.accessForm;
  }

  protected initForm() { }

  protected handleSubmission(beenThroughTheWizard: boolean = false) {
    if (this.isInitialEnrolment) {
      // Allow routing to occur without invoking the deactivation,
      // modal to persist form state being dirty between views
      this.allowRoutingWhenDirty = true;
      this.nextRouteAfterSubmit();
    }
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = PhsaLabtechRoutes.DEMOGRAPHIC;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }
}
