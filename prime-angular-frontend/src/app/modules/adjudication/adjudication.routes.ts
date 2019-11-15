import { BaseRoute } from '@core/models/base-route';

export class AdjudicationRoutes extends BaseRoute {
  public static get MODULE_PATH(): string {
    return AdjudicationRoutes.ADJUDICATION;
  }

  // Module
  public static ADJUDICATION = 'adjudication';
  // Adjudication
  public static ENROLMENTS = 'enrolments';
}
