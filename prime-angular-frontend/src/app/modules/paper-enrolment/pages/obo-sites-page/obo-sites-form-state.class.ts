import { UntypedFormArray, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { OboSitesForm } from './obo-sites-form.model';

export class OboSiteFormState extends AbstractFormState<OboSitesForm> {
  public constructor(
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get communityHealthSites(): UntypedFormArray {
    return this.form.get('communityHealthSites') as UntypedFormArray;
  }

  public get communityPharmacySites(): UntypedFormArray {
    return this.form.get('communityPharmacySites') as UntypedFormArray;
  }

  public get deviceProviderSites(): UntypedFormArray {
    return this.form.get('deviceProviderSites') as UntypedFormArray;
  }

  public get healthAuthoritySites(): UntypedFormGroup {
    return this.form.get('healthAuthoritySites') as UntypedFormGroup;
  }

  /**
   * @description
   * Get the sites for a specific health authority.
   *
   * NOTE: The health authority control may not exist
   * if no sites exists.
   */
  public healthAuthorityCodeSites(healthAuthorityCode: number): UntypedFormArray | null {
    return this.healthAuthoritySites.get(`${healthAuthorityCode}`) as UntypedFormArray;
  }

  public get json(): OboSitesForm {
    if (!this.formInstance) {
      return;
    }

    const sites = this.formInstance.getRawValue();
    const oboSites = [
      sites.communityHealthSites,
      sites.communityPharmacySites,
      sites.deviceProviderSites,
      Object.keys(sites.healthAuthoritySites)
        .flatMap((healthAuthSiteCode: string) => sites.healthAuthoritySites[healthAuthSiteCode])
    ].flat();

    return { oboSites };
  }

  public patchValue({ oboSites }: OboSitesForm, enrollee: HttpEnrollee): void {
    if (!this.formInstance) {
      return;
    }

    const careSettingCodes = enrollee.enrolleeCareSettings.map(ecs => ecs.careSettingCode);
    const healthAuthCodes = enrollee.enrolleeHealthAuthorities.map(eha => eha.healthAuthorityCode);

    // Each care setting must have at least one site
    careSettingCodes.forEach(csc => {
      switch (csc) {
        case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
          const oboSite = oboSites.find(os => os.careSettingCode === csc);
          const site = this.buildOboSiteForm();
          const value = (oboSite) ? oboSite : { careSettingCode: csc };
          site.patchValue(value);
          return this.addNonHealthAuthorityOboSite(site, this.communityHealthSites);
        }
        case CareSettingEnum.COMMUNITY_PHARMACIST: {
          const oboSite = oboSites.find(os => os.careSettingCode === csc);
          const site = this.buildOboSiteForm();
          const value = (oboSite) ? oboSite : { careSettingCode: csc };
          site.patchValue(value);
          return this.addNonHealthAuthorityOboSite(site, this.communityPharmacySites);
        }
        case CareSettingEnum.DEVICE_PROVIDER: {
          const oboSite = oboSites.find(os => os.careSettingCode === csc);
          const site = this.buildOboSiteForm();
          const value = (oboSite) ? oboSite : { careSettingCode: csc };
          site.patchValue(value);
          return this.addNonHealthAuthorityOboSite(site, this.deviceProviderSites);
        }
        case CareSettingEnum.HEALTH_AUTHORITY: {
          return healthAuthCodes.forEach(hac => {
            const oboSite = oboSites.find(os => os.careSettingCode === csc && os.healthAuthorityCode === hac);
            const site = this.buildOboSiteForm();
            const value = (oboSite) ? oboSite : { careSettingCode: csc, healthAuthorityCode: hac };
            site.patchValue(value);
            this.addHealthAuthorityOboSite(site, this.healthAuthoritySites, hac);
          });
        }
      }
    });
  }

  public buildForm(): void {
    // Types of sites grouped by care setting
    this.formInstance = this.fb.group({
      communityHealthSites: this.fb.array([]),
      communityPharmacySites: this.fb.array([]),
      deviceProviderSites: this.fb.array([]),
      // Keyed by health authority code
      healthAuthoritySites: this.fb.group({})
    });
  }

  public addOboSite(careSettingCode: number, healthAuthorityCode?: number): void {
    const site = this.buildOboSiteForm();
    site.get('careSettingCode').patchValue(careSettingCode);
    site.get('healthAuthorityCode').patchValue(healthAuthorityCode);

    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        return this.addNonHealthAuthorityOboSite(site, this.communityHealthSites);
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        return this.addNonHealthAuthorityOboSite(site, this.communityPharmacySites);
      }
      case CareSettingEnum.DEVICE_PROVIDER: {
        return this.addNonHealthAuthorityOboSite(site, this.deviceProviderSites);
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        return this.addHealthAuthorityOboSite(site, this.healthAuthoritySites, healthAuthorityCode);
      }
    }
  }

  public removeOboSite(index: number, careSettingCode: number, healthAuthCode?: number): void {
    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        return this.communityHealthSites.removeAt(index);
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        return this.communityPharmacySites.removeAt(index);
      }
      case CareSettingEnum.DEVICE_PROVIDER: {
        return this.deviceProviderSites.removeAt(index);
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        return this.healthAuthorityCodeSites(healthAuthCode).removeAt(index);
      }
    }
  }

  private addNonHealthAuthorityOboSite(site: UntypedFormGroup, sites: UntypedFormArray) {
    sites.push(site);
  }

  private addHealthAuthorityOboSite(site: UntypedFormGroup, sites: UntypedFormGroup, healthAuthorityCode: number) {
    // Check for health authority control and associated sites, otherwise
    // add the health authority with validations before appending the site
    let healthAuthoritySites = this.healthAuthorityCodeSites(healthAuthorityCode);
    if (!healthAuthoritySites) {
      healthAuthoritySites = this.fb.array([]);
      healthAuthoritySites.setValidators([FormArrayValidators.atLeast(1)]);
      sites.setControl(`${healthAuthorityCode}`, healthAuthoritySites);
    }
    this.formUtilsService.setValidators(site.get('facilityName') as UntypedFormControl, [Validators.required]);
    healthAuthoritySites.push(site);
  }

  private buildOboSiteForm(): UntypedFormGroup {
    return this.fb.group({
      careSettingCode: [null, []],
      healthAuthorityCode: [null, []],
      siteName: [null, []],
      facilityName: [null, []],
      jobTitle: [null, [Validators.required]],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: [],
        exclude: ['street', 'city', 'postal', 'street2', 'provinceCode'],
        useDefaults: ['countryCode'],
        areDisabled: ['countryCode']
      })
    });
  }
}
