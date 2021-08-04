import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

import { RemoteUsersForm } from './remote-users-form.model';

// TODO drop specific routes out of remote-user-review and always pass in
// TODO should be a message of no remote users added if none
@Component({
  selector: 'app-remote-users-overview',
  template: `
    <app-remote-user-review [remoteUsers]="remoteUsers?.remoteUsers"
                            [showEditRedirect]="showEditRedirect"
                            [editRoute]="HealthAuthSiteRegRoutes.REMOTE_USERS"
                            (route)="onRoute($event)"></app-remote-user-review>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RemoteUsersOverviewComponent extends AbstractOverview {
  @Input() public remoteUsers: RemoteUsersForm;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }
}
