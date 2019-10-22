import { Injectable, Inject } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthTokenService } from '../services/auth-token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthRedirectGuard implements CanActivate {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private tokenService: AuthTokenService
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    return this.checkAuth();
  }

  /**
   * Check the authentication of the user.
   *
   * @private
   * @returns {(boolean | Observable<boolean>)}
   * @memberof AuthRedirectGuard
   */
  private checkAuth(): boolean | Observable<boolean> {
    if (this.tokenService.tokenHasNotExpired()) {
      // Already logged in
      this.router.navigate([this.config.routes.dashboard]);
      return false;
    }

    // Expired tokens should be removed to prevent tokens
    // being sent during re-authentication, which responds
    // with an HTTP error status code
    this.tokenService.removeToken();

    return true;
  }
}
