import { Inject, Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PaperEnrolmentGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private paperEnrolmentService: PaperEnrolmentService
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const enrolleeId = params.eid;
    if (enrolleeId != 0) {
      return this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
        .pipe(
          map((enrolment: Enrolment) => {
            // Store the site for access throughout creation and updating of a
            // site, which will allows provide the most up-to-date site
            this.paperEnrolmentService.enrolment = enrolment;

            return this.routeDestination(routePath, enrolment);
          })
        );
    } else {
      return new Promise(async (resolve, reject) => resolve(true))
    }
  }

  /**
   * @description
   * Determine the route destination based on the enrolment.
   */
  private routeDestination(routePath: string, enrolment: Enrolment) {
    return (enrolment)
      ? true
      : false;
  }
}
