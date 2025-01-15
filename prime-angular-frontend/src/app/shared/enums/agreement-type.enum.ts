const COMMUNITY_PHARMACIST_TOA = 'Pharmacy Regulated User';
const REGULATED_USER_TOA = 'Regulated User';
const OBO_TOA = 'On Behalf Of User';
const COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT = 'Community Practice Organization';
const COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT = 'Community Pharmacy Organization';
const PHARMACY_OBO_TOA = 'Pharmacy On Behalf Of User';
const DEVICE_PROVIDER_ORG_AGREEMENT = 'Device Provider Organization';
const PHARMACY_TECHNICIAN_TOA = 'Pharmacy Technician Toa';
const LICENCED_PRACTICAL_NURSE_TOA = 'Licenced Practical Nurse Toa';
const DEVICE_PROVIDER_RU_TOA = 'Device Provider RU Toa';
const DEVICE_PROVIDER_OBO_TOA = 'Device Provider OBO Toa';
const PRESCRIBER_OBO_TOA = 'On Behalf Of User (can prescribe independently)';

export enum AgreementType {
  COMMUNITY_PHARMACIST_TOA = 1,
  REGULATED_USER_TOA,
  OBO_TOA,
  COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT,
  COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT,
  PHARMACY_OBO_TOA,
  DEVICE_PROVIDER_ORGANIZATION_AGREEMENT,
  PHARMACY_TECHNICIAN_TOA,
  LICENCED_PRACTICAL_NURSE_TOA,
  DEVICE_PROVIDER_RU_TOA,
  DEVICE_PROVIDER_OBO_TOA,
  PRESCRIBER_OBO_TOA
}

export const AgreementTypeNameMap: Record<AgreementType, string> = {
  [AgreementType.COMMUNITY_PHARMACIST_TOA]: COMMUNITY_PHARMACIST_TOA,
  [AgreementType.REGULATED_USER_TOA]: REGULATED_USER_TOA,
  [AgreementType.COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT]: COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT,
  [AgreementType.COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT]: COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT,
  [AgreementType.OBO_TOA]: OBO_TOA,
  [AgreementType.PHARMACY_OBO_TOA]: PHARMACY_OBO_TOA,
  [AgreementType.DEVICE_PROVIDER_ORGANIZATION_AGREEMENT]: DEVICE_PROVIDER_ORG_AGREEMENT,
  [AgreementType.PHARMACY_TECHNICIAN_TOA]: PHARMACY_TECHNICIAN_TOA,
  [AgreementType.LICENCED_PRACTICAL_NURSE_TOA]: LICENCED_PRACTICAL_NURSE_TOA,
  [AgreementType.DEVICE_PROVIDER_RU_TOA]: DEVICE_PROVIDER_RU_TOA,
  [AgreementType.DEVICE_PROVIDER_OBO_TOA]: DEVICE_PROVIDER_OBO_TOA,
  [AgreementType.PRESCRIBER_OBO_TOA]: PRESCRIBER_OBO_TOA,
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

export const termsOfAccessAgreements = [
  { type: 0, name: 'None' },
  { type: AgreementType.REGULATED_USER_TOA, name: 'RU' },
  { type: AgreementType.OBO_TOA, name: 'OBO' },
  { type: AgreementType.COMMUNITY_PHARMACIST_TOA, name: 'PharmRU' },
  { type: AgreementType.PHARMACY_OBO_TOA, name: 'PharmOBO' },
  { type: AgreementType.PHARMACY_TECHNICIAN_TOA, name: 'PharmTech' },
  { type: AgreementType.LICENCED_PRACTICAL_NURSE_TOA, name: 'LPNRU' },
  { type: AgreementType.DEVICE_PROVIDER_RU_TOA, name: 'DP RU' },
  { type: AgreementType.DEVICE_PROVIDER_OBO_TOA, name: 'DP OBO' },
  { type: AgreementType.PRESCRIBER_OBO_TOA, name: 'OBO Prescriber' },
];
