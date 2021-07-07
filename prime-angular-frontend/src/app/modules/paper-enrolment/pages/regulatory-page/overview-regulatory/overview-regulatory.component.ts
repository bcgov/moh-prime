import { Component, OnInit, Input } from '@angular/core';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-overview-regulatory',
  templateUrl: './overview-regulatory.component.html',
  styleUrls: ['./overview-regulatory.component.scss']
})
export class OverviewRegulatoryComponent implements OnInit {
  @Input() certifications = [];
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
