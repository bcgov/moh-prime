import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { IProgressIndicator } from '@shared/components/progress-indicator/progress-indicator.component';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

@Component({
  selector: 'app-site-progress-indicator',
  templateUrl: './site-progress-indicator.component.html',
  styleUrls: ['./site-progress-indicator.component.scss']
})
export class SiteProgressIndicatorComponent implements OnInit, IProgressIndicator {
  @Input() public inProgress: boolean;
  @Input() public message: string;
  @Input() public template: TemplateRef<any>;
  @Input() public noContent: boolean;
  @Input() public steps: object[];


  public currentRoute: string;
  public routes: string[];
  public prefix: string;

  public SiteRoutes = HealthAuthSiteRegRoutes;

  constructor(
    private router: Router
  ) {
    this.currentRoute = RouteUtils.currentRoutePath(this.router.url);
    this.routes = HealthAuthSiteRegRoutes.siteRegistrationRouteOrder();
    this.prefix = 'Registration';
    this.steps = HealthAuthSiteRegRoutes.siteSteps();
  }

  public ngOnInit() { }
}
