import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigService } from '@config/config.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-authorized-users-page',
  templateUrl: './authorized-users-page.component.html',
  styleUrls: ['./authorized-users-page.component.scss']
})
export class AuthorizedUsersPageComponent implements OnInit {
  public busy: Subscription;
  public haName: string;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private configService: ConfigService,
  ) {
    this.haName = this.configService.healthAuthorities.find(ha => ha.code === this.route.snapshot.params.haid)?.name;
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public ngOnInit(): void { }
}
