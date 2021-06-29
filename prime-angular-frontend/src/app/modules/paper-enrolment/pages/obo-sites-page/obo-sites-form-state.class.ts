import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ConfigService } from '@config/config.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { OboSitesFormModel } from './obo-sites-form.model';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolleeHealthAuthority } from '@shared/models/enrollee-health-authority.model';

export class OboSiteFormState extends AbstractFormState<OboSitesFormModel> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
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
    const site = this.buildOboSiteForm();
    site.get('careSettingCode').patchValue(careSettingCode);
    site.get('healthAuthorityCode').patchValue(healthAuthorityCode);

    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        this.addNonHealthAuthorityOboSite(site, this.communityHealthSites);
        break;
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        this.addNonHealthAuthorityOboSite(site, this.communityPharmacySites);
        break;
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        this.addHealthAuthorityOboSite(site, this.healthAuthoritySites, healthAuthorityCode);
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

  public removeCareSettingSites() {
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

  public get json(): OboSitesFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(pageModel: OboSitesFormModel): void {
    if (!this.formInstance) {
      return;
    }

    if (pageModel.oboSites.length) {
      this.oboSites.clear();
      this.communityHealthSites.clear();
      this.communityPharmacySites.clear();
      Object.keys(this.healthAuthoritySites.controls)
        .forEach(healthAuthorityCode => this.healthAuthoritySites.removeControl(healthAuthorityCode));

      pageModel.oboSites.forEach((s: OboSite) => {
        const site = this.buildOboSiteForm();
        site.patchValue(s);
        this.oboSites.push(site);

        switch (s.careSettingCode) {
          case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
            this.addNonHealthAuthorityOboSite(site, this.communityHealthSites);
            break;
          }
          case CareSettingEnum.COMMUNITY_PHARMACIST: {
            this.addNonHealthAuthorityOboSite(site, this.communityPharmacySites);
            break;
          }
          case CareSettingEnum.HEALTH_AUTHORITY: {
            this.addHealthAuthorityOboSite(site, this.healthAuthoritySites, s.healthAuthorityCode);
            break;
          }
        }
      });
    }
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      oboSites: this.fb.array([]),
      communityHealthSites: this.fb.array([]),
      communityPharmacySites: this.fb.array([]),
      healthAuthoritySites: this.fb.group({})
    });
  }

  public buildOboSiteForm(): FormGroup {
    return this.fb.group({
      careSettingCode: [null, []],
      healthAuthorityCode: [null, []],
      siteName: [null, []],
      facilityName: [null, []],
      jobTitle: [null, [Validators.required]],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        exclude: ['street2'],
        useDefaults: ['provinceCode', 'countryCode'],
        areDisabled: ['provinceCode', 'countryCode']
      }),
      pec: [null, []]
    });
  }

  public addNonHealthAuthorityOboSite(siteForm: FormGroup, siteFormList: FormArray) {
    const siteName = siteForm.get('siteName') as FormControl;
    this.formUtilsService.setValidators(siteName, [Validators.required]);
    siteFormList.push(siteForm);
  }

  /**
   * @param haSiteForm - aka Health Authority Facility Form
   * @param healthAuthoritySites - a FormArray where each element, representing a Health Authority where the enrollee
   * works, contains a FormArray. This nested FormArray contains a FormGroup for each facility that the enrollee works
   * at, in that Health Authority
   */
  public addHealthAuthorityOboSite(haSiteForm: FormGroup, healthAuthoritySites: FormGroup, healthAuthorityCode: number) {
    const facilityName = haSiteForm.get('facilityName') as FormControl;
    this.formUtilsService.setValidators(facilityName, [Validators.required]);
    let sitesOfHealthAuthority = healthAuthoritySites.get(String(healthAuthorityCode)) as FormArray;
    if (!sitesOfHealthAuthority) {
      sitesOfHealthAuthority = this.fb.array([]);
      sitesOfHealthAuthority.setValidators([FormArrayValidators.atLeast(1)]);
      healthAuthoritySites.setControl(String(healthAuthorityCode), sitesOfHealthAuthority);
    }
    sitesOfHealthAuthority.push(haSiteForm);
  }

  public addOboSitesByCareSettingCode(
    careSettingCodes: number[],
    enrolleeHealthAuthorities: EnrolleeHealthAuthority[]
  ) {
    // Add at least one site for each careSetting selected by enrollee
    careSettingCodes?.forEach((careSettingCode) => {
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
        case CareSettingEnum.HEALTH_AUTHORITY: {
          enrolleeHealthAuthorities?.forEach(ha => {
            const sitesOfHealthAuthority = this.healthAuthoritySites.get(`${ha.healthAuthorityCode}`) as FormArray;
            if (!sitesOfHealthAuthority) {
              this.addOboSite(careSettingCode, ha.healthAuthorityCode);
            }
          });
          break;
        }
      }
    });
  }
}
