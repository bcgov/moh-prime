import { route as login } from './shared/modules/gis-login/gis-login-routing.module';
import { route as ldapUser } from './pages/ldap-user-page/ldap-user-page-routing.module';
import { route as ldapInfo } from './pages/ldap-information-page/ldap-information-page-routing.module';
import { route as orgInfo } from './pages/organization-information-page/organization-information-page-routing.module';
import { route as enrolleeInfo } from './pages/enrollee-information-page/enrollee-information-page-routing.module';
import { route as submissionConfirmation } from './pages/submission-confirmation-page/submission-confirmation-page-routing.module';

export class GisEnrolmentRoutes {
  public static MODULE_PATH = 'gis-enrolment';

  public static LOGIN_PAGE = login;
  public static LDAP_USER_PAGE = ldapUser;
  public static LDAP_INFO_PAGE = ldapInfo;
  public static ORG_INFO_PAGE = orgInfo;
  public static ENROLLEE_INFO_PAGE = enrolleeInfo;
  public static SUBMISSION_CONFIRMATION = submissionConfirmation;

  public static routePath(route: string): string {
    return `/${GisEnrolmentRoutes.MODULE_PATH}/${route}`;
  }
}
