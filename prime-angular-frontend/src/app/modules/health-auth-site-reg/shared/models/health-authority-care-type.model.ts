import { HealthAuthorityVendor } from './health-authority-vendor.model';

export interface HealthAuthorityCareType {
  id: number;
  healthAuthorityOrganizationId: number;
  careType: string;
  vendors: HealthAuthorityVendor[];
}
