import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-site-review',
  templateUrl: './site-review.component.html',
  styleUrls: ['./site-review.component.scss']
})
export class SiteReviewComponent {
  @Input() public site: Site;
  @Input() public showEditRedirect: boolean;
  @Output() public route: EventEmitter<string>;

  public SiteRoutes = SiteRoutes;

  constructor() {
    this.showEditRedirect = false;
    this.route = new EventEmitter<string>();
  }

  public onRoute(routePath: string): void {
    this.route.emit(routePath);
  }
}
