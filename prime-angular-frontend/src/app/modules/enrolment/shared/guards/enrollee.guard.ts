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
export class EnrolleeGuard extends BaseGuard {
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
    return new Promise((resolve, reject) => {
      let destinationRoute = this.config.routes.denied;

      if (!authenticated) {
        destinationRoute = this.config.routes.auth;
      } else if (this.permissionService.hasRoles(Role.ENROLLEE)) {
        // Allow route to resolve
        return resolve(true);
      } else if (this.permissionService.hasRoles(Role.ADMIN)) {
        destinationRoute = this.config.routes.adjudication;
      }

      // Otherwise, redirect to an appropriate destination
      this.router.navigate([destinationRoute]);
      return reject(false);
    });
  }
}
