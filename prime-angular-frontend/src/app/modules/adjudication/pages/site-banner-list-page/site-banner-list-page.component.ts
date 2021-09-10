import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { Banner } from '@shared/models/banner.model';
import { BannerResourceService } from '@shared/services/banner-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-site-banner-list-page',
  templateUrl: './site-banner-list-page.component.html',
  styleUrls: ['./site-banner-list-page.component.scss']
})
export class SiteBannerListPageComponent implements OnInit {
  public busy: Subscription;
  public banners: Banner[];

  private routeUtils: RouteUtils;

  constructor(
    private bannerResource: BannerResourceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.LOGIN_PAGE));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['../']);
  }

  public ngOnInit(): void {
    this.getBanners();
  }

  private getBanners(): void {
    this.bannerResource.getSiteLandingBanners()
      .subscribe((banners: Banner[]) => this.banners = banners.reverse());
  }
}
