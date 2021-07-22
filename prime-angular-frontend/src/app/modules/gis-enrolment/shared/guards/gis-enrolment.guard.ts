import { Inject, Injectable } from '@angular/core';
import { Router, Params } from '@angular/router';

import { Observable, of } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentService } from '../services/gis-enrolment.service';
import { GisEnrolmentResource } from '../resources/gis-enrolment-resource.service';
import { GisEnrolment } from '../models/gis-enrolment.model';

@Injectable({
  providedIn: 'root'
})
export class GisEnrolmentGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private gisEnrolmentService: GisEnrolmentService,
    private gisEnrolmentResource: GisEnrolmentResource
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    return this.authService.getUser$()
      .pipe(
        exhaustMap((user: BcscUser) =>
          this.gisEnrolmentResource.getEnrolmentByUserId(user.userId)
            .pipe(
              exhaustMap((enrolment: GisEnrolment) =>
                (enrolment)
                  ? of(enrolment)
                  : this.gisEnrolmentResource.createEnrolment(GisEnrolment.fromBcscUser(user))
              ),
              map((enrolment: GisEnrolment) => {
                // Store the enrolment for access throughout registration, which
                // will allows be the most up-to-date version
                this.gisEnrolmentService.enrolment = enrolment;
                return this.routeDestination(routePath, enrolment);
              })
            )
        )
      );
  }

  /**
   * @description
   * Determine the route destination based on the enrolment status.
   */
  private routeDestination(routePath: string, enrolment: GisEnrolment) {
    // On login the user will always be redirected to the collection notice
    if (routePath.includes(GisEnrolmentRoutes.COLLECTION_NOTICE)) {
      return true;
    } else if (enrolment) {
      return (enrolment.submittedDate)
        ? this.manageCompleteEnrolmentRouting(routePath, enrolment)
        : this.manageIncompleteEnrolmentRouting(routePath, enrolment);
    }

    // Otherwise, prevent the route from resolving
    return false;
  }

  private manageCompleteEnrolmentRouting(routePath: string, enrolment: GisEnrolment): boolean {
    return this.navigate(routePath, GisEnrolmentRoutes.SUBMISSION_CONFIRMATION);
  }

  private manageIncompleteEnrolmentRouting(routePath: string, enrolment: GisEnrolment): boolean {
    return this.manageRouting(routePath, GisEnrolmentRoutes.LDAP_USER_PAGE, enrolment);
  }

  private manageRouting(routePath: string, defaultRoute: string, enrolment: GisEnrolment): boolean {
    // TODO can skip over the LDAP route if this is a feature that is wanted
    // Prevent re-authentication with LDAP when already
    // performed by skipping over the route
    // if (
    //   enrolment.ldapLoginSuccessDate &&
    //   [
    //     routePath.includes(GisEnrolmentRoutes.LDAP_USER_PAGE),
    //     routePath.includes(GisEnrolmentRoutes.LDAP_INFO_PAGE)
    //   ].some(Boolean)
    // ) {
    //   return this.navigate(routePath, GisEnrolmentRoutes.LDAP_USER_PAGE);
    // }

    // Otherwise, allow access to the route
    return true;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(routePath: string, destinationPath: string): boolean {
    if (routePath === `/${GisEnrolmentRoutes.MODULE_PATH}/${destinationPath}`) {
      return true;
    } else {
      this.router.navigate([GisEnrolmentRoutes.MODULE_PATH, destinationPath]);
      return false;
    }
  }
}
