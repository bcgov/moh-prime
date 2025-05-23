import { Injectable } from '@angular/core';
import { Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Params } from '@angular/router';

import { Observable } from 'rxjs';

import { AuthService } from '@auth/shared/services/auth.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

@Injectable({
  providedIn: 'root'
})
export class BaseGuard  {
  private authenticated: boolean;

  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService
  ) { }

  public get isAuthenticated(): boolean {
    return this.authenticated;
  }

  public canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const url = this.getUrl(segments);
    return this.checkAccess(url);
  }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const url = this.getUrl(state);
    return this.checkAccess(url, next.params);
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const url = this.getUrl(state);
    return this.checkAccess(url, next.params);
  }

  /**
   * @description
   * Hook for customizing access, which defaults to validating
   * access based on authentication.
   */
  protected canAccess(authenticated: boolean, routePath: string = null, params?: Params): Promise<boolean> {
    return new Promise((resolve, reject) => (authenticated) ? resolve(true) : reject(false));
  }

  /**
   * @description
   * Check the access of a user based on the resolution of a hook.
   */
  protected checkAccess(routePath: string = null, params?: Params): Observable<boolean> | Promise<boolean> {
    return new Promise(async (resolve, reject) => {
      try {
        this.authenticated = await this.authService.isLoggedIn();
        const result = await this.canAccess(this.authenticated, routePath, params);
        resolve(result);
      } catch (error) {
        const destination = (routePath) ? ` to ${routePath} ` : ' ';
        const message = `Route access${destination}has been denied`;
        this.logger.warn(message);
        reject(`${message}: ${error}`);
      }
    });
  }

  /**
   * @description
   * Get the current route.
   *
   * NOTE: Only care about the second parameter to determine route
   * access, and assumes that all child routes are allowed.
   */
  protected routePath(routePath: string): string {
    return routePath.slice(1).split('/')[1];
  }

  /**
   * @description
   * Construct a common route URL.
   */
  private getUrl(routeParam: UrlSegment[] | RouterStateSnapshot): string {
    return (Array.isArray(routeParam))
      ? routeParam.reduce((path, segment) => `${path}/${segment.path}`, '')
      : routeParam.url;
  }
}
