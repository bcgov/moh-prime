import { Inject, Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Party } from '@lib/models/party.model';
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

    // // When the organization ID mismatches the organizations route ID
    // // correct the route immediately
    // const redirectPath = this.detectRouteMismatch(routePath, params, organization?.id);
    // if (redirectPath) {
    //   this.router.navigate([redirectPath]);
    //   return false;
    // }
    //
    // if (organization) {
    //   return (organization.completed)
    //     ? this.manageCompleteOrganizationRouting(routePath, organization)
    //     : this.manageIncompleteOrganizationRouting(routePath, organization, party);
    // }
    //
    // // Otherwise, no organization exists
    // return this.manageNoOrganizationRouting(routePath, party, hasOrgClaim);
  }
}
