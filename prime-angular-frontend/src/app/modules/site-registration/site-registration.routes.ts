export class SiteRoutes {
  public static SITE_REGISTRATION = 'site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';
  public static MULTIPLE_SITES = 'multiple-sites';
  public static SITE_INFORMATION = 'site-information';
  public static HOURS_OPERATION = 'hours-operation';
  public static VENDOR = 'vendor';
  public static SIGNING_AUTHORITY = 'signing-authority';

  public static MODULE_PATH = SiteRoutes.SITE_REGISTRATION;


  public static routePath(route: string): string {
    return `/${SiteRoutes.MODULE_PATH}/${route}`;
  }
}
