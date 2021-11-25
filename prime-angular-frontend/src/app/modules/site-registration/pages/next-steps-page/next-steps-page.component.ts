import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-next-steps-page',
  templateUrl: './next-steps-page.component.html',
  styleUrls: ['./next-steps-page.component.scss']
})
export class NextStepsPageComponent implements OnInit {
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public onSubmit() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS]);
  }

  public ngOnInit(): void {
    this.isCompleted = this.organizationService.organization.completed;
  }
}
