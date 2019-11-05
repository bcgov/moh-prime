import { KeycloakOptions } from 'keycloak-angular';

import { environment } from '@env/environment';
import { LoggerService } from '@core/services/logger.service';
import { AuthService } from '@auth/shared/services/auth.service';

export class MockAuthService extends AuthService {
  constructor(
    protected logger: LoggerService
  ) {
    super(logger);

    this.init(environment.keycloakConfig as KeycloakOptions);
  }
}
