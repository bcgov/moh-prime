import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, FormControl, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
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
  public careSettingCtrl: FormControl;
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
    private fb: FormBuilder
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

  public get careSettings(): FormArray {
    return this.form.get('careSettings') as FormArray;
  }

  /**
   * @description
   *  Representing possible health authorities to select from and whether a given one was selected
   */
  public get enrolleeHealthAuthorities(): FormArray {
    return this.form.get('enrolleeHealthAuthorities') as FormArray;
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

    // If an individual health authority was deselected, its Obo Sites should be removed as well
    this.enrolmentFormStateService.removeUnselectedHAOboSites();

    super.onSubmit();
  }

  public addCareSetting() {
    const careSetting = this.enrolmentFormStateService.buildCareSettingForm();
    this.careSettings.push(careSetting);
    this.setHealthAuthorityValidator();
  }

  public disableCareSetting(careSettingCode: number): boolean {
    return ![
      CareSettingEnum.COMMUNITY_PHARMACIST,
      CareSettingEnum.HEALTH_AUTHORITY,
      CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE
    ].includes(careSettingCode);
  }

  public removeCareSetting(index: number, careSettingCode: number) {
    this.careSettings.removeAt(index);
    this.setHealthAuthorityValidator();
  }

  public filterCareSettingTypes(careSetting: FormGroup) {
    // Create a list of filtered care settings
    if (this.careSettings.length) {
      // All the currently chosen care settings
      const selectedCareSettingCodes = this.careSettings.value
        .map((cs: CareSetting) => cs.careSettingCode);
      // Current care setting selected
      const currentCareSetting = this.careSettingTypes
        .find(cs => cs.code === careSetting.get('careSettingCode').value);
      // Filter the list of possible care settings using the selected care setting
      const filteredCareSettingTypes = this.careSettingTypes
        .filter((c: Config<number>) => !selectedCareSettingCodes.includes(c.code));

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

  public routeBackTo() {
    this.authService.identityProvider$()
      .subscribe((identityProvider: IdentityProviderEnum) => {
        const routePath = (identityProvider === IdentityProviderEnum.BCSC)
          ? EnrolmentRoutes.BCSC_DEMOGRAPHIC
          : EnrolmentRoutes.BCEID_DEMOGRAPHIC;

        this.routeTo(routePath);
      });
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const canDeactivate = super.canDeactivate();

    return (canDeactivate instanceof Observable)
      ? canDeactivate.pipe(tap(() => this.removeIncompleteOrganizations()))
      : canDeactivate;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  public ngOnDestroy() {
    this.removeIncompleteOrganizations();
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

  protected onSubmitFormIsValid(): void {
    this.hasNoHealthAuthoritiesError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (this.hasSelectedHACareSetting() && this.enrolleeHealthAuthorities.hasError) {
      this.hasNoHealthAuthoritiesError = true;
    }
  }

  protected nextRouteAfterSubmit() {
    const oboSites = this.enrolmentFormStateService.oboSitesForm.get('oboSites').value as OboSite[];
    const certifications = this.enrolmentFormStateService.regulatoryFormState.certifications;

    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.REGULATORY;
    } else if (oboSites?.length) {
      // Should edit existing Job/OboSites next
      nextRoutePath = EnrolmentRoutes.OBO_SITES;
    } else if (!certifications.length && !oboSites?.length) {
      // No College Licence and need to enter Job information
      nextRoutePath = EnrolmentRoutes.OBO_SITES;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  private removeIncompleteOrganizations() {
    this.careSettings.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('careSettingCode').value;

        // Remove if care setting is empty or the group is invalid
        if (!value || control.invalid) {
          this.removeCareSetting(index, value);
        }
      });

    // Always have a single care setting available, and it prevents
    // the page from jumping too much when routing
    if (!this.careSettings.controls.length) {
      this.addCareSetting();
    }
  }

  /**
   * @description
   * Remove obo sites by care setting if a care setting was removed from the enrolment
   */
  private removeOboSites(careSettingCode: number): void {
    const form = this.enrolmentFormStateService.oboSitesForm;
    const oboSites = form.get('oboSites') as FormArray;

    oboSites.value?.forEach((site: OboSite, index: number) => {
      if (site.careSettingCode === careSettingCode) {
        oboSites.removeAt(index);
      }
    });

    const clear = (fa: FormArray) => {
      fa.clear();
      fa.clearValidators();
      fa.updateValueAndValidity();
    };

    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        return clear(form.get('communityHealthSites') as FormArray);
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        return clear(form.get('communityPharmacySites') as FormArray);
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        const healthAuthoritySites = form.get('healthAuthoritySites') as FormGroup;
        Object.keys(healthAuthoritySites.controls).forEach(healthAuthorityCode => {
          const sitesOfHealthAuthority = healthAuthoritySites.get(`${healthAuthorityCode}`) as FormArray;
          sitesOfHealthAuthority.clearValidators();
          sitesOfHealthAuthority.updateValueAndValidity();
          healthAuthoritySites.removeControl(healthAuthorityCode);
        });
      }
    }
  }

  private setHealthAuthorityValidator(): void {
    this.hasSelectedHACareSetting()
      ? this.enrolleeHealthAuthorities.setValidators(FormArrayValidators.atLeast(1, (control: FormControl) => control.value))
      : this.enrolleeHealthAuthorities.clearValidators();
  }
}
