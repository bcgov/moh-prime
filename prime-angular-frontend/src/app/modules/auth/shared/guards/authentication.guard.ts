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
import { environment } from '@env/environment';

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
      if (this.authenticated) {
        // Allow current route access
        return resolve(true);
      }

      const routes = this.config.routes;
      const adminRoutes = [routes.provision, routes.admin];
      const moduleRoutes = [routes.enrolment, ...adminRoutes];
      const targetModule = state.url.slice(1).split('/').shift();

      // Attempt to directly redirect the user to authenticate
      // using Keycloak, otherwise redirect to the enrollee
      // authentication
      if (moduleRoutes.includes(targetModule)) {
        // Capture the user's current location, and provide it to
        // Keycloak to redirect the user to where they originated
        // once authenticated
        const redirectUri = `${environment.loginRedirectUrl}${state.url}`;
        const idpHint = (adminRoutes.includes(targetModule))
          ? AuthProvider.IDIR
          : AuthProvider.BCSC;
        const options: KeycloakLoginOptions = {
          redirectUri,
          idpHint
        };

        this.keycloakAngular.login(options)
          .catch((e) => {
            this.logger.error(e);
            this.router.navigate([this.config.routes.auth]);
          });
      } else {
        this.router.navigate([this.config.routes.auth]);
      }

      return reject(false);
    });
  }
}
