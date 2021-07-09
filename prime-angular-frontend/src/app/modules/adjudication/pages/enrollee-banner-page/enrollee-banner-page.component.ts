import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-enrollee-banner-page',
  templateUrl: './enrollee-banner-page.component.html',
  styleUrls: ['./enrollee-banner-page.component.scss']
})
export class EnrolleeBannerPageComponent implements OnInit {
  public locationCode: BannerLocationCode;
  public path: AdjudicationRoutes;

  public busy: Subscription;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.LOGIN_PAGE));
    this.locationCode = BannerLocationCode.ENROLMENT_LANDING_PAGE;
    this.path = AdjudicationRoutes.ENROLLEES;
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.BANNERS]);
  }

  public ngOnInit(): void { }
}
