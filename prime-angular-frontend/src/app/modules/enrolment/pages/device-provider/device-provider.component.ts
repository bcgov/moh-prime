import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { ProgressStatusType } from '@enrolment/shared/enums/progress-status-type.enum';

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
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    super(route, router, dialog);

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

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrollee(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Device Provider information has been saved');
            this.form.markAsPristine();

            const nextRoutePath = (!payload.certifications.length)
              ? EnrolmentRoutes.JOB
              : EnrolmentRoutes.SELF_DECLARATION;
            const routePath = (!this.isProfileComplete)
              ? nextRoutePath
              : EnrolmentRoutes.REVIEW;
            this.routeTo(routePath);
          },
          (error: any) => {
            this.toastService.openErrorToast('Device Provider information could not be saved');
            this.logger.error('[Enrolment] Device Provider::onSubmit error has occurred: ', error);
          }
        );
      this.form.markAsPristine();
    } else {
      this.form.markAllAsTouched();
    }
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  protected createFormInstance() {
    this.form = this.enrolmentStateService.deviceProviderForm;
  }

  protected initForm() {
    this.deviceProviderNumber.valueChanges
      .subscribe((value) => {
        if (!value) {
          this.isInsulinPumpProvider.reset(false, { emitEvent: false });
        }
      });
  }

  protected patchForm() {
    const enrolment = this.enrolmentService.enrolment;

    this.isProfileComplete = enrolment.profileCompleted;
    this.enrolmentStateService.enrolment = enrolment;
    this.isInitialEnrolment = enrolment.progressStatus !== ProgressStatusType.FINISHED;
  }
}
