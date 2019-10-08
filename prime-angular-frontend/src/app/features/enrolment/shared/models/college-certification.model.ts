export interface CollegeCertification {
  id: number;
  collegeCode: number;
  licenseNumber: string;
  licenseCode: number;
  renewalDate: Date;
  practiceCode?: number;
}
