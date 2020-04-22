import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, from, of } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';


import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { User } from '@auth/shared/models/user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';

// TODO duplication with enrolment.guard should be split out for reuse
@Injectable({
  providedIn: 'root'
})
export class RegistrationGuard extends BaseGuard {

  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    const user$ = from(this.authService.getUser());
    const createSite$ = user$
      .pipe(
        map((user: User) => new Party(user)),
        exhaustMap((party: Party) => this.siteRegistrationResource.createSite(party)),
        map((site: Site) => [site, true])
      );

    return this.siteRegistrationResource.getSites()
      .pipe(
        // TODO based on single site per user
        map((sites: Site[]) => (sites.length) ? sites.shift() : null),
        exhaustMap((site: Site) =>
          (site)
            ? of([site, false])
            : createSite$
        ),
        map(([site, isNewSite]: [Site, boolean]) => {
          // Store the site for access throughout registration, which
          // will allows be the most up-to-date enrolment
          this.siteRegistrationService.site$.next(site);
          return this.routeDestination(routePath, site, isNewSite);
        })
      );
  }

  private route(routePath: string): string {
    // Only care about the second parameter to determine route access, and
    // assumes that all child routes are allowed
    return routePath.slice(1).split('/')[1];
  }

  /**
   * @description
   * Determine the route destination based on the site status.
   */
  private routeDestination(routePath: string, site: Site, isNewSite: boolean = false) {
    console.log('TEST', routePath, site, isNewSite);

    // On login the user will always be redirected to
    // the collection notice
    if (routePath.includes(SiteRoutes.COLLECTION_NOTICE)) {
      console.log('COLLECTION_NOTICE');
      return true;
    } else if (isNewSite) {
      console.log('MULTIPLE_SITES');
      this.navigate(routePath, SiteRoutes.MULTIPLE_SITES);
    }

    // Otherwise, routes are directed based on enrolment status
    (site.completed)
      ? this.manageCompleteSiteRouting(routePath, site)
      : this.manageIncompleteSiteRouting(routePath, site);

    // Otherwise, prevent the route from resolving
    return false;
  }

  private manageCompleteSiteRouting(routePath: string, site: Site) {
    console.log('COMPLETE');
    return this.manageRouting(routePath, SiteRoutes.SITE_REVIEW, site);
  }

  private manageIncompleteSiteRouting(routePath: string, site: Site) {
    console.log('INCOMPLETE');
    return this.manageRouting(routePath, SiteRoutes.MULTIPLE_SITES, site);
  }

  private manageRouting(routePath: string, defaultRoute: string, site: Site): boolean {
    const route = this.route(routePath);
    // Allow access to an extend set of routes
    const whiteListedRoutes = [];

    if (!whiteListedRoutes.includes(route)) {
      return this.navigate(routePath, defaultRoute);
    }

    return true;
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
