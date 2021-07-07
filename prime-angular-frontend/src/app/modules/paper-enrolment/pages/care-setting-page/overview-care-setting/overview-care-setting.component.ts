import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
@Component({
  selector: 'app-overview-care-setting',
  templateUrl: './overview-care-setting.component.html',
  styleUrls: ['./overview-care-setting.component.scss']
})
export class OverviewCareSettingComponent implements OnInit {
  @Input() careSettings = [];
  @Input() healthAuthorities = [];
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  public routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  ngOnInit(): void {
  }

  public onRoute(routePath: string | string[]) {
    routePath = (Array.isArray(routePath)) ? routePath : [routePath];
    this.routeUtils.routeRelativeTo(routePath);
  }
}
