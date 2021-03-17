import { Inject, Injectable } from '@angular/core';
import { Router, Params } from '@angular/router';

import { Observable, of } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { Party } from '@registration/shared/models/party.model';
import { GisEnrolmentService } from '../services/gis-enrolment.service';
import { GisEnrolmentResource } from '../resources/gis-enrolment-resource.service';
import { GisEnrolment } from '../models/gis-enrolment.model';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';

@Injectable({
  providedIn: 'root'
})
export class GisEnrolmentGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private gisEnrolmentService: GisEnrolmentService,
    private gisEnrolmentResource: GisEnrolmentResource
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const user$ = this.authService.getUser$();
    const createEnrolment$ = user$
      .pipe(
        map((user: BcscUser) => new Party(user)),
        exhaustMap((party: Party) => this.gisEnrolmentResource.createEnrolment(party))
      );

    return this.gisEnrolmentResource.getEnrolment()
      .pipe(
        exhaustMap((enrolment: GisEnrolment) =>
          (enrolment)
            ? of(enrolment)
            : createEnrolment$
        ),
        map((enrolment: GisEnrolment) => {
          // Store the enrolment for access throughout registration, which
          // will allows be the most up-to-date version
          this.gisEnrolmentService.enrolment = enrolment;
          return this.routeDestination(routePath, enrolment);
        })
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
      return (enrolment.completed)
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
    const destinationPath = (enrolment.organization && enrolment.role)
      ? GisEnrolmentRoutes.ENROLLEE_INFO_PAGE
      : (enrolment.ldapLoginSuccessDate)
        ? GisEnrolmentRoutes.ORG_INFO_PAGE
        : GisEnrolmentRoutes.LDAP_USER_PAGE;

    return this.navigate(routePath, destinationPath);
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  protected navigate(routePath: string, destinationPath: string): boolean {
    if (routePath === `/${ GisEnrolmentRoutes.MODULE_PATH }/${ destinationPath }`) {
      return true;
    } else {
      this.router.navigate([GisEnrolmentRoutes.MODULE_PATH, destinationPath]);
      return false;
    }
  }
}
