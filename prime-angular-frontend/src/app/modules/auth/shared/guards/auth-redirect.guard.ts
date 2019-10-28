import { Injectable, Inject } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';

import { KeycloakAuthGuard, KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { Role } from '../enum/role.enum';

@Injectable({
  providedIn: 'root'
})
export class AuthRedirectGuard extends KeycloakAuthGuard implements CanActivate {
  constructor(
    protected router: Router,
    protected keycloakAngular: KeycloakService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private logger: LoggerService
  ) {
    super(router, keycloakAngular);
  }

  /**
   * @description
   * Check the access of the authenticated user.
   */
  public isAccessAllowed(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    return new Promise((resolve, reject) => {
      if (!this.authenticated) {
        this.keycloakAngular.login()
          .catch(e => this.logger.error(e));
        return reject(false);
      }

      if (this.keycloakAngular.isUserInRole(Role.ENROLLEE)) {
        this.router.navigate([this.config.routes.enrolment]);
        reject(false);
      } else if (
        this.keycloakAngular.isUserInRole(Role.PROVISIONER) ||
        this.keycloakAngular.isUserInRole(Role.ADMIN)
      ) {
        this.router.navigate([this.config.routes.provision]);
        reject(false);
      }

      resolve(true);
    });
  }
}
