import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';
import { ConfigService } from '@config/config.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { ProvisionerCareSettingEnum } from '@shared/enums/provisioner-care-setting.enum';
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
  public hasReadAgreement: boolean;

  public ProvisionerCareSettingEnum = ProvisionerCareSettingEnum;
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

  public careSettingConfigs: {
    setting: string,
    settingPlural: string,
    settingCode: string,
    formArray: FormArray,
    formArrayName: string,
    subheaderContent: string;
  }[];

  public decisions: { code: boolean, name: string }[];

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
    this.title = 'Notify Pharmanet administrator of my PRIME approval';
    this.careSettingConfigs = [];

    this.decisions = [
      { code: false, name: 'No' },
      { code: true, name: 'Yes' }
    ];
  }

  /*
  public get careSettings() {
    return (this.enrolment) ? this.enrolment.careSettings : null;
  }
  */

  public get provisionerCareSetting() {
    var result: String[];

    if (this.enrolment) {
      result = this.enrolment.careSettings
        .filter(c => c.careSettingCode != CareSettingEnum.HEALTH_AUTHORITY)
        .map(c => {
          switch (c.careSettingCode) {
            case CareSettingEnum.COMMUNITY_PHARMACIST:
              return ProvisionerCareSettingEnum.COMMUNITY_PHARMACIST;
            case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
              return ProvisionerCareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;
          }
        });

      if (this.enrolment.careSettings.find(c => c.careSettingCode == CareSettingEnum.HEALTH_AUTHORITY)) {
        this.enrolment.enrolleeHealthAuthorities.forEach(ha => {
          switch (ha.healthAuthorityCode) {
            case HealthAuthorityEnum.FRASER_HEALTH:
              result.push(ProvisionerCareSettingEnum.FRASER_HEALTH_AUTHORITY);
              break;
            case HealthAuthorityEnum.INTERIOR_HEALTH:
              result.push(ProvisionerCareSettingEnum.INTERIOR_HEALTH_AUTHORITY);
              break;
            case HealthAuthorityEnum.ISLAND_HEALTH:
              result.push(ProvisionerCareSettingEnum.VANCOUVER_ISLAND_HEALTH_AUTHORITY);
              break;
            case HealthAuthorityEnum.NORTHERN_HEALTH:
              result.push(ProvisionerCareSettingEnum.NORTHERN_HEALTH_AUTHORITY);
              break;
            case HealthAuthorityEnum.PROVINCIAL_HEALTH_SERVICES_AUTHORITY:
              result.push(ProvisionerCareSettingEnum.PROVINCIAL_HEALTH_SERVICES_AUTHORITY);
              break;
            case HealthAuthorityEnum.VANCOUVER_COASTAL_HEALTH:
              result.push(ProvisionerCareSettingEnum.VANCOUVER_COASTAL_HEALTH_AUTHORITY);
              break;
          }
        });
      }
    }

    return result;
  }

  public get islandHealthEmails(): FormArray {
    return this.form.get('islandHealthEmails') as FormArray;
  }

  public get fraserHealthEmails(): FormArray {
    return this.form.get('fraserHealthEmails') as FormArray;
  }

  public get interiorHealthEmails(): FormArray {
    return this.form.get('interiorHealthEmails') as FormArray;
  }

  public get vancouverCoastalHealthEmails(): FormArray {
    return this.form.get('vancouverCoastalHealthEmails') as FormArray;
  }

  public get northernHealthEmails(): FormArray {
    return this.form.get('northernHealthEmails') as FormArray;
  }

  public get provincialHealthServicesAuthorityEmails(): FormArray {
    return this.form.get('provincialHealthServicesAuthorityEmails') as FormArray;
  }

  public get pharmacistEmails(): FormArray {
    return this.form.get('pharmacistEmails') as FormArray;
  }

  public get communityHealthEmails(): FormArray {
    return this.form.get('communityHealthEmails') as FormArray;
  }

  public get deviceProviderEmails(): FormArray {
    return this.form.get('deviceProviderEmails') as FormArray;
  }

  /*
  public isEmailVisible(provisionerCareSettingCode: number): boolean {
    switch (provisionerCareSettingCode) {
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
  */


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
            this.complete = true;

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
        this.toastService.openSuccessToast('Email was successfully sent');
        this.router.navigate([EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY],
          { relativeTo: this.route.parent, queryParams: { initialEnrolment: this.initialEnrolment } });
      });
    this.onPageChange({ atEnd: true });
  }

  public GetEmailsGroup(settingCode: string) {
    let formArray: FormArray;

    switch (settingCode) {
      case ProvisionerCareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        formArray = this.communityHealthEmails;
        break;
      }
      case ProvisionerCareSettingEnum.COMMUNITY_PHARMACIST: {
        formArray = this.pharmacistEmails;
        break;
      }
      case ProvisionerCareSettingEnum.DEVICE_PROVIDER: {
        formArray = this.deviceProviderEmails;
        break;
      }
      case ProvisionerCareSettingEnum.FRASER_HEALTH_AUTHORITY: {
        formArray = this.fraserHealthEmails;
        break;
      }
      case ProvisionerCareSettingEnum.INTERIOR_HEALTH_AUTHORITY: {
        formArray = this.interiorHealthEmails;
        break;
      }
      case ProvisionerCareSettingEnum.FRASER_HEALTH_AUTHORITY: {
        formArray = this.fraserHealthEmails;
        break;
      }
      case ProvisionerCareSettingEnum.FRASER_HEALTH_AUTHORITY: {
        formArray = this.fraserHealthEmails;
        break;
      }
      case ProvisionerCareSettingEnum.FRASER_HEALTH_AUTHORITY: {
        formArray = this.fraserHealthEmails;
        break;
      }
      case ProvisionerCareSettingEnum.FRASER_HEALTH_AUTHORITY: {
        formArray = this.fraserHealthEmails;
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

    this.careSettingConfigs = this.provisionerCareSetting.map(s => {
      switch (s) {
        case ProvisionerCareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
          return {
            setting: 'Private Community Health Practice',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE,
            formArray: this.communityHealthEmails,
            formArrayName: 'communityHealthEmails',
            subheaderContent: `Send your approval to your private community health practice's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case ProvisionerCareSettingEnum.COMMUNITY_PHARMACIST: {
          return {
            setting: 'Community Pharmacist',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.COMMUNITY_PHARMACIST,
            formArray: this.pharmacistEmails,
            formArrayName: 'pharmacistEmails',
            subheaderContent: `Send your approval to your community pharmacy's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case ProvisionerCareSettingEnum.DEVICE_PROVIDER: {
          return {
            setting: 'Device Provider',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.DEVICE_PROVIDER,
            formArray: this.deviceProviderEmails,
            formArrayName: 'deviceProviderEmails',
            subheaderContent: `Send your approval to your device provider's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case ProvisionerCareSettingEnum.FRASER_HEALTH_AUTHORITY: {
          return {
            setting: 'Fraser Health',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.FRASER_HEALTH_AUTHORITY,
            formArray: this.fraserHealthEmails,
            formArrayName: 'fraserHealthEmails',
            subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case ProvisionerCareSettingEnum.INTERIOR_HEALTH_AUTHORITY: {
          return {
            setting: 'Interior Health',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.INTERIOR_HEALTH_AUTHORITY,
            formArray: this.interiorHealthEmails,
            formArrayName: 'interiorHealthEmails',
            subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case ProvisionerCareSettingEnum.NORTHERN_HEALTH_AUTHORITY: {
          return {
            setting: 'Northen Health',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.NORTHERN_HEALTH_AUTHORITY,
            formArray: this.northernHealthEmails,
            formArrayName: 'northernHealthEmails',
            subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case ProvisionerCareSettingEnum.PROVINCIAL_HEALTH_SERVICES_AUTHORITY: {
          return {
            setting: 'Provincial Health',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.PROVINCIAL_HEALTH_SERVICES_AUTHORITY,
            formArray: this.provincialHealthServicesAuthorityEmails,
            formArrayName: 'provincialHealthServicesAuthorityEmails',
            subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case ProvisionerCareSettingEnum.VANCOUVER_COASTAL_HEALTH_AUTHORITY: {
          return {
            setting: 'Vancouver Coastal Health',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.VANCOUVER_COASTAL_HEALTH_AUTHORITY,
            formArray: this.vancouverCoastalHealthEmails,
            formArrayName: 'vancouverCoastalHealthEmails',
            subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
        case ProvisionerCareSettingEnum.VANCOUVER_ISLAND_HEALTH_AUTHORITY: {
          return {
            setting: 'Vancouver Island Health',
            settingPlural: '',
            settingCode: ProvisionerCareSettingEnum.VANCOUVER_ISLAND_HEALTH_AUTHORITY,
            formArray: this.islandHealthEmails,
            formArrayName: 'islandHealthEmails',
            subheaderContent: `Send your approval to your health authority's PharmaNet administrator (e.g. office manager). If you work in more than one clinic make sure you include every PharmaNet administrator's email. Your PharmaNet administrator(s) will contact you once your PharmaNet access has been set up.`
          };
        }
      }
    });
  }

  public addEmptyEmailInput(settingCode: string) {
    let emailsArray = this.GetEmailsGroup(settingCode);
    this.addEmail(emailsArray);
  }

  public removeEmail(settingCode: string, index: number): void {
    let emailsArray = this.GetEmailsGroup(settingCode);
    emailsArray.removeAt(index);
  }

  protected initForm() {
    if (!this.pharmacistEmails.length) {
      this.addEmail(this.provincialHealthServicesAuthorityEmails);
      this.addEmail(this.islandHealthEmails);
      this.addEmail(this.fraserHealthEmails);
      this.addEmail(this.interiorHealthEmails);
      this.addEmail(this.northernHealthEmails);
      this.addEmail(this.vancouverCoastalHealthEmails);
      this.addEmail(this.pharmacistEmails);
      this.addEmail(this.communityHealthEmails);
      this.addEmail(this.deviceProviderEmails);
    }
  }

  protected createFormInstance(): void {
    this.form = this.buildEmailGroup();
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
      communityHealthEmails: this.fb.array([], [Validators.required]),
      pharmacistEmails: this.fb.array([], [Validators.required]),
      deviceProviderEmails: this.fb.array([], [Validators.required]),
      provincialHealthServicesAuthorityEmails: this.fb.array([], [Validators.required]),
      islandHealthEmails: this.fb.array([], [Validators.required]),
      fraserHealthEmails: this.fb.array([], [Validators.required]),
      interiorHealthEmails: this.fb.array([], [Validators.required]),
      northernHealthEmails: this.fb.array([], [Validators.required]),
      vancouverCoastalHealthEmails: this.fb.array([], [Validators.required]),
    });
  }
}
