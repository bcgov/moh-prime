import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, CanLoad, Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Configuration } from '@config/config.model';
import { ConfigService } from '@config/config.service';

@Injectable({
  providedIn: 'root'
})
export class ConfigGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(
    private configService: ConfigService
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.getConfiguration();
  }

  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.getConfiguration();
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return this.getConfiguration();
  }

  /**
   * @description
   * Resolves the run-time configuration outside of initialization
   * of the application since the configuration endpoint is an
   * authenticated endpoint, and is placed appropriately to
   * guarantee authentication.
   */
  private getConfiguration(): Observable<boolean> {
    return this.configService.load()
      .pipe(
        map((config: Configuration) => !!config)
      );
  }
}
