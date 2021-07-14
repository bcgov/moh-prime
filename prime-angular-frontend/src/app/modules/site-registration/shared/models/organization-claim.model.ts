// TODO: Somehow combine with OrganizationClaimFormModel?
export interface OrganizationClaim {
  id: number;
  newSigningAuthorityId: number;
  organizationId: number;
  details: string;
}
