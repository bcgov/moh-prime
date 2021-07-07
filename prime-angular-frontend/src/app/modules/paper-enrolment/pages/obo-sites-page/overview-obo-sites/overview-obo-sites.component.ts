import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';


@Component({
  selector: 'app-overview-obo-sites',
  templateUrl: './overview-obo-sites.component.html',
  styleUrls: ['./overview-obo-sites.component.scss']
})
export class OverviewOboSitesComponent implements OnInit {
  @Input() careSettings = [];
  @Input() oboSites = [];
  @Input() healthAuthority = null;
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
