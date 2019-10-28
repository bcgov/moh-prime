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
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard extends KeycloakAuthGuard implements CanActivateChild {
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
        // Capture the user's current location, and provide it to
        // Keycloak to redirect the user to where they originated
        // once authenticated
        const options: KeycloakLoginOptions = {
          redirectUri: state.url
        };

        this.keycloakAngular.login(options)
          .catch(e => this.logger.error(e));

        return reject(false);
      }

      return resolve(true);
    });
  }

  /**
   * Check authentication of the user.
   *
   * @private
   * @param {string} url
   * @returns {boolean}
   * @memberof AuthGuard
   */
  private checkAuth(url?: string): boolean {
    if (this.authService.isLoggedIn()) {
      // Already logged in
      return true;
    }

    // Capture the user's current location, and store it in the
    // URL for redirecting back to where the originated before
    // they were determined to be unauthenticated
    // TODO: add to utils service for managing unauthenticated users
    const navigationExtras: NavigationExtras = (url)
      ? { queryParams: { redirectUrl: this.decodeUrl(url) } }
      : {};

    // Navigate to the login page with redirect URL
    this.router.navigate([this.config.routes.auth], navigationExtras);
    return false;
  }

  /**
   * Decode the URL.
   *
   * @param {string} url
   * @returns {string}
   */
  private decodeUrl(url: string): string {
    try {
      return decodeURI(url);
    } catch (e) {
      return '/';
    }
  }
}
