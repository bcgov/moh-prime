import { Injectable, Inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { Role } from '@auth/shared/enum/role.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolleeGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
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
  protected canAccess(authenticated: boolean, roles: string[], routePath: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      let destinationRoute = this.config.routes.denied;

      if (!authenticated) {
        destinationRoute = this.config.routes.auth;
      } else if (roles.includes(Role.ENROLLEE)) {
        // Allow route to resolve
        return resolve(true);
      } else if (roles.includes(Role.ADJUDICATOR) || roles.includes(Role.ADMIN)) {
        destinationRoute = this.config.routes.adjudication;
      }

      // Otherwise, redirect to an appropriate destination
      this.router.navigate([destinationRoute]);
      return reject(false);
    });
  }
}
