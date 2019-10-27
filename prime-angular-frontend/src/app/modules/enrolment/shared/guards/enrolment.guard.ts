import { Injectable, Inject } from '@angular/core';
import {
  CanActivate, CanLoad, CanActivateChild, ActivatedRouteSnapshot,
  RouterStateSnapshot, Router, Route, UrlSegment, UrlTree
} from '@angular/router';

import { Observable, EMPTY } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { EnrolmentResource } from '../services/enrolment-resource.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private enrolmentResource: EnrolmentResource
  ) { }

  public canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {

    return this.checkEnrolment();
  }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.checkEnrolment();
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.canActivate(next, state);
  }

  /**
   * @description
   * Check for an enrolment, and attempt to redirect
   * to an appropriate destination on failure.
   */
  private checkEnrolment(): Observable<boolean> {
    return this.enrolmentResource.enrolments()
      .pipe(
        map((enrolment: Enrolment) => {
          const routes = this.config.routes;

          if (!enrolment) {
            this.router.navigate([routes.enrolment, 'profile']);
          }

          if (enrolment) {
            switch (enrolment.currentStatus.status.code) {
              case EnrolmentStatus.IN_PROGRESS:
                // Allow access to the route and provide the enrolment
                return true;
              case EnrolmentStatus.SUBMITTED:
                // TODO: update to redirect to the actual status view
                this.router.navigate([routes.enrolment, 'confirmation']);
                break;
              // TODO: should there be more status based redirects?
              // case EnrolmentStatus.ADJUDICATED_APPROVED:
              // case EnrolmentStatus.DECLINED:
              // case EnrolmentStatus.ACCEPTED_TOS:
              // case EnrolmentStatus.DECLINED_TOS:
              //   this.router.navigate([routes.???, '...']);
              //   break;
            }
          }

          // Otherwise, prevent the route from resolving
          return false;
        })
      );
  }
}
