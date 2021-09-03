export class LdapErrorResponse {
  constructor(
    public remainingAttempts: number,
    public lockoutTimeInHours: number
  ) {
    this.remainingAttempts = remainingAttempts;
    this.lockoutTimeInHours = lockoutTimeInHours;
  }
}
