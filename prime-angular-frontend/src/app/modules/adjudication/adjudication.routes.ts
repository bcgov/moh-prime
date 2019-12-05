export class AdjudicationRoutes {
  public static ADJUDICATION = 'adjudication';
  public static ENROLMENTS = 'enrolments';

  public static MODULE_PATH = AdjudicationRoutes.ADJUDICATION;

  public static routePath(route: string): string {
    return `/${AdjudicationRoutes.MODULE_PATH}/${route}`;
  }
}
