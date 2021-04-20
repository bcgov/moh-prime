import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { EMPTY, noop, of, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { HealthAuthorityResourceService } from '@core/resources/health-authority-resource.service';
import { HAAuthorizedUser } from '@shared/models/ha-authorized-user.model';
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
  public authorizedUsers: HAAuthorizedUser[];

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected healthAuthorityResourceService: HealthAuthorityResourceService,
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
        this.healthAuthorityResourceService
          .deleteAuthorizedUser(this.route.snapshot.params.haid, id)
          .subscribe(() => this.getAuthorizedUser())
      );
  }

  public onEdit(id: number) {
    this.routeUtils.routeRelativeTo([id]);
  }

  public getUserPropertiess(user: HAAuthorizedUser) {
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

  ngOnInit(): void {
    this.getAuthorizedUser();
  }

  private getAuthorizedUser(): void {
    this.busy = this.healthAuthorityResourceService.getAuthorizedUsersByHA(this.route.snapshot.params.haid)
      .subscribe((users: HAAuthorizedUser[]) => this.authorizedUsers = users);
  }

}
