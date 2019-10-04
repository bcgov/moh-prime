export interface ProfessionalInfo {
  is_college_certified: boolean;
  college_certifications: CollegeCertification[];
  is_device_provider: boolean;
  device_provider_number: string;
  is_insulin_pump_provider: boolean;
}

interface CollegeCertification {
  college_certifications: string;
  license_number: string;
  license_class: string;
  renewal_date: string;
  advanced_practice?: string;
}
