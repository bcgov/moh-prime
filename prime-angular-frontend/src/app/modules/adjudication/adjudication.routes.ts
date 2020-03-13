export class AdjudicationRoutes {
  public static ADJUDICATION = 'adjudication';
  public static ENROLLEES = 'enrolments';
  public static ENROLLEE = 'enrolment';
  public static ENROLLEE_EVENTS = 'events';
  public static ENROLLEE_REVIEW_STATUS = 'review-status';
  public static ENROLLEE_PROFILE_HISTORY = 'history';
  public static ENROLLEE_TERMS_HISTORY = 'terms-of-access-history';
  public static ENROLLEE_LIMITS_CONDITIONS = 'limits-and-conditions';
  public static ENROLLEE_ADJUDICATOR_NOTES = 'notes';

  public static MODULE_PATH = AdjudicationRoutes.ADJUDICATION;

  public static routePath(route: string): string {
    return `/${AdjudicationRoutes.MODULE_PATH}/${route}`;
  }
}
