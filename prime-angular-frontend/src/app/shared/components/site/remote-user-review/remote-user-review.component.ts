import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { RemoteUser } from '@lib/models/remote-user.model';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-remote-user-review',
  templateUrl: './remote-user-review.component.html',
  styleUrls: ['./remote-user-review.component.scss']
})
export class RemoteUserReviewComponent implements OnInit {
  /**
   * @description
   * List of remote users to be displayed.
   */
  @Input() public remoteUsers: RemoteUser[];
  /**
   * @description
   * Show the edit remote user(s) redirect icon.
   */
  @Input() public showEditRedirect: boolean;
  /**
   * @description
   * Route path for editing the remote user(s).
   */
  @Input() public editRoute: string;
  /**
   * @description
   * Route action emitter.
   */
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
