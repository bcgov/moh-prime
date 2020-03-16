import { Injectable, Inject } from '@angular/core';
import { Observable, from, of } from 'rxjs';
import { BaseGuard } from '@core/guards/base.guard';
import { AuthService } from '@auth/shared/services/auth.service';
import { LoggerService } from '@core/services/logger.service';
import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { Router } from '@angular/router';
import { exhaustMap, map } from 'rxjs/operators';
import { Registrant } from '@shared/models/registrant';
import { User } from '@auth/shared/models/user.model';
import { RegistrantResource } from '../../services/registrant-resource.service';
import { RegistrantService } from '../../services/registrant.service';
import { SiteRoutes } from '../../site-registration.routes';
import { Role } from '@auth/shared/enum/role.enum';

@Injectable({
  providedIn: 'root'
})
export class SiteRegistrationGuard extends BaseGuard {

  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private registrantResource: RegistrantResource,
    private registrantService: RegistrantService,
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
  protected canAccess(authenticated: boolean, routePath: string): Promise<boolean> {
    return new Promise(async (resolve, reject) => {
      const currentBaseRoute = this.router.url.slice(1).split('/')[0];
      const currentRoute = this.router.url.slice(1).split('/')[1];
      // console.log('currentBaseRoute: ', currentBaseRoute);
      // console.log('currentRoute: ', currentRoute);

      if (this.authService.isRegistrant()) {
        if (currentRoute === '') {
          this.router.navigate([SiteRoutes.COLLECTION_NOTICE]);
        }
        return resolve(true);
      } else if (this.authService.isEnrollee()) {
        this.router.navigate([this.config.routes.enrolment]);
      }
      return reject(false);
    });
  }



}
