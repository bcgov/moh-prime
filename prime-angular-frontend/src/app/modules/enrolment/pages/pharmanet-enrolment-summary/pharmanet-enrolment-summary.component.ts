import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { WindowRefService } from '@core/services/window-ref.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

@Component({
  selector: 'app-pharmanet-enrolment-summary',
  templateUrl: './pharmanet-enrolment-summary.component.html',
  styleUrls: ['./pharmanet-enrolment-summary.component.scss']
})
export class PharmanetEnrolmentSummaryComponent extends BaseEnrolmentPage implements OnInit {
  public form: FormGroup;
  public enrolment: Enrolment;
  public showProgressBar: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private dialog: MatDialog,
    private toastService: ToastService,
    private logger: LoggerService,
    private windowRef: WindowRefService,
    private fb: FormBuilder
  ) {
    super(route, router);
    this.showProgressBar = false;
    this.form = this.buildVendorEmailGroup();
  }

  public get enrollee() {
    return (this.enrolment) ? this.enrolment.enrollee : null;
  }

  public get mailingAddress() {
    return (this.enrollee) ? this.enrollee.mailingAddress : null;
  }

  public get careSettings() {
    return (this.enrolment) ? this.enrolment.careSettings : null;
  }

  public get enrolmentCertificateNote() {
    return (this.enrolment.enrolmentCertificateNote)
      ? this.enrolment.enrolmentCertificateNote.note
      : null;
  }

  // TODO temporary removed and may be removed permanently
  // public get careconnectRecipients(): FormControl {
  //   return this.form.get('careconnectRecipients') as FormControl;
  // }

  // public get excellerisRecipients(): FormControl {
  //   return this.form.get('excellerisRecipients') as FormControl;
  // }

  // public get plexiaRecipients(): FormControl {
  //   return this.form.get('plexiaRecipients') as FormControl;
  // }

  // public get otherRecipients(): FormControl {
  //   return this.form.get('otherRecipients') as FormControl;
  // }

  public get administratorRecipients(): FormControl {
    return this.form.get('administratorRecipients') as FormControl;
  }

  public getTokenUrl(tokenId: string): string {
    return `${this.config.loginRedirectUrl}/provisioner-access/${tokenId}`;
  }

  public sendProvisionerAccessLinkTo(provisionerName: string) {
    const formControl = this.form.get(`${provisionerName.toLowerCase()}Recipients`) as FormControl;
    if (!formControl) { return; }

    const emails = formControl.value.split(',').map((email: string) => email.trim()).join(',') || null;

    (formControl.valid)
      ? this.sendProvisionerAccessLink(provisionerName, emails, formControl)
      : formControl.markAllAsTouched();
  }

  public sendProvisionerAccessLink(provisionerName: string, emails: string = null, formControl: FormControl = null) {
    const data: DialogOptions = {
      title: 'Confirm Email',
      message: `Are you sure you want to send your Approval Notification to ${provisionerName}?`,
      actionText: 'Send',
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          result
            ? this.enrolmentResource.sendProvisionerAccessLink(provisionerName, emails)
            : EMPTY
        )
      )
      .subscribe(
        () => {
          this.toastService.openSuccessToast('Email was successfully sent');
          if (formControl) {
            formControl.reset();
          }
          this.router.navigate([EnrolmentRoutes.NOTIFICATION_CONFIRMATION], { relativeTo: this.route.parent });
        },
        (error: any) => {
          this.logger.error('[Enrolment] Error occurred sending email', error);
          this.toastService.openErrorToast('Email could not be sent');
        }
      );
  }

  public ngOnInit() {
    // Only shown the first time the enrollee reaches the summary
    const routeState = this.windowRef.nativeWindow.history.state;
    this.showProgressBar = (routeState && routeState.showProgressBar)
      ? routeState.showProgressBar
      : false;

    this.enrolment = this.enrolmentService.enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
  }

  private buildVendorEmailGroup(): FormGroup {
    return this.fb.group({
      // TODO temporary removed and may be removed permanently
      // careconnectRecipients: [null, [Validators.required, FormControlValidators.multipleEmails]],
      // excellerisRecipients: [null, [Validators.required, FormControlValidators.multipleEmails]],
      // plexiaRecipients: [null, [Validators.required, FormControlValidators.multipleEmails]],
      // otherRecipients: [null, [Validators.required, FormControlValidators.email]],
      administratorRecipients: [null, [Validators.required, FormControlValidators.multipleEmails]]
    });
  }
}
