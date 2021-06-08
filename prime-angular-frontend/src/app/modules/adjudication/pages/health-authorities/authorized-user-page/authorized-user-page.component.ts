import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-authorized-user-page',
  templateUrl: './authorized-user-page.component.html',
  styleUrls: ['./authorized-user-page.component.scss']
})
export class AuthorizedUserPageComponent implements OnInit {
  public busy: Subscription;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public ngOnInit(): void { }
}
