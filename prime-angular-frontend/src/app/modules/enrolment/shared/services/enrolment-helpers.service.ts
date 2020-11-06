import { Injectable } from '@angular/core';

import { ConfigService } from '@config/config.service';
import { LicenseWeightedConfig } from '@config/config.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { CollegeLicenceClass } from '@shared/enums/college-licence-class.enum';

import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentHelpersService {
  constructor(
    private configService: ConfigService
  ) { }

  /**
   * @description
   * Determine whether an enrollee can request remote access.
   *
   * Remote access rules:
   * - No College of Pharmacist can request remote access
   * - No Community Pharmacist care setting
   * - Licences "Named in IM Reg" or "Licenced to Provide Care"
   */
  public canRequestRemoteAccess(certifications: CollegeCertification[], careSettings: CareSetting[]) {
    const isCollegeOfPharmacists = certifications
      .some(cert => cert.collegeCode === CollegeLicenceClass.CPBC);
    const isCommunityPharmacist = careSettings
      .some(cs => cs.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST);

    if (isCollegeOfPharmacists || isCommunityPharmacist) {
      return false;
    }

    const enrolleeLicenceCodes = certifications
      .map((certification: CollegeCertification) => certification.licenseCode);

    const hasRemoteAccessLicence = this.configService.licenses
      .filter((licence: LicenseWeightedConfig) => enrolleeLicenceCodes.includes(licence.code))
      .some((licence: LicenseWeightedConfig) => (licence.licensedToProvideCare && licence.namedInImReg));

    return hasRemoteAccessLicence;
  }
}
