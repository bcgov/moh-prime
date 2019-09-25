import { Injectable, Inject } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RedirectGuard implements CanActivate {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private authService: AuthService
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.checkPermissions();
  }

  /**
   * Check permissions of the user, and attempt to redirect
   * to an appropriate destination when applicable.
   *
   * @private
   * @returns {boolean}
   * @memberof AdminRedirectGuard
   */
  private checkPermissions(): boolean {
    if (this.authService.isAdmin()) {
      this.router.navigate([this.config.routes.admin]);
      return false;
    }

    return true;
  }
}
