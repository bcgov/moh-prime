import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';
import { ConfigModule } from '@config/config.module';
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
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';
@Component({
  selector: 'app-next-steps',
  templateUrl: './next-steps.component.html',
  styleUrls: ['./next-steps.component.scss']
})
export class NextStepsComponent extends BaseEnrolmentProfilePage implements OnInit {
  //public vendorForm: FormGroup;
  //public formState: NextStepsFormState;
  public title: string;
  public enrolment: Enrolment;
  public hasReadAgreement: boolean;

  public CareSettingEnum = CareSettingEnum;
  public EnrolmentStatus = EnrolmentStatusEnum;

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
  }

  public get careSettings() {
    return (this.enrolment) ? this.enrolment.careSettings : null;
  }

  public get communityHealthEmails(): FormArray {
    return this.form.get('communityHealthEmails') as FormArray;
  }

  public get pharmacistEmails(): FormArray {
    return this.form.get('pharmacistEmails') as FormArray;
  }

  public get healthAuthorityEmails(): FormArray {
    return this.form.get('healthAuthorityEmails') as FormArray;
  }

  public get deviceProviderEmails(): FormArray {
    return this.form.get('deviceProviderEmails') as FormArray;
  }

  public getAgreementDescription() {
    switch (this.currentAgreementGroup) {
      case AgreementTypeGroup.ON_BEHALF_OF:
        return 'You are an on behalf of user';
      case AgreementTypeGroup.REGULATED_USER:
        return 'You are an independant user';
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

  public GetEmailsGroup(careSettingCode: number) {
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

  public addEmptyEmailInput(settingCode: number) {
    let emailsArray = this.GetEmailsGroup(settingCode);
    this.addEmail(emailsArray);
  }

  public removeEmail(settingCode: number, index: number): void {
    let emailsArray = this.GetEmailsGroup(settingCode);
    emailsArray.removeAt(index);
  }

  protected initForm() {
    if (!this.communityHealthEmails.length) {
      this.addEmail(this.communityHealthEmails);
      this.addEmail(this.pharmacistEmails);
      this.addEmail(this.healthAuthorityEmails);
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

  protected addEmail(emailsGroup: FormArray, email?: string): void {
    const emailForm = this.fb.group({
      email: ['', []]
    });
    emailForm.patchValue({ email });
    emailsGroup.push(emailForm);
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
