export enum Role {
  ENROLLEE = 'prime_user',
  ADMIN = 'prime_admin',
  SUPER_ADMIN = 'prime_super_admin',
  READONLY_ADMIN = 'prime_readonly_admin',

  PHSA_LABTECH = 'phsa_eforms_labtech',
  PHSA_IMMUNIZER = 'phsa_eforms_immunizer_covid19',

  // Feature Flags
  FEATURE_VC_ISSUANCE = 'feature_vc_issuance',
  FEATURE_SITE_PHARMACIST = 'feature_site_pharmacist',
}
