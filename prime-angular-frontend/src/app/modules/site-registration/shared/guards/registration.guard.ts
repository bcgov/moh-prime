import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, from, of } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { User } from '@auth/shared/models/user.model';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';

// TODO duplication with enrolment.guard should be split out for reuse
// TODO drop once organization and site guards are in place
@Injectable({
  providedIn: 'root'
})
export class RegistrationGuard extends BaseGuard {

  constructor(
    protected authenticationService: AuthenticationService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService
  ) {
    super(authenticationService, logger);
  }

  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    const user$ = from(this.authenticationService.getUser());
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
          // will allows be the most up-to-date site
          this.siteRegistrationService.site$.next(site);
          return this.routeDestination(routePath, site, isNewSite);
        })
      );
  }

  /**
   * @description
   * Determine the route destination based on the site status.
   */
  private routeDestination(routePath: string, site: Site, isNewSite: boolean = false) {
    // On login the user will always be redirected to
    // the collection notice
    if (routePath.includes(SiteRoutes.COLLECTION_NOTICE)) {
      return true;
    } else if (site) {
      return (site.completed)
        ? this.manageCompleteSiteRouting(routePath, site)
        : this.manageIncompleteSiteRouting(routePath, site);
    }

    // Otherwise, prevent the route from resolving
    return false;
  }

  private manageCompleteSiteRouting(routePath: string, site: Site) {
    return this.manageRouting(routePath, SiteRoutes.SITE_REVIEW, site);
  }

  private manageIncompleteSiteRouting(routePath: string, site: Site) {
    // TODO set to SITE_REVIEW to allow removal of MULTIPLE_SITES, but definitely the wrong route
    return this.manageRouting(routePath, SiteRoutes.SITE_REVIEW, site);
  }

  private manageRouting(routePath: string, defaultRoute: string, site: Site): boolean {
    const currentRoute = this.route(routePath);
    // Allow access to a set of routes
    // let whiteListedRoutes = SiteRoutes.registrationRoutes();

    if (!site.location.organization.acceptedAgreementDate) {
      // No routing beyond the organization agreement without accepting
      // whiteListedRoutes = whiteListedRoutes
      //   .filter((route: string) => SiteRoutes.noOrganizationAgreementRoutes().includes(route));
    } else if (!site.completed) {
      // No reviewing without completing the registration
      // whiteListedRoutes = whiteListedRoutes
      //   .filter((route: string) => route !== SiteRoutes.SITE_REVIEW);
    }

    // Redirect to an appropriate default route
    // if (!whiteListedRoutes.includes(currentRoute)) {
    return this.navigate(routePath, defaultRoute);
    // }

    // Otherwise, allow access to the route
    return true;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(routePath: string, destinationPath: string): boolean {
    const modulePath = this.config.routes.site;

    if (routePath === `/${modulePath}/${destinationPath}`) {
      return true;
    } else {
      this.router.navigate([modulePath, destinationPath]);
      return false;
    }
  }
}
