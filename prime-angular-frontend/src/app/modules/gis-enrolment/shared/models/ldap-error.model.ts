export interface LdapThrottlingParameters {
  remainingAttempts: number;
  lockoutTimeInHours: number;
}
