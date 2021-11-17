import { Params } from '@angular/router';

import { forkJoin, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { Party } from '@lib/models/party.model';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteSegments } from '@lib/utils/route-utils.class';

export abstract class AbstractRoutingWorkflowGuard extends BaseGuard {
  protected constructor(
    protected organizationService: OrganizationService,
    protected organizationResource: OrganizationResource,
    protected authService: AuthService,
    protected logger: ConsoleLoggerService
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    return this.authService.getUser$()
      .pipe(
        // Having no signing authority or organizations results in the same
        // redirection logic for the user, and therefore not handled individually
        exhaustMap((user: BcscUser) =>
          forkJoin([
            this.organizationResource.getSigningAuthorityByUserId(user.userId),
            this.organizationResource.getSigningAuthorityOrganizationByUserId(user.userId),
            this.organizationResource.getOrganizationClaim({ userId: user.userId })
          ])
        ),
        map(([party, organization, claimed]: [Party | null, Organization | null, boolean]) => {
          // Store the organization for access throughout registration, which
          // will allows be the most up-to-date organization
          this.organizationService.organization = organization;
          return this.routeDestination(routePath, params, organization, party, claimed);
        })
      );
  }

  /**
   * @description
   * Determine the route destination based on organization.
   */
  protected abstract routeDestination(routePath: string, params: Params, organization: Organization | null, party: Party, hasOrgClaim: boolean): boolean;

  /**
   * @description
   * Manage routing when an organization does not exist, or initial
   * registration has not been completed.
   */
  protected abstract manageNoOrganizationRouting(routePath: string, party: Party, hasOrgClaim: boolean): boolean;

  /**
   * @description
   * Detect an organization ID mismatch to provide confidence in
   * the organization ID URI param.
   *
   * NOTE: Dependent on the assumption that there is only a
   * single organization per signing authority.
   */
  protected detectRouteMismatch(routePath, params: Params, organizationId: number): string | null {
    return (params.oid && (
      (organizationId && organizationId !== +params.oid) || (!organizationId && +params.oid !== 0)
    ))
      ? routePath.replace(`${SiteRoutes.ORGANIZATIONS}/${params.oid}`, `${SiteRoutes.ORGANIZATIONS}/${organizationId}`)
      : null;
  }

  /**
   * @description
   * Get the route URI param that is the current route attempting
   * to be resolved, or replace as required.
   */
  protected getCurrentRoute(routePath: string) {
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
   * Provides the route segments to redirect to the existing
   * claim confirmation route.
   */
  protected getExistingClaimRouteRedirect(organizationId: number): RouteSegments {
    return [
      SiteRoutes.CHANGE_SIGNING_AUTHORITY_WORKFLOW,
      SiteRoutes.ORGANIZATIONS,
      organizationId,
      SiteRoutes.ORGANIZATION_CLAIM_CONFIRMATION
    ];
  }
}
