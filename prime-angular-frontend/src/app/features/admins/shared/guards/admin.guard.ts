import { Injectable, Inject } from '@angular/core';
import { CanActivate, CanLoad, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, Router, Route } from '@angular/router';
import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanLoad, CanActivate, CanActivateChild {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private authService: AuthService
  ) { }

  canLoad(route: Route): boolean | Observable<boolean> | Promise<boolean> {

    return this.checkPermissions();
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    return this.checkPermissions();
  }

  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    return this.canActivate(next, state);
  }

  /**
   * Check permissions of the user, and attempt to redirect
   * to an appropriate destination on failure.
   *
   * @private
   * @returns {boolean}
   * @memberof AdminGuard
   */
  private checkPermissions(): boolean {
    if (this.authService.isAdmin()) {
      return true;
    }

    // Access has been denied
    this.router.navigate([this.config.routes.denied]);
    return false;
  }
}
