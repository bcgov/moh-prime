export class LdapErrorResponse {
  constructor(
    public unauthorized: boolean | null,
    public locked: boolean | null
  ) {
    this.unauthorized = unauthorized;
    this.locked = locked;
  }
}
