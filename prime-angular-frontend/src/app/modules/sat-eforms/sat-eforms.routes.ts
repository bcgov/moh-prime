export class SatEformsRoutes {
  public static LOGIN_PAGE = 'sat-eforms';

  public static MODULE_PATH = 'sat-eforms-enrolment';
  public static COLLECTION_NOTICE = 'collection-notice';

  public static ENROLMENTS = 'enrolments';

  public static DEMOGRAPHIC = 'demographic';
  public static REGULATORY = 'regulatory';
  public static SUBMISSION_CONFIRMATION = 'submission-confirmation';

  public static routePath(route: string): string {
    return `/${SatEformsRoutes.MODULE_PATH}/${route}`;
  }

  // Used to indicate the routes and order of eforms enrolment
  public static satEformsRouteOrder(): string[] {
    return [
      SatEformsRoutes.DEMOGRAPHIC,
      SatEformsRoutes.REGULATORY,
      SatEformsRoutes.SUBMISSION_CONFIRMATION
    ];
  }
}
