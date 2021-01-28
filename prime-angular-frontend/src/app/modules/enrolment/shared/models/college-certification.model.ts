export interface CollegeCertification {
  id?: number;
  collegeCode: number;
  licenseCode: number;
  licenseNumber: string;
  practitionerId: string;
  renewalDate: string;
  practiceCode?: number;
}
