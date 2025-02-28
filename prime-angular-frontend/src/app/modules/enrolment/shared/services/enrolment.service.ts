import { Injectable } from '@angular/core';

import { BehaviorSubject, exhaustMap, Observable, of } from 'rxjs';

import { PAPER_ENROLLEE_GPID_PREFIX } from '@lib/constants';
import { ConfigService } from '@config/config.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';
import { Enrollee } from '@shared/models/enrollee.model';
import { Enrolment } from '@shared/models/enrolment.model';

import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { SiteResource } from '@core/resources/site-resource.service';

export interface IEnrolmentService {
  enrolment$: BehaviorSubject<Enrolment>;
  enrolment: Enrolment;
  enrollee: Enrollee;
  isInitialEnrolment: boolean;
  isProfileComplete: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class EnrolmentService implements IEnrolmentService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private readonly _enrolment: BehaviorSubject<Enrolment>;
  private _isMatchingPaperEnrollee: boolean | null;
  private _remoteAccess: boolean;

  constructor(
    protected siteResource: SiteResource,
    private configService: ConfigService
  ) {
    this._enrolment = new BehaviorSubject<Enrolment>(null);
    // Default indicates value has never been set
    this._isMatchingPaperEnrollee = null;
  }

  public get enrolment$(): BehaviorSubject<Enrolment> {
    return this._enrolment;
  }

  public get enrolment(): Enrolment {
    return this._enrolment.value;
  }

  public get enrollee(): Enrollee {
    return this._enrolment.value.enrollee;
  };

  public get isMatchingPaperEnrollee(): boolean {
    return this._isMatchingPaperEnrollee;
  }

  public set isMatchingPaperEnrollee(isMatchingPaperEnrollee: boolean) {
    this._isMatchingPaperEnrollee = isMatchingPaperEnrollee;
  }

  public get isInitialEnrolment(): boolean {
    return !this.enrolment || (this.enrolment && !this.enrolment.expiryDate);
  }

  public get isProfileComplete(): boolean {
    return this.enrolment && this.enrolment.profileCompleted;
  }

  public hasPossibleRemoteAccessCareSetting(careSettings: CareSetting[]): boolean {
    return careSettings
      .some(cs => cs.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE);
  }

  public shouldShowCollegePrefix(licenseCode: number): boolean {
    // No college prefix for:
    // Pharmacy Technician (29),
    // Non-Practicing Pharmacy Technician (31), and
    // Podiatrists (59, 65, 66, 67)
    return ![29, 31, 59, 65, 66, 67].includes(licenseCode);
  }

  public getPrescriberIdType(licenceCode: number): PrescriberIdTypeEnum {
    const prescriberIdTypes = this.configService.licenses
      .filter(licenseConfig => licenseConfig.code === licenceCode)
      .map(licenseConfig => licenseConfig.prescriberIdType);

    return (prescriberIdTypes.length)
      ? prescriberIdTypes[0]
      : PrescriberIdTypeEnum.NA;
  }

  public isPaperEnrollee(enrollee: Enrollee): boolean {
    return (enrollee?.gpid?.startsWith(PAPER_ENROLLEE_GPID_PREFIX));
  }
}
