export class SiteRoutes {
  public static SITE_REGISTRATION = 'site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';

  // Kept as organizations so route paths indicate hierarchy of resources
  // TODO maybe don't make site management the root so paths make sense on site management
  public static SITE_MANAGEMENT = 'organizations';
  // TODO make this the roote path for organizations and sites
  // public static ORGANIZATIONS = 'organizations';

  public static ORGANIZATION_SIGNING_AUTHORITY = 'organization-signing-authority';
  public static ORGANIZATION_NAME = 'organization-name';
  public static ORGANIZATION_REVIEW = 'organization-review';
  public static ORGANIZATION_AGREEMENT = 'organization-agreement';

  public static SITES = 'sites';

  public static CARE_SETTING = 'care-setting';
  public static BUSINESS_LICENCE = 'business-licence';
  public static SITE_ADDRESS = 'site-address';
  public static HOURS_OPERATION = 'hours-operation';
  public static ADMINISTRATOR = 'site-administrator';
  public static PRIVACY_OFFICER = 'privacy-officer';
  public static TECHNICAL_SUPPORT = 'technical-support';
  public static REMOTE_USERS = 'remote-users';
  public static SITE_REVIEW = 'site-review';

  public static MODULE_PATH = SiteRoutes.SITE_REGISTRATION;

  /**
   * @description
   * Useful for redirecting to module root-level routes.
   */
  public static routePath(route: string): string {
    return `/${SiteRoutes.MODULE_PATH}/${route}`;
  }

  // Used to indicate the routes and order of registration for an initial
  // organization, and a subsequent associated site
  public static initialRegistrationRouteOrder(): string[] {
    return [
      ...this.organizationRegistrationRouteOrder(),
      ...this.siteRegistrationRouteOrder(),
      // Only occurs once when creating the organization
      this.ORGANIZATION_AGREEMENT
    ];
  }

  // Used to indicate the routes and order of registration for organizations
  public static organizationRegistrationRouteOrder(): string[] {
    return [
      this.COLLECTION_NOTICE,
      this.ORGANIZATION_SIGNING_AUTHORITY,
      this.ORGANIZATION_NAME
    ];
  }

  // Used to indicate the routes and order of registration for sites
  public static siteRegistrationRouteOrder(): string[] {
    return [
      this.CARE_SETTING,
      this.BUSINESS_LICENCE,
      this.SITE_ADDRESS,
      this.HOURS_OPERATION,
      this.ADMINISTRATOR,
      this.PRIVACY_OFFICER,
      this.TECHNICAL_SUPPORT,
      this.REMOTE_USERS
    ];
  }

  public static siteRegistrationRoutes(): string[] {
    return [
      ...this.organizationRegistrationRouteOrder(),
      ...this.siteRegistrationRouteOrder()
    ];
  }
}
