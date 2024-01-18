import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { AuthorizedUserResource } from '@core/resources/authorized-user-resource.service';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { Role } from '@auth/shared/enum/role.enum';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';

@Component({
  selector: 'app-authorized-user-review',
  templateUrl: './authorized-user-review.component.html',
  styleUrls: ['./authorized-user-review.component.scss']
})
export class AuthorizedUserReviewComponent implements OnInit {
  public routeUtils: RouteUtils;
  public busy: Subscription;
  public user: AuthorizedUser;
  public healthAuthorities: Config<number>[];

  public Role = Role;
  public AccessStatusEnum = AccessStatusEnum;

  constructor(
    private route: ActivatedRoute,
    private authorizedUserResource: AuthorizedUserResource,
    private configService: ConfigService,
    private dialog: MatDialog,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.HEALTH_AUTHORITIES);
    this.healthAuthorities = this.configService.healthAuthorities;
  }

  public onApprove() {
    this.busy = this.authorizedUserResource
      .approveAuthorizedUser(this.route.snapshot.params.auid)
      .subscribe(() => this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.HEALTH_AUTH_AUTHORIZED_USERS]));
  }

  public onDelete() {

    const data: DialogOptions = {
      title: 'Delete Authorized User',
      message: 'Are you sure you want to delete this authorized user request?',
      actionText: "Yes"
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe((result: boolean) => {
        if (result) {
          this.authorizedUserResource
            .deleteAuthorizedUser(this.route.snapshot.params.auid)
            .subscribe(() => this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.HEALTH_AUTH_AUTHORIZED_USERS]));
        }
      });
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.HEALTH_AUTH_AUTHORIZED_USERS]);
  }

  public ngOnInit(): void {
    this.getAuthorizedUser();
  }

  protected getAuthorizedUser() {
    this.busy = this.authorizedUserResource
      .getAuthorizedUserById(this.route.snapshot.params.auid)
      .subscribe((user: AuthorizedUser) => this.user = user);
  }
}
