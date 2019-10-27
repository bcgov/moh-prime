import { Injectable, Inject } from '@angular/core';
import {
  CanActivate, CanActivateChild, CanLoad, Route, UrlSegment,
  ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router
} from '@angular/router';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProvisionGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private authService: AuthService
  ) { }

  public canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {

    return this.checkPermissions();
  }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.checkPermissions();
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.canActivate(next, state);
  }

  /**
   * @description
   * Check permissions of the use, and attempt to redirect
   * to an appropriate destination on failure.
   */
  private async checkPermissions(): Promise<boolean> {
    if (this.authService.isProvisioner() || this.authService.isAdmin()) {
      return true;
    } else if (await this.authService.isEnrollee()) {
      // WARNING: Don't redirect if they are an admin instead let the
      // AdminRedirect guard manage the redirection, otherwise routes
      // are called rapidly in quick succession, which causes conflicts!
      return false;
    }

    // Access has been denied
    this.router.navigate([this.config.routes.denied]);
    return false;
  }
}
