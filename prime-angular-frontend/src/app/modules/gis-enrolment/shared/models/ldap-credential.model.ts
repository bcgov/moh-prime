export interface LdapCredential {
  ldapUsername: string;
  ldapPassword: string;
}

export interface LdapThrottlingParameters {
  remainingAttempts: number;
  lockoutTimeInHours: number;
}
