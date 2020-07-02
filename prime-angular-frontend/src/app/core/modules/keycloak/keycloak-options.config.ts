import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { SiteRoutes } from '@registration/site-registration.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AuthRoutes } from '@auth/auth.routes';

export class KeycloakOptionsConfig {
  public readonly defaultClientId: string;
  public readonly clientConfig = [
    {
      clientId: 'prime-application-enrolment',
      routes: [
        AuthRoutes.INFO,
        EnrolmentRoutes.MODULE_PATH
      ]
    },
    {
      clientId: 'prime-application-site',
      routes: [
        AuthRoutes.SITE,
        SiteRoutes.MODULE_PATH
      ]
    },
    {
      clientId: 'prime-application-admin',
      routes: [
        AuthRoutes.ADMIN,
        AdjudicationRoutes.MODULE_PATH
      ]
    }
  ];

  constructor() {
    this.defaultClientId = this.clientConfig[0].clientId;
  }
}
