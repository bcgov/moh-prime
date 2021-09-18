export class LdapErrorResponse {
  constructor(
    public unauthorized: boolean,
    public locked: boolean
  ) {
    this.unauthorized = unauthorized;
    this.locked = locked;
  }
}
