import { Injectable, Inject } from '@angular/core';
import {
  CanLoad, CanActivate, CanActivateChild,
  ActivatedRouteSnapshot, RouterStateSnapshot,
  Route, Router, NavigationExtras
} from '@angular/router';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanLoad, CanActivate, CanActivateChild {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private authService: AuthService
  ) { }

  canLoad(route: Route): boolean | Observable<boolean> | Promise<boolean> {

    return this.checkAuth();
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    const url: string = state.url;

    return this.checkAuth(url);
  }

  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    return this.canActivate(next, state);
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
