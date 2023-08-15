import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable, concat, EMPTY, pipe } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { DeviceProviderRoleConfig } from '@config/config.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { DeviceProviderSite } from '@shared/models/device-provider-site.model';

import { RegulatoryFormState } from './regulatory-form-state';
import { ConfigService } from '@config/config.service';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public formState: RegulatoryFormState;
  public cannotRequestRemoteAccess: boolean;
  public isDeviceProvider: boolean;
  public hasOtherCareSetting: boolean;
  public deviceProviderRoles: DeviceProviderRoleConfig[];
  public deviceProviderSite: DeviceProviderSite;
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

    this.cannotRequestRemoteAccess = false;
    this.deviceProviderRoles = this.configService.deviceProviderRoles;
  }

  public get selectedCollegeCodes(): number[] {
    return this.formState.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
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

  public ngOnInit() {
    this.isDeviceProvider = this.enrolmentService.enrolment.careSettings.some((careSetting) =>
      careSetting.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);
    this.hasOtherCareSetting = this.enrolmentService.enrolment.careSettings.some((careSetting) =>
      careSetting.careSettingCode !== CareSettingEnum.DEVICE_PROVIDER);
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  public ngOnDestroy() {
    this.removeIncompleteCertifications(true);
  }

  protected createFormInstance() {
    this.formState = this.enrolmentFormStateService.regulatoryFormState;
    this.form = this.formState.form;
  }

  protected initForm() {
    this.setupDeviceProvider();

    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.formState.certifications.length) {
      this.addEmptyCollegeCertification();
    }

    const initialRemoteAccess = this.canRequestRemoteAccess();

    if (this.formState.deviceProviderId.value) {
      this.enrolmentResource.getDeviceProviderSite(this.formState.deviceProviderId.value)
        .subscribe((site) => this.deviceProviderSite = site);
    }

    this.formState.form.valueChanges
      .pipe(map((_) => initialRemoteAccess && !this.isInitialEnrolment))
      .subscribe((couldRequestRemoteAccess: boolean) =>
        this.cannotRequestRemoteAccess = couldRequestRemoteAccess && !this.canRequestRemoteAccess()
      );

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
    const { certifications, enrolleeDeviceProviders } = this.enrolmentService.enrolment;
    this.formState.patchValue({ certifications, enrolleeDeviceProviders });
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {

      if (this.isDeviceProvider && this.formState.deviceProviderId.value) {
        let siteName = this.deviceProviderSite ?
          this.deviceProviderSite.siteName : "Site not found.";
        let siteAddress = this.deviceProviderSite ?
          `${this.deviceProviderSite.siteAddress}, ${this.deviceProviderSite.city}, ${this.deviceProviderSite.prov}` : "";
        let message = this.deviceProviderSite ?
          "Is this the correct location where you are the device provider?" : "If you are not sure the Device Provider ID, you might continue."
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
              this.removeOboSites();
              // Remove remote access data when enrollee is no longer eligible, e.g., licence type changes
              if (this.cannotRequestRemoteAccess) {
                this.removeRemoteAccessData();
              }
              super.handleSubmission();
            }
          });
      } else {
        super.handleSubmission();
      }
    } else {
      this.utilService.scrollToErrorSection();
    }
  }

  protected afterSubmitIsSuccessful() {
    this.removeIncompleteCertifications(true);
  }

  protected nextRouteAfterSubmit() {
    const certifications = this.formState.collegeCertifications;
    const careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value as CareSetting[];

    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = (!certifications.length && (!this.isDeviceProvider || !this.formState.certificationNumber.value))
        ? EnrolmentRoutes.OBO_SITES
        : (this.enrolmentService.canRequestRemoteAccess(certifications, careSettings))
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

  /**
   * @description
   * Remove obo sites from the enrolment as enrollees can not have
   * certificate(s), as well as, OBO site(s).
   */
  private removeOboSites() {
    this.removeIncompleteCertifications(true);

    if (this.formState.certifications.length) {
      const form = this.enrolmentFormStateService.oboSitesForm;
      const oboSites = form.get('oboSites') as FormArray;
      oboSites.clear();
    }
  }

  private canRequestRemoteAccess(): boolean {
    const certifications = this.enrolmentFormStateService.regulatoryFormState.collegeCertifications;
    const careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value;

    return this.enrolmentService
      .canRequestRemoteAccess(certifications, careSettings);
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
}
