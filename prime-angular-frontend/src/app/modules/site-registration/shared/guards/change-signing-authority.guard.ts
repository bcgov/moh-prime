import { Inject, Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Party } from '@lib/models/party.model';
import { RouteSegments } from '@lib/utils/route-utils.class';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { AbstractRoutingWorkflowGuard } from '@registration/shared/classes/abstract-routing-workflow-guard.class';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SigningAuthorityService } from '@registration/shared/services/signing-authority.service';

@Injectable({
  providedIn: 'root'
})
export class ChangeSigningAuthorityGuard extends AbstractRoutingWorkflowGuard {
  constructor(
    protected signingAuthorityService: SigningAuthorityService,
    protected organizationService: OrganizationService,
    protected organizationResource: OrganizationResource,
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router
  ) {
    super(
      signingAuthorityService,
      organizationService,
      organizationResource,
      authService,
      logger
    );
  }

  protected routeDestination(
    routePath: string,
    params: Params,
    organization: Organization | null,
    signingAuthority: Party | null,
    hasClaim: boolean
  ): boolean {
    if (!signingAuthority) {
      return this.manageNoPartyExistsRouting(routePath);
    }

    if (organization) {
      return this.manageAlreadySigningAuthorityRouting(routePath);
    }

    if (hasClaim) {
      return this.manageSigningAuthorityHasExistingClaimRouting(routePath);
    }

    // Route exclusions when no organization or claim exists
    if ([
      SiteRoutes.ORGANIZATION_CLAIM_CONFIRMATION,
      SiteRoutes.ORGANIZATION_CLAIMED
    ].includes(this.getCurrentRoute(routePath))) {
      return this.manageRestrictedRouting(routePath);
    }

    // Otherwise, allow the user to attempt claiming an organization
    return true;
  }

  /**
   * @description
   * Manage routing when the authenticated user has no
   * associated party.
   *
   * NOTE: Creation of a signing authority from an
   * authenticated user can occur in both workflows.
   */
  protected manageNoPartyExistsRouting(routePath: string): boolean {
    return this.navigate(routePath, [
      SiteRoutes.CHANGE_SIGNING_AUTHORITY_WORKFLOW,
      SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY
    ]);
  }

  /**
   * @description
   * Manage routing when the party is already the signing authority
   * for an organization.
   *
   * NOTE: Organization management can only occur in the
   * default workflow.
   */
  protected manageAlreadySigningAuthorityRouting(routePath: string): boolean {
    return this.navigate(routePath, [
      SiteRoutes.CHANGE_SIGNING_AUTHORITY_WORKFLOW,
      SiteRoutes.ORGANIZATION_CLAIMED
    ]);
  }

  /**
   * @description
   * Manage routing when the authenticated user already
   * has a claim.
   */
  protected manageSigningAuthorityHasExistingClaimRouting(routePath: string): boolean {
    return this.navigate(routePath, this.getExistingClaimRouteRedirect());
  }

  /**
   * @description
   * Restrict routing from terminal
   */
  protected manageRestrictedRouting(routePath: string): boolean {
    return this.navigate(routePath, [
      SiteRoutes.CHANGE_SIGNING_AUTHORITY_WORKFLOW,
      SiteRoutes.ORGANIZATION_CLAIM
    ]);
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
