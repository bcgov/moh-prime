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
  [AgreementType.COMMUNITY_PHARMACIST_TOA]: COMMUNITY_PHARMACIST_TOA,
  [AgreementType.REGULATED_USER_TOA]: REGULATED_USER_TOA,
  [AgreementType.COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT]: COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT,
  [AgreementType.COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT]: COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT,
  [AgreementType.OBO_TOA]: OBO_TOA,
  [AgreementType.PHARMACY_OBO_TOA]: PHARMACY_OBO_TOA
};

// Paper Enrolment
export enum PaperEnrolmentAgreementType {
  COMMUNITY_PHARMACIST_TOA = 1,
  REGULATED_USER_TOA,
  OBO_TOA,
  PHARMACY_OBO_TOA
}

export const PaperEnrolmentAgreementTypeNameMap: Record<PaperEnrolmentAgreementType, string> = {
  [PaperEnrolmentAgreementType.REGULATED_USER_TOA]: REGULATED_USER_TOA,
  [PaperEnrolmentAgreementType.COMMUNITY_PHARMACIST_TOA]: COMMUNITY_PHARMACIST_TOA,
  [PaperEnrolmentAgreementType.OBO_TOA]: OBO_TOA,
  [PaperEnrolmentAgreementType.PHARMACY_OBO_TOA]: PHARMACY_OBO_TOA
};

