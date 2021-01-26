export class AdjudicationRoutes {
  public static ADJUDICATION = 'adjudication';
  public static METABASE_REPORTS = 'metabase-reports';

  public static ENROLLEES = 'enrollees';
  public static ENROLLEE_ENROLMENTS = 'enrolments';
  public static ENROLLEE_CURRENT_ENROLMENT = 'current-enrolment';
  public static ENROLLEE_ACCESS_TERM_ENROLMENT = 'terms-of-access-enrolment';
  public static ENROLLEE_ACCESS_TERM = 'terms-of-access';
  public static ENROLLEE_REVIEW = 'review';
  public static ENROLLEE_LIMITS_CONDITIONS = 'limits-and-conditions';
  public static ADJUDICATOR_NOTES = 'notes';
  public static EVENT_LOG = 'event-log';
  public static DOCUMENT_UPLOAD = 'documents';
  public static ENROLLEE_OVERVIEW = 'overview';

  // SITE_REGISTRATIONS is an alias for ORGANIZATIONS in the routing hierarchy
  public static SITE_REGISTRATIONS = 'site-registrations';
  // SITE_REGISTRATION is an alias for SITES in the routing hierarchy
  public static SITE_REGISTRATION = 'site-overview';

  public static MODULE_PATH = AdjudicationRoutes.ADJUDICATION;

  public static routePath(route: string): string {
    return `/${AdjudicationRoutes.MODULE_PATH}/${route}`;
  }
}
