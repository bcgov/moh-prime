import { Injectable } from '@angular/core';
import { Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import * as moment from 'moment';

import { MINIMUM_AGE } from '@lib/constants';

import { AppRoutes } from 'app/app.routes';
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

@Injectable({
  providedIn: 'root'
})
export class UnderagedGuard  {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.checkAge();
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.canActivate(next, state);
  }

  public canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return this.checkAge();
  }

  private checkAge(): Observable<boolean> | Promise<boolean> | boolean {
    return this.authService.getUser$()
      .pipe(
        map((user: BcscUser) => moment(user.dateOfBirth, 'YYYY-MM-DD')),
        map((dateOfBirth: moment.Moment) => moment().diff(dateOfBirth, 'years')),
        map((age: number) => age < MINIMUM_AGE),
        map((isUnderAged: boolean) => {
          if (isUnderAged) {
            this.router.navigate([AppRoutes.UNDERAGED]);
            return false;
          }

          return true;
        })
      );
  }
}
