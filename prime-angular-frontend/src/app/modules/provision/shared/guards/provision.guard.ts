import { Injectable, Inject } from '@angular/core';
import {
  CanActivateChild, ActivatedRouteSnapshot,
  RouterStateSnapshot, UrlTree, Router
} from '@angular/router';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { Role } from '@auth/shared/enum/role.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProvisionGuard implements CanActivateChild {
  private authenticated: boolean;
  private roles: string[];

  constructor(
    protected router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private logger: LoggerService
  ) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    return new Promise(async (resolve, reject) => {
      try {
        this.authenticated = await this.authService.isLoggedIn();
        this.roles = await this.authService.getUserRoles(true);

        const result = await this.isAccessAllowed(route, state);
        resolve(result);
      } catch (error) {
        reject('An error happened during access validation. Details:' + error);
      }
    });
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

      if (
        this.authService.isUserInRole(Role.PROVISIONER) ||
        this.authService.isUserInRole(Role.ADMIN)
      ) {
        return resolve(true);
      }

      // Access has been denied
      this.router.navigate([this.config.routes.denied]);
      return reject(false);
    });
  }
}
