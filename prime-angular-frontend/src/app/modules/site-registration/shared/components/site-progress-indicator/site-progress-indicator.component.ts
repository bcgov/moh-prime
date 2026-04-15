import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { IProgressIndicator, IStep } from '@shared/components/progress-indicator/progress-indicator.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteService } from '@registration/shared/services/site.service';

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
  @Input() public steps: IStep[];

  public currentRoute: string;
  public routes: string[];
  public prefix: string;

  public SiteRoutes = SiteRoutes;

  constructor(
    private router: Router,
    private organizationService: OrganizationService,
    private siteService: SiteService,
  ) {
    this.currentRoute = RouteUtils.currentRoutePath(this.router.url);

    const routePaths = this.getWorkflowRoutePaths();

    this.routes = routePaths.filter(rp => rp.includes(this.currentRoute)).shift();
    this.prefix = 'Registration';
  }

  public ngOnInit() {
    if (this.siteService.site) {
      if (this.siteService.site.careSettingCode) {
        switch (this.siteService.site.careSettingCode) {
          case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
            this.steps = SiteRoutes.pchpSiteSteps();
            break;
          case CareSettingEnum.DEVICE_PROVIDER:
            this.steps = SiteRoutes.deviceProviderSiteSteps();
            break;
          case CareSettingEnum.COMMUNITY_PHARMACY:
            this.steps = SiteRoutes.pharmacySiteSteps();
            break;
        }
      } else {
        //default to Pharmacy site steps
        this.steps = SiteRoutes.pharmacySiteSteps();
      }
    }
  }

  private getWorkflowRoutePaths() {
    // Possible route paths within claim organization workflow, otherwise
    // the default site registration workflow
    if (this.router.url.includes(SiteRoutes.CHANGE_SIGNING_AUTHORITY_WORKFLOW)) {
      return [SiteRoutes.claimOrganizationRoutes()];
    }

    return (!this.organizationService.organization?.hasAcceptedAgreement)
      // Combine organization and site routes, which includes
      // the organization agreement
      ? [SiteRoutes.initialRegistrationRouteOrder()]
      // Otherwise, split organization and site routes for
      // multiple registrations
      : [SiteRoutes.organizationRegistrationRouteOrder(), SiteRoutes.siteRegistrationRouteOrder()];
  }
}
