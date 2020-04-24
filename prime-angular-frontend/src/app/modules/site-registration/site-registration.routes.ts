export class SiteRoutes {
  public static SITE_REGISTRATION = 'site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';
  public static MULTIPLE_SITES = 'multiple-sites';
  public static ORGANIZATION_INFORMATION = 'organization-information';
  public static SITE_ADDRESS = 'site-address';
  public static ORGANIZATION_AGREEMENT = 'access-agreement';
  public static HOURS_OPERATION = 'hours-operation';
  public static VENDORS = 'vendor';
  public static SIGNING_AUTHORITY = 'signing-authority';
  public static ADMINISTRATOR = 'administrator';
  public static PRIVACY_OFFICER = 'privacy-officer';
  public static TECHNICAL_SUPPORT = 'technical-support-contact';
  public static SITE_REVIEW = 'site-review';
  public static CONFIRMATION = 'confirmation';

  public static MODULE_PATH = SiteRoutes.SITE_REGISTRATION;

  public static routePath(route: string): string {
    return `/${SiteRoutes.MODULE_PATH}/${route}`;
  }

  // Use by the progress indicator to calculate percent completion
  // of the registration process
  public static initialRegistrationRouteOrder(): string[] {
    return [
      SiteRoutes.MULTIPLE_SITES,
      SiteRoutes.ORGANIZATION_INFORMATION,
      SiteRoutes.SITE_ADDRESS,
      SiteRoutes.ORGANIZATION_AGREEMENT,
      SiteRoutes.HOURS_OPERATION,
      SiteRoutes.VENDORS,
      SiteRoutes.SIGNING_AUTHORITY,
      SiteRoutes.ADMINISTRATOR,
      SiteRoutes.PRIVACY_OFFICER,
      SiteRoutes.TECHNICAL_SUPPORT,
      SiteRoutes.SITE_REVIEW,
      SiteRoutes.CONFIRMATION
    ];
  }
}
