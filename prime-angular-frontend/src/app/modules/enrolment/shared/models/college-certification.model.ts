import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';
import { NursingLicenseCode } from '@shared/enums/nursing-license-code.enum';

export class CollegeCertification {
  /**
   * @description
   * Certain nursing licences have advanced practices.
   */
  public static readonly havePractices = [
    NursingLicenseCode.NON_PRACTICING_REGISTERED_NURSE,
    NursingLicenseCode.PRACTICING_REGISTERED_NURSE,
    NursingLicenseCode.PROVISIONAL_REGISTERED_NURSE,
    NursingLicenseCode.TEMPORARY_REGISTERED_NURSE_EMERGENCY,
    NursingLicenseCode.TEMPORARY_REGISTERED_NURSE_SPECIAL_EVENT
  ];

  constructor(
    public collegeCode: number,
    public licenseCode: number,
    public licenseNumber: string,
    public practitionerId: string,
    public renewalDate: string,
    public practiceCode?: number,
    public id?: number,
    public deviceProviderNumber?: string
  ) { }

  public static hasPractice(collegeCode: number, licenseCode: number): boolean {
    // Only display Advanced Practices for certain nursing licences
    return collegeCode === CollegeLicenceClassEnum.BCCNM && this.havePractices.includes(licenseCode);
  }
}
