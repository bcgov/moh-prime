import { Injectable, Inject } from '@angular/core';
import { Router, Params } from '@angular/router';

import { Party } from '@lib/models/party.model';
import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';

import { AuthService } from '@auth/shared/services/auth.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { AbstractRoutingWorkflowGuard } from '@registration/shared/classes/abstract-routing-workflow-guard.class';

@Injectable({
  providedIn: 'root'
})
export class OrganizationGuard extends AbstractRoutingWorkflowGuard {
  constructor(
    protected organizationService: OrganizationService,
    protected organizationResource: OrganizationResource,
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router
  ) {
    super(organizationService, organizationResource, authService, logger);
  }

  /**
   * @description
   * Determine the route destination based on the organization status.
   */
  protected routeDestination(
    routePath: string,
    params: Params,
    organization: Organization | null,
    party: Party,
    hasOrgClaim: boolean
  ): boolean {
    // On login the user will always be redirected to the collection notice
    if (routePath.includes(SiteRoutes.COLLECTION_NOTICE)) {
      return true;
    }

    // When the organization ID mismatches the organizations route ID
    // correct the route immediately
    const redirectPath = this.detectRouteMismatch(routePath, params, organization?.id);
    if (redirectPath) {
      this.router.navigate([redirectPath]);
      return false;
    }

    if (organization) {
      return (organization.completed)
        ? this.manageCompleteOrganizationRouting(routePath, organization)
        : this.manageIncompleteOrganizationRouting(routePath, organization, party);
    }

    // Otherwise, no organization exists
    return this.manageNoOrganizationRouting(routePath, party, hasOrgClaim);
  }

  /**
   * @description
   * Detect an organization ID mismatch to provide confidence in
   * the organization ID URI param.
   *
   * NOTE: Dependent on the assumption that there is only a
   * single organization per signing authority.
   */
  private detectRouteMismatch(routePath, params: Params, organizationId: number): string | null {
    return (params.oid && (
      (organizationId && organizationId !== +params.oid) || (!organizationId && +params.oid !== 0)
    ))
      ? routePath.replace(`${SiteRoutes.ORGANIZATIONS}/${params.oid}`, `${SiteRoutes.ORGANIZATIONS}/${organizationId}`)
      : null;
  }

  /**
   * @description
   * Manage routing when an organization exists.
   */
  private manageCompleteOrganizationRouting(routePath: string, organization: Organization) {
    // Provides a default of the main management view unless current view
    // can be determined through state of the organization
    return this.manageRouting(routePath, SiteRoutes.ORGANIZATIONS, organization);
  }

  /**
   * @description
   * Manage routing when an organization initial registration has
   * not been completed.
   */
  private manageIncompleteOrganizationRouting(routePath: string, organization: Organization, party: Party) {
    // Provides a default of the initial site registration view unless the current view
    // can be determined through state of the organization
    const destPath = (party)
      ? SiteRoutes.ORGANIZATION_NAME
      : SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY;
    return this.manageRouting(routePath, destPath, organization);
  }

  /**
   * @description
   * Manage routing when an organization does not exist, or initial
   * registration has not been completed.
   */
  private manageNoOrganizationRouting(routePath: string, party: Party, hasOrgClaim: boolean) {
    const destPath = (party)
      ? SiteRoutes.ORGANIZATION_NAME
      : SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY;

    // During initial registration the ID will be set to zero indicating the
    // organization does not exist
    return this.navigate(routePath, SiteRoutes.ORGANIZATIONS, destPath, 0);
  }

  private manageRouting(routePath: string, defaultRoute: string, organization: Organization): boolean {
    const currentRoute = this.getCurrentRoute(routePath);
    const allowedRoutes = this.getAllowedRoutes(organization);

    // Redirect to an appropriate default route when the current
    // route does not exist in the list of allowed routes
    if (!allowedRoutes.includes(currentRoute)) {
      return (organization.completed)
        ? this.navigate(routePath, SiteRoutes.ORGANIZATIONS)
        : this.navigate(routePath, SiteRoutes.ORGANIZATIONS, defaultRoute, organization.id);
    }

    // Otherwise, allow access to the route
    return true;
  }

  /**
   * @description
   * Get the route URI param that is the current route attempting
   * to be resolved, or replace as required.
   */
  private getCurrentRoute(routePath: string) {
    // Remote users has a child view that should not be a point of
    // redirection so the parent is purposefully targeted, otherwise
    // get the last URI param to determine the current route
    let currentRoute = routePath.includes(SiteRoutes.REMOTE_USERS)
      ? SiteRoutes.REMOTE_USERS
      : routePath.split('/').pop();

    // Strip off queries when they exist
    if (currentRoute.includes('?')) {
      currentRoute = currentRoute.split('?')[0];
    }

    return currentRoute;
  }

  /**
   * @description
   * Get a list of allowed routes based on the state of the creation
   * of an organization during an initial registration, otherwise
   * attempt to redirect to an appropriate point during a registration.
   */
  private getAllowedRoutes(organization: Organization) {
    // Allow access to a set of routes based on initial registration
    // having been completed (default)
    let allowedRoutes = SiteRoutes.siteRegistrationRoutes();

    if (!organization.completed) {
      // Initial organization creation has not been completed and should
      // be replaced with the allowed initial registration route order
      allowedRoutes = allowedRoutes.filter((route: string) =>
        SiteRoutes.organizationRegistrationRouteOrder().includes(route)
      );
    } else {
      // Completed indicates the data entry has occurred, but does not
      // mean the organization agreement has been accepted or the
      // registration submitted so allow access to these routes
      allowedRoutes.push(SiteRoutes.ORGANIZATION_AGREEMENT);
      allowedRoutes.push(SiteRoutes.ORGANIZATION_REVIEW);

      if (organization.hasAcceptedAgreement) {
        allowedRoutes.push(SiteRoutes.SITE_REVIEW);
        allowedRoutes.push(SiteRoutes.BUSINESS_LICENCE_RENEWAL);
        allowedRoutes.push(SiteRoutes.NEXT_STEPS);
      }
    }

    return allowedRoutes;
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
    const comparePath = (destinationPath && oid !== null)
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
