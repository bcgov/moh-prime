import { Injectable, Inject } from '@angular/core';
import { Router, Params } from '@angular/router';

import { Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { OrganizationAgreement } from '@shared/models/agreement.model';

import { AuthService } from '@auth/shared/services/auth.service';

import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';

// TODO PRIME-1131
// Should this guard be dropped and only use the registration guard
@Injectable({
  providedIn: 'root'
})
export class OrganizationGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private organizationResource: OrganizationResource,
    private organizationService: OrganizationService
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const organizationId = params.oid;
    return this.organizationResource.getOrganizationById(organizationId)
      .pipe(
        exhaustMap((organization: Organization) =>
          // TODO PRIME-1131
          // TODO revisit when guards are re-evaluated
          // TODO use a resolver since only data is updated
          // TODO only invoke when when null (default), and only update
          // when an organization agreement has been accepted
          this.organizationResource.getOrganizationAgreements(organization.id)
            .pipe(
              map((organizationAgreements: OrganizationAgreement[]) =>
                [organization, organizationAgreements]
              )
            )
        ),
        map(([organization, organizationAgreements]: [Organization, OrganizationAgreement[]]) => {
          // Store the organization for access throughout creation and updating of a
          // organization, which will allows provide the most up-to-date organization
          this.organizationService.organization = organization;
          // TODO PRIME-1131
          // TODO always resolve until routes are lock down
          return true;
        })
      );
  }
}
