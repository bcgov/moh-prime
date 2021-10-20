import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { RemoteUser } from '@lib/models/remote-user.model';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-remote-user-review',
  templateUrl: './remote-user-review.component.html',
  styleUrls: ['./remote-user-review.component.scss']
})
export class RemoteUserReviewComponent implements OnInit {
  @Input() public remoteUsers: RemoteUser[];
  @Input() public showEditRedirect: boolean;
  @Input() public editRoute: string;
  @Output() public route: EventEmitter<string>;
  public SiteRoutes = SiteRoutes;

  constructor() {
    this.showEditRedirect = false;
    this.editRoute = SiteRoutes.REMOTE_USERS;
    this.route = new EventEmitter<string>();
  }

  public onRoute(routePath: string) {
    this.route.emit(routePath);
  }

  public ngOnInit(): void { }
}
