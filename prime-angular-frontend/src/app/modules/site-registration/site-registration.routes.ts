export class SiteRoutes {
  // TODO add titles for routes for routing module and views
  public static SITE_REGISTRATION = 'site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';
  public static MULTIPLE_SITES = 'multiple-sites';
  public static ORGANIZATION_INFORMATION = 'organization-information';
  public static ORGANIZATION_TYPE = 'organization-type';
  public static SITE_ADDRESS = 'site-address';
  public static ORGANIZATION_AGREEMENT = 'access-agreement';
  public static VENDOR = 'vendor';
  public static HOURS_OPERATION = 'hours-operation';
  public static SIGNING_AUTHORITY = 'signing-authority';
  public static ADMINISTRATOR = 'administrator';
  public static PRIVACY_OFFICER = 'privacy-officer';
  public static TECHNICAL_SUPPORT = 'technical-support';
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
      ...SiteRoutes.registrationRoutes()
    ];
  }

  // Registration routes are ordered from the perspective of an
  // "initial" registration. The order is important for directing the
  // user incrementally through creating their registration
  public static registrationRoutes(): string[] {
    return [
      ...SiteRoutes.noOrganizationAgreementRoutes(),
      SiteRoutes.HOURS_OPERATION,
      SiteRoutes.VENDOR,
      SiteRoutes.SIGNING_AUTHORITY,
      SiteRoutes.ADMINISTRATOR,
      SiteRoutes.PRIVACY_OFFICER,
      SiteRoutes.TECHNICAL_SUPPORT,
      SiteRoutes.SITE_REVIEW,
      ...SiteRoutes.siteRegistrationSubmissionRoutes()
    ];
  }

  public static noOrganizationAgreementRoutes(): string[] {
    return [
      SiteRoutes.MULTIPLE_SITES,
      SiteRoutes.ORGANIZATION_INFORMATION,
      SiteRoutes.SITE_ADDRESS,
      SiteRoutes.ORGANIZATION_TYPE,
      SiteRoutes.ORGANIZATION_AGREEMENT,
    ];
  }

  public static siteRegistrationSubmissionRoutes(): string[] {
    return [
      SiteRoutes.CONFIRMATION
    ];
  }
}
