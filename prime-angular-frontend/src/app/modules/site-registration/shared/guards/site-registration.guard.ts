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
  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    const user$ = from(this.authService.getUser());
    const createRegistrant$ = user$
      .pipe(
        exhaustMap(({ userId, hpdid, firstName, lastName, dateOfBirth, physicalAddress }: User) => {
          const registrant = {
            // Providing only the minimum required fields for creating a registrant
            userId,
            hpdid,
            firstName,
            lastName,
            dateOfBirth,
            physicalAddress,
            voicePhone: null,
            contactEmail: null
          } as Registrant;

          return this.registrantResource.createRegistrant(registrant);
        }),
        map((registrant: Registrant) => registrant)
      );

    return createRegistrant$
      .pipe(
        exhaustMap((registrant: Registrant) =>
          (!registrant)
            ? createRegistrant$
            : of([registrant, false])
        ),
        map((registrant: Registrant) => {
          // Store the registrant for access throughout registrant, which
          // will allows be the most up-to-date registrant
          this.registrantService.registrant$.next(registrant);
          return this.routeDestination(routePath, registrant);
        })
      );

  }

  // /**
  //  * @description
  //  * Check the user is authenticated, otherwise redirect
  //  * them to an appropriate destination.
  //  */
  // protected canAccess(authenticated: boolean, roles: string[], routePath: string): Promise<boolean> {
  //   return new Promise((resolve, reject) => {
  //     let destinationRoute = this.config.routes.denied;

  //     if (!authenticated) {
  //       destinationRoute = this.config.routes.auth;
  //     } else if (roles.includes(Role.ENROLLEE)) {
  //       // Allow route to resolve
  //       return resolve(true);
  //     }

  //     // Otherwise, redirect to an appropriate destination
  //     this.router.navigate([destinationRoute]);
  //     return reject(false);
  //   });
  // }

  private route(routePath: string): string {
    // Only care about the second parameter to determine route access, and
    // assumes that all child routes are allowed
    return routePath.slice(1).split('/')[1];
  }

  /**
   * @description
   * Determine the route destination based on the enrolment status.
   */
  private routeDestination(routePath: string, enrolment: Registrant, isNewRegistrant: boolean = false) {
    // On login the enrollees will always be redirected to
    // the collection notice
    if (routePath.includes(SiteRoutes.COLLECTION_NOTICE)) {
      return true;
    }
    // else if (isNewRegistrant) {
    //   this.navigate(routePath, SiteRoutes.OVERVIEW);
    // }

    // Otherwise, routes are directed based on enrolment status
    // if (!enrolment) {
    //   return this.navigate(routePath, SiteRoutes.DEMOGRAPHIC);
    // } else if (enrolment) {

    // }

    // Otherwise, prevent the route from resolving
    return false;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
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
