import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { IProgressIndicator } from '@enrolment/shared/components/progress-indicator/progress-indicator.component';

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
    this.currentRoute = this.getCurrentRoute();

    // Possible route pathways within site registration
    let routePaths = [
      SiteRoutes.organizationRegistrationRouteOrder(),
      SiteRoutes.siteRegistrationRouteOrder()
    ];

    if (!organizationService.organization.acceptedAgreementDate) {
      routePaths = [
        SiteRoutes.siteRegistrationRoutes()
      ]
    }

    this.routes = routePaths.filter(rp => rp.includes(this.currentRoute)).shift();
    this.prefix = 'Registration';
  }

  public ngOnInit() { }

  /**
   * @description
   * Determine the current route by removing query and URI params
   * that can't be mapped to existing module routes.
   */
  private getCurrentRoute(): string {
    const routerUrl = this.router.url;
    return routerUrl
      // Truncate query parameters
      .split('?')
      .shift()
      // List the remaining URI params
      .split('/')
      // Remove URI params that are numbers
      .filter(p => !/^\d+$/.test(p))
      // Remove blacklisted URI params
      .filter(p => !['new'].includes(p))
      .pop(); // Current route is the last index
  }
}
