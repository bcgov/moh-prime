export class VaccinationsRoutes {
  public static LOGIN_PAGE = 'vaccination';

  public static MODULE_PATH = 'vaccinations';
  public static CREDENTIALS = 'credentials';
  public static ISSUANCE = 'issuance';

  /**
   * @description
   * Useful for redirecting to module root-level routes.
   */
  public static routePath(route: string): string {
    return `/${VaccinationsRoutes.MODULE_PATH}/${route}`;
  }

  // Used to indicate the routes and order of registration for organizations
  public static organizationRegistrationRouteOrder(): string[] {
    return [
      this.CREDENTIALS,
      this.ISSUANCE
    ];
  }
}
