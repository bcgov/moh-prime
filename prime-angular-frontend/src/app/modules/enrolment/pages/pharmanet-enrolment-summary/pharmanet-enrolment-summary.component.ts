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
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';

@Component({
  selector: 'app-pharmanet-enrolment-summary',
  templateUrl: './pharmanet-enrolment-summary.component.html',
  styleUrls: ['./pharmanet-enrolment-summary.component.scss']
})
export class PharmanetEnrolmentSummaryComponent extends BaseEnrolmentPage implements OnInit {
  public form: FormGroup;
  public enrolment: Enrolment;
  public showProgressBar: boolean;

  public CareSettingEnum = CareSettingEnum;
  public EnrolmentStatus = EnrolmentStatus;

  public showCommunityHealth: boolean;
  public showPharmacist: boolean;
  public showHealthAuthority: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private fb: FormBuilder,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private dialog: MatDialog,
    private toastService: ToastService,
    private logger: LoggerService,
    private windowRef: WindowRefService
  ) {
    super(route, router);
    this.showProgressBar = false;
    this.showCommunityHealth = true;
    this.showPharmacist = true;
    this.showHealthAuthority = true;
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

  public isEmailVisible(careSettingCode: number) {
    switch (careSettingCode) {
      case this.CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        return this.showCommunityHealth;
      }
      case this.CareSettingEnum.COMMUNITY_PHARMACIST: {
        return this.showPharmacist;
      }
      case this.CareSettingEnum.HEALTH_AUTHORITY: {
        return this.showHealthAuthority;
      }
      default: {
        return false;
      }
    }
  }

  public setShowEmail(careSettingCode: number, show: boolean) {
    switch (careSettingCode) {
      case this.CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        this.showCommunityHealth = show;
        break;
      }
      case this.CareSettingEnum.COMMUNITY_PHARMACIST: {
        this.showPharmacist = show;
        break;
      }
      case this.CareSettingEnum.HEALTH_AUTHORITY: {
        this.showHealthAuthority = show;
        break;
      }
      default: {
        return show;
      }
    }
  }


  public get enrolmentCertificateNote() {
    return (this.enrolment.enrolmentCertificateNote)
      ? this.enrolment.enrolmentCertificateNote.note
      : null;
  }

  public get recipients(): FormControl {
    return this.form.get('recipients') as FormControl;
  }

  public getTokenUrl(tokenId: string): string {
    return `${this.config.loginRedirectUrl}/provisioner-access/${tokenId}`;
  }

  public sendProvisionerAccessLinkTo(careSettingCode: number) {
    const formControl = this.form.get(`recipients`) as FormControl;
    if (!formControl) { return; }

    const emails = formControl.value.split(',').map((email: string) => email.trim()).join(',') || null;

    (formControl.valid)
      ? this.sendProvisionerAccessLink(emails, formControl, careSettingCode)
      : formControl.markAllAsTouched();
  }

  public sendProvisionerAccessLink(emails: string = null, formControl: FormControl = null, careSettingCode) {
    // const data: DialogOptions = {
    //   title: 'Confirm Email',
    //   message: `Are you sure you want to send your Approval Notification?`,
    //   actionText: 'Send',
    // };
    // this.busy = this.dialog.open(ConfirmDialogComponent, { data })
    //   .afterClosed()
    //   .pipe(
    //     exhaustMap((result: boolean) =>
    //       result
    //         ? this.enrolmentResource.sendProvisionerAccessLink(emails, careSettingCode)
    //         : EMPTY
    //     )
    //   )
    //   .subscribe(() => {
    //     this.toastService.openSuccessToast('Email was successfully sent');
    //     if (formControl) {
    //       formControl.reset();
    //     }
    //     this.setShowEmail(careSettingCode, false);
    //   });

    this.setShowEmail(careSettingCode, false);
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
      recipients: [null, [Validators.required, FormControlValidators.multipleEmails]]
    });
  }
}
