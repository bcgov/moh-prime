import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { IProgressIndicator2 } from '@shared/components/progress-indicator2/progress-indicator2.component';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-enrolment-progress-indicator',
  templateUrl: './enrolment-progress-indicator.component.html',
  styleUrls: ['./enrolment-progress-indicator.component.scss']
})
export class EnrolmentProgressIndicatorComponent implements OnInit, IProgressIndicator2 {
  @Input() public inProgress: boolean;
  @Input() public message: string;
  @Input() public template: TemplateRef<any>;
  @Input() public noContent: boolean;

  @Input() public currentRoute: string;
  // public currentRoute: string;
  public routes: string[];
  public prefix: string;

  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private router: Router,
    private enrolmentService: EnrolmentService
  ) {
    this.currentRoute = RouteUtils.currentRoutePath(this.router.url);

    // // Possible route pathways within site registration
    // const routePaths = (!organizationService.organization.acceptedAgreementDate)
    //   // Combine organization and site routes, which includes
    //   // the organization agreement
    //   ? [SiteRoutes.initialRegistrationRouteOrder()]
    //   // Otherwise, split organization and site routes for
    //   // multiple registrations
    //   : [SiteRoutes.organizationRegistrationRouteOrder(), SiteRoutes.siteRegistrationRouteOrder()];

    this.routes = EnrolmentRoutes.initialEnrolmentRouteOrder();
    this.prefix = 'Enrolment';
  }

  public ngOnInit(): void { }
}
