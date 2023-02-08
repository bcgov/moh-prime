import { IStep } from '@shared/components/progress-indicator/progress-indicator.component';

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
  public static SITE_INFORMATION = 'site-information';
  public static HEALTH_AUTH_CARE_TYPE = 'health-auth-care-type';
  public static HOURS_OPERATION = 'hours-operation';
  public static ADMINISTRATOR = 'administrator';
  public static SITE_OVERVIEW = 'site-overview';

  public static STEP_CARE_TYPE = 'Care Type';
  public static STEP_SITE_INFORMATION = 'Site Details';
  public static STEP_HOURS_OPERATION = 'Hours';
  public static STEP_SUPPORT = 'Support';
  public static STEP_OVERVIEW = 'Review';
  public static STEP_COMPLETE = 'Completed';

  public static routePath(route: string): string {
    return `/${HealthAuthSiteRegRoutes.MODULE_PATH}/${route}`;
  }

  /**
   * @description
   * Used to indicate the routes and order of registration for sites.
   */
  public static siteRegistrationRouteOrder(): string[] {
    return [
      this.HEALTH_AUTH_CARE_TYPE,
      this.SITE_INFORMATION,
      this.HOURS_OPERATION,
      this.ADMINISTRATOR,
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
      this.HOURS_OPERATION,
      this.ADMINISTRATOR,
      this.SITE_OVERVIEW
    ];
  }

  /**
   * @description
   * Mapping between routes and step text
   */
  public static siteSteps(): IStep[] {
    return [
      { routes: [this.HEALTH_AUTH_CARE_TYPE], step: this.STEP_CARE_TYPE },
      { routes: [this.SITE_INFORMATION], step: this.STEP_SITE_INFORMATION },
      { routes: [this.HOURS_OPERATION], step: this.STEP_HOURS_OPERATION },
      { routes: [this.ADMINISTRATOR], step: this.STEP_SUPPORT },
      { routes: [this.SITE_OVERVIEW], step: this.STEP_OVERVIEW },
      { routes: [''], step: this.STEP_COMPLETE },
    ];
  }
}
