export enum IdentityProviderEnum {
  BCSC = 'bcsc',
  IDIR = 'idir',
  IDIR_AAD = "idir_aad", // IDIR MFA
  BCEID = 'bceid',
  PHSA = 'phsa',
  BCSC_MOH = 'bcsc_prime'  // `kc_idp_hint` expected by MoH KeyCloak
}
