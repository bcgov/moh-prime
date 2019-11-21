import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolent.routes';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private router: Router
  ) {
    super(authService, logger);
  }

  /**
   * @description
   * Check an enrollee enrolment status, and attempt to redirect
   * to an appropriate destination based on its existence or
   * status.
   */
  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    return this.enrolmentResource.enrolments()
      .pipe(
        map((enrolment: Enrolment) => {
          // Store the enrolment for access throughout enrolment
          this.enrolmentService.enrolment$.next(enrolment);
          return this.routeDestination(routePath, enrolment);
        })
      );
  }

  /**
   * @description
   * Determine the route destination based on the enrolment status.
   */
  private routeDestination(routePath: string, enrolment: Enrolment) {
    // On login the enrollees will always be redirected to
    // the collection notice
    if (routePath.includes(EnrolmentRoutes.COLLECTION_NOTICE)) {
      return true;
    }

    // Otherwise, routes are directed based on enrolment status
    if (!enrolment) {
      return this.navigate(routePath, EnrolmentRoutes.PROFILE);
    } else if (enrolment) {
      switch (enrolment.currentStatus.status.code) {
        case EnrolmentStatus.IN_PROGRESS:
          const postEnrolmentRoutes = EnrolmentRoutes.postEnrolmentRoutes();
          const route = routePath.split('/').pop();

          return (postEnrolmentRoutes.includes(route))
            // Prevent access to post enrolment routes
            ? this.navigate(routePath, EnrolmentRoutes.PROFILE)
            // Otherwise, allow the route to resolve
            : true;
        case EnrolmentStatus.SUBMITTED:
          return this.navigate(routePath, EnrolmentRoutes.CONFIRMATION);
        case EnrolmentStatus.ADJUDICATED_APPROVED:
          return this.navigate(routePath, EnrolmentRoutes.ACCESS_AGREEMENT);
        // case EnrolmentStatus.DECLINED:
        case EnrolmentStatus.ACCEPTED_TOS:
          return this.navigate(routePath, EnrolmentRoutes.SUMMARY);
        // case EnrolmentStatus.DECLINED_TOS:
      }
    }

    // Otherwise, prevent the route from resolving
    return false;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route
   * only when the current route path is not the destination
   * path.
   */
  private navigate(routePath: string, destinationPath: string): boolean {
    const enrolmentRoutePath = this.config.routes.enrolment;

    if (routePath === `/${enrolmentRoutePath}/${destinationPath}`) {
      return true;
    } else {
      this.router.navigate([enrolmentRoutePath, destinationPath]);
      return false;
    }
  }
}
