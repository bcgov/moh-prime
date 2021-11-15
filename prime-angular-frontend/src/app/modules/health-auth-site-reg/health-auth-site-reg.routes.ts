export class HealthAuthSiteRegRoutes {
  public static LOGIN_PAGE = 'health-authority';

  public static MODULE_PATH = 'health-authority-site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';

  public static ACCESS = 'access';
  public static ACCESS_AUTHORIZED_USER = 'authorized-user';
  public static ACCESS_REQUESTED = 'access-requested';
  public static ACCESS_APPROVED = 'access-approved';
  public static ACCESS_DECLINED = 'access-declined';

  public static AUTHORIZED_USER = 'authorized-user';
  public static SITE_MANAGEMENT = 'site-management';

  public static HEALTH_AUTHORITIES = 'health-authorities';
  public static SITES = 'sites';
  public static VENDOR = 'vendor';
  public static SITE_INFORMATION = 'site-information';
  public static HEALTH_AUTH_CARE_TYPE = 'health-auth-care-type';
  public static SITE_ADDRESS = 'site-address';
  public static HOURS_OPERATION = 'hours-operation';
  public static ADMINISTRATOR = 'administrator';
  public static TECHNICAL_SUPPORT = 'technical-support';
  public static SITE_OVERVIEW = 'site-overview';

  public static routePath(route: string): string {
    return `/${HealthAuthSiteRegRoutes.MODULE_PATH}/${route}`;
  }

  /**
   * @description
   * Used to indicate the routes and order of registration for sites.
   */
  public static siteRegistrationRouteOrder(): string[] {
    return [
      this.VENDOR,
      this.SITE_INFORMATION,
      this.HEALTH_AUTH_CARE_TYPE,
      this.SITE_ADDRESS,
      this.HOURS_OPERATION,
      this.ADMINISTRATOR,
      this.TECHNICAL_SUPPORT,
      this.SITE_OVERVIEW
    ];
  }

  /**
   * @description
   * Routes allowed when site registration is incomplete.
   */
  public static siteIsIncompleteRoutes(): string[] {
    return this.siteRegistrationRouteOrder();
  }

  /**
   * @description
   * Routes allowed when site registration is incomplete.
   */
  public static siteIsInReviewRoutes(): string[] {
    return [
      this.SITE_OVERVIEW
    ];
  }

  /**
   * @description
   * Routes allowed when site is locked.
   */
  public static siteIsLockedRoutes(): string[] {
    return this.siteIsInReviewRoutes();
  }

  /**
   * @description
   * Routes allowed when site is approved.
   */
  public static siteIsApprovedRoutes(): string[] {
    return [
      this.ADMINISTRATOR,
      this.TECHNICAL_SUPPORT,
      this.SITE_OVERVIEW
    ];
  }

  /**
   * @description
   * Routes allowed when site is within renewal period.
   */
  public static siteIsApprovedAndWithinRenewalPeriod(): string[] {
    return [
      this.SITE_INFORMATION,
      this.HEALTH_AUTH_CARE_TYPE,
      this.SITE_ADDRESS,
      this.HOURS_OPERATION,
      this.ADMINISTRATOR,
      this.TECHNICAL_SUPPORT,
      this.SITE_OVERVIEW
    ];
  }
}
