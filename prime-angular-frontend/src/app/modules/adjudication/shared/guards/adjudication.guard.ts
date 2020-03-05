import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { Role } from '@auth/shared/enum/role.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { BaseGuard } from '@core/guards/base.guard';
import { from, Observable, of } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';
import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationResource } from '../services/adjudication-resource.service';

@Injectable({
  providedIn: 'root'
})
export class AdjudicationGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
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
    const admin$ = from(this.authService.getAdmin())
      .pipe(
        exhaustMap(({ userId, firstName, lastName, email, idir }: Admin) => {
          const admin = {
            userId,
            firstName,
            lastName,
            email,
            idir
          } as Admin;

          if (this.authService.isAdmin()) {
            return this.adjudicationResource.createAdmin(admin);
          }
          return Promise.resolve(admin);
        }),
      ).toPromise();

    const redirect$ = new Promise(async (resolve, reject) => {

      const authenticated = await this.authService.isLoggedIn();
      let destinationRoute = this.config.routes.denied;
      if (!authenticated) {
        destinationRoute = this.config.routes.auth;
      } else if (this.authService.hasAdminView()) {
        // Allow route to resolve
        return resolve(true);
      }

      // Otherwise, redirect to an appropriate destination
      this.router.navigate([destinationRoute]);
      return reject(false);
    });


    return Promise.all([admin$, redirect$])
      .then(([admin, result]: [Admin, boolean]) => result);
  }
}
