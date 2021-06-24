import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { IProgressIndicator } from '@shared/components/progress-indicator/progress-indicator.component';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

@Component({
  selector: 'app-paper-enrolment-progress-indicator',
  templateUrl: './paper-enrolment-progress-indicator.component.html',
  styleUrls: ['./paper-enrolment-progress-indicator.component.scss']
})
export class PaperEnrolmentProgressIndicatorComponent implements OnInit, IProgressIndicator {
  @Input() public inProgress: boolean;
  @Input() public message: string;
  @Input() public template: TemplateRef<any>;
  @Input() public noContent: boolean;

  @Input() public currentRoute: string;
  public routes: string[];
  public prefix: string;

  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    private router: Router
  ) {
    this.currentRoute = RouteUtils.currentRoutePath(this.router.url);
    this.routes = PaperEnrolmentRoutes.routeOrder();
    this.prefix = 'Enrolment';
  }

  public ngOnInit(): void { }
}
