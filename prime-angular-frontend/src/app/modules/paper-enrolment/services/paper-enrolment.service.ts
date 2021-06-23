import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { LicenseConfig } from '@config/config.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { HttpEnrollee } from '@shared/models/enrolment.model';

export interface IPaperEnrolmentService {
  enrollee$: BehaviorSubject<HttpEnrollee>;
  enrollee: HttpEnrollee;
  isProfileComplete: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class PaperEnrolmentService implements IPaperEnrolmentService {
  // tslint:disable-next-line: variable-name
  private readonly _enrollee: BehaviorSubject<HttpEnrollee>;

  constructor(
    private configService: ConfigService
  ) {
    this._enrollee = new BehaviorSubject<HttpEnrollee>(null);
  }

  public get enrollee$(): BehaviorSubject<HttpEnrollee> {
    return this._enrollee;
  }

  public get enrollee(): HttpEnrollee {
    return this._enrollee.value;
  }

  public set enrollee(enrollee: HttpEnrollee) {
    // Store a copy to prevent updates by reference
    this._enrollee.next({ ...enrollee });
  }

  public get isProfileComplete(): boolean {
    return this.enrollee && this.enrollee.profileCompleted;
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
  public canRequestRemoteAccess(certifications: CollegeCertification[], careSettings: CareSetting[]): boolean {
    const isCollegeOfPharmacists = certifications
      .some(cert => cert.collegeCode === CollegeLicenceClassEnum.CPBC);

    if (isCollegeOfPharmacists || !this.hasAllowedRemoteAccessCareSetting(careSettings)) {
      return false;
    }

    const enrolleeLicenceCodes = certifications
      .map((certification: CollegeCertification) => certification.licenseCode);

    const hasRemoteAccessLicence = this.configService.licenses
      .filter((licence: LicenseConfig) => enrolleeLicenceCodes.includes(licence.code))
      .some(this.hasAllowedRemoteAccessLicences);

    return hasRemoteAccessLicence;
  }

  public hasAllowedRemoteAccessCareSetting(careSettings: CareSetting[]): boolean {
    return careSettings
      .some(cs => cs.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE);
  }

  public hasAllowedRemoteAccessLicences(licenceConfig: LicenseConfig): boolean {
    return (licenceConfig.licensedToProvideCare && licenceConfig.namedInImReg);
  }

  public shouldShowCollegePrefix(licenseCode: number): boolean {
    // No college prefix for:
    // Pharmacy Technician (29),
    // Non-Practicing Pharmacy Technician (31), and
    // Podiatrists (59, 65, 66, 67)
    return ![29, 31, 59, 65, 66, 67].includes(licenseCode);
  }
}
