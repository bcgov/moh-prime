import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { WindowRefService } from '@core/services/window-ref.service';
import { ProgressStatus } from '@enrolment/shared/enums/progress-status.enum';
import moment from 'moment';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { FormControlValidators } from '@shared/validators/form-control.validators';

@Component({
  selector: 'app-pharmanet-enrolment-certificate',
  templateUrl: './pharmanet-enrolment-certificate.component.html',
  styleUrls: ['./pharmanet-enrolment-certificate.component.scss']
})
export class PharmanetEnrolmentCertificateComponent extends BaseEnrolmentPage implements OnInit {
  public enrolment: Enrolment;
  public tokens: EnrolmentCertificateAccessToken[];
  public showProgressBar: boolean;
  public expiryDate: string;

  public form: FormGroup;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private toastService: ToastService,
    private logger: LoggerService,
    private windowRef: WindowRefService,
    private fb: FormBuilder
  ) {
    super(route, router);
    this.tokens = [];
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

  public getTokenUrl(tokenId: string): string {
    return `${this.config.loginRedirectUrl}/provisioner-access/${tokenId}`;
  }

  public get vendorEmail(): FormControl {
    return this.form.get('vendorEmail') as FormControl;
  }

  public sendProvisionerAccessLink() {
    if (this.vendorEmail.value && this.vendorEmail.valid) {
      this.enrolmentResource.sendProvisionerAccessLink(this.vendorEmail.value)
        .subscribe((token: EnrolmentCertificateAccessToken) => this.tokens.push(token));
    }
  }



  public ngOnInit() {
    // Only shown the first time the enrollee reaches the summary
    const routeState = this.windowRef.nativeWindow.history.state;
    this.showProgressBar = (routeState && routeState.showProgressBar)
      ? routeState.showProgressBar
      : false;

    this.enrolment = this.enrolmentService.enrolment;
    this.isInitialEnrolment = this.enrolment.progressStatus !== ProgressStatus.FINISHED;

    if (this.enrolment.enrollee && this.enrolment.enrollee.expiryDate) {
      const expiryMoment = moment(this.enrolment.enrollee.expiryDate);
      this.expiryDate = expiryMoment.isAfter(moment.now())
        ? expiryMoment.format('MMMM Do, YYYY') : null;
    }

    this.busy = this.enrolmentResource.enrolmentCertificateAccessTokens()
      .subscribe(
        (tokens: EnrolmentCertificateAccessToken[]) => this.tokens = tokens,
        (error: any) => {
          this.toastService.openErrorToast('Access tokens could be found.');
          this.logger.error('[EnrolmentCertificate] Summary::ngOnInit error has occurred: ', error);
        }
      );
  }

  private buildVendorEmailGroup(): FormGroup {
    return this.fb.group({
      vendorEmail: [null, [Validators.required, FormControlValidators.email]],
    });
  }
}
