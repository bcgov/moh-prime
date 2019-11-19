import { BaseRoute } from '@core/models/base-route';

export class AuthRoutes extends BaseRoute {
  public static AUTH = '';
  public static INFO = 'info';
  public static ADMIN = 'admin';

  public static MODULE_PATH = AuthRoutes.AUTH;
}
