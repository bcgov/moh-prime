import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { SatEnrollee } from '@sat/shared/models/sat-enrollee.model';
import { SatEformsEnrolmentResource } from '@sat/shared/resource/sat-eforms-enrolment-resource.service';
import { SatEnrolleeService } from '@sat/shared/services/sat-enrollee.service';

@Injectable({
  providedIn: 'root'
})
export class SatEformsGuard extends BaseGuard {
  public constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolleeService: SatEnrolleeService,
    private enrolmentResource: SatEformsEnrolmentResource,
    private router: Router
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    // Current route path is empty when URL manually inserted by user
    const currentRoutePath = RouteUtils.currentRoutePath(this.router.url);
    const nextRoutePath = RouteUtils.currentRoutePath(routePath);

    return this.authService.getUser$()
      .pipe(
        exhaustMap(({ userId }: BcscUser) => this.enrolmentResource.getSatEnrolleeByUserId(userId)),
        tap((enrollee: SatEnrollee) => this.enrolleeService.enrollee = enrollee),
        map((enrollee: SatEnrollee) => {
          const enrolleeId = enrollee?.id ?? 0;

          if(enrollee.submittedDate) {
            return this.navigate(routePath, enrolleeId, SatEformsRoutes.SUBMISSION_CONFIRMATION);
          }

          if (
            currentRoutePath === SatEformsRoutes.DEMOGRAPHIC &&
            nextRoutePath === SatEformsRoutes.REGULATORY
          ) {
            return this.navigate(routePath, enrolleeId, SatEformsRoutes.REGULATORY);
          } else if (
            currentRoutePath === SatEformsRoutes.REGULATORY &&
            nextRoutePath === SatEformsRoutes.SUBMISSION_CONFIRMATION
          ) {
            return this.navigate(routePath, enrolleeId, SatEformsRoutes.SUBMISSION_CONFIRMATION);
          } else {
            // Otherwise, start at the beginning of the enrolment process
            return this.navigate(routePath, enrolleeId, SatEformsRoutes.DEMOGRAPHIC);
          }
        })
      );
  }

  private navigate(routePath: string, enrolleeId: number, destinationPath: string): boolean {
    const satRoutePath = this.config.routes.sat;

    if (routePath === `/${satRoutePath}/${SatEformsRoutes.ENROLMENTS}/${enrolleeId}/${destinationPath}`) {
      return true;
    } else {
      this.router.navigate([satRoutePath, SatEformsRoutes.ENROLMENTS, enrolleeId, destinationPath]);
      return false;
    }
  }
}
