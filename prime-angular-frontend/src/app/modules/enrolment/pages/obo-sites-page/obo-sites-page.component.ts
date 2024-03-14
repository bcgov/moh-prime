import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { UtilsService } from '@core/services/utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

@Component({
  selector: 'app-obo-sites-page',
  templateUrl: './obo-sites-page.component.html',
  styleUrls: ['./obo-sites-page.component.scss']
})
export class OboSitesPageComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public jobNames: Config<number>[];
  public healthAuthorities: Config<number>[];
  public allowDefaultOption: boolean;
  public showHAJobSiteError: boolean;
  public defaultOptionLabel: string;
  public CareSettingEnum = CareSettingEnum;
  public HealthAuthorityEnum = HealthAuthorityEnum;

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
    this.healthAuthorities = this.configService.healthAuthorities;
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

  public get deviceProviderSites(): FormArray {
    return this.form.get('deviceProviderSites') as FormArray;
  }

  public get healthAuthoritySites(): FormGroup {
    return this.form.get('healthAuthoritySites') as FormGroup;
  }

  public get chosenHealthAuthorityCodes(): string[] {
    return Object.keys(this.healthAuthoritySites.value);
  }

  public get careSettings() {
    let careSettings = (this.enrolment?.careSettings) ? this.enrolment.careSettings : null;
    if (this.enrolmentFormStateService.isPatched) {
      careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value;
    }
    return careSettings;
  }

  public get enrolleeHealthAuthorities(): Config<number>[] {
    return this.healthAuthorities.filter(a =>
      this.enrolmentFormStateService.json?.enrolleeHealthAuthorities.find((aa) => aa.healthAuthorityCode === a.code)
    );
  }

  public get showHASelectError(): boolean {
    let result = false;
    Object.values(this.healthAuthoritySites.value).forEach((s: any[]) => {
      if (s[0] && !s[0].healthAuthorityCode) {
        result = true;
      }
    })
    return result;
  }

  public healthAuthoritySiteControl(healthAuthorityCode: string): AbstractControl[] {
    const sites = this.healthAuthoritySites.get(healthAuthorityCode) as FormArray;
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
    site.patchValue({ careSettingCode, healthAuthorityCode });

    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        this.enrolmentFormStateService.addNonHealthAuthorityOboSite(site, this.communityHealthSites);
        break;
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        this.enrolmentFormStateService.addNonHealthAuthorityOboSite(site, this.communityPharmacySites);
        break;
      }
      case CareSettingEnum.DEVICE_PROVIDER: {
        this.enrolmentFormStateService.addNonHealthAuthorityOboSite(site, this.deviceProviderSites);
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
      case CareSettingEnum.DEVICE_PROVIDER: {
        this.deviceProviderSites.removeAt(index);
        break;
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        const sitesOfHealthAuthority = this.healthAuthoritySites.get(`${healthAuthorityCode}`) as FormArray;
        if (sitesOfHealthAuthority) {
          sitesOfHealthAuthority.removeAt(index);
        }
        break;
      }
    }
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteOboSites(true);
    this.removeCareSettingSites();
  }

  public onSubmit(): void {
    this.showHAJobSiteError = !this.eachSelectedHAHasOboJobSite();
    if (!this.showHAJobSiteError) {
      super.onSubmit();
    } else {
      this.utilService.scrollTop();
    }
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.oboSitesForm;
  }

  protected initForm() {
    if (this.enrolmentService.isInitialEnrolment) {
      this.patchForm(true).subscribe(() => {
        this.setOboSites();
      });
    } else {
      //for renewal, pull the enrolment from enrolment service instead
      this.enrolment = this.enrolmentService.enrolment;
      this.enrolmentFormStateService.patchOboSitesForm(this.filterOboSitesByCareSetting(this.enrolment.oboSites));
      this.setOboSites();
    }
  }

  //filter obo site by the care setting in the form - remove any site not in the care settings
  private filterOboSitesByCareSetting(oboSites: OboSite[]): OboSite[] {
    for (var i = 0; i < oboSites.length;) {
      if (oboSites[i].careSettingCode === CareSettingEnum.HEALTH_AUTHORITY) {
        if (oboSites[i].healthAuthorityCode && this.enrolleeHealthAuthorities.some(ha => ha.code !== oboSites[i].healthAuthorityCode)) {
          oboSites.splice(i, 1);
        } else {
          i++;
        }
      } else {
        if (this.careSettings.some(cs => cs.careSettingCode !== oboSites[i].careSettingCode)) {
          oboSites.splice(i, 1);
        } else {
          i++;
        }
      }
    }
    return oboSites;
  }

  private setOboSites() {
    // Add at least one site for each careSetting selected by enrollee
    this.careSettings.forEach(({ careSettingCode }) => {
      switch (careSettingCode) {
        case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
          this.communityHealthSites.setValidators([FormArrayValidators.atLeast(1)]);
          if (!this.communityHealthSites.length) {
            this.addOboSite(careSettingCode);
          }
          break;
        }
        case CareSettingEnum.COMMUNITY_PHARMACIST: {
          this.communityPharmacySites.setValidators([FormArrayValidators.atLeast(1)]);
          if (!this.communityPharmacySites.length) {
            this.addOboSite(careSettingCode);
          }
          break;
        }
        case CareSettingEnum.DEVICE_PROVIDER: {
          this.deviceProviderSites.setValidators([FormArrayValidators.atLeast(1)]);
          if (!this.deviceProviderSites.length) {
            this.addOboSite(careSettingCode);
          }
          break;
        }
        case CareSettingEnum.HEALTH_AUTHORITY: {
          //determine if necessary to add HA job site
          if (this.oboSites?.length) {

            this.enrolleeHealthAuthorities.forEach(ea => {
              let haSiteNum = this.oboSites?.value.filter((s) => s.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY
                && s.healthAuthorityCode == ea.code).length;

              if (haSiteNum === 0) {
                let hasSiteWithoutHASpecified = false;
                if (haSiteNum > 0) {
                  hasSiteWithoutHASpecified = this.oboSites?.value.filter((s) => s.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY && s.healthAuthorityCode === null).length > 0;
                }

                if (hasSiteWithoutHASpecified) {
                  for (let i = 0; i < (this.enrolleeHealthAuthorities.length - haSiteNum); i++) {
                    this.addOboSite(careSettingCode);
                  }
                } else {
                  this.enrolmentFormStateService.json.enrolleeHealthAuthorities.forEach(ha => {
                    if (!this.healthAuthoritySites.get(`${ha.healthAuthorityCode}`)) {
                      this.addOboSite(careSettingCode, ha.healthAuthorityCode);
                    }
                  });
                }
              }
            });
          } else {
            this.enrolmentFormStateService.json.enrolleeHealthAuthorities.forEach(ha => {
              if (!this.healthAuthoritySites.get(`${ha.healthAuthorityCode}`)) {
                this.addOboSite(careSettingCode, ha.healthAuthorityCode);
              }
            });
          }
          break;
        }
      }
    });
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Replace previous values on deactivation so updates are discarded
    this.enrolmentFormStateService.patchOboSitesForm(this.enrolmentService.enrolment.oboSites);
  }

  protected onSubmitFormIsValid() {
    // Enrollees can not have jobs and certifications
    this.removeCollegeCertifications();
    this.removeIncompleteOboSites(true);

    this.oboSites.clear();
    this.communityHealthSites.controls.forEach((site) => this.oboSites.push(site));
    this.communityPharmacySites.controls.forEach((site) => this.oboSites.push(site));
    this.deviceProviderSites.controls.forEach((site) => this.oboSites.push(site));
    Object.keys(this.healthAuthoritySites.controls).forEach(healthAuthorityCode => {
      const sitesOfHealthAuthority = this.healthAuthoritySites.get(healthAuthorityCode) as FormArray;
      sitesOfHealthAuthority.controls.forEach((site) => this.oboSites.push(site));
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
    this.communityHealthSites.clear();

    this.communityPharmacySites.clearValidators();
    this.communityPharmacySites.updateValueAndValidity();
    this.communityPharmacySites.clear();

    this.deviceProviderSites.clearValidators();
    this.deviceProviderSites.updateValueAndValidity();
    this.deviceProviderSites.clear();

    Object.keys(this.healthAuthoritySites.controls).forEach(healthAuthorityCode => {
      const sitesOfHealthAuthority = this.healthAuthoritySites.get(healthAuthorityCode) as FormArray;
      sitesOfHealthAuthority.clearValidators();
      sitesOfHealthAuthority.updateValueAndValidity();
      sitesOfHealthAuthority.clear();
    });
  }

  /**
   * Check if each selected HA has at least one obo job site
   * @returns true if pass
   */
  private eachSelectedHAHasOboJobSite(): boolean {
    let result = true;
    if (this.careSettings.find((c) => c.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY)) {
      if (this.enrolleeHealthAuthorities.length > 0) {

        let jobSiteHAs = [];

        Object.values(this.healthAuthoritySites.controls).forEach((formArray: FormArray) => {
          jobSiteHAs = [...jobSiteHAs, ...formArray.value.map((s) => s.healthAuthorityCode)];
        });

        this.enrolmentFormStateService.json.enrolleeHealthAuthorities.forEach(ha => {
          if (!jobSiteHAs.find(s => s === ha.healthAuthorityCode)) {
            result = false;
          }
        });
      }
    }
    return result;
  }
}
