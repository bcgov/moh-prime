import { Injectable, Inject } from '@angular/core';
import { Router, Params } from '@angular/router';

import { Party } from '@lib/models/party.model';
import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';

import { AuthService } from '@auth/shared/services/auth.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { SigningAuthorityService } from '@registration/shared/services/signing-authority.service';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { AbstractRoutingWorkflowGuard } from '@registration/shared/classes/abstract-routing-workflow-guard.class';

@Injectable({
  providedIn: 'root'
})
export class OrganizationGuard extends AbstractRoutingWorkflowGuard {
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

  /**
   * @description
   * Determine the route destination based on the organization state.
   */
  protected routeDestination(
    routePath: string,
    params: Params,
    organizations: Organization[] | null,
    signingAuthority: Party | null,
    hasClaim: boolean
  ): boolean {
    // On login the user will always be redirected to the collection notice
    if (routePath.includes(SiteRoutes.COLLECTION_NOTICE)) {
      return true;
    }

    // When a claim exists for a signing authority access to the organization
    // is not allowed, and they are redirected to the claim organization workflow
    if (hasClaim && (!organizations || organizations.length === 0)) {
      this.router.navigate([
        SiteRoutes.MODULE_PATH,
        ...this.getExistingClaimRouteRedirect()
      ]);
      return false;
    }


    if (params.oid) {
      // When the organization ID mismatches the organizations route ID
      // correct the route immediately
      const redirectPath = this.detectRouteMismatch(routePath, params, organizations);
      if (redirectPath) {
        this.router.navigate([redirectPath]);
        return false;
      }

    }

    const organization = this.organizationService.organization;
    if (organization) {
      return (organization.completed)
        ? this.manageCompleteOrganizationRouting(routePath, organization)
        : this.manageIncompleteOrganizationRouting(routePath, organization, signingAuthority);
    }


    // Otherwise, no organization exists
    return this.manageNoOrganizationRouting(routePath, signingAuthority);
  }

  /**
   * @description
   * Manage routing when an organization does not exist, or initial
   * registration has not been completed.
   */
  private manageNoOrganizationRouting(routePath: string, signingAuthority: Party): boolean {
    const destPath = (signingAuthority)
      ? SiteRoutes.ORGANIZATION_NAME
      : SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY;

    // During initial registration the ID will be set to zero indicating the
    // organization does not exist
    return this.navigate(routePath, SiteRoutes.ORGANIZATIONS, destPath, 0);
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
  private manageIncompleteOrganizationRouting(routePath: string, organization: Organization, signingAuthority: Party) {
    // Provides a default of the initial site registration view unless the current view
    // can be determined through state of the organization
    const destPath = (signingAuthority)
      ? SiteRoutes.ORGANIZATION_NAME
      : SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY;
    return this.manageRouting(routePath, destPath, organization);
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
   * Detect an organization ID mismatch to provide confidence in
   * the organization ID URI param.
   *
   */
  private detectRouteMismatch(routePath, params: Params, organizations: Organization[]): string | null {
    // return when user trying to create a new organization
    if (params.oid && params.oid === '0') {
      // if adding to new site for new organization, replace the organization ID
      if (routePath.includes(SiteRoutes.CARE_SETTING) && this.organizationService.organization) {
        return routePath.replace(`${SiteRoutes.ORGANIZATIONS}/${params.oid}`, `${SiteRoutes.ORGANIZATIONS}/${this.organizationService.organization.id}`);
      } else {
        return null;
      }
    }
    if (params.oid && params.oid !== '0') {
      // if the oid does not belong to current user, update url with the oid does.
      if (!organizations.some((org) => org.id === +params.oid)) {
        return routePath.replace(`${SiteRoutes.ORGANIZATIONS}/${params.oid}`, `${SiteRoutes.ORGANIZATIONS}/${organizations[0].id}`);
      } else if (this.organizationService.organization.id !== +params.oid) {
        this.organizationService.organization = this.organizationService.organizations.find((org) => org.id === +params.oid);
      }
    }
    return null;
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
