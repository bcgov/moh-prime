import { Injectable, Inject } from '@angular/core';
import { Router, Params } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { AuthService } from '@auth/shared/services/auth.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { SiteService } from '@registration/shared/services/site.service';
import { ArrayUtils } from '@lib/utils/array-utils.class';

@Injectable({
  providedIn: 'root'
})
export class SiteGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private siteResource: SiteResource,
    private siteService: SiteService
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const siteId = params.sid;
    return this.siteResource.getSiteById(siteId)
      .pipe(
        map((site: Site) => {
          // Store the site for access throughout creation and updating of a
          // site, which will allows provide the most up-to-date site
          this.siteService.site = site;

          return this.routeDestination(routePath, site);
        })
      );
  }

  /**
   * @description
   * Determine the route destination based on the site status.
   */
  private routeDestination(routePath: string, site: Site) {
    if (site) {
      return (site.submittedDate)
        ? this.manageSubmittedSiteRouting(routePath, site)
        : true;
    }

    // Otherwise, prevent the route from resolving
    return false;
  }

  private manageSubmittedSiteRouting(routePath: string, site: Site) {
    return this.manageRouting(routePath, SiteRoutes.SITE_MANAGEMENT, site);
  }

  private manageRouting(routePath: string, defaultRoute: string, site: Site): boolean {
    let childRoute = routePath.includes(SiteRoutes.REMOTE_USERS)
      ? SiteRoutes.REMOTE_USERS
      : routePath.split('/').pop();

    if (childRoute.includes('?')) {
      childRoute = childRoute.split('?')[0];
    }

    const allowlistRoutes = site.submittedDate
      ? [
        ...SiteRoutes.editRegistrationRouteAccess()
      ]
      : SiteRoutes.siteRegistrationRoutes();

    if (site.approvedDate) {
      allowlistRoutes.push(SiteRoutes.BUSINESS_LICENCE_RENEWAL);
      allowlistRoutes.push(SiteRoutes.NEXT_STEPS);
    }

    // Redirect to an appropriate default route
    if (!allowlistRoutes.includes(childRoute)) {
      return this.navigate(routePath, SiteRoutes.SITE_MANAGEMENT);
    }

    // Otherwise, allow access to the route
    return true;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(routePath: string, loopPath: string): boolean {
    const modulePath = this.config.routes.site;
    const comparePath = `/${modulePath}/${loopPath}`;

    if (routePath === comparePath) {
      return true;
    } else {
      this.router.navigate([comparePath]);
      return false;
    }
  }
}
