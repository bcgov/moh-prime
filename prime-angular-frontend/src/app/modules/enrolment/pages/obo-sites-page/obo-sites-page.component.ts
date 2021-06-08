import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';

@Component({
  selector: 'app-obo-sites-page',
  templateUrl: './obo-sites-page.component.html',
  styleUrls: ['./obo-sites-page.component.scss']
})
export class OboSitesPageComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public jobNames: Config<number>[];
  public allowDefaultOption: boolean;
  public defaultOptionLabel: string;

  public CareSettingEnum = CareSettingEnum;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService,
    private configService: ConfigService,
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

    this.jobNames = this.configService.jobNames;
    this.allowDefaultOption = false;
    this.defaultOptionLabel = 'None';
  }

  public get oboSites(): FormArray {
    return this.form.get('oboSites') as FormArray;
  }

  public get communityHealthSites(): FormArray {
    return this.form.get('communityHealthSites') as FormArray;
  }

  public get communityPharmacySites(): FormArray {
    return this.form.get('communityPharmacySites') as FormArray;
  }

  public get healthAuthoritySites(): FormGroup {
    return this.form.get('healthAuthoritySites') as FormGroup;
  }

  public get careSettings() {
    let careSettings = (this.enrolment?.careSettings) ? this.enrolment.careSettings : null;
    if (this.enrolmentFormStateService.isPatched) {
      careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value;
    }
    return careSettings;
  }

  public get hasHealthAuthoritySites(): boolean {
    return Object.keys(this.healthAuthoritySites.controls).length > 0;
  }

  public healthAuthoritySitesAsControls(healthAuthorityCode: number): AbstractControl[] {
    const sites = this.healthAuthoritySites.get(`${healthAuthorityCode}`) as FormArray;
    return sites?.controls;
  }

  public oboSitesByCareSetting(careSettingCode: number): FormArray {
    const sites: FormArray = this.fb.array([]);
    if (this.oboSites?.length) {
      this.oboSites.controls.forEach((site, i) => {
        if (site.value.careSettingCode === careSettingCode) {
          sites.push(site as FormGroup);
        }
      });
    }
    return sites as FormArray;
  }

  public addOboSite(careSettingCode: number, healthAuthorityCode?: number) {
    const site = this.enrolmentFormStateService.buildOboSiteForm();
    site.get('careSettingCode').patchValue(careSettingCode);
    site.get('healthAuthorityCode').patchValue(healthAuthorityCode);

    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        this.enrolmentFormStateService.addNonHealthAuthorityOboSite(site, this.communityHealthSites);
        break;
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        this.enrolmentFormStateService.addNonHealthAuthorityOboSite(site, this.communityPharmacySites);
        break;
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        this.enrolmentFormStateService.addHealthAuthorityOboSite(site, this.healthAuthoritySites, healthAuthorityCode);
        break;
      }
    }
  }

  public removeOboSite(index: number, careSettingCode: number, healthAuthorityCode?: number) {
    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        this.communityHealthSites.removeAt(index);
        break;
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        this.communityPharmacySites.removeAt(index);
        break;
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        const sitesOfHealthAuthority = this.healthAuthoritySites.get(`${healthAuthorityCode}`) as FormArray;
        sitesOfHealthAuthority.removeAt(index);
        break;
      }
    }
  }

  public routeBackTo() {
    this.routeTo(EnrolmentRoutes.REGULATORY);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteOboSites(true);
    this.removeCareSettingSites();
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.jobsForm;
  }

  // TODO refactor and make this invoke initForm
  protected initForm() {
    // Initialize listeners before patching
    this.patchForm().subscribe(() => {
      // Add at least one site for each careSetting selected by enrollee
      this.careSettings?.forEach((careSetting) => {
        switch (careSetting.careSettingCode) {
          case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
            this.communityHealthSites.setValidators([FormArrayValidators.atLeast(1)]);
            if (!this.communityHealthSites.length) {
              this.addOboSite(careSetting.careSettingCode);
            }
            break;
          }
          case CareSettingEnum.COMMUNITY_PHARMACIST: {
            this.communityPharmacySites.setValidators([FormArrayValidators.atLeast(1)]);
            if (!this.communityPharmacySites.length) {
              this.addOboSite(careSetting.careSettingCode);
            }
            break;
          }
          case CareSettingEnum.HEALTH_AUTHORITY: {
            this.enrolment.enrolleeHealthAuthorities.forEach(ha => {
              const sitesOfHealthAuthority = this.healthAuthoritySites.get(`${ha.healthAuthorityCode}`) as FormArray;
              if (!sitesOfHealthAuthority) {
                this.addOboSite(careSetting.careSettingCode, ha.healthAuthorityCode);
              }
            });
            break;
          }
        }
      });
    });
  }

  protected onSubmitFormIsValid() {
    // Enrollees can not have jobs and certifications
    this.removeCollegeCertifications();
    this.removeIncompleteOboSites(true);

    this.oboSites.clear();
    this.communityHealthSites.controls.forEach((site) => this.oboSites.push(site));
    this.communityPharmacySites.controls.forEach((site) => this.oboSites.push(site));
    Object.keys(this.healthAuthoritySites.controls).forEach(healthAuthorityCode => {
      const sitesOfHealthAuthority = this.healthAuthoritySites.get(healthAuthorityCode) as FormArray;
      sitesOfHealthAuthority.controls.forEach((site) =>
        this.oboSites.push(site));
    });
    this.removeCareSettingSites();
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.SELF_DECLARATION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  /**
   * @description
   * Removes incomplete oboSites from the list in preparation for submission, and
   * allows for an empty list of oboSites if no jobs are solected.
   */
  private removeIncompleteOboSites(noEmptyOboSites: boolean = false) {
    this.oboSites.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('physicalAddress').value.city;
        const careSetting = control.get('careSettingCode').value;

        // Remove when empty, default option, or group is invalid
        if (!value || value === this.defaultOptionLabel || control.invalid) {
          this.removeOboSite(index, careSetting);
        }
      });

    // Add at least one site for each careSetting selected by enrollee
    this.careSettings?.forEach((careSetting) => {
      if (!noEmptyOboSites && !this.oboSitesByCareSetting(careSetting.careSettingCode)?.length) {
        this.addOboSite(careSetting.careSettingCode);
      }
    });
  }

  /**
   * @description
   * Remove college certifications from the enrolment as enrollees can not have
   * job(s), as well as, college certification(s).
   */
  private removeCollegeCertifications() {
    this.enrolmentFormStateService.regulatoryFormState.removeCollegeCertifications();
  }

  private removeCareSettingSites() {
    // Clear out sites so validation doesn't interrupt submissions
    this.communityHealthSites.clearValidators();
    this.communityHealthSites.updateValueAndValidity();
    this.communityPharmacySites.clearValidators();
    this.communityPharmacySites.updateValueAndValidity();
    Object.keys(this.healthAuthoritySites.controls).forEach(healthAuthorityCode => {
      const sitesOfHealthAuthority = this.healthAuthoritySites.get(healthAuthorityCode) as FormArray;
      sitesOfHealthAuthority.clearValidators();
      sitesOfHealthAuthority.updateValueAndValidity();
    });
  }
}
