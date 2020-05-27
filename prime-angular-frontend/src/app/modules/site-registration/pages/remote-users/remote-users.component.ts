import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-remote-users',
  templateUrl: './remote-users.component.html',
  styleUrls: ['./remote-users.component.scss']
})
export class RemoteUsersComponent implements OnInit {
  public busy: Subscription;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public hasRemoteUsers: boolean;
  public remoteUsers: RemoteUser[];
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onSubmit() {
    // TODO apply validation if remote users is on, but there are no remote users
    // TODO submit remote users
    //   const payload = this.siteRegistrationStateService.site;
    //   this.siteRegistrationResource
    //     .updateSite(payload)
    //     .subscribe(() =>
    this.nextRoute();
    //     );
  }

  public onRemove(index: number) {
    // TODO update and then remove the from the list locally
    this.remoteUsers.splice(index, 1);
  }

  public onToggleRemoteUsers(change: MatSlideToggleChange) {
    this.hasRemoteUsers = change.checked;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.HOURS_OPERATION);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.SIGNING_AUTHORITY);
    }
  }

  // TODO toggle on/off
  // TODO toggle show if no users in list

  public ngOnInit(): void {
    // TODO get a list of remote users
    this.remoteUsers = [
      {
        firstName: 'Martin',
        lastName: 'Pultz',

      }
    ];
    // Automatically set based on results, but has to be
    // manually toggled by the user to change it and
    // avoid validations
    this.hasRemoteUsers = !!this.remoteUsers.length;
  }
}
