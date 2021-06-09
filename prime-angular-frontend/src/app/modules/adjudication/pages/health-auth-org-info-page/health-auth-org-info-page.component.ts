import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-health-auth-org-info-page',
  templateUrl: './health-auth-org-info-page.component.html',
  styleUrls: ['./health-auth-org-info-page.component.scss']
})
export class HealthAuthOrgInfoPageComponent implements OnInit {
  public busy: Subscription;
  public AdjudicationRoutes = AdjudicationRoutes;

  private routeUtils: RouteUtils;

  constructor(
    private configService: ConfigService,
    private activatedRoute: ActivatedRoute,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.routeUtils.routeWithin(routePath);
  }

  public addOrgInfo() {
    this.routeUtils.routeRelativeTo(
      [AdjudicationRoutes.HEALTH_AUTH_CARE_TYPES],
      { queryParams: { initial: true } }
    );
  }

  public ngOnInit(): void { }
}
