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
            this.organizationResource.getSigningAuthorityOrganizationsByUserId(user.userId)
              .pipe(
                map((organizations: Organization[]) => (organizations?.length) ? organizations[0] : null)
              ),
            this.organizationResource.getSigningAuthorityByUserId(user.userId),
            this.organizationResource.getOrganizationClaim({ userId: user.userId })
          ])
        ),
        map(([organization, party, claimed]: [Organization | null, Party, boolean]) => {
          // Store the organization for access throughout registration, which
          // will allows be the most up-to-date organization
          this.organizationService.organization = organization;

          // Determine the next route based on whether this is the initial
          // registration of an organization and site, or subsequent
          // registration of sites under an existing organization
          return this.routeDestination(routePath, params, organization, party, claimed);
        })
      );
  }

  protected abstract routeDestination(routePath: string, params: Params, organization: Organization | null, party: Party, hasOrgClaim: boolean): boolean;
}
