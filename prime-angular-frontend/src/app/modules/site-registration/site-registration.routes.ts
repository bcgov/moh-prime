// TODO add titles and options for routes for routing module and views
export class SiteRoutes {
  public static SITE_REGISTRATION = 'site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';

  public static ORGANIZATIONS = 'organizations';
  public static ORGANIZATION_SIGNING_AUTHORITY = 'organization-signing-authority';
  public static ORGANIZATION_INFORMATION = 'organization-information';
  public static ORGANIZATION_TYPE = 'organization-type';
  public static ORGANIZATION_REVIEW = 'organization-review';
  public static ORGANIZATION_AGREEMENT = 'access-agreement';

  public static SITES = 'sites';
  public static SITE_ADDRESS = 'site-address';
  public static BUSINESS_LICENCE = 'business-licence';
  public static HOURS_OPERATION = 'hours-operation';
  public static VENDOR = 'vendor';
  // TODO remote user(s)
  public static ADMINISTRATOR = 'site-administrator';
  public static PRIVACY_OFFICER = 'privacy-officer';
  public static TECHNICAL_SUPPORT = 'technical-support';
  public static SITE_REVIEW = 'site-review';
  public static CONFIRMATION = 'confirmation';

  public static MODULE_PATH = SiteRoutes.SITE_REGISTRATION;

  /**
   * @description
   * Useful for redirecting to module root-level routes.
   */
  public static routePath(route: string): string {
    return `/${SiteRoutes.MODULE_PATH}/${route}`;
  }

  // TODO need to refactor route workflow for organizations and sites

  // Use by the progress indicator to calculate percent completion
  // of the registration process
  public static initialRegistrationRouteOrder(): string[] {
    return [
      // ...SiteRoutes.registrationRoutes()
    ];
  }

  // Registration routes are ordered from the perspective of an
  // "initial" registration. The order is important for directing the
  // user incrementally through creating their registration
  public static registrationRoutes(): string[] {
    return [
      // ...SiteRoutes.noOrganizationAgreementRoutes(),
      // SiteRoutes.HOURS_OPERATION,
      // SiteRoutes.VENDOR,
      // SiteRoutes.SIGNING_AUTHORITY,
      // SiteRoutes.ADMINISTRATOR,
      // SiteRoutes.PRIVACY_OFFICER,
      // SiteRoutes.TECHNICAL_SUPPORT,
      // SiteRoutes.SITE_REVIEW,
      // ...SiteRoutes.siteRegistrationSubmissionRoutes()
    ];
  }

  public static noOrganizationAgreementRoutes(): string[] {
    return [
      // SiteRoutes.MULTIPLE_SITES,
      // SiteRoutes.ORGANIZATION_INFORMATION,
      // SiteRoutes.BUSINESS_LICENCE,
      // SiteRoutes.SITE_ADDRESS,
      // SiteRoutes.ORGANIZATION_TYPE,
      // SiteRoutes.ORGANIZATION_AGREEMENT,
    ];
  }

  public static siteRegistrationSubmissionRoutes(): string[] {
    return [
      // SiteRoutes.CONFIRMATION
    ];
  }
}
