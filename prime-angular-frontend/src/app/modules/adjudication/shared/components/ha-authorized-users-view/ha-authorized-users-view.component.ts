import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, noop, of, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

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
    private dialog: MatDialog,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onAdd() {
    this.routeUtils.routeRelativeTo([AdjudicationRoutes.CREATE_USER]);
  }

  public onRemove(id: number) {
    const data: DialogOptions = {
      title: 'Remove Authorized User',
      message: 'Are you sure you want to remove this authorized user?',
      actionText: 'Remove Authorized User',
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? of(noop)
            : EMPTY
        ),
      )
      .subscribe(() =>
        this.healthAuthorityResource.deleteAuthorizedUser(id)
          // TODO Nathan this is an observable in an observable you'll want to use exhaustMap, and
          //  put this.busy in front of this observable instead of the inner one
          .subscribe(() => this.getAuthorizedUser())
      );
  }

  public onEdit(id: number) {
    this.routeUtils.routeRelativeTo([id]);
  }

  public getUserPropertiess(user: AuthorizedUser) {
    const formatDatePipe = new FormatDatePipe();
    return [
      {
        key: 'Date of Birth',
        value: formatDatePipe.transform(user.dateOfBirth)
      },
      {
        key: 'Email',
        value: user.email
      }
    ];
  }

  public ngOnInit(): void {
    // TODO you'll want to put this.busy in front of this call, and then use an empty subscribe
    this.getAuthorizedUser();
  }

  private getAuthorizedUser(): void {
    // TODO commented out to draw attention to the above TODOs
    // this.busy = this.healthAuthorityResource.getAuthorizedUsersByHealthAuthority(this.route.snapshot.params.haid)
    // TODO you'll want to use a pipe and map to set this instead of subscribe
    //   .subscribe((users: AuthorizedUser[]) => this.authorizedUsers = users);
  }
}
