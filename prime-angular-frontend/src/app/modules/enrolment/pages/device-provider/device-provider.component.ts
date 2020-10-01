import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
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
  selector: 'app-device-provider',
  templateUrl: './device-provider.component.html',
  styleUrls: ['./device-provider.component.scss']
})
export class DeviceProviderComponent extends BaseEnrolmentProfilePage implements OnInit {
  public decisions: { code: boolean, name: string }[];

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
    protected formUtilsService: FormUtilsService
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

    this.decisions = [
      { code: false, name: 'No' },
      { code: true, name: 'Yes' }
    ];
  }

  public get deviceProviderNumber(): FormControl {
    return this.form.get('deviceProviderNumber') as FormControl;
  }

  public get isInsulinPumpProvider(): FormControl {
    return this.form.get('isInsulinPumpProvider') as FormControl;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.deviceProviderForm;
  }

  protected initForm() {
    this.deviceProviderNumber.valueChanges
      .subscribe((value) => {
        if (!value) {
          this.isInsulinPumpProvider.reset(false, { emitEvent: false });
        }
      });
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = (!this.enrolmentFormStateService.json.certifications.length)
        ? EnrolmentRoutes.JOB
        : EnrolmentRoutes.SELF_DECLARATION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }
}
