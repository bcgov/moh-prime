import { Component, Input, Output, EventEmitter } from '@angular/core';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization } from '@registration/shared/models/organization.model';

@Component({
  selector: 'app-organization-review',
  templateUrl: './organization-review.component.html',
  styleUrls: ['./organization-review.component.scss']
})
export class OrganizationReviewComponent {
  @Input() public organization: Organization;
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
