import { Inject, Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

@Injectable({
  providedIn: 'root'
})
export class PaperEnrolmentGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private paperEnrolmentResource: PaperEnrolmentResource
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const enrolleeId = +params.eid;
    return (enrolleeId)
      ? this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
        .pipe(
          map((enrollee: HttpEnrollee) => this.routeDestination(routePath, enrollee, params))
        )
      : of(true);
  }

  /**
   * @description
   * Determine the route destination based on the enrolment.
   */
  private routeDestination(routePath: string, httpEnrollee: HttpEnrollee, params: Params) {
    return (httpEnrollee?.approvedDate)
      ? this.navigate(`/${this.config.routes.paperEnrolment}/${+params.eid}/${PaperEnrolmentRoutes.NEXT_STEPS}`, `${routePath}`, params)
      : true;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(routePath: string, destinationPath: string, params: Params): boolean {
    if (routePath === destinationPath) {
      return true;
    } else {
      this.router.navigate([routePath]);
      return false;
    }
  }
}
