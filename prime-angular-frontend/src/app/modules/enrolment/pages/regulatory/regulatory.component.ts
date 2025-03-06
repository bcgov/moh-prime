import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY } from 'rxjs';
import { exhaustMap, take } from 'rxjs/operators';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { DeviceProviderRoleConfig, LicenseConfig } from '@config/config.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { DeviceProviderSite } from '@shared/models/device-provider-site.model';

import { RegulatoryFormState } from './regulatory-form-state';
import { ConfigService } from '@config/config.service';
import { ToggleContentChange } from '@shared/components/toggle-content/toggle-content.component';
import { SiteResource } from '@core/resources/site-resource.service';
import { CertSearch } from '@enrolment/shared/models/cert-search.model';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public formState: RegulatoryFormState;
  public hasMatchingRemoteUser: boolean;
  public isDeviceProvider: boolean;
  public hasOtherCareSetting: boolean;
  public deviceProviderRoles: DeviceProviderRoleConfig[];
  public deviceProviderSite: DeviceProviderSite;
  public hasUnlistedCertification: boolean;
  public unlistedCertificationRequired: boolean;
  public disableUnlistedCertificationToggle: boolean;
  public multijurisdictionalLicences: LicenseConfig[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService,
    protected siteResource: SiteResource,
    protected configService: ConfigService
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

    this.hasUnlistedCertification = false;
    this.hasMatchingRemoteUser = false;
    this.disableUnlistedCertificationToggle = false;
    this.deviceProviderRoles = this.configService.deviceProviderRoles;
  }

  public get selectedCollegeCodes(): number[] {
    return this.formState.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  public get selectedLicenseCodes(): number[] {
    return this.formState.certifications.value
      .map((certification: CollegeCertification) => +certification.licenseCode);
  }

  public addEmptyCollegeCertification() {
    this.formState.addCollegeCertification();
  }

  /**
   * @description
   * Removes a certification from the list in response to an
   * emitted event from college certifications. Does not allow
   * the list of certifications to empty.
   *
   * @param index to be removed
   */
  public removeCertification(index: number) {
    this.formState.certifications.removeAt(index);
  }

  public licenceCodeSelected(code: number) {
    if (this.multijurisdictionalLicences.some(l => l.code === code)) {
      this.disableUnlistedCertificationToggle = true;
      this.hasUnlistedCertification = true;
      if (!this.formState.unlistedCertifications.length) {
        this.formState.addEmptyUnlistedCollegeCertification();
      }
    } else {
      this.disableUnlistedCertificationToggle = false;
    }
  }

  public ngOnInit() {
    this.isDeviceProvider = this.enrolmentService.enrolment.careSettings.some((careSetting) =>
      careSetting.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);
    this.hasOtherCareSetting = this.enrolmentService.enrolment.careSettings.some((careSetting) =>
      careSetting.careSettingCode !== CareSettingEnum.DEVICE_PROVIDER);
    this.createFormInstance();
    this.patchForm().subscribe(() => {
      this.initForm();
    });
    this.checkRemoteAccess();
    this.multijurisdictionalLicences = this.configService.licenses.filter(l => l.multijurisdictional);
  }

  public ngOnDestroy() {
    this.removeIncompleteCertifications(true);
    this.removeIncompleteUnlistedCertification();
  }

  protected createFormInstance() {
    this.formState = this.enrolmentFormStateService.regulatoryFormState;
    this.form = this.formState.form;
  }

  protected initForm() {

    if (this.formState.unlistedCertifications.length) {
      this.hasUnlistedCertification = true;
    }
    this.setupDeviceProvider();

    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.formState.certifications.length) {
      this.addEmptyCollegeCertification();
    }

    if (this.formState.deviceProviderId.value) {
      this.enrolmentResource.getDeviceProviderSite(this.formState.deviceProviderId.value)
        .subscribe((site) => this.deviceProviderSite = site);
    }

    this.formState.deviceProviderRoleCode.valueChanges.subscribe(() =>
      this.toggleCertificationNumberValidation()
    );

    this.formState.deviceProviderId.valueChanges
      .pipe(exhaustMap(value => {
        if (value && value.length === 8) {
          return this.enrolmentResource.getDeviceProviderSite(value);
        }
        return EMPTY;
      }
      ))
      .subscribe(site =>
        this.deviceProviderSite = site
      );

    // Check if there is validation error, mark as touched to show the error message
    this.formState.certifications.controls.forEach((c: FormGroup) => {
      Object.keys(c.controls).forEach(key => {
        if (c.get(key).errors) {
          c.get(key).markAsTouched();
        }
      });
    });
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Replace previous values on deactivation so updates are discarded
    const { certifications, enrolleeDeviceProviders, unlistedCertifications } = this.enrolmentService.enrolment;
    this.formState.patchValue({ certifications, enrolleeDeviceProviders, unlistedCertifications });
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.checkRemoteAccess();

      if (this.isDeviceProvider && this.formState.deviceProviderId.value) {
        let siteName = this.deviceProviderSite ?
          this.deviceProviderSite.siteName : "Site not found.";
        let siteAddress = this.deviceProviderSite ?
          `${this.deviceProviderSite.siteAddress}, ${this.deviceProviderSite.city}, ${this.deviceProviderSite.prov}` : "";
        let message = this.deviceProviderSite ?
          "Is this the correct location where you are the device provider?" : "If you are not sure of the Device Provider ID, you may continue."
        let actionText = this.deviceProviderSite ?
          "Agree" : "Continue";

        const data: DialogOptions = {
          title: 'Device Provider Site',
          boldMessage: siteName,
          message: siteAddress,
          message2: message,
          actionText: actionText
        };

        this.busy = this.dialog.open(ConfirmDialogComponent, { data })
          .afterClosed()
          .subscribe((result: boolean) => {
            if (result) {
              // Enrollees can not have certifications and jobs
              this.removeCertificationsAndOboSites();
              // Remove remote access data when enrollee is no longer eligible, e.g., licence type changes
              if (!this.hasMatchingRemoteUser) {
                this.removeRemoteAccessData();
              }
              super.handleSubmission();
            }
          });
      } else {
        if (this.formState.certifications.value.some(c => c.collegeCode !== '')) {
          this.enrolmentFormStateService.patchOboSitesForm(null);
        }
        if (!this.hasMatchingRemoteUser) {
          this.removeRemoteAccessData();
        }

        super.handleSubmission();
      }
    } else {
      this.utilService.scrollToErrorSection();
    }
  }

  protected afterSubmitIsSuccessful() {
    this.removeIncompleteCertifications(true);
    this.removeIncompleteUnlistedCertification();
  }

  protected nextRouteAfterSubmit() {
    const certifications = this.formState.collegeCertifications;

    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      // If DP Role Code is "None", we go to Job Site page
      nextRoutePath = (!certifications.length || (this.isDeviceProvider && this.formState.deviceProviderRoleCode.value === 15))
        ? EnrolmentRoutes.OBO_SITES
        : (this.hasMatchingRemoteUser)
          ? EnrolmentRoutes.REMOTE_ACCESS
          : EnrolmentRoutes.SELF_DECLARATION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  private removeIncompleteCertifications(noEmptyCert: boolean = false) {
    this.formState.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeCode').value || (control.invalid && !this.enrolmentService.isProfileComplete)) {
          this.removeCertification(index);
        }
      });

    // Always have a single certification available, and it prevents
    // the page from jumping too much when routing
    if (!noEmptyCert && !this.formState.certifications.controls.length) {
      this.addEmptyCollegeCertification();
    }
  }

  private removeIncompleteUnlistedCertification() {
    this.formState.unlistedCertifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeName').value || (control.invalid && !this.enrolmentService.isProfileComplete)) {
          this.formState.unlistedCertifications.removeAt(index);
        }
      });
  }

  /**
   * @description
   * Remove obo sites from the enrolment as enrollees can not have
   * certificate(s), as well as, OBO site(s).
   */
  private removeCertificationsAndOboSites() {
    this.removeIncompleteCertifications(true);

    if (this.formState.certifications.length || (this.isDeviceProvider && this.formState.deviceProviderRoleCode.value !== 15)) {
      const form = this.enrolmentFormStateService.oboSitesForm;
      const oboSites = form.get('oboSites') as FormArray;
      oboSites.clear();
    }
  }

  private async checkRemoteAccess(): Promise<void> {
    const careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value;

    if (!careSettings.some(cs => cs.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE)) {
      this.hasMatchingRemoteUser = false;
    } else {
      const certifications = this.enrolmentFormStateService.regulatoryFormState.collegeCertifications;
      const certSearch: CertSearch[] = certifications
        .map(c => ({
          collegeCode: c.collegeCode,
          licenseCode: c.licenseCode,
          licenceNumber: c.licenseNumber,
          practitionerId: c.practitionerId
        }));

      if (certSearch.length && this.form.valid && certSearch.filter(c => c.licenseCode).length === certSearch.length) {
        var remoteAccessSearch = await this.siteResource.getSitesByRemoteUserInfo(certSearch)
          .pipe(take(1)).toPromise();
        if (remoteAccessSearch.length) {
          this.hasMatchingRemoteUser = true
        } else {
          this.hasMatchingRemoteUser = false
        }
      }
    }
  }

  private removeRemoteAccessData(): void {
    const remoteAccessForm = this.enrolmentFormStateService.remoteAccessForm;
    const remoteAccessSites = remoteAccessForm.get('remoteAccessSites') as FormArray;
    const enrolleeRemoteUsers = remoteAccessForm.get('enrolleeRemoteUsers') as FormArray;
    const remoteLocations = this.enrolmentFormStateService.remoteAccessLocationsForm.get('remoteAccessLocations') as FormArray;
    [remoteAccessSites, enrolleeRemoteUsers, remoteLocations].forEach(f => f.clear());
  }

  private setupDeviceProvider(): void {
    if (this.isDeviceProvider) {
      if (!this.formState.deviceProviderId.value || this.formState.deviceProviderId.value === "") {
        //this.formState.deviceProviderId.setValue("P1-90");
      }
      this.formUtilsService.setValidators(this.formState.deviceProviderId, [
        FormControlValidators.deviceProviderId
      ]);
      this.toggleCertificationNumberValidation();
    } else {
      this.formUtilsService.resetAndClearValidators(this.formState.deviceProviderId);
    }
  }

  private toggleCertificationNumberValidation(): void {
    if (this.formState.deviceProviderRoleCode.value &&
      this.deviceProviderRoles.find(r => r.code === this.formState.deviceProviderRoleCode.value).certified) {
      this.formUtilsService.setValidators(this.formState.certificationNumber, [Validators.required]);
      this.formState.certificationNumber.enable();
    } else {
      this.formUtilsService.resetAndClearValidators(this.formState.certificationNumber);
      this.formState.certificationNumber.markAsUntouched();
      this.formState.certificationNumber.disable();
    }
  }

  public toggleUnlistedCertification({ checked }: ToggleContentChange) {
    if (!checked) {
      this.hasUnlistedCertification = false;
      this.formState.unlistedCertifications.clear();
    } else {
      this.hasUnlistedCertification = true;
      if (!this.formState.unlistedCertifications.length) {
        this.formState.addEmptyUnlistedCollegeCertification();
      }
    }
  }

  public removeUnlistedCertification(index: number): void {
    if (index === 0 && this.disableUnlistedCertificationToggle) {
      // Force enrollee to complete sub-form
      return;
    }

    this.formState.unlistedCertifications.removeAt(index);
    if (!this.formState.unlistedCertifications.length) {
      this.hasUnlistedCertification = false;
    }
  }
}
