const COMMUNITY_PHARMACIST_TOA = 'Pharmacy Regulated User';
const REGULATED_USER_TOA = 'Regulated User';
const OBO_TOA = 'On Behalf Of User';
const COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT = 'Community Practice Organization';
const COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT = 'Community Pharmacy Organization';
const PHARMACY_OBO_TOA = 'Pharmacy On Behalf Of User';

export enum AgreementType {
  COMMUNITY_PHARMACIST_TOA = 1,
  REGULATED_USER_TOA,
  OBO_TOA,
  COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT,
  COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT,
  PHARMACY_OBO_TOA
}


export const AgreementTypeNameMap: Record<AgreementType, string> = {
  [AgreementType.COMMUNITY_PHARMACIST_TOA]: 'Pharmacy Regulated User',
  [AgreementType.REGULATED_USER_TOA]: 'Regulated User',
  [AgreementType.COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT]: 'Community Practice Organization',
  [AgreementType.COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT]: 'Community Pharmacy Organization',
  [AgreementType.OBO_TOA]: 'On Behalf Of User',
  [AgreementType.PHARMACY_OBO_TOA]: 'Pharmacy On Behalf Of User'
};

// Paper Enrolment
export enum AgreementTypePaperEnrolment {
  COMMUNITY_PHARMACIST_TOA = 1,
  REGULATED_USER_TOA,
  OBO_TOA,
  PHARMACY_OBO_TOA
}

export const AgreementTypeNameMapPaperEnrolment: Record<AgreementTypePaperEnrolment, string> = {
  [AgreementTypePaperEnrolment.REGULATED_USER_TOA]: 'Regulated User',
  [AgreementTypePaperEnrolment.COMMUNITY_PHARMACIST_TOA]: 'Pharmacy Regulated User',
  [AgreementTypePaperEnrolment.OBO_TOA]: 'On-behalf-of User',
  [AgreementTypePaperEnrolment.PHARMACY_OBO_TOA]: 'Pharmacy On-behalf-of User'
};

