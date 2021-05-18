import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { AuthorizedUser } from '@shared/models/authorized-user.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';

@Component({
  selector: 'app-ha-authorized-users-view',
  templateUrl: './ha-authorized-users-view.component.html',
  styleUrls: ['./ha-authorized-users-view.component.scss']
})
export class HaAuthorizedUsersViewComponent implements OnInit {
  public busy: Subscription;
  public authorizedUsers: AuthorizedUser[];

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private healthAuthorityResource: HealthAuthorityResource,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onView(id: number) {
    this.routeUtils.routeRelativeTo([id]);
  }

  public isApproved(user: AuthorizedUser) {
    return (user.status === AccessStatusEnum.APPROVED);
  }

  public isUnderReview(user: AuthorizedUser) {
    return (user.status === AccessStatusEnum.UNDER_REVIEW);
  }

  public getUserProperties(user: AuthorizedUser) {
    return [
      {
        key: 'Job Title',
        value: user.jobRoleTitle
      }
    ];
  }

  public ngOnInit(): void {
    this.busy = this.healthAuthorityResource
      .getAuthorizedUsersByHealthAuthority(this.route.snapshot.params.haid)
      .subscribe((users: AuthorizedUser[]) => this.authorizedUsers = users);
  }
}
