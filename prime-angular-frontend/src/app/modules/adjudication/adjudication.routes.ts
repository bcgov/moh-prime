export class AdjudicationRoutes {
  public static ADJUDICATION = 'adjudication';
  public static ENROLMENTS = 'enrolments';
  public static ENROLMENT = 'enrolment';
  public static PROFILE_HISTORY = 'history';
  public static ACCESS_TERMS = 'access-terms';

  public static MODULE_PATH = AdjudicationRoutes.ADJUDICATION;

  public static routePath(route: string): string {
    return `/${AdjudicationRoutes.MODULE_PATH}/${route}`;
  }
}
