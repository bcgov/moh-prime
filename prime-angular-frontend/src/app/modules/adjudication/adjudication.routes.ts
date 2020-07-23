export class AdjudicationRoutes {
  public static ADJUDICATION = 'adjudication';
  public static ENROLLEES = 'enrollees';
  public static ENROLLEE_ENROLMENTS = 'enrolments';
  public static ENROLLEE_CURRENT_ENROLMENT = 'current-enrolment';
  public static ENROLLEE_ACCESS_TERM_ENROLMENT = 'terms-of-access-enrolment';
  public static ENROLLEE_ACCESS_TERM = 'terms-of-access';
  public static ENROLLEE_REVIEW = 'review';
  public static ENROLLEE_LIMITS_CONDITIONS = 'limits-and-conditions';
  public static ENROLLEE_ADJUDICATOR_NOTES = 'notes';
  public static ENROLLEE_EVENT_LOG = 'event-log';

  public static SITE_REGISTRATIONS = 'site-registrations';
  public static SITE_REGISTRATION = 'site-registration';
  public static SITE_ADJUDICATION = 'site-adjudication';

  public static ORGANIZATION_INFORMATION = 'organization-information';

  public static MODULE_PATH = AdjudicationRoutes.ADJUDICATION;

  public static routePath(route: string): string {
    return `/${AdjudicationRoutes.MODULE_PATH}/${route}`;
  }
}
