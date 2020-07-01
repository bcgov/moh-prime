import { Injectable, Inject } from '@angular/core';
import { exhaustMap, map } from 'rxjs/operators';
import { Router } from '@angular/router';

import { from, Observable, of } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { Role } from '@auth/shared/enum/role.enum';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Injectable({
  providedIn: 'root'
})
export class AdjudicationGuard extends BaseGuard {
  constructor(
    protected authenticationService: AuthenticationService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    super(authenticationService, logger);
  }

  /**
   * @description
   * Check the user is authenticated, otherwise redirect
   * them to an appropriate destination.
   */
  // TODO update to be two observables merged and resolved using combineLatest,
  // but requires wrapping the Keycloak service so it uses obseravables first
  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    const admin$ = from(this.authenticationService.getAdmin())
      .pipe(
        exhaustMap(({ userId, firstName, lastName, email, idir }: Admin) => {
          const admin = {
            userId,
            firstName,
            lastName,
            email,
            idir
          } as Admin;

          // Attempt to create an admin if they don't exist
          return (this.authenticationService.isAdmin())
            ? this.adjudicationResource.createAdmin(admin)
            : Promise.resolve(admin);
        })
      ).toPromise();

    const redirect$ = new Promise(async (resolve, reject) => {
      const authenticated = await this.authenticationService.isLoggedIn();
      let destinationRoute = this.config.routes.denied;
      if (!authenticated) {
        destinationRoute = this.config.routes.auth;
      } else if (this.authenticationService.hasAdminView()) {
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
