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
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolmentResource: EnrolmentResource,
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
  // TODO: expensive since invoked on each route in guard and component
  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    return this.enrolmentResource.enrolments()
      .pipe(
        map((enrolment: Enrolment) => {
          if (!enrolment) {
            return this.navigate(routePath, 'profile');
          } else if (enrolment) {
            switch (enrolment.currentStatus.status.code) {
              case EnrolmentStatus.IN_PROGRESS:
                const postEnrolmentRoutes = ['confirmation', 'agreement', 'summary'];
                return (postEnrolmentRoutes.includes(routePath))
                  // Prevent access to post enrolment routes
                  ? this.navigate(routePath, 'profile')
                  // Otherwise, allow the route to resolve
                  : true;
              case EnrolmentStatus.SUBMITTED:
                return this.navigate(routePath, 'confirmation');
              case EnrolmentStatus.ADJUDICATED_APPROVED:
                return this.navigate(routePath, 'agreement');
              // case EnrolmentStatus.DECLINED:
              case EnrolmentStatus.ACCEPTED_TOS:
                return this.navigate(routePath, 'summary');
              // case EnrolmentStatus.DECLINED_TOS:
            }
          }

          // Otherwise, prevent the route from resolving
          return false;
        })
      );
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
