import { Params } from '@angular/router';

import { forkJoin, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { Party } from '@lib/models/party.model';
import { RouteSegments } from '@lib/utils/route-utils.class';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { SigningAuthorityService } from '@registration/shared/services/signing-authority.service';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteRoutes } from '@registration/site-registration.routes';

export abstract class AbstractRoutingWorkflowGuard extends BaseGuard {
  protected constructor(
    protected signingAuthorityService: SigningAuthorityService,
    protected organizationService: OrganizationService,
    protected organizationResource: OrganizationResource,
    protected authService: AuthService,
    protected logger: ConsoleLoggerService
  ) {
    super(authService, logger);
  }

  /**
   * @description
   * Get the signing authority, organization, and claim information
   * required to make appropriate decisions in the guards.
   */
  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    return this.authService.getUser$()
      .pipe(
        // Having no signing authority or organizations results in the same
        // redirection logic for the user, and therefore not handled individually
        exhaustMap(({ userId }: BcscUser) =>
          forkJoin([
            this.organizationResource.getSigningAuthorityByUserId(userId),
            this.organizationResource.getSigningAuthorityOrganizationByUserId(userId),
            this.organizationResource.getOrganizationClaim({ userId })
          ])
        ),
        map(([signingAuthority, organization, claimed]: [Party | null, Organization | null, boolean]) => {
          // Store the organization for access throughout registration, which
          // will allows be the most up-to-date organization
          this.signingAuthorityService.signingAuthority = signingAuthority;
          this.organizationService.organization = organization;
          return this.routeDestination(routePath, params, organization, signingAuthority, claimed);
        })
      );
  }

  /**
   * @description
   * Determine the route destination based on organization.
   */
  protected abstract routeDestination(
    routePath: string,
    params: Params,
    organization: Organization | null,
    party: Party | null,
    hasOrgClaim: boolean
  ): boolean;

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
   *
   * NOTE: Can be detected in either workflow, but will always
   * redirect to the change signing authority workflow
   */
  protected getExistingClaimRouteRedirect(): RouteSegments {
    return [
      SiteRoutes.CHANGE_SIGNING_AUTHORITY_WORKFLOW,
      SiteRoutes.ORGANIZATION_CLAIM_CONFIRMATION
    ];
  }
}
