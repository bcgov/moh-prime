import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { AdminStatusType } from '../models/admin-status.enum';

@Injectable({
  providedIn: 'root'
})
export class AdjudicationGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    private permissionService: PermissionService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    super(authService, logger);
  }

  /**
   * @description
   * Check the user is authenticated, otherwise redirect
   * them to an appropriate destination.
   */
  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {

    return this.authService.getAdmin$()
      .pipe(
        exhaustMap((admin: Admin) => {
          return this.adjudicationResource.getAdjudicatorByUserId(admin.userId)
            .pipe(
              exhaustMap((primeAdmin: Admin) => {
                return of([primeAdmin, admin]);
              }));
        }),
        exhaustMap(([primeAdmin, admin]: [Admin, Admin]) => {
          if (primeAdmin) {
            //if PRIME admin record found, pass the status to next step
            admin.status = primeAdmin.status
          } else {
            // if PRIME does not have any admin user with the user id, create it
            this.adjudicationResource.createAdmin(admin).subscribe();
          }
          return of(admin);
        }),
        exhaustMap((admin: Admin) => {
          let destinationRoute = this.config.routes.denied;
          if (!this.authService.isLoggedIn()) {
            destinationRoute = this.config.routes.auth;
          } else {
            if (admin.status !== AdminStatusType.DISABLED) {
              return of(true);
            }
          }

          this.router.navigate([destinationRoute]);
          return of(false);
        })
      );
  }
}
