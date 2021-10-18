import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { IProgressIndicator } from '@shared/components/progress-indicator/progress-indicator.component';

@Component({
  selector: 'app-sat-eforms-progress-indicator',
  templateUrl: './sat-eforms-progress-indicator.component.html',
  styleUrls: ['./sat-eforms-progress-indicator.component.scss']
})
export class SatEformsProgressIndicatorComponent implements OnInit, IProgressIndicator {
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
    this.routes = SatEformsRoutes.satEformsRouteOrder();
    this.prefix = 'Enrolment';
    // SAT enrolments are one time so never not in progress
    this.inProgress = true;
  }

  public ngOnInit(): void { }
}
