export class SiteRoutes {
  public static SITE_REGISTRATION = 'site-registration';
  public static COLLECTION_NOTICE = 'collection-notice';

  public static MODULE_PATH = SiteRoutes.SITE_REGISTRATION;


  public static routePath(route: string): string {
    return `/${SiteRoutes.MODULE_PATH}/${route}`;
  }
}
