export enum Role {
  ENROLLEE = 'prime_user',
  ADMIN = 'prime_admin',
  SUPER_ADMIN = 'prime_super_admin',
  READONLY_ADMIN = 'prime_readonly_admin',

  // Feature Flags
  FEATURE_SITE_REGISTRATION = 'feature_site_registration',
  FEATURE_COMMUNITY_PHARMACIST = 'feature_community_pharmacist',
  FEATURE_HEALTH_AUTHORITY = 'feature_health_authority',
  FEATURE_VC_ISSUANCE = 'feature_vc_issuance',
  FEATURE_SITE_PHARMACIST = 'feature_site_pharmacist'
}
