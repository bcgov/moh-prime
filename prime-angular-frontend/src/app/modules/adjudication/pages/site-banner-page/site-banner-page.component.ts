import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-site-banner-page',
  templateUrl: './site-banner-page.component.html',
  styleUrls: ['./site-banner-page.component.scss']
})
export class SiteBannerPageComponent implements OnInit {
  public locationCode: BannerLocationCode;
  public path: AdjudicationRoutes;

  public busy: Subscription;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.LOGIN_PAGE));
    this.locationCode = BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE;
    this.path = AdjudicationRoutes.SITE_REGISTRATIONS;
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.BANNERS]);
  }

  public ngOnInit(): void { }
}
