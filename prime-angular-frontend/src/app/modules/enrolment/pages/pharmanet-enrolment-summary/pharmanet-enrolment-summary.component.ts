import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators, FormArray } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ToastService } from '@core/services/toast.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { ImageComponent } from '@shared/components/dialogs/content/image/image.component';
import { Role } from '@auth/shared/enum/role.enum';

/**
 * TODO: https://bcgovmoh.atlassian.net/browse/PRIME-2325 (Refactor common code in both PharmanetEnrolmentSummaryComponent and NextStepsComponent)
 */
@Component({
  selector: 'app-pharmanet-enrolment-summary',
  templateUrl: './pharmanet-enrolment-summary.component.html',
  styleUrls: ['./pharmanet-enrolment-summary.component.scss']
})
export class PharmanetEnrolmentSummaryComponent extends BaseEnrolmentPage implements OnInit {
  public enrolment: Enrolment;
  public emailForm: FormGroup;

  public CareSettingEnum = CareSettingEnum;
  public EnrolmentStatus = EnrolmentStatusEnum;
  public Role = Role;

  public showCommunityHealth: boolean;
  public showPharmacist: boolean;
  public showHealthAuthority: boolean;
  public showDeviceProvider: boolean;
  public currentAgreementGroup: AgreementTypeGroup;

  public initialEnrolment: boolean;
  public complete: boolean;

  public careSettingConfigs: {
    setting: string,
    settingPlural: string,
    settingCode: number,
    formArray: FormArray,
    formArrayName: string,
    subheaderContent: string;
  }[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private fb: FormBuilder,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private dialog: MatDialog,
    private toastService: ToastService,
  ) {
    super(route, router);
    this.showCommunityHealth = true;
    this.showPharmacist = true;
    this.showHealthAuthority = true;
    this.showDeviceProvider = true;

    this.careSettingConfigs = [];
    this.complete = true;
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

  public get communityHealthEmails(): FormArray {
    return this.emailForm.get('communityHealthEmails') as FormArray;
  }

  public get pharmacistEmails(): FormArray {
    return this.emailForm.get('pharmacistEmails') as FormArray;
  }

  public get healthAuthorityEmails(): FormArray {
    return this.emailForm.get('healthAuthorityEmails') as FormArray;
  }

  public get deviceProviderEmails(): FormArray {
    return this.emailForm.get('deviceProviderEmails') as FormArray;
  }

  public get GPID(): string {
    return this.enrollee.gpid;
  }

  public onCopy() {
    this.toastService.openSuccessToast('Your GPID has been copied to clipboard');
  }

  public setShowEmail(careSettingCode: number, show: boolean, formControl: FormControl = null) {
    if (formControl) {
      formControl.reset();
    }
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
      case this.CareSettingEnum.DEVICE_PROVIDER: {
        this.showDeviceProvider = show;
        break;
      }
    }
  }

  public getTokenUrl(tokenId: string): string {
    return `${this.config.loginRedirectUrl}/provisioner-access/${tokenId}`;
  }

  public sendProvisionerAccessLinkTo() {
    let valid = true;
    this.careSettingConfigs.forEach((config) => {
      valid = valid && config.formArray.valid;
      if (!config.formArray.valid) {
        // trigger UI vaildation warning
        config.formArray.markAllAsTouched();
      }
    });

    if (valid) {
      this.sendProvisionerAccessLink();
    }
  }

  public sendProvisionerAccessLink() {
    const data: DialogOptions = {
      title: 'Confirm Email',
      message: `Are you sure you want to send your Approval Notification?`,
      actionText: 'Send',
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) => {
          if (result) {
            let emailPairs = this.careSettingConfigs.map((config) => {
              return {
                emails: config.formArray.value.map(email => email.email),
                careSettingCode: config.settingCode,
              };
            })

            return this.enrolmentResource.sendProvisionerAccessLink(emailPairs, this.enrolment.id);
          } else {
            return EMPTY;
          }
        })
      )
      .subscribe(() => {
        let emails = new Array<string>();
        this.careSettingConfigs.forEach((config) => {
          emails.push(config.formArray.value.map(email => email.email));
        });
        this.toastService.openSuccessToast(`Email was successfully sent to ${emails.join(", ")}`);
        this.emailForm.reset();
      });
  }

  //No long in used at the moment.
  public openQR(event: Event): void {
    event.preventDefault();

    this.enrolmentResource.getQrCode(this.enrolment.id)
      .subscribe((qrCode: string) => {
        var data = qrCode
          ? { base64Image: qrCode }
          : null;
        var message = qrCode
          ? 'Scan this QR code to receive an invitation to your verifiable credential that can be stored in your digital wallet.'
          : 'No credential invitation found.';

        const options: DialogOptions = {
          title: 'Verified Credential',
          message,
          actionHide: true,
          cancelText: 'Close',
          data,
          component: ImageComponent
        };

        this.busy = this.dialog.open(ConfirmDialogComponent, { data: options })
          .afterClosed()
          .subscribe();
      });
  }

  public getTitle() {
    if (!this.initialEnrolment) {
      return 'Share my Global PharmaNet ID (GPID)';
    } else if (this.complete) {
      return 'PRIME Enrolment Complete';
    }
    return 'Next Steps to Get PharmaNet';
  }

  public getAgreementDescription() {
    switch (this.currentAgreementGroup) {
      case AgreementTypeGroup.ON_BEHALF_OF:
        return 'You are an on behalf of user';
      case AgreementTypeGroup.REGULATED_USER:
        return 'You are an independent user';
      default:
        return '';
    }
  }

  public getEmailsGroup(careSettingCode: number): FormArray {
    let formArray: FormArray;

    switch (careSettingCode) {
      case this.CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        formArray = this.communityHealthEmails;
        break;
      }
      case this.CareSettingEnum.COMMUNITY_PHARMACIST: {
        formArray = this.pharmacistEmails;
        break;
      }
      case this.CareSettingEnum.HEALTH_AUTHORITY: {
        formArray = this.healthAuthorityEmails;
        break;
      }
      case this.CareSettingEnum.DEVICE_PROVIDER: {
        formArray = this.deviceProviderEmails;
        break;
      }
    }
    return formArray;
  }

  public addEmptyEmailInput(settingCode: number) {
    let emailsArray = this.getEmailsGroup(settingCode);
    this.addEmail(emailsArray);
  }

  public removeEmail(settingCode: number, index: number): void {
    let emailsArray = this.getEmailsGroup(settingCode);
    emailsArray.removeAt(index);
  }

  public ngOnInit(): void {
    this.enrolment = this.enrolmentService.enrolment;
    this.createFormInstance();
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    this.initialEnrolment = this.route.snapshot.queryParams?.initialEnrolment === 'true';

    this.enrolmentResource.getCurrentAgreementGroupForAnEnrollee(this.enrolment.id)
      .subscribe((group: AgreementTypeGroup) => this.currentAgreementGroup = group)

    this.careSettingConfigs = this.careSettings.map(careSetting => {
      switch (careSetting.careSettingCode) {
        case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
          return {
            setting: 'Private Community Health Practice',
            settingPlural: 'Private Community Health Practices',
            settingCode: careSetting.careSettingCode,
            formArray: this.communityHealthEmails,
            formArrayName: 'communityHealthEmails',
            subheaderContent: `Send your approval to your private community health practice's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case CareSettingEnum.COMMUNITY_PHARMACIST: {
          return {
            setting: 'Community Pharmacy',
            settingPlural: 'Community Pharmacies',
            settingCode: careSetting.careSettingCode,
            formArray: this.pharmacistEmails,
            formArrayName: 'pharmacistEmails',
            subheaderContent: `Send your approval to your community pharmacy's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case CareSettingEnum.HEALTH_AUTHORITY: {
          return {
            setting: 'Health Authority',
            settingPlural: 'Health Authorities',
            settingCode: careSetting.careSettingCode,
            formArray: this.healthAuthorityEmails,
            formArrayName: 'healthAuthorityEmails',
            subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case CareSettingEnum.DEVICE_PROVIDER: {
          return {
            setting: 'Device Provider',
            settingPlural: 'Device Providers',
            settingCode: careSetting.careSettingCode,
            formArray: this.deviceProviderEmails,
            formArrayName: 'deviceProviderEmails',
            subheaderContent: `Send your approval to your device provider's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
      }
    });
  }

  protected addEmail(emailsArray: FormArray, email?: string): void {
    const emailForm = this.fb.group({
      email: ['', []]
    });
    emailForm.patchValue({ email });
    emailsArray.push(emailForm);
  }

  protected createFormInstance(): void {
    this.emailForm = this.buildEmailGroup();
    this.initForm();
  }

  protected initForm() {
    if (!this.communityHealthEmails.length) {
      this.addEmail(this.communityHealthEmails);
      this.addEmail(this.pharmacistEmails);
      this.addEmail(this.healthAuthorityEmails);
      this.addEmail(this.deviceProviderEmails);
    }
  }

  private buildEmailGroup(): FormGroup {
    return this.fb.group({
      communityHealthEmails: this.fb.array([], [Validators.required]),
      pharmacistEmails: this.fb.array([], [Validators.required]),
      healthAuthorityEmails: this.fb.array([], [Validators.required]),
      deviceProviderEmails: this.fb.array([], [Validators.required]),
    });
  }
}
