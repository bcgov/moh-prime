import { Injectable } from '@angular/core';
import { Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppRoutes } from 'app/app.routes';
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

@Injectable({
  providedIn: 'root'
})
export class IdentityAssuranceLevelGuard {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.checkIdentityAssuranceLevel();
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.canActivate(next, state);
  }

  public canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return this.checkIdentityAssuranceLevel();
  }

  private checkIdentityAssuranceLevel(): Observable<boolean> | Promise<boolean> | boolean {
    return this.authService.getUser$()
      .pipe(
        map((user: BcscUser) => user.identityAssuranceLevel),
        map((identityAssuranceLevel: number) => {
          return identityAssuranceLevel === undefined || identityAssuranceLevel < 3;
        }),
        map((unauthorized: boolean) => {
          if (unauthorized) {
            this.router.navigate([AppRoutes.IDENTITY_ASSURANCE_LEVEL]);
            return false;
          }

          return true;
        })
      );
  }
}
