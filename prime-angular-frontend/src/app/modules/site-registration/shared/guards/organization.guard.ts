import { Injectable, Inject } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Params } from '@angular/router';

import { Observable, from, of } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { User } from '@auth/shared/models/user.model';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Injectable({
  providedIn: 'root'
})
export class OrganizationGuard extends BaseGuard {
  constructor(
    protected authenticationService: AuthenticationService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private organizationResource: OrganizationResource,
    private organizationService: OrganizationService
  ) {
    super(authenticationService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const organizationId = params.oid;
    return this.organizationResource.getOrganizationById(organizationId)
      .pipe(
        map((organization: Organization) => {
          // Store the site for access throughout creation and updating of a
          // site, which will allows provide the most up-to-date site
          this.organizationService.organization = organization;
          // TODO always resolve until routes are lock down
          return true;
        })
      );
  }
}
