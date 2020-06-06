import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { IProgressIndicator } from '@shared/components/progress-indicator/progress-indicator.component';

import { SiteRoutes } from '@registration/site-registration.routes';

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
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.currentRoute = this.getCurrentRoute();

    // Possible route pathways within site registration
    const routePaths = [
      SiteRoutes.organizationRegistrationRouteOrder(),
      SiteRoutes.siteRegistrationRouteOrder()
    ];

    this.routes = routePaths.filter(rp => rp.includes(this.currentRoute)).shift();
    this.prefix = 'Registration';
  }

  public ngOnInit() { }

  private getCurrentRoute(): string {
    return this.router.url
      .split('/')
      // Remove URL params that are numbers
      .filter(p => !/^\d+$/.test(p))
      // Blacklisted URL params
      .filter(p => !['new'].includes(p))
      .pop(); // Current route is the last index
  }
}
