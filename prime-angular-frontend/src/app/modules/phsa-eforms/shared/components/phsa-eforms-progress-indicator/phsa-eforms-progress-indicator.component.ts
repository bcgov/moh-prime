import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { IProgressIndicator } from '@shared/components/progress-indicator/progress-indicator.component';

import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';

@Component({
  selector: 'app-phsa-eforms-progress-indicator',
  templateUrl: './phsa-eforms-progress-indicator.component.html',
  styleUrls: ['./phsa-eforms-progress-indicator.component.scss']
})
export class PhsaEformsProgressIndicatorComponent implements OnInit, IProgressIndicator {
  @Input() public inProgress: boolean;
  @Input() public message: string;
  @Input() public template: TemplateRef<any>;
  @Input() public noContent: boolean;

  @Input() public currentRoute: string;
  public routes: string[];
  public prefix: string;

  constructor(
    private router: Router
  ) {
    this.currentRoute = RouteUtils.currentRoutePath(this.router.url);
    this.routes = PhsaEformsRoutes.initialEnrolmentRouteOrder();
    this.prefix = 'Enrolment';
  }

  public ngOnInit(): void { }
}
