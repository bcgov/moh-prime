import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-site-information-page',
  templateUrl: './site-information-page.component.html',
  styleUrls: ['./site-information-page.component.scss']
})
export class SiteInformationPageComponent implements OnInit {
  public busy: Subscription;
  public site: HealthAuthoritySite;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private healthAuthorityResource: HealthAuthorityResource
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public ngOnInit(): void {
    const params = this.route.snapshot.params;
    this.busy = this.healthAuthorityResource.getHealthAuthoritySiteById(+params.haid, +params.sid)
      .subscribe((site: HealthAuthoritySite) => {
        this.site = site;
      });
  }
}
