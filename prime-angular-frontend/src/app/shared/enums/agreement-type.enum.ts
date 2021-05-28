export enum AgreementType {
  COMMUNITY_PHARMACIST_TOA = 1,
  REGULATED_USER_TOA,
  OBO_TOA,
  COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT,
  COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT,
  PHARMACY_OBO_TOA
}

export const AgreementTypeNameMap: Record<AgreementType, string> = {
  [AgreementType.COMMUNITY_PHARMACIST_TOA]: 'Pharma Regulated Users',
  [AgreementType.REGULATED_USER_TOA]: 'Regulated Users',
  [AgreementType.OBO_TOA]: 'On Behalf Of',
  [AgreementType.COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT]: 'Community Practice Organization',
  [AgreementType.COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT]: 'Community Pharmacy Organization',
  [AgreementType.PHARMACY_OBO_TOA]: 'Pharma On Behalf Of'
};
