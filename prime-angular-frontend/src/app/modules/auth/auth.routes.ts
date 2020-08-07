export class AuthRoutes {
  public static AUTH = '';
  public static INFO = 'info';
  public static ADMIN = 'admin';
  public static SITE = 'site';

  public static MODULE_PATH = AuthRoutes.AUTH;

  /**
   * @description
   * Useful for redirecting to module root-level routes.
   */
  public static routePath(route: string): string {
    return `/${AuthRoutes.MODULE_PATH}/${route}`;
  }
}
