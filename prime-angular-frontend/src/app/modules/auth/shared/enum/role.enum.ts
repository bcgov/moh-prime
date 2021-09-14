export enum Role {
  ENROLLEE = 'prime_user',
  ADMIN = 'prime_administrant',
  SUPER_ADMIN = 'prime_super_admin',
  MAINTENANCE = 'prime_maintenance',

  PHSA_LABTECH = 'phsa_eforms_labtech',
  PHSA_IMMUNIZER = 'phsa_eforms_immunizer_covid19',

  // Feature Flags
  FEATURE_VC_ISSUANCE = 'feature_vc_issuance',
  FEATURE_SITE_PHARMACIST = 'feature_site_pharmacist',
  FEATURE_SITE_DEVICE_PROVIDER = 'feature_site_device_provider',

  VIEW_ENROLLEE = 'enrollee_view',
  TRIAGE_ENROLLEE = 'enrollee_triage',
  APPROVE_ENROLLEE = 'enrollee_approve',
  MANAGE_ENROLLEE = 'enrollee_elevated_management',
  VIEW_SITE = 'site_view',
  EDIT_SITE = 'site_edit'
}
