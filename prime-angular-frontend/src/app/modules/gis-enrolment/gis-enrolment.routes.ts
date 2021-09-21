export class GisEnrolmentRoutes {
  public static LOGIN_PAGE = 'gis';

  public static MODULE_PATH = 'gis-enrolment';
  public static COLLECTION_NOTICE = 'collection-notice';

  public static LDAP_USER_PAGE = 'ldap-user';
  public static LDAP_INFO_PAGE = 'ldap-info';
  public static ENROLLEE_INFO_PAGE = 'enrollee-info';
  public static SUBMISSION_CONFIRMATION = 'confirmation';

  public static routePath(route: string): string {
    return `/${ GisEnrolmentRoutes.MODULE_PATH }/${ route }`;
  }

  // Use by the progress indicator to calculate percent completion
  // of the enrolment process
  public static initialEnrolmentRouteOrder(): string[] {
    return [
      GisEnrolmentRoutes.LDAP_USER_PAGE,
      GisEnrolmentRoutes.LDAP_INFO_PAGE,
      GisEnrolmentRoutes.ENROLLEE_INFO_PAGE,
      GisEnrolmentRoutes.SUBMISSION_CONFIRMATION
    ];
  }
}
