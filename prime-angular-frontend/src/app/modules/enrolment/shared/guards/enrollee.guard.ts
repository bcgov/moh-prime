import { Injectable, Inject } from '@angular/core';
import {
  CanActivateChild, ActivatedRouteSnapshot,
  RouterStateSnapshot, UrlTree, Router
} from '@angular/router';

import { Observable } from 'rxjs';

import { KeycloakAuthGuard } from 'keycloak-angular';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { Role } from '@auth/shared/enum/role.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolleeGuard extends KeycloakAuthGuard implements CanActivateChild {
  constructor(
    protected router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private logger: LoggerService
  ) {
    super(router, authService);
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.canActivate(next, state);
  }

  /**
   * @description
   * Check the access of the authenticated user, and
   * redirect to an appropriate destination.
   */
  public isAccessAllowed(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    return new Promise((resolve, reject) => {
      if (!this.authenticated) {
        this.router.navigate([this.config.routes.auth]);
        return reject(false);
      }

      if (this.authService.isUserInRole(Role.ENROLLEE)) {
        return resolve(true);
      } else if (
        this.authService.isUserInRole(Role.PROVISIONER) ||
        this.authService.isUserInRole(Role.ADMIN)
      ) {
        this.router.navigate([this.config.routes.provision]);
        return reject(false);
      }

      // Access has been denied
      this.router.navigate([this.config.routes.denied]);
      return reject(false);
    });
  }
}
