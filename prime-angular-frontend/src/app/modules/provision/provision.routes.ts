import { BaseRoute } from '@core/models/base-route';

export class ProvisionRoutes extends BaseRoute {
  public static get MODULE_PATH(): string {
    return ProvisionRoutes.PROVISION;
  }

  // Module
  public static PROVISION = 'provision';
  // Adjudication
  public static ENROLMENTS = 'enrolments';
}
