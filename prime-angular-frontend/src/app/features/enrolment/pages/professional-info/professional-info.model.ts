export interface ProfessionalInfo {
  hasCertification: boolean;
  certifications: CollegeCertification[];
  isDeviceProvider: boolean;
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  isAccessingPharmaNetOnBehalfOf: boolean;
  jobs: Job[];
}

export interface CollegeCertification {
  id: number;
  collegeCode: number;
  licenseNumber: string;
  licenseCode: number;
  renewalDate: Date;
  practiceCode?: number;
}

export interface Job {
  id: number;
  title: string;
}
