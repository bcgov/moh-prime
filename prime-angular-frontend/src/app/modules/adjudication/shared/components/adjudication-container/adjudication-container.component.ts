import { Component, OnInit, Input, TemplateRef, Output, EventEmitter, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';

import { Observable, Subscription, EMPTY, of, noop, concat, pipe } from 'rxjs';
import { map, exhaustMap, tap } from 'rxjs/operators';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { SubmissionAction } from '@shared/enums/submission-action.enum';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import {
  ClaimEnrolleeComponent,
  ClaimEnrolleeAction,
  ClaimActionEnum
} from '@shared/components/dialogs/content/claim-enrollee/claim-enrollee.component';
import { MatTableDataSourceUtils } from '@shared/modules/ngx-material/mat-table-data-source-utils.class';
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudication-container',
  templateUrl: './adjudication-container.component.html',
  styleUrls: ['./adjudication-container.component.scss']
})
export class AdjudicationContainerComponent implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public content: TemplateRef<any>;
  @Output() public action: EventEmitter<void>;

  public busy: Subscription;
  public dataSource: MatTableDataSource<HttpEnrollee>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  private routeUtils: RouteUtils;

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
    protected route: ActivatedRoute,
    protected router: Router,
    private authService: AuthService,
    private adjudicationResource: AdjudicationResource,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));

    this.action = new EventEmitter<void>();

    this.hasActions = false;
    this.dataSource = new MatTableDataSource<HttpEnrollee>([]);

    this.showSearchFilter = false;
  }

  public onSearch(search: string | null): void {
    this.routeUtils.updateQueryParams({ search });
  }

  public onFilter(status: EnrolmentStatus | null): void {
    this.routeUtils.updateQueryParams({ status });
  }

  public onRefresh(): void {
    this.getDataset(this.route.snapshot.queryParams);
  }

  public onNotify(enrolleeId: number) {
    this.adjudicationResource
      .sendEnrolleeReminderEmail(enrolleeId)
      .subscribe();
  }

  public onClaim(enrolleeId: number) {
    this.adjudicationResource
      .setEnrolleeAdjudicator(enrolleeId)
      .subscribe((updatedEnrollee: HttpEnrollee) => this.updateEnrollee(updatedEnrollee));
  }

  public onDisclaim(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Disclaim Enrolment',
      component: ManualFlagNoteComponent
    };

    this.busy = this.dialog.open(ClaimEnrolleeComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: ClaimEnrolleeAction }) => {
          if (!result) { return EMPTY; }

          if (result.output.action === ClaimActionEnum.Disclaim) {
            return this.adjudicationResource.removeEnrolleeAdjudicator(enrolleeId);
          } else if (result.output.action === ClaimActionEnum.Claim) {
            return concat(
              this.adjudicationResource.removeEnrolleeAdjudicator(enrolleeId),
              this.adjudicationResource.setEnrolleeAdjudicator(enrolleeId)
            );
          }
        })
      )
      .subscribe((updatedEnrollee: HttpEnrollee) => this.updateEnrollee(updatedEnrollee));
  }

  public onApprove(enrollee: HttpEnrollee) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: 'Are you sure you want to approve this enrolment?',
      actionText: 'Approve Enrolment',
      component: NoteComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrollee.id, SubmissionAction.APPROVE)
      )
      .subscribe((approvedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(approvedEnrollee);
        this.action.emit();
      });
  }

  public onDecline(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Decline Enrollee',
      message: `When declined the enrollee will not have access to PRIME and their Terms of Access will be revoked,
         Are you sure you want to lock this enrollee ?`,
      actionType: 'warn',
      actionText: 'Decline Enrollee',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, SubmissionAction.DECLINE_PROFILE)
      )
      .subscribe((declinedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(declinedEnrollee);
        this.action.emit();
      });
  }

  public onLock(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Lock Enrollee',
      message: 'When locked the enrollee will not have access to PRIME. Are you sure you want to lock this enrollee?',
      actionType: 'warn',
      actionText: 'Lock Enrollee',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, SubmissionAction.LOCK_PROFILE)
      )
      .subscribe((lockedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(lockedEnrollee);
        this.action.emit();
      });
  }

  public onUnlock(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Unlock Enrollee',
      message: 'When unlocked the enrollee will be able to update their enrolment. Are you sure you want to unlock this enrollee?',
      actionType: 'warn',
      actionText: 'Unlock Enrollee',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, SubmissionAction.ENABLE_EDITING)
      )
      .subscribe((lockedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(lockedEnrollee);
        this.action.emit();
      });
  }

  public onEnableEnrollee(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Enable Enrollee',
      message: 'When enabled the enrollee will be able to access PRIME. Are you sure you want to unlock this enrollee?',
      actionType: 'warn',
      actionText: 'Enable Enrollee',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, SubmissionAction.ENABLE_PROFILE)
      )
      .subscribe((enableEnrollee: HttpEnrollee) => {
        this.updateEnrollee(enableEnrollee);
        this.action.emit();
      });
  }

  public onEnableEditing(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Enable Editing',
      message: 'Are you sure you want to enable editing for this enrollee?',
      actionType: 'warn',
      actionText: 'Enable Editing',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, SubmissionAction.ENABLE_EDITING)
      )
      .subscribe((enableEnrollee: HttpEnrollee) => {
        this.updateEnrollee(enableEnrollee);
        this.action.emit();
      });
  }

  public onRerunRules(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Rerun Rules',
      message: 'Are you sure you want to rerun the rules for this enrollee?',
      actionType: 'warn',
      actionText: 'Rerun Rules',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, SubmissionAction.RERUN_RULES)
      )
      .subscribe((enableEnrollee: HttpEnrollee) => {
        this.updateEnrollee(enableEnrollee);
        this.action.emit();
      });
  }

  public onDelete(enrolleeId: number) {
    const data: DialogOptions = {
      ...this.defaultOptions.delete('enrollee'),
      component: NoteComponent,
    };

    if (this.authService.isSuperAdmin()) {
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: { output: string }) => {
            if (result) {
              return (result.output)
                ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, result.output)
                : of(noop);
            }
            return EMPTY;
          }),
          exhaustMap(() => this.adjudicationResource.deleteEnrollee(enrolleeId)),
        )
        .subscribe(() => this.routeUtils.routeTo(AdjudicationRoutes.MODULE_PATH));
    }
  }

  public onToggleManualAdj(enrollee: HttpEnrollee) {
    const flagText = enrollee.alwaysManual ? 'Unflag' : 'Flag';
    const data: DialogOptions = {
      title: `${flagText} Enrollee`,
      message: `Are you sure you want to ${flagText} this enrollee for manual adjudication?`,
      actionText: `${flagText} Enrollee`,
      data: { enrollee }
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? of(noop)
            : EMPTY
        ),
        exhaustMap(() =>
          this.adjudicationResource.updateEnrolleeAlwaysManual(enrollee.id, !enrollee.alwaysManual)
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrollee.id))
      )
      .subscribe((flaggedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(flaggedEnrollee);
        this.action.emit();
      });
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public ngOnInit() {
    // Use existing query params for initial search, and
    // update results on query param change
    this.route.queryParams
      .subscribe((queryParams: { [key: string]: any }) => this.getDataset(queryParams));
  }

  private getDataset(queryParams: { search?: string, status?: number }) {
    const enrolleeId = this.route.snapshot.params.id;
    const results$ = (enrolleeId)
      ? this.getEnrolleeById(enrolleeId)
      : this.getEnrollees(queryParams);

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

  private getEnrollees({ search, status }: { search?: string, status?: number }) {
    return this.adjudicationResource.getEnrollees(search, status)
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
  }

  private updateEnrollee(enrollee: HttpEnrollee) {
    this.dataSource.data = MatTableDataSourceUtils
      .update<HttpEnrollee>(this.dataSource, 'id', enrollee);
  }

  private removeEnrollee(enrollee: HttpEnrollee) {
    this.dataSource.data = MatTableDataSourceUtils
      .delete<HttpEnrollee>(this.dataSource, 'id', enrollee.id);
  }

  private adjudicationActionPipe(enrolleeId: number, action: SubmissionAction) {
    let note: string;

    return pipe(
      exhaustMap((result: { output: string }) => {
        if (result?.output) {
          note = result.output;
        }

        return (result)
          ? of(noop)
          : EMPTY;
      }),
      exhaustMap(() => this.adjudicationResource.submissionAction(enrolleeId, action)),
      exhaustMap(() => this.adjudicationResource.createEnrolmentReference(enrolleeId)),
      exhaustMap(() =>
        (note)
          ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, note, true)
          : of(noop)
      ),
      exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
    );
  }
}
