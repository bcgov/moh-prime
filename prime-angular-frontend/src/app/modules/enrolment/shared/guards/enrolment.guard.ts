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
          const routes = this.config.routes;

          if (!enrolment) {
            if (routePath !== 'profile') {
              this.router.navigate([routes.enrolment, 'profile']);
            } else {
              return true;
            }
          } else if (enrolment) {
            switch (enrolment.currentStatus.status.code) {
              case EnrolmentStatus.IN_PROGRESS:
                // Allow access to the route and provide the enrolment
                return true;
              case EnrolmentStatus.SUBMITTED:
                // TODO: update to redirect to the actual status view
                this.router.navigate([routes.enrolment, 'confirmation']);
                break;
              case EnrolmentStatus.ADJUDICATED_APPROVED:
                this.router.navigate([routes.enrolment, 'agreement']);
                break;
              // case EnrolmentStatus.DECLINED:
              case EnrolmentStatus.ACCEPTED_TOS:
                this.router.navigate([routes.enrolment, 'summary']);
                break;
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
