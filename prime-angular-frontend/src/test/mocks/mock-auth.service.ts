import { AuthService } from '@auth/shared/services/auth.service';
import { LoggerService } from '@core/services/logger.service';
import { environment } from '@env/environment';
import { KeycloakOptions } from 'keycloak-angular';

export class MockAuthService extends AuthService {
  constructor(
    protected logger: LoggerService
  ) {
    super(logger);

    this.init(environment.keycloakConfig as KeycloakOptions);
  }
}
