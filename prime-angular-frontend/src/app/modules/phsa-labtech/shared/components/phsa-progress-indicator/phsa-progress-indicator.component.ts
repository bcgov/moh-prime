import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';
import { IProgressIndicator } from '@shared/components/progress-indicator/progress-indicator.component';

@Component({
  selector: 'app-phsa-progress-indicator',
  templateUrl: './phsa-progress-indicator.component.html',
  styleUrls: ['./phsa-progress-indicator.component.scss']
})
export class PhsaProgressIndicatorComponent implements OnInit, IProgressIndicator {
  @Input() public inProgress: boolean;
  @Input() public message: string;
  @Input() public template: TemplateRef<any>;
  @Input() public noContent: boolean;

  @Input() public currentRoute: string;
  public routes: string[];
  public prefix: string;

  public PhsaLabtechRoutes = PhsaLabtechRoutes;

  constructor(
    private router: Router
  ) {
    this.currentRoute = RouteUtils.currentRoutePath(this.router.url);
    this.routes = PhsaLabtechRoutes.initialEnrolmentRouteOrder();
    this.prefix = 'Enrolment';
  }

  public ngOnInit(): void { }
}
