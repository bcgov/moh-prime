import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Role } from '@auth/shared/enum/role.enum';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';

@Injectable({
  providedIn: 'root'
})
export class RegistrantGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    private permissionService: PermissionService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router
  ) {
    super(authService, logger);
  }

  /**
   * @description
   * Check the user is authenticated, otherwise redirect
   * them to an appropriate destination.
   */
  protected canAccess(authenticated: boolean, routePath: string): Promise<boolean> {
    return new Promise(async (resolve, reject) => {
      if (authenticated && this.permissionService.hasRoles(Role.ENROLLEE)) {
        return resolve(true);
      }

      const destinationRoute = authenticated
        ? this.config.routes.denied // Deny if authenticated but not a prime_user, i.e. adjudicator, BCeID, etc.
        : this.config.routes.auth;

      this.router.navigate([destinationRoute]);
      return reject(false);
    });
  }
}
