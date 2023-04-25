export interface HealthAuthorityVendor {
  id: number;
  // One of `healthAuthorityOrganizationId` or `healthAuthorityCareTypeId`
  // will be set, depending on the context
  healthAuthorityOrganizationId?: number;
  healthAuthorityCareTypeId?: number;
  vendorCode: number;
}
