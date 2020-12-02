import { Injectable } from '@angular/core';
import {
  CanActivate, CanActivateChild, CanLoad, Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router
} from '@angular/router';

import { Observable } from 'rxjs';

import { AppRoutes } from 'app/app.routes';
import { UtilsService } from '@core/services/utils.service';

@Injectable({
  providedIn: 'root'
})
export class UnsupportedGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(
    private router: Router,
    private utilsService: UtilsService
  ) { }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.checkUnsupported();
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.canActivate(next, state);
  }

  public canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return this.checkUnsupported();
  }

  private checkUnsupported(): Observable<boolean> | Promise<boolean> | boolean {
    if (this.utilsService.isIEOrPreChromiumEdge()) {
      this.router.navigate([AppRoutes.UNSUPPORTED]);
      return false;
    }

    return true;
  }
}
