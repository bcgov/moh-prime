export class LdapErrorResponse {
  constructor(
    public remainingAttempts: number
  ) {
    this.remainingAttempts = remainingAttempts;
  }
}
