import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AuthService } from '@auth/shared/services/auth.service';

import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { AppConfig, APP_CONFIG } from 'app/app-config.module';

@Component({
  selector: 'app-collection-notice-page',
  templateUrl: './collection-notice-page.component.html',
  styleUrls: ['./collection-notice-page.component.scss']
})
export class CollectionNoticePageComponent implements OnInit {
  public isFull: boolean;
  public routeUtils: RouteUtils;
  public satEformsSupportEmail: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    @Inject(APP_CONFIG) private config: AppConfig,
  ) {
    this.isFull = true;
    this.routeUtils = new RouteUtils(route, router, SatEformsRoutes.MODULE_PATH);
    this.satEformsSupportEmail = config.satEformsSupportEmail;
  }

  public onAccept(): void {
    this.authService.hasJustLoggedIn = false;
    this.nextRoute();
  }

  public ngOnInit(): void {
    this.authService.hasJustLoggedIn = true;
  }

  private nextRoute(): void {
    // Redirect to demographic and allow the guards to figure
    // out the proper routing and enrollee existence
    this.routeUtils.routeRelativeTo([SatEformsRoutes.ENROLMENTS, 0, SatEformsRoutes.DEMOGRAPHIC]);
  }
}
