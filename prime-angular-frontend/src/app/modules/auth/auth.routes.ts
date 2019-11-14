import { BaseRoute } from '@core/models/base-route';

export class AuthRoutes extends BaseRoute {

  public static get MODULE_PATH(): string {
    return AuthRoutes.AUTH;
  }

  // Module
  public static AUTH = BaseRoute.MODULE_PATH;
  // Authentication
  public static INFO = 'info';
  public static ADMIN = 'admin';
}
