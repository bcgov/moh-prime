import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';

import moment from 'moment';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { WindowRefService } from '@core/services/window-ref.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { AccessTerm } from '@enrolment/shared/models/access-term.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';

@Component({
  selector: 'app-pharmanet-enrolment-certificate',
  templateUrl: './pharmanet-enrolment-certificate.component.html',
  styleUrls: ['./pharmanet-enrolment-certificate.component.scss']
})
export class PharmanetEnrolmentCertificateComponent extends BaseEnrolmentPage implements OnInit {
  public enrolment: Enrolment;
  public showProgressBar: boolean;
  public expiryDate: string;
  public accessTerm: AccessTerm;

  public form: FormGroup;

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
    return (this.enrolment.enrolmentCertificateNote) ? this.enrolment.enrolmentCertificateNote.note : null;
  }

  public get vendorEmail(): FormControl {
    return this.form.get('vendorEmail') as FormControl;
  }

  public get isRu(): boolean {
    return (this.accessTerm)
      ? this.accessTerm.userClause.enrolleeClassification === EnrolleeClassification.RU
      : false;
  }

  public getTokenUrl(tokenId: string): string {
    return `${this.config.loginRedirectUrl}/provisioner-access/${tokenId}`;
  }

  public sendProvisionerAccessLink() {
    if (!this.vendorEmail.value || !this.vendorEmail.valid) {
      return;
    }

    const data: DialogOptions = {
      title: 'Confirm Email',
      message: `Are you sure you want to send your PharmaNet certificate to ${this.vendorEmail.value}?`,
      actionText: 'Send',
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          result
            ? this.enrolmentResource.sendProvisionerAccessLink(this.vendorEmail.value)
            : EMPTY
        )
      )
      .subscribe(
        () => this.toastService.openSuccessToast('Email was successfully sent'),
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

    if (this.enrolment.enrollee && this.enrolment.expiryDate) {
      const expiryMoment = moment(this.enrolment.expiryDate);
      this.expiryDate = expiryMoment.isAfter(moment.now())
        ? expiryMoment.format('MMMM Do, YYYY') : null;
    }

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
      vendorEmail: [null, [Validators.required, FormControlValidators.email]],
    });
  }
}
