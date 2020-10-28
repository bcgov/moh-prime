export interface CollegeCertification {
  id?: number;
  prefix?: string;
  collegeCode: number;
  licenseNumber: string;
  licenseCode: number;
  renewalDate: string;
  practiceCode?: number;
}
