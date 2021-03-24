export interface IRoutes { }

export class GisEnrolmentRoutes implements IRoutes {
  public static LOGIN_PAGE = 'gis';

  public static MODULE_PATH = 'gis-enrolment';
  public static LDAP_USER_PAGE = 'ldap-user';
  public static LDAP_INFO_PAGE = 'ldap-info';
  public static ORG_INFO_PAGE = 'org-info';
  public static ENROLLEE_INFO_PAGE = 'enrollee-info';
  public static SUBMISSION_CONFIRMATION = 'confirmation';

  public static routePath(route: string): string {
    return `/${GisEnrolmentRoutes.MODULE_PATH}/${route}`;
  }
}
