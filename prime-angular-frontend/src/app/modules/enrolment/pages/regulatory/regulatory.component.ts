import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { RegulatoryFormState } from './regulatory-form-state';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public formState: RegulatoryFormState;
  public cannotRequestRemoteAccess: boolean;
  public isDeviceProvider: boolean;

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
    protected authService: AuthService
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
  }

  public get certifications(): FormArray {
    return this.formState.certifications as FormArray;
  }

  public get selectedCollegeCodes(): number[] {
    return this.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  public addEmptyCollegeCertification() {
    this.formState.addCollegeCertification();
  }

  public get deviceProviderNumber(): FormControl {
    return this.formState.deviceProviderNumber;
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
    this.certifications.removeAt(index);
  }

  public ngOnInit() {
    this.isDeviceProvider = this.enrolmentService.enrolment.careSettings.some((careSetting) =>
      careSetting.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);
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
    if (this.isDeviceProvider) {
      this.enableDeviceProviderValidator();
    }
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.certifications.length) {
      this.addEmptyCollegeCertification();
    }

    const initialRemoteAccess = this.canRequestRemoteAccess();

    this.formState.form.valueChanges
      .pipe(map((_) => initialRemoteAccess && !this.isInitialEnrolment))
      .subscribe((couldRequestRemoteAccess: boolean) =>
        this.cannotRequestRemoteAccess = couldRequestRemoteAccess && !this.canRequestRemoteAccess()
      );
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Replace previous values on deactivation so updates are discarded
    this.formState.patchValue({ certifications: this.enrolmentService.enrolment.certifications });
  }

  protected onSubmitFormIsValid() {
    // Enrollees can not have certifications and jobs
    this.removeOboSites();
    // Remove remote access data when enrollee is no longer eligible, e.g. licence type changes
    if (this.cannotRequestRemoteAccess) {
      this.removeRemoteAccessData();
    }
  }

  protected afterSubmitIsSuccessful() {
    this.removeIncompleteCertifications(true);
  }

  protected nextRouteAfterSubmit() {
    const certifications = this.formState.collegeCertifications;
    const careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value as CareSetting[];
    const formDeviceProviderNumber = this.formState.deviceProviderNumber.value;

    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = (!this.certifications.length || (this.isDeviceProvider && !formDeviceProviderNumber))
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
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeCode').value || control.invalid) {
          this.removeCertification(index);
        }
      });

    // Always have a single certification available, and it prevents
    // the page from jumping too much when routing
    if (!noEmptyCert && !this.certifications.controls.length) {
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

    if (this.certifications.length) {
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

  private enableDeviceProviderValidator(): void {
    this.formUtilsService.setValidators(this.formState.deviceProviderNumber, [
      FormControlValidators.requiredLength(5)
    ]);
  }
}
