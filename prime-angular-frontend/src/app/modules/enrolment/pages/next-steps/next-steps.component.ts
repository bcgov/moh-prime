import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';
import { ConfigService } from '@config/config.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Config } from '@config/config.model';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

/**
 * TODO: https://bcgovmoh.atlassian.net/browse/PRIME-2325 (Refactor common code in both PharmanetEnrolmentSummaryComponent and NextStepsComponent)
 */
@Component({
  selector: 'app-next-steps',
  templateUrl: './next-steps.component.html',
  styleUrls: ['./next-steps.component.scss']
})
export class NextStepsComponent extends BaseEnrolmentProfilePage implements OnInit {
  public title: string;
  public enrolment: Enrolment;
  public emailForm: FormGroup;
  public hasReadAgreement: boolean;

  public CareSettingEnum = CareSettingEnum;
  public EnrolmentStatus = EnrolmentStatusEnum;
  public HealthAuthorityEnum = HealthAuthorityEnum;

  public showCommunityHealth: boolean;
  public showPharmacist: boolean;
  public showHealthAuthority: boolean;
  public showDeviceProvider: boolean;
  public currentAgreementGroup: AgreementTypeGroup;

  public initialEnrolment: boolean;
  public complete: boolean;
  public healthAuthorities: Config<number>[];

  public careSettingConfigs: {
    setting: string,
    settingPlural: string,
    settingCode: number,
    healthAuthorityCode: number,
    formArray: FormArray,
    formArrayName: string,
    subheaderContent: string;
  }[];

  public constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    private changeDetectorRef: ChangeDetectorRef,
    private fb: FormBuilder,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService,
    private configService: ConfigService,
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
      formUtilsService,
      authService
    );
    this.showCommunityHealth = true;
    this.showPharmacist = true;
    this.showHealthAuthority = true;
    this.showDeviceProvider = true;
    this.title = 'Next Steps To Get PharmaNet';
    this.careSettingConfigs = [];
    this.healthAuthorities = this.configService.healthAuthorities;
  }

  public get careSettings() {
    return (this.enrolment) ? this.enrolment.careSettings : null;
  }

  public get communityHealthEmails(): FormArray {
    return this.emailForm.get('communityHealthEmails') as FormArray;
  }

  public get pharmacistEmails(): FormArray {
    return this.emailForm.get('pharmacistEmails') as FormArray;
  }

  public get healthAuthorityFraserEmails(): FormArray {
    return this.emailForm.get('healthAuthorityFraserEmails') as FormArray;
  }

  public get healthAuthorityNorthernEmails(): FormArray {
    return this.emailForm.get('healthAuthorityNorthernEmails') as FormArray;
  }

  public get healthAuthorityIslandEmails(): FormArray {
    return this.emailForm.get('healthAuthorityIslandEmails') as FormArray;
  }

  public get healthAuthorityInteriorEmails(): FormArray {
    return this.emailForm.get('healthAuthorityInteriorEmails') as FormArray;
  }

  public get healthAuthorityPHSAEmails(): FormArray {
    return this.emailForm.get('healthAuthorityPHSAEmails') as FormArray;
  }

  public get healthAuthorityVancouverCoastalEmails(): FormArray {
    return this.emailForm.get('healthAuthorityVancouverCoastalEmails') as FormArray;
  }

  public get deviceProviderEmails(): FormArray {
    return this.emailForm.get('deviceProviderEmails') as FormArray;
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

  public isEmailVisible(careSettingCode: number): boolean {
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
      case this.CareSettingEnum.DEVICE_PROVIDER: {
        return this.showDeviceProvider;
      }
      default: {
        return false;
      }
    }
  }

  public sendProvisionerAccessLink() {

    if (!this.atLeastOneEmailFilled()) {
      const data: DialogOptions = {
        title: 'Missing Email',
        message: `Please enter at least one email for the Approval Notification?`,
        cancelText: 'Close',
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
              this.complete = true;

              let emailPairs = this.careSettingConfigs.map((config) => {
                return {
                  emails: config.formArray.value.map(email => email.email),
                  careSettingCode: config.settingCode,
                  healthAuthorityCode: config.settingCode === CareSettingEnum.HEALTH_AUTHORITY ? config.healthAuthorityCode : null,
                }
              });

              return this.enrolmentResource.sendProvisionerAccessLink(emailPairs, this.enrolment.id);
            } else {
              return EMPTY;
            }
          })
        )
        .subscribe(() => {
          this.toastService.openSuccessToast('Email was successfully sent');
          this.router.navigate([EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY],
            { relativeTo: this.route.parent, queryParams: { initialEnrolment: this.initialEnrolment } });
        });
      this.onPageChange({ atEnd: true });
    }
  }

  public getEmailsGroup(careSettingCode: number, healthAuthorityCode: number) {
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

  public sendProvisionerAccessLinkTo() {
    let valid = true;
    this.careSettingConfigs.forEach((config) => {
      valid = valid && config.formArray.valid;
      if (!config.formArray.valid) {
        config.formArray.markAllAsTouched();
      }
    });

    if (valid) {
      this.sendProvisionerAccessLink();
    }
  }

  public onPageChange(agreement: { atEnd: boolean }) {
    if (agreement.atEnd) {
      this.hasReadAgreement = agreement.atEnd;
      this.changeDetectorRef.detectChanges();
    }
  }

  public ngOnInit(): void {

    this.enrolment = this.enrolmentService.enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    this.initialEnrolment = this.route.snapshot.queryParams?.initialEnrolment === 'true';
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());

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
              subheaderContent: `Send your approval to your private community health practice's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
            });
        }
          break;
        case CareSettingEnum.COMMUNITY_PHARMACIST: {
          this.careSettingConfigs.push({
            setting: 'Community Pharmacy',
            settingPlural: 'Community Pharmacies',
            settingCode: careSetting.careSettingCode,
            healthAuthorityCode: null,
            formArray: this.pharmacistEmails,
            formArrayName: 'pharmacistEmails',
            subheaderContent: `Send your approval to your community pharmacy's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
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
              subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
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
            subheaderContent: `Send your approval to your device provider's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
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

  public addEmptyEmailInput(settingCode: number, healthAuthorityCode: number) {
    let emailsArray = this.getEmailsGroup(settingCode, healthAuthorityCode);
    this.addEmail(emailsArray);
  }

  public removeEmail(settingCode: number, healthAuthorityCode: number, index: number): void {
    let emailsArray = this.getEmailsGroup(settingCode, healthAuthorityCode);
    emailsArray.removeAt(index);
  }

  protected initForm() {
    if (!this.communityHealthEmails.length) {
      this.addEmail(this.communityHealthEmails);
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

  protected createFormInstance(): void {
    this.emailForm = this.buildEmailGroup();
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  protected addEmail(emailsArray: FormArray, email?: string): void {
    const emailForm = this.fb.group({
      email: ['', []]
    });
    emailForm.patchValue({ email });
    emailsArray.push(emailForm);
  }

  private buildEmailGroup(): FormGroup {
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
      const emailArray = this.emailForm.controls[emailArrayKey] as FormArray;
      Object.keys(emailArray.controls).forEach((emailKey) => {
        let emailControl = emailArray.controls[emailKey] as FormGroup;
        if (emailControl.controls['email'].value && emailControl.controls['email'].value !== "") {
          emailFilled = true;
        }
      });
    });

    return emailFilled;
  }
}
