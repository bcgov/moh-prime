import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { IProgressIndicator } from '@shared/components/progress-indicator/progress-indicator.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationService } from '@registration/shared/services/organization.service';

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

  public currentRoute: string;
  public routes: string[];
  public prefix: string;

  public SiteRoutes = SiteRoutes;

  constructor(
    private router: Router,
    private organizationService: OrganizationService
  ) {
    this.currentRoute = RouteUtils.currentRoutePath(this.router.url);

    // Possible route pathways within site registration
    const routePaths = (!organizationService.organization?.hasAcceptedAgreement)
      // Combine organization and site routes, which includes
      // the organization agreement
      ? [SiteRoutes.initialRegistrationRouteOrder()]
      // Otherwise, split organization and site routes for
      // multiple registrations
      : [SiteRoutes.organizationRegistrationRouteOrder(), SiteRoutes.siteRegistrationRouteOrder()];

    this.routes = routePaths.filter(rp => rp.includes(this.currentRoute)).shift();
    this.prefix = 'Registration';
  }

  public ngOnInit() { }
}
