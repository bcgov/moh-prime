import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import {
  CanLoad, CanActivate, CanActivateChild, Route, UrlSegment,
  ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Params
} from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { Role } from '../enum/role.enum';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Observable, of } from 'rxjs';
import { KeycloakUtilsService } from '@core/services/keycloak-utils.service';
import { environment } from '@env/environment';
import { KeycloakOptions } from 'keycloak-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationRedirectGuard implements CanLoad {
  private authenticated: boolean;

  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    private permissionService: PermissionService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private keycloakUtil: KeycloakUtilsService,
  ) { }

  canLoad(route: Route, segments: UrlSegment[]): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    const url = this.getUrl(segments);
    // TODO pass params to checkAccess
    return this.checkAccess(url);
  }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const url = this.getUrl(state);
    return this.checkAccess(url, next.params);
  }

  /**
   * @description
   * Check the access of a user based on the resolution of a hook.
   */
  protected checkAccess(routePath: string = null, params?: Params): Observable<boolean> | Promise<boolean> {
    return new Promise(async (resolve, reject) => {
      try {
        this.authenticated = await this.isAuthenticated();
        const result = await this.canAccess(this.authenticated, routePath);
        resolve(result);
      } catch (error) {
        const destination = (routePath) ? ` to ${routePath} ` : ' ';
        const message = `Route access${destination}has been denied`;
        this.logger.error(message);
        reject(`${message}: ${error}`);
      }
    });
  }

  /**
   * @description
   * Attempt to redirect an authenticated user to an appropriate
   * destination when possible, otherwise prompt user to
   * authenticate.
   */
  protected canAccess(authenticated: boolean, routePath: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      if (!authenticated) {
        // Allow route to resolve for user to authenticate
        return resolve(true);
      }

      let destinationRoute = this.config.routes.denied;

      if (this.permissionService.hasAnyRole([Role.PHSA_LABTECH, Role.PHSA_IMMUNIZER])) {
        destinationRoute = this.config.routes.phsa;
      } else if (this.permissionService.hasRoles(Role.ENROLLEE)) {
        destinationRoute = (routePath.slice(1) === SiteRoutes.LOGIN_PAGE)
          ? this.config.routes.site
          : this.config.routes.enrolment;
      } else if (this.permissionService.hasRoles(Role.ADMIN)) {
        destinationRoute = this.config.routes.adjudication;
      }

      // Otherwise, redirect to an appropriate destination
      return resolve(this.navigate(routePath, destinationRoute));
    });
  }

  private async isAuthenticated(): Promise<boolean> {
    let authenticated = false;
    // Initialize Prime Keycloak and see if authenticated.
    await this.keycloakUtil.initialize(environment.keycloakConfig as KeycloakOptions);
    authenticated = await this.authService.isLoggedIn();

    // If not authenticated try Ministry of Health Keycloak
    if (!authenticated) {
      await this.keycloakUtil.initialize(environment.mohKeycloakConfig as KeycloakOptions);
      authenticated = await this.authService.isLoggedIn();
    }

    return authenticated;
  }

  /**
   * @description
   * Construct a common route URL.
   */
  // TODO needs updating to account for query params using router.parseUrl(...)
  private getUrl(routeParam: UrlSegment[] | RouterStateSnapshot): string {
    return (Array.isArray(routeParam))
      ? routeParam.reduce((path, segment) => `${path}/${segment.path}`, '')
      : routeParam.url;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(routePath: string, destinationPath: string): boolean {
    if (routePath === destinationPath) {
      return true;
    } else {
      this.router.navigate([destinationPath]);
      return false;
    }
  }
}
