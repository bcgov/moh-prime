import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-device-provider',
  templateUrl: './device-provider.component.html',
  styleUrls: ['./device-provider.component.scss']
})
export class DeviceProviderComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];
  public hasInitialStatus: boolean;
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
  }

  public get deviceProviderNumber(): FormControl {
    return this.form.get('deviceProviderNumber') as FormControl;
  }

  public get isInsulinPumpProvider(): FormControl {
    return this.form.get('isInsulinPumpProvider') as FormControl;
  }

  public onRoute(routePath: EnrolmentRoutes) {
    this.router.navigate([routePath], { relativeTo: this.route.parent });
  }

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrollee(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Device Provider information has been saved');
            this.form.markAsPristine();
            if (payload.certifications.length > 0) {
              this.router.navigate([EnrolmentRoutes.SELF_DECLARATION], { relativeTo: this.route.parent });
            } else {
              this.router.navigate([EnrolmentRoutes.JOB], { relativeTo: this.route.parent });
            }
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


  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // Initialize form changes before patching
    this.initForm();

    const enrolment = this.enrolmentService.enrolment;
    this.hasInitialStatus = enrolment.initialStatus;

    this.enrolmentStateService.enrolment = enrolment;
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.deviceProviderForm;
  }

  private initForm() {
    this.deviceProviderNumber.valueChanges
      .subscribe((value) => {
        if (!value) {
          this.isInsulinPumpProvider.reset(false, { emitEvent: false });
        }
      });
  }
}
