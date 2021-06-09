import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-health-authority-organization-information-page',
  templateUrl: './health-authority-organization-information-page.component.html',
  styleUrls: ['./health-authority-organization-information-page.component.scss']
})
export class HealthAuthorityOrganizationInformationPageComponent implements OnInit {
  public busy: Subscription;
  public

  private routeUtils: RouteUtils;

  constructor(
    private configService: ConfigService,
    private activatedRoute: ActivatedRoute,
    private route: ActivatedRoute,
    private router: Router) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.routeUtils.routeWithin(routePath);
  }

  public addOrgInfo(): void {

  }

  public uploadOrgAgreement(): void {

  }

  public ngOnInit(): void {

  }
}
