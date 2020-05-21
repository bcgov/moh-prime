import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-site-review',
  templateUrl: './site-review.component.html',
  styleUrls: ['./site-review.component.scss']
})
export class SiteReviewComponent {
  @Input() public showEditRedirect: boolean;
  @Input() public siteInput: Site;
  @Output() public route: EventEmitter<string>;

  public SiteRoutes = SiteRoutes;

  constructor() {
    this.showEditRedirect = false;
    this.route = new EventEmitter<string>();
  }

  public get site() {
    return (this.siteInput) ? this.siteInput : null;
  }

  public onRoute(routePath: string): void {
    this.route.emit(routePath);
  }

}
