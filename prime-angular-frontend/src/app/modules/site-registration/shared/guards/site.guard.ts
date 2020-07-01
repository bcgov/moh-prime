import { Injectable, Inject } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Params } from '@angular/router';

import { Observable, from, of } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { User } from '@auth/shared/models/user.model';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';
import { SiteResource } from '../services/site-resource.service';
import { SiteService } from '../services/site.service';

@Injectable({
  providedIn: 'root'
})
export class SiteGuard extends BaseGuard {
  constructor(
    protected authenticationService: AuthenticationService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private siteResource: SiteResource,
    private siteService: SiteService
  ) {
    super(authenticationService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const siteId = params.sid;
    return this.siteResource.getSiteById(siteId)
      .pipe(
        map((site: Site) => {
          // Store the site for access throughout creation and updating of a
          // site, which will allows provide the most up-to-date site
          this.siteService.site = site;
          // TODO always resolve until routes are lock down
          return true;
        })
      );
  }
}
