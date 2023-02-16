import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { IProgressIndicator, IStep } from '@shared/components/progress-indicator/progress-indicator.component';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-enrolment-progress-indicator',
  templateUrl: './enrolment-progress-indicator.component.html',
  styleUrls: ['./enrolment-progress-indicator.component.scss']
})
export class EnrolmentProgressIndicatorComponent implements OnInit, IProgressIndicator {
  @Input() public inProgress: boolean;
  @Input() public message: string;
  @Input() public template: TemplateRef<any>;
  @Input() public noContent: boolean;
  @Input() public steps: IStep[];

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
    this.routes = EnrolmentRoutes.initialEnrolmentRouteOrder();
    this.prefix = 'Enrolment';
  }

  public ngOnInit(): void {
    switch (this.currentRoute) {
      case EnrolmentRoutes.CARE_SETTING:
      case EnrolmentRoutes.BCSC_DEMOGRAPHIC:
      case EnrolmentRoutes.REMOTE_ACCESS:
      case EnrolmentRoutes.OBO_SITES:
      case EnrolmentRoutes.REGULATORY:
      case EnrolmentRoutes.SELF_DECLARATION:
      case EnrolmentRoutes.OVERVIEW:
      case EnrolmentRoutes.SUBMISSION_CONFIRMATION:
        this.steps = EnrolmentRoutes.enrolmentSteps();
        break;
      case EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY:
      case EnrolmentRoutes.PENDING_ACCESS_TERM:
      case EnrolmentRoutes.NEXT_STEPS:
        this.steps = EnrolmentRoutes.toaSteps();
        break;
    }
  }
}
