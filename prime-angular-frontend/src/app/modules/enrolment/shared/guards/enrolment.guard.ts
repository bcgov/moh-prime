import { Injectable, Inject } from '@angular/core';
import {
  CanActivate, CanLoad, CanActivateChild, ActivatedRouteSnapshot,
  RouterStateSnapshot, Router, Route, UrlSegment, UrlTree
} from '@angular/router';

import { Observable } from 'rxjs';
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

    return this.checkEnrolment(route.path);
  }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const [segment] = next.url;

    return this.checkEnrolment(segment.path);
  }

  public canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return this.canActivate(next, state);
  }

  public navigate(routePath: string, navPath: string) {
    const routes = this.config.routes;
    if (routePath !== navPath) {
      this.router.navigate([routes.enrolment, navPath]);
    } else {
      return true;
    }
  }

  /**
   * @description
   * Check for an enrolment, and attempt to redirect to an appropriate
   * destination based on its existence or status.
   */
  // TODO: potentially expensive since it would be invoked on each route
  // TODO: export enrolment steps into an enum, and reuse for routes
  private checkEnrolment(routePath: string): Observable<boolean> {
    return this.enrolmentResource.enrolments()
      .pipe(
        map((enrolment: Enrolment) => {
          // return true;
          const routes = this.config.routes;
          const submittedRoutes: string[] = ['confirmation', 'agreement', 'summary'];

          if (!enrolment) {
            return this.navigate(routePath, 'profile');
          } else if (enrolment) {
            switch (enrolment.currentStatus.status.code) {
              // Allow access to the route and provide the enrolment
              case EnrolmentStatus.IN_PROGRESS:
                if (submittedRoutes.includes(routePath)) {
                  // Should not see till after submit, redirect to profile page
                  return this.navigate(routePath, 'profile');
                }
                return true;
              case EnrolmentStatus.SUBMITTED:
                return this.navigate(routePath, 'confirmation');
              case EnrolmentStatus.ADJUDICATED_APPROVED:
                return this.navigate(routePath, 'agreement');
              // case EnrolmentStatus.DECLINED:
              case EnrolmentStatus.ACCEPTED_TOS:
                return this.navigate(routePath, 'summary');
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
