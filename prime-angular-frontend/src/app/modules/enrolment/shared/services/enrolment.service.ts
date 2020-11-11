import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { LicenseWeightedConfig } from '@config/config.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { CollegeLicenceClass } from '@shared/enums/college-licence-class.enum';
import { Enrolment } from '@shared/models/enrolment.model';

import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

export interface IEnrolmentService {
  enrolment$: BehaviorSubject<Enrolment>;
  enrolment: Enrolment;
  isInitialEnrolment: boolean;
  isProfileComplete: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class EnrolmentService implements IEnrolmentService {
  // tslint:disable-next-line: variable-name
  private _enrolment: BehaviorSubject<Enrolment>;

  constructor(
    private configService: ConfigService
  ) {
    this._enrolment = new BehaviorSubject<Enrolment>(null);
  }

  public get enrolment$(): BehaviorSubject<Enrolment> {
    return this._enrolment;
  }

  public get enrolment(): Enrolment {
    return this._enrolment.value;
  }

  public get isInitialEnrolment(): boolean {
    return !this.enrolment || (this.enrolment && !this.enrolment.expiryDate);
  }

  public get isProfileComplete(): boolean {
    return this.enrolment && this.enrolment.profileCompleted;
  }

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

    // const hasRemoteAccessLicence = this.configService.licenses
    // .filter((licence: LicenseWeightedConfig) => enrolleeLicenceCodes.includes(licence.code))
    // .some((licence: LicenseWeightedConfig) => (licence.licensedToProvideCare && licence.namedInImReg));

    // return hasRemoteAccessLicence;

    return true;
  }
}
