import { Component, OnInit, Input } from '@angular/core';

import { IProgressIndicator } from '@shared/components/progress-indicator/progress-indicator.component';

import { SiteRoutes } from 'app/modules/site-registration/site-registration.routes';

@Component({
  selector: 'app-site-progress-indicator',
  templateUrl: './site-progress-indicator.component.html',
  styleUrls: ['./site-progress-indicator.component.scss']
})
export class SiteProgressIndicatorComponent implements OnInit, IProgressIndicator {
  @Input() public currentRoute: string;
  @Input() public inProgress: boolean;
  @Input() public message: string;
  public routes: string[];
  public prefix: string;

  public SiteRoutes = SiteRoutes;

  constructor() {
    this.routes = SiteRoutes.initialRegistrationRouteOrder();
    this.prefix = 'Registration';
  }

  public ngOnInit() { }
}
