import { Inject, Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Party } from '@lib/models/party.model';
import { RoutePath, RouteSegments } from '@lib/utils/route-utils.class';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { AbstractRoutingWorkflowGuard } from '@registration/shared/classes/abstract-routing-workflow-guard.class';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteRoutes } from '@registration/site-registration.routes';

@Injectable({
  providedIn: 'root'
})
export class ChangeSigningAuthorityGuard extends AbstractRoutingWorkflowGuard {
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

    // TODO if already a signing authority of a different organization redirect to page with link

    // No organization exists and should be pushed into the default
    // workflow which the only place you can create an organization
    if (!organization) {
      return this.manageNoOrganizationRouting(routePath);
    }

    if (organization.signingAuthorityId === party.id) {
      return this.manageAlreadySigningAuthorityRouting(routePath);
    }

    if (organization.hasClaim) {
      return this.manageExistingClaimRouting(routePath, organization.id);
    }

    // When the organization ID mismatches the organizations route ID
    // correct the route immediately
    const redirectPath = this.detectRouteMismatch(routePath, params, organization?.id);
    if (redirectPath) {
      this.router.navigate([redirectPath]);
      return false;
    }

    // Otherwise, allow the user to attempt claiming an organization
    return true;
  }

  /**
   * @description
   * Manage routing when an organization does not exist, or initial
   * registration has not been completed.
   *
   * NOTE: Organization creation can only occur in the default workflow.
   */
  protected manageNoOrganizationRouting(routePath: string): boolean {
    // During initial registration the ID will be set to zero indicating the
    // organization does not exist
    return this.navigate(routePath, [SiteRoutes.ORGANIZATIONS, 0]);
  }

  /**
   * @description
   * Manage routing when the party is already the signing authority
   * for the organization.
   *
   * NOTE: Organization management can only occur in the default workflow.
   */
  protected manageAlreadySigningAuthorityRouting(routePath: string): boolean {
    return this.navigate(routePath, [SiteRoutes.ORGANIZATIONS]);
  }

  /**
   * @description
   * Manage routing when a claim exists on an organization.
   *
   * NOTE: Claiming an organization can only occur in the change
   * signing authority workflow.
   */
  protected manageExistingClaimRouting(routePath: string, organizationId: number): boolean {
    return this.navigate(routePath, this.getExistingClaimRouteRedirect(organizationId));
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(routePath: string, destinationSegments: RouteSegments): boolean {
    const destinationPath = SiteRoutes.routePath(destinationSegments.join('/'));

    // Route path may contain query parameters, which should be ignored
    // during the check, but allowed to persist
    if (routePath.split('?').shift() === destinationPath) {
      return true;
    } else {
      this.router.navigate([destinationPath]);
      return false;
    }
  }
}
