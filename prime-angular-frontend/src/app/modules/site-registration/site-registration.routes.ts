// TODO add titles and options for routes for routing module and views
export class SiteRoutes {
  public static SITE_REGISTRATION = 'site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';

  public static ORGANIZATIONS = 'organizations';

  public static ORGANIZATION_SIGNING_AUTHORITY = 'organization-signing-authority';
  public static ORGANIZATION_INFORMATION = 'organization-information';
  public static ORGANIZATION_TYPE = 'organization-type';
  public static ORGANIZATION_REVIEW = 'organization-review';
  public static ORGANIZATION_AGREEMENT = 'organization-agreement';

  public static SITES = 'sites';

  public static SITE_ADDRESS = 'site-address';
  public static BUSINESS_LICENCE = 'business-licence';
  public static HOURS_OPERATION = 'hours-operation';
  public static VENDOR = 'vendor';
  public static REMOTE_USERS = 'remote-users';
  public static ADMINISTRATOR = 'site-administrator';
  public static PRIVACY_OFFICER = 'privacy-officer';
  public static TECHNICAL_SUPPORT = 'technical-support';
  public static SITE_REVIEW = 'site-review';

  public static MODULE_PATH = SiteRoutes.SITE_REGISTRATION;

  /**
   * @description
   * Useful for redirecting to module root-level routes.
   */
  public static routePath(route: string): string {
    return `/${SiteRoutes.MODULE_PATH}/${route}`;
  }

  // Used to indicate the routes and order of registration for organizations
  public static organizationRegistrationRouteOrder(): string[] {
    return [
      this.ORGANIZATION_SIGNING_AUTHORITY,
      this.ORGANIZATION_INFORMATION,
      this.ORGANIZATION_TYPE,
      this.ORGANIZATION_REVIEW
    ];
  }

  // Used to indicate the routes and order of registration for sites
  public static siteRegistrationRouteOrder(): string[] {
    return [
      this.SITE_ADDRESS,
      this.BUSINESS_LICENCE,
      this.HOURS_OPERATION,
      this.VENDOR,
      this.REMOTE_USERS,
      this.ADMINISTRATOR,
      this.PRIVACY_OFFICER,
      this.TECHNICAL_SUPPORT,
      this.SITE_REVIEW
    ];
  }

  public static siteRegistrationRoutes(): string[] {
    return [
      ...this.organizationRegistrationRouteOrder(),
      ...this.siteRegistrationRouteOrder(),
      this.ORGANIZATION_AGREEMENT
    ];
  }
}
