import { Injectable, Inject } from '@angular/core';
import { Router, Params } from '@angular/router';

import { Observable, of } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';

import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Injectable({
  providedIn: 'root'
})
export class RegistrationGuard extends BaseGuard {

  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const user$ = this.authService.getUser$();
    const createOrganization$ = user$
      .pipe(
        map((user: BcscUser) => new Party(user)),
        exhaustMap((party: Party) => this.organizationResource.createOrganization(party))
      );

    return this.organizationResource.getOrganizations()
      .pipe(
        map((organizations: Organization[]) =>
          (organizations.length)
            ? organizations.shift()
            : null
        ),
        exhaustMap((organization: Organization) =>
          (organization)
            ? of(organization)
            : createOrganization$
        ),
        map((organization: Organization) => {
          // TODO PRIME-1131
          // Store the organization for access throughout registration, which
          // will allows be the most up-to-date organization
          // TODO drop this and see if the organization guard is enough
          this.organizationService.organization = organization;
          return this.routeDestination(routePath, organization);
        })
      );
  }

  /**
   * @description
   * Determine the route destination based on the organization status.
   */
  private routeDestination(routePath: string, organization: Organization) {
    // On login the user will always be redirected to the collection notice
    if (routePath.includes(SiteRoutes.COLLECTION_NOTICE)) {
      return true;
    } else if (organization) {
      return (organization.completed)
        ? this.manageCompleteOrganizationRouting(routePath, organization)
        : this.manageIncompleteOrganizationRouting(routePath, organization);
    }

    // Otherwise, prevent the route from resolving
    return false;
  }

  private manageCompleteOrganizationRouting(routePath: string, organization: Organization) {
    return this.manageRouting(routePath, SiteRoutes.SITE_MANAGEMENT, organization);
  }

  private manageIncompleteOrganizationRouting(routePath: string, organization: Organization) {
    return this.manageRouting(routePath, SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY, organization);
  }

  private manageRouting(routePath: string, defaultRoute: string, organization: Organization): boolean {
    const currentRoute = this.route(routePath);

    let childRoute = routePath.includes(SiteRoutes.REMOTE_USERS)
      ? SiteRoutes.REMOTE_USERS
      : routePath.split('/').pop();

    if (childRoute.includes('?')) {
      childRoute = childRoute.split('?')[0];
    }
    // Allow access to a set of routes
    let whiteListedRoutes = SiteRoutes.siteRegistrationRoutes();

    if (!organization.completed) {
      // Initial organization is not completed, use initialRegistration route order
      whiteListedRoutes = whiteListedRoutes
        .filter((route: string) =>
          SiteRoutes.organizationRegistrationRouteOrder().includes(route)
        );
    } else {
      whiteListedRoutes.push(SiteRoutes.ORGANIZATION_AGREEMENT);
      whiteListedRoutes.push(SiteRoutes.ORGANIZATION_REVIEW);

      if (organization.hasAcceptedAgreement) {
        whiteListedRoutes.push(SiteRoutes.SITE_REVIEW);
      }
    }

    // Redirect to an appropriate default route
    if (!whiteListedRoutes.includes(childRoute)) {
      return (organization.completed)
        ? this.navigate(routePath, SiteRoutes.SITE_MANAGEMENT)
        : this.navigate(routePath, SiteRoutes.SITE_MANAGEMENT, defaultRoute, organization.id);
    }

    // Otherwise, allow access to the route
    return true;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(
    routePath: string,
    loopPath: string,
    destinationPath: string = null,
    oid: number = null
  ): boolean {
    const modulePath = this.config.routes.site;
    const comparePath = (destinationPath && oid)
      ? `/${modulePath}/${loopPath}/${oid}/${destinationPath}`
      : `/${modulePath}/${loopPath}`;

    if (routePath === comparePath) {
      return true;
    } else {
      this.router.navigate([comparePath]);
      return false;
    }
  }
}
