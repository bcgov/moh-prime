import { Injectable, Inject } from '@angular/core';
import {
  CanActivateChild, ActivatedRouteSnapshot,
  RouterStateSnapshot, UrlTree, Router
} from '@angular/router';

import { Observable } from 'rxjs';

import { KeycloakLoginOptions } from 'keycloak-js';
import { KeycloakAuthGuard, KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard extends KeycloakAuthGuard implements CanActivateChild {
  constructor(
    protected router: Router,
    protected keycloakAngular: KeycloakService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private logger: LoggerService
  ) {
    super(router, keycloakAngular);
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
        const routes = this.config.routes;
        const adminRoutes = [routes.provision, routes.admin];
        const moduleRoutes = [routes.enrolment, ...adminRoutes];

        const redirectUri = state.url;
        const modulePath = redirectUri.slice(1).split('/').shift();

        // Attempt to directly redirect the user to authenticate
        // using Keycloak, otherwise redirect to the enrollee
        // authentication
        if (moduleRoutes.includes(modulePath)) {
          const idpHint = (adminRoutes.includes(modulePath))
            ? AuthProvider.IDIR
            : AuthProvider.BCSC;

          // Capture the user's current location, and provide it to
          // Keycloak to redirect the user to where they originated
          // once authenticated
          // TODO: does the redirect URI need to be fully qualified?
          const options: KeycloakLoginOptions = {
            redirectUri,
            idpHint
          };

          this.keycloakAngular.login(options)
            .catch(e => this.logger.error(e));
        } else {
          this.router.navigate([this.config.routes.auth]);
        }

        return reject(false);
      }

      // Otherwise, allow current route access
      return resolve(true);
    });
  }
}
