import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { MatDialog } from '@angular/material';

import moment from 'moment';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { WindowRefService } from '@core/services/window-ref.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { AccessTerm } from '@shared/models/access-term.model';

@Component({
  selector: 'app-pharmanet-enrolment-summary',
  templateUrl: './pharmanet-enrolment-summary.component.html',
  styleUrls: ['./pharmanet-enrolment-summary.component.scss']
})
export class PharmanetEnrolmentSummaryComponent extends BaseEnrolmentPage implements OnInit {
  public form: FormGroup;
  public enrolment: Enrolment;
  public showProgressBar: boolean;
  public accessTerm: AccessTerm;

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

  public get privileges() {
    return (this.enrolment) ? this.enrolment.privileges : null;
  }

  public get organizations() {
    return (this.enrolment) ? this.enrolment.organizations : null;
  }

  public get enrolmentCertificateNote() {
    return (this.enrolment.enrolmentCertificateNote)
      ? this.enrolment.enrolmentCertificateNote.note
      : null;
  }

  public get careconnectRecipients(): FormControl {
    return this.form.get('careconnectRecipients') as FormControl;
  }

  public get excellerisRecipients(): FormControl {
    return this.form.get('excellerisRecipients') as FormControl;
  }

  public get plexiaRecipients(): FormControl {
    return this.form.get('plexiaRecipients') as FormControl;
  }

  public get otherRecipients(): FormControl {
    return this.form.get('otherRecipients') as FormControl;
  }

  public get isRu(): boolean {
    return (this.accessTerm)
      ? this.accessTerm.userClause.enrolleeClassification === EnrolleeClassification.RU
      : false;
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
      message: `Are you sure you want to send your PharmaNet certificate to ${provisionerName}?`,
      actionText: 'Send',
    };
    this.dialog.open(ConfirmDialogComponent, { data })
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

    this.enrolmentResource.getAccessTermLatest(this.enrolment.id, true)
      .subscribe(
        (accessTerm: AccessTerm) => this.accessTerm = accessTerm,
        (error: any) => {
          this.toastService.openErrorToast(`Terms of access could not be found`);
          this.logger.error('[Enrolment] AccessAgreement::ngOnInit error has occurred: ', error);
        }
      );
  }

  private buildVendorEmailGroup(): FormGroup {
    return this.fb.group({
      careconnectRecipients: [null, [Validators.required, FormControlValidators.multipleEmails]],
      excellerisRecipients: [null, [Validators.required, FormControlValidators.multipleEmails]],
      plexiaRecipients: [null, [Validators.required, FormControlValidators.multipleEmails]],
      otherRecipients: [null, [Validators.required, FormControlValidators.email]]
    });
  }
}
