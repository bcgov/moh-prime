import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormGroup, UntypedFormControl, UntypedFormBuilder, UntypedFormArray } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { Config } from '@config/config.model';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { ImageComponent } from '@shared/components/dialogs/content/image/image.component';
import { Role } from '@auth/shared/enum/role.enum';
import { ConfigService } from '@config/config.service';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { Site } from '@registration/shared/models/site.model';

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
  public emailForm: UntypedFormGroup;

  public CareSettingEnum = CareSettingEnum;
  public HealthAuthorityEnum = HealthAuthorityEnum;
  public EnrolmentStatus = EnrolmentStatusEnum;
  public Role = Role;

  public showCommunityHealth: boolean;
  public showPharmacist: boolean;
  public showHealthAuthority: boolean;
  public showDeviceProvider: boolean;
  public currentAgreementGroup: AgreementTypeGroup;

  public initialEnrolment: boolean;
  public isNextStep: boolean;
  public isEnrolmentComplete: boolean;
  public complete: boolean;
  public healthAuthorities: Config<number>[];
  public remoteAccessSites: RemoteAccessSite[] = [];

  public careSettingConfigs: {
    setting: string,
    settingPlural: string,
    settingCode: number,
    healthAuthorityCode: number,
    formArray: UntypedFormArray,
    formArrayName: string,
    subheaderContent: string;
  }[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private fb: UntypedFormBuilder,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private dialog: MatDialog,
    private toastService: ToastService,
    private configService: ConfigService,
  ) {
    super(route, router);
    this.showCommunityHealth = true;
    this.showPharmacist = true;
    this.showHealthAuthority = true;
    this.showDeviceProvider = true;

    this.careSettingConfigs = [];
    this.complete = true;
    this.healthAuthorities = this.configService.healthAuthorities;
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

  public get communityHealthEmails(): UntypedFormArray {
    return this.emailForm.get('communityHealthEmails') as UntypedFormArray;
  }

  public get pharmacistEmails(): UntypedFormArray {
    return this.emailForm.get('pharmacistEmails') as UntypedFormArray;
  }

  public get healthAuthorityFraserEmails(): UntypedFormArray {
    return this.emailForm.get('healthAuthorityFraserEmails') as UntypedFormArray;
  }

  public get healthAuthorityNorthernEmails(): UntypedFormArray {
    return this.emailForm.get('healthAuthorityNorthernEmails') as UntypedFormArray;
  }

  public get healthAuthorityIslandEmails(): UntypedFormArray {
    return this.emailForm.get('healthAuthorityIslandEmails') as UntypedFormArray;
  }

  public get healthAuthorityInteriorEmails(): UntypedFormArray {
    return this.emailForm.get('healthAuthorityInteriorEmails') as UntypedFormArray;
  }

  public get healthAuthorityPHSAEmails(): UntypedFormArray {
    return this.emailForm.get('healthAuthorityPHSAEmails') as UntypedFormArray;
  }

  public get healthAuthorityVancouverCoastalEmails(): UntypedFormArray {
    return this.emailForm.get('healthAuthorityVancouverCoastalEmails') as UntypedFormArray;
  }

  public get deviceProviderEmails(): UntypedFormArray {
    return this.emailForm.get('deviceProviderEmails') as UntypedFormArray;
  }

  public get GPID(): string {
    return this.enrollee.gpid;
  }

  public onCopy() {
    this.toastService.openSuccessToast('Your GPID has been copied to clipboard');
  }

  public setShowEmail(careSettingCode: number, show: boolean, formControl: UntypedFormControl = null) {
    if (formControl) {
      formControl.reset();
    }
    switch (careSettingCode) {
      case this.CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        this.showCommunityHealth = show;
        break;
      }
      case this.CareSettingEnum.COMMUNITY_PHARMACY: {
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

    if (!this.atLeastOneEmailFilled()) {
      const data: DialogOptions = {
        title: 'Missing Email',
        message: `Please enter at least one email for the Approval Notification.`,
        cancelText: 'Close',
        actionType: 'warn',
        actionHide: true,
      };
      this.dialog.open(ConfirmDialogComponent, { data }).afterClosed();

    } else {
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
                  emails: config.formArray.value.map(email => {
                    return config.settingCode === this.CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE
                      ? {
                        email: email.email,
                        remoteAccessSiteIds: email.sitesIds.map((selected: boolean, index: string | number) =>
                          selected ? this.remoteAccessSites[index].site.id : null).filter(id => id)
                      } : { email: email.email, remoteAccessSiteIds: [] };
                  }),
                  careSettingCode: config.settingCode,
                  healthAuthorityCode: config.healthAuthorityCode,
                }
              });
              return this.enrolmentResource.sendProvisionerAccessLink(emailPairs.filter((ep) => ep.emails && ep.emails[0].email !== undefined && ep.emails[0].email !== null), this.enrolment.id);
            } else {
              return EMPTY;
            }
          })
        )
        .subscribe(() => {
          let emails = new Array<string>();
          this.careSettingConfigs.forEach((config) => {
            if (config.formArray.value.map(email => email.email).filter(e => e)[0]) {
              emails.push(config.formArray.value.map(email => email.email).filter(e => e));
            }
          });
          this.toastService.openSuccessToast(`Email was successfully sent to ${emails.join(", ")}`);
          this.emailForm.reset();
          this.router.navigate([this.EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY],
            { relativeTo: this.route.parent, queryParams: { initialEnrolment: this.initialEnrolment, complete: 'true' } });
        });
    }
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
    if (this.isNextStep) {
      return 'Next Steps to Get PharmaNet';
    } else {
      if (!this.initialEnrolment) {
        return 'Share my PRIME approval and provisioning details';
      } else {
        return 'PRIME Enrolment Complete';
      }
    }
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

  public getEmailsGroup(careSettingCode: number, healthAuthorityCode: number): UntypedFormArray {
    let formArray: UntypedFormArray;

    switch (careSettingCode) {
      case this.CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        formArray = this.communityHealthEmails;
        break;
      }
      case this.CareSettingEnum.COMMUNITY_PHARMACY: {
        formArray = this.pharmacistEmails;
        break;
      }
      case this.CareSettingEnum.HEALTH_AUTHORITY: {
        switch (healthAuthorityCode) {
          case this.HealthAuthorityEnum.FRASER_HEALTH: {
            formArray = this.healthAuthorityFraserEmails;
            break;
          }
          case this.HealthAuthorityEnum.INTERIOR_HEALTH: {
            formArray = this.healthAuthorityInteriorEmails;
            break;
          }
          case this.HealthAuthorityEnum.ISLAND_HEALTH: {
            formArray = this.healthAuthorityIslandEmails;
            break;
          }
          case this.HealthAuthorityEnum.PROVINCIAL_HEALTH_SERVICES_AUTHORITY: {
            formArray = this.healthAuthorityPHSAEmails;
            break;
          }
          case this.HealthAuthorityEnum.NORTHERN_HEALTH: {
            formArray = this.healthAuthorityNorthernEmails;
            break;
          }
          case this.HealthAuthorityEnum.VANCOUVER_COASTAL_HEALTH: {
            formArray = this.healthAuthorityVancouverCoastalEmails;
            break;
          }
        }
        break;
      }
      case this.CareSettingEnum.DEVICE_PROVIDER: {
        formArray = this.deviceProviderEmails;
        break;
      }
    }
    return formArray;
  }

  public addEmptyEmailInput(settingCode: number, healthAuthorityCode: number) {
    let emailsArray = this.getEmailsGroup(settingCode, healthAuthorityCode);
    this.addEmail(emailsArray, null, settingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE);
  }

  public removeEmail(settingCode: number, healthAuthorityCode: number, index: number): void {
    let emailsArray = this.getEmailsGroup(settingCode, healthAuthorityCode);
    emailsArray.removeAt(index);
  }

  public ngOnInit(): void {
    this.enrolment = this.enrolmentService.enrolment;
    this.remoteAccessSites = this.enrolment.remoteAccessSites ?? [];
    this.createFormInstance();
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    this.initialEnrolment = this.route.snapshot.queryParams?.initialEnrolment === 'true';
    this.isNextStep = this.route.snapshot.url.some(v => v.path === "next-steps");
    this.isEnrolmentComplete = this.route.snapshot.queryParams?.complete === 'true';

    this.enrolmentResource.getCurrentAgreementGroupForAnEnrollee(this.enrolment.id)
      .subscribe((group: AgreementTypeGroup) => this.currentAgreementGroup = group)

    this.careSettingConfigs = [];
    this.careSettings.forEach((careSetting) => {
      switch (careSetting.careSettingCode) {
        case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
          this.careSettingConfigs.push(
            {
              setting: 'Private Community Health Practice',
              settingPlural: 'Private Community Health Practices',
              settingCode: careSetting.careSettingCode,
              healthAuthorityCode: null,
              formArray: this.communityHealthEmails,
              formArrayName: 'communityHealthEmails',
              subheaderContent: `Enter the email address of your clinic’s PharmaNet administrator below. If you work in more than one private community health practice, you will need to send the email separately to each PharmaNet administrator on a separate line. Either your PharmaNet administrator(s), or your PharmaNet software vendor, will contact you once your PharmaNet access has been set up.`
            });
        }
          break;
        case CareSettingEnum.COMMUNITY_PHARMACY: {
          this.careSettingConfigs.push({
            setting: 'Community Pharmacy',
            settingPlural: 'Community Pharmacies',
            settingCode: careSetting.careSettingCode,
            healthAuthorityCode: null,
            formArray: this.pharmacistEmails,
            formArrayName: 'pharmacistEmails',
            subheaderContent: `Send your approval to your community pharmacy's PharmaNet administrator (e.g. pharmacy manager or software vendor). If you work in more than one community pharmacy, make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          });
        }
          break;
        case CareSettingEnum.HEALTH_AUTHORITY: {
          this.enrolment.enrolleeHealthAuthorities.forEach((eha) => {
            this.careSettingConfigs.push({
              setting: `Health Authority - ${this.healthAuthorities.find((ha) => ha.code === +eha.healthAuthorityCode).name}`,
              settingPlural: 'Health Authorities',
              settingCode: careSetting.careSettingCode,
              healthAuthorityCode: eha.healthAuthorityCode,
              formArray: this.getEmailsGroup(careSetting.careSettingCode, eha.healthAuthorityCode),
              formArrayName: this.getHAFormArrayName(eha.healthAuthorityCode),
              subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. PharmaNet support, general IT department, or PharmaNet software vendor). If you work in more than one clinic/facility make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
            });
          });
        }
          break;
        case CareSettingEnum.DEVICE_PROVIDER: {
          this.careSettingConfigs.push({
            setting: 'Device Provider',
            settingPlural: 'Device Providers',
            settingCode: careSetting.careSettingCode,
            healthAuthorityCode: null,
            formArray: this.deviceProviderEmails,
            formArrayName: 'deviceProviderEmails',
            subheaderContent: `Send your approval to your device provider's PharmaNet administrator (e.g. office manager or PharmaNet software vendor). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          });
        }
          break;
      }
    });
  }

  protected getHAFormArrayName(healthAuthorityCode: number): string {
    switch (healthAuthorityCode) {
      case HealthAuthorityEnum.FRASER_HEALTH:
        return "healthAuthorityFraserEmails";
      case HealthAuthorityEnum.INTERIOR_HEALTH:
        return "healthAuthorityInteriorEmails";
      case HealthAuthorityEnum.ISLAND_HEALTH:
        return "healthAuthorityIslandEmails";
      case HealthAuthorityEnum.NORTHERN_HEALTH:
        return "healthAuthorityNorthernEmails";
      case HealthAuthorityEnum.PROVINCIAL_HEALTH_SERVICES_AUTHORITY:
        return "healthAuthorityPHSAEmails";
      case HealthAuthorityEnum.VANCOUVER_COASTAL_HEALTH:
        return "healthAuthorityVancouverCoastalEmails";
    }
    return "";
  }

  protected getRemoteAccessSiteControls(index: number): UntypedFormArray {
    const remoteAccessSiteGroup = this.communityHealthEmails.at(index) as UntypedFormGroup;
    return remoteAccessSiteGroup.get('sitesIds') as UntypedFormArray;
  }

  protected getRemoteAccessSite(index: number): Site {
    return this.remoteAccessSites[index].site;
  }

  protected addEmail(emailsArray: UntypedFormArray, email?: string, withSiteIds?: boolean): void {

    const emailForm = withSiteIds ? this.fb.group({
      email: ['', []],
      sitesIds: this.fb.array(this.remoteAccessSites.map(() => this.fb.control(false)), [])
    }) : this.fb.group({
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
      this.addEmail(this.communityHealthEmails, null, true);
      this.addEmail(this.pharmacistEmails);
      this.addEmail(this.healthAuthorityFraserEmails);
      this.addEmail(this.healthAuthorityInteriorEmails);
      this.addEmail(this.healthAuthorityIslandEmails);
      this.addEmail(this.healthAuthorityNorthernEmails);
      this.addEmail(this.healthAuthorityPHSAEmails);
      this.addEmail(this.healthAuthorityVancouverCoastalEmails);
      this.addEmail(this.deviceProviderEmails);
    }
  }

  private buildEmailGroup(): UntypedFormGroup {
    return this.fb.group({
      communityHealthEmails: this.fb.array([], []),
      pharmacistEmails: this.fb.array([], []),
      healthAuthorityFraserEmails: this.fb.array([], []),
      healthAuthorityInteriorEmails: this.fb.array([], []),
      healthAuthorityIslandEmails: this.fb.array([], []),
      healthAuthorityNorthernEmails: this.fb.array([], []),
      healthAuthorityPHSAEmails: this.fb.array([], []),
      healthAuthorityVancouverCoastalEmails: this.fb.array([], []),
      deviceProviderEmails: this.fb.array([], []),
    });
  }

  public atLeastOneEmailFilled(): boolean {
    let emailFilled = false;

    Object.keys(this.emailForm.controls).forEach((emailArrayKey) => {
      const emailArray = this.emailForm.controls[emailArrayKey] as UntypedFormArray;
      Object.keys(emailArray.controls).forEach((emailKey) => {
        let emailControl = emailArray.controls[emailKey] as UntypedFormGroup;
        if (emailControl.controls['email'].value && emailControl.controls['email'].value !== "") {
          emailFilled = true;
        }
      });
    });

    return emailFilled;
  }

  public getEmailLabel(emailIndex: number): string {
    return emailIndex === 0 ? 'Email' : `Email #${emailIndex + 1}`;
  }
}
