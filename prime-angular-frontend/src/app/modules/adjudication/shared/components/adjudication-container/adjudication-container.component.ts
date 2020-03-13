import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatTableDataSource, MatDialog } from '@angular/material';

import { Observable, Subscription, EMPTY, of, noop } from 'rxjs';
import { map, exhaustMap, tap } from 'rxjs/operators';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ApproveEnrolmentComponent } from '@shared/components/dialogs/content/approve-enrolment/approve-enrolment.component';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudication-container',
  templateUrl: './adjudication-container.component.html',
  styleUrls: ['./adjudication-container.component.scss']
})
export class AdjudicationContainerComponent extends AbstractComponent implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public content: TemplateRef<any>;

  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<HttpEnrollee>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private authService: AuthService,
    private adjudicationResource: AdjudicationResource,
    private dialog: MatDialog
  ) {
    super(route, router);

    this.hasActions = false;
    this.columns = ['uniqueId', 'name', 'appliedDate', 'status', 'approvedDate', 'adjudicator', 'actions'];
    this.dataSource = new MatTableDataSource<HttpEnrollee>([]);

    this.showSearchFilter = !!this.route.snapshot.params.id;
    this.baseRoutePath = [AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.ENROLLEES];
  }

  public onSearch(search: string | null): void {
    this.setQueryParams({ search });
    this.getDataset();
  }

  public onFilter(status: EnrolmentStatus | null): void {
    this.setQueryParams({ status });
    this.getDataset();
  }

  public onRefresh(): void {
    this.getDataset();
  }

  public onClaim(enrolleeId: number) {
    this.adjudicationResource
      .setEnrolleeAdjudicator(enrolleeId)
      .subscribe((updatedEnrollee: HttpEnrollee) => this.updateEnrollee(updatedEnrollee));
  }

  public onDisclaim(enrolleeId: number) {
    this.adjudicationResource
      .removeEnrolleeAdjudicator(enrolleeId)
      .subscribe((updatedEnrollee: HttpEnrollee) => this.updateEnrollee(updatedEnrollee));
  }

  public onApprove(enrollee: HttpEnrollee) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: 'Are you sure you want to approve this enrolment?',
      actionText: 'Approve Enrolment',
      data: { enrollee },
      component: ApproveEnrolmentComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: boolean }) => {
          if (result) {
            (result.output)
              ? this.adjudicationResource.updateEnrolleeAlwaysManual(enrollee.id, result.output)
              : of(noop);
          }

          return EMPTY;
        }),
        exhaustMap(() =>
          this.adjudicationResource.createEnrolmentStatus(enrollee.id, EnrolmentStatus.REQUIRES_TOA)
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrollee.id))
      )
      .subscribe((approvedEnrollee: HttpEnrollee) => this.updateEnrollee(approvedEnrollee));
  }

  public onDecline(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Decline Enrolment',
      message: 'Are you sure you want to decline this enrolment?',
      actionType: 'warn',
      actionText: 'Decline Enrolment'
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.createEnrolmentStatus(enrolleeId, EnrolmentStatus.LOCKED)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId)),
      )
      .subscribe((declinedEnrollee: HttpEnrollee) => this.updateEnrollee(declinedEnrollee));
  }

  public onUnlock(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Unlock for Editing',
      message: 'When unlocked the enrollee will be able to update their enrolment. Are you sure you want to unlock this enrolment?',
      actionType: 'warn',
      actionText: 'Unlock for Editing'
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.createEnrolmentStatus(enrolleeId, EnrolmentStatus.ACTIVE)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
      )
      .subscribe((lockedEnrollee: HttpEnrollee) => this.updateEnrollee(lockedEnrollee));
  }

  public onDelete(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Delete Enrolment',
      message: 'Are you sure you want to delete this enrolment?',
      actionType: 'warn',
      actionText: 'Delete Enrolment'
    };

    if (this.authService.isSuperAdmin()) {
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.adjudicationResource.deleteEnrollee(enrolleeId)
              : EMPTY
          )
        )
        .subscribe((enrollee: HttpEnrollee) => this.routeTo(this.baseRoutePath));
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeWithin(routePath);
  }

  public ngOnInit() {
    this.getDataset();
  }

  private getDataset() {
    const enrolleeId = this.route.snapshot.params.id;
    const results$ = (enrolleeId)
      ? this.getEnrolleeById(enrolleeId)
      : this.getEnrollees();

    this.busy = results$
      .subscribe((enrollees: HttpEnrollee[]) => this.dataSource.data = enrollees);
  }

  private getEnrolleeById(enrolleeId: number): Observable<HttpEnrollee[]> {
    return this.adjudicationResource
      .getEnrolleeById(enrolleeId)
      .pipe(
        map((enrollee: HttpEnrollee) => [enrollee]),
        tap(() => this.showSearchFilter = false)
      );
  }

  private getEnrollees() {
    const { search, status } = this.route.snapshot.queryParams;
    return this.adjudicationResource.getEnrollees(search, status)
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
  }

  private setQueryParams(params: { search?: string, status?: number, page?: number } = { search: null, status: null, page: null }) {
    // Passing `null` removes the query parameter from the URL
    const queryParams = { ...this.route.snapshot.queryParams, ...params };
    this.router.navigate([], { queryParams });
  }

  // TODO split out into service and use generics for managing data tables, and update with add row
  private updateEnrollee(enrollee: HttpEnrollee) {
    this.dataSource.data = this.dataSource.data
      .map((currentEnrollee: HttpEnrollee) =>
        (currentEnrollee.id === enrollee.id) ? enrollee : currentEnrollee
      );
  }

  // TODO split out into service and use generics for managing data tables, and update with add row
  private removeEnrollee(enrollee: HttpEnrollee) {
    this.dataSource.data = this.dataSource.data
      .filter((currentEnrollee: HttpEnrollee) => currentEnrollee.id !== enrollee.id);
  }
}
