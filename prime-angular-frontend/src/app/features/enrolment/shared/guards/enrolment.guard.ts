import { Injectable, Inject } from '@angular/core';
import { CanActivate, CanLoad, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, Router, Route, UrlSegment, UrlTree } from '@angular/router';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private authService: AuthService
  ) { }

  canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {

    return this.checkPermissions();
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.checkPermissions();
  }

  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.canActivate(next, state);
  }

  /**
   * Check permissions of the user, and attempt to redirect
   * to an appropriate destination on failure.
   *
   * @private
   * @returns {boolean}
   * @memberof EnrolmentGuard
   */
  private checkPermissions(): boolean {
    if (this.authService.isApplicant()) {
      return true;
    } else if (this.authService.isAdmin()) {
      // WARNING: Don't redirect if they are an admin instead let the
      // AdminRedirect guard manage the redirection, otherwise routes
      // called rapidly in quick succession, which causes conflicts!
      return false;
    }

    // Access has been denied
    this.router.navigate([this.config.routes.denied]);
    return false;
  }
}
