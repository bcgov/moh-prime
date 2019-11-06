import { Injectable } from '@angular/core';
import {
  CanActivate, CanActivateChild, CanLoad, Route, UrlSegment,
  ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree
} from '@angular/router';

import { Observable } from 'rxjs';

import { LoggerService } from '@core/services/logger.service';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseGuard implements CanActivate, CanActivateChild, CanLoad {
  private authenticated: boolean;

  constructor(
    protected authService: AuthService,
    protected logger: LoggerService
  ) { }

  public get isAuthenticated() {
    return this.authenticated;
  }

  public canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return this.checkAccess();
  }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.checkAccess();
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.checkAccess();
  }

  /**
   * @description
   * Hook for customizing access, which defaults to validating
   * access based on authentication.
   */
  protected canAccess(authenticated: boolean, roles: string[]): Promise<boolean> {
    return new Promise((resolve, reject) => (authenticated) ? resolve(true) : reject(false));
  }

  private checkAccess(): Promise<boolean> {
    return new Promise(async (resolve, reject) => {
      try {
        this.authenticated = await this.authService.isLoggedIn();
        const roles = this.authService.getUserRoles(true);
        const result = await this.canAccess(this.authenticated, roles);
        resolve(result);
      } catch (error) {
        const message = 'Error has occurred during access validation';
        this.logger.error(message, error);
        reject(`${message}. Details: ${error}`);
      }
    });
  }
}
