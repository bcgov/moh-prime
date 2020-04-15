export class SiteRoutes {
  public static SITE_REGISTRATION = 'site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';
  public static MULTIPLE_SITES = 'multiple-sites';
  public static ORGANIZATION_INFORMATION = 'organization-information';
  public static SITE_ADDRESS = 'site-address';
  public static HOURS_OPERATION = 'hours-operation';
  public static VENDOR = 'vendor';
  public static SIGNING_AUTHORITY = 'signing-authority';
  public static ADMINISTRATOR = 'administrator';
  public static PRIVACY_OFFICER = 'privacy-officer';
  public static TECHNICAL_SUPPORT_CONTACT = 'technical-support-contact';
  public static SITE_REVIEW = 'site-review';
  public static CONFIRMATION = 'confirmation';

  public static MODULE_PATH = SiteRoutes.SITE_REGISTRATION;

  public static routePath(route: string): string {
    return `/${SiteRoutes.MODULE_PATH}/${route}`;
  }

  // Use by the progress indicator to calculate percent completion
  // of the registration process
  public static routeOrder(): string[] {
    return [
      SiteRoutes.SITE_REGISTRATION,
      SiteRoutes.COLLECTION_NOTICE,
      SiteRoutes.MULTIPLE_SITES,
      SiteRoutes.ORGANIZATION_INFORMATION,
      SiteRoutes.SITE_ADDRESS,
      SiteRoutes.HOURS_OPERATION,
      SiteRoutes.VENDOR,
      SiteRoutes.SIGNING_AUTHORITY,
      SiteRoutes.ADMINISTRATOR,
      SiteRoutes.PRIVACY_OFFICER,
      SiteRoutes.TECHNICAL_SUPPORT_CONTACT,
      SiteRoutes.SITE_REVIEW,
      SiteRoutes.CONFIRMATION
    ];
  }
}
