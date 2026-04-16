import { Component, OnInit, OnDestroy } from '@angular/core';
import { UntypedFormGroup, UntypedFormArray, UntypedFormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

@Component({
  selector: 'app-care-setting',
  templateUrl: './care-setting.component.html',
  styleUrls: ['./care-setting.component.scss']
})
export class CareSettingComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {

  public careSettingTypes: Config<number>[];
  public filteredCareSettingTypes: Config<number>[];
  public healthAuthorities: Config<number>[];
  public hasNoHealthAuthoritiesError: boolean;

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
    private configService: ConfigService,
    protected authService: AuthService,
    private permissionService: PermissionService
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

    this.careSettingTypes = this.configService.careSettings;
    this.healthAuthorities = this.configService.healthAuthorities;
    this.hasNoHealthAuthoritiesError = false;
  }

  public get careSettings(): UntypedFormArray {
    return this.form.get('careSettings') as UntypedFormArray;
  }

  /**
   * @description
   *  Representing possible health authorities to select from and whether a given one was selected
   */
  public get enrolleeHealthAuthorities(): UntypedFormArray {
    return this.form.get('enrolleeHealthAuthorities') as UntypedFormArray;
  }

  public onSubmit() {
    const controls = this.careSettings.controls;

    // Remove any oboSites belonging to careSetting which is no longer selected
    this.careSettingTypes.forEach(type => {
      if (!controls.some(c => c.value.careSettingCode === type.code)) {
        this.removeOboSites(type.code);
      }
    });

    // Remove health authorities if health authority care setting not chosen
    if (!controls.some(c => c.value.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY)) {
      this.enrolmentFormStateService.removeHealthAuthorities();
      this.setHealthAuthorityValidator();
    }

    if (!controls.some(c => c.value.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE)) {
      this.enrolmentFormStateService.removeRemoteAccess();
    }

    // Remove device provider identifier if Device Provider is no longer selected
    if (!controls.some(c => c.value.careSettingCode === CareSettingEnum.DEVICE_PROVIDER)) {
      this.enrolmentFormStateService.regulatoryFormState.deviceProviderIdentifier.reset();
    }

    // If an individual health authority was deselected, its Obo Sites
    // should be removed as well
    this.enrolmentFormStateService.removeUnselectedHAOboSites();

    this.enrolmentFormStateService.removeDeviceProvider();

    super.onSubmit();
  }

  public addCareSetting() {
    const careSetting = this.enrolmentFormStateService.buildCareSettingForm();
    this.careSettings.push(careSetting);
    this.setHealthAuthorityValidator();
  }

  public removeCareSetting(index: number) {
    this.careSettings.removeAt(index);
    this.setHealthAuthorityValidator();
  }

  public filterCareSettingTypes(careSetting: UntypedFormGroup) {
    // Create a list of filtered care settings
    if (this.careSettings.length) {
      // All the currently chosen care settings
      const selectedCareSettingCodes = this.careSettings.value
        .map((cs: CareSetting) => cs.careSettingCode);

      // Current care setting selected
      const currentCareSetting = this.careSettingTypes
        .find(cs => cs.code === careSetting.get('careSettingCode').value);
      // Filter the list of possible care settings using the selected care setting
      let filteredCareSettingTypes = this.careSettingTypes
        .filter((c: Config<number>) => !selectedCareSettingCodes.includes(c.code));

      if (selectedCareSettingCodes[0] === CareSettingEnum.DEVICE_PROVIDER
        && !currentCareSetting) {
        // Remove other options if device provider is first selected
        filteredCareSettingTypes = [];
      } else if (selectedCareSettingCodes[0] && selectedCareSettingCodes[0] !== CareSettingEnum.DEVICE_PROVIDER
        && (!currentCareSetting || currentCareSetting.code != selectedCareSettingCodes[0])) {
        // if the currenct selected care setting is not the first dropdown, remove device provider
        filteredCareSettingTypes = filteredCareSettingTypes.filter(t => t.code !== CareSettingEnum.DEVICE_PROVIDER);
      }



      if (currentCareSetting) {
        // Add the current careSetting to the list of filtered
        // careSettings so it remains visible
        filteredCareSettingTypes.unshift(currentCareSetting);
      }

      return filteredCareSettingTypes;
    }

    // Otherwise, provide the entire list of care setting types
    return this.careSettingTypes;
  }


  public hasSelectedHACareSetting(): boolean {
    return (this.careSettings.value.some(e => e.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY));
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  public ngOnDestroy(): void {
    this.removeIncompleteCareSettings();
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.careSettingsForm;
  }

  protected initForm() {
    // Always have at least one care setting ready for
    // the enrollee to fill out
    if (!this.careSettings.length) {
      this.addCareSetting();
    }

    this.setHealthAuthorityValidator();

    this.careSettings.valueChanges.subscribe(() => this.setHealthAuthorityValidator());
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Replace previous values on deactivation so updates are discarded
    const { careSettings, enrolleeHealthAuthorities } = this.enrolmentService.enrolment;
    this.enrolmentFormStateService.patchCareSettingsForm({ careSettings, enrolleeHealthAuthorities });
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoHealthAuthoritiesError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (this.hasSelectedHACareSetting() && this.enrolleeHealthAuthorities.hasError) {
      this.hasNoHealthAuthoritiesError = true;
    }
  }

  protected nextRouteAfterSubmit() {
    super.nextRouteAfterSubmit(
      this.isProfileComplete
        ? EnrolmentRoutes.OVERVIEW
        : EnrolmentRoutes.REGULATORY
    );
  }

  private removeIncompleteCareSettings(allowEmptyCareSettings: boolean = true) {
    this.careSettings.value
      .reduce((indexes, careSetting, index) =>
        (!careSetting.careSettingCode)
          ? [...indexes, index]
          : indexes
        , [])
      .reverse()
      .forEach((index: number) => this.removeCareSetting(index));

    if (allowEmptyCareSettings) {
      return;
    }

    // Always have a single care setting available, and it prevents
    // the page from jumping too much when routing
    if (!this.careSettings.controls.length) {
      this.addCareSetting();
    }
  }

  /**
   * @description
   * Remove obo sites by care setting if a care setting
   * was removed from the enrolment.
   */
  private removeOboSites(careSettingCode: number): void {
    const form = this.enrolmentFormStateService.oboSitesForm;
    const oboSites = form.get('oboSites') as UntypedFormArray;

    for (var i = oboSites.length - 1; i >= 0; i--) {
      if (oboSites.value[i].careSettingCode === careSettingCode) {
        oboSites.removeAt(i);
      }
    }

    const clear = (fa: UntypedFormArray) => {
      fa.clear();
      fa.clearValidators();
      fa.updateValueAndValidity();
    };

    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        return clear(form.get('communityHealthSites') as UntypedFormArray);
      }
      case CareSettingEnum.COMMUNITY_PHARMACY: {
        return clear(form.get('communityPharmacySites') as UntypedFormArray);
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        const healthAuthoritySites = form.get('healthAuthoritySites') as UntypedFormGroup;
        Object.keys(healthAuthoritySites.controls).forEach(healthAuthorityCode => {
          const sitesOfHealthAuthority = healthAuthoritySites.get(`${healthAuthorityCode}`) as UntypedFormArray;
          sitesOfHealthAuthority.clearValidators();
          sitesOfHealthAuthority.updateValueAndValidity();
          healthAuthoritySites.removeControl(healthAuthorityCode);
        });
      }
    }
  }

  private setHealthAuthorityValidator(): void {
    this.hasSelectedHACareSetting()
      ? this.enrolleeHealthAuthorities.setValidators(FormArrayValidators.atLeast(1, (control: UntypedFormControl) => control.value))
      : this.enrolleeHealthAuthorities.clearValidators();
  }
}
