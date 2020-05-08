import { Component, OnInit, Input, TemplateRef, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';

import { Observable, Subscription, EMPTY, of, noop, concat } from 'rxjs';
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
import {
  ManualFlagNoteComponent,
  ManualFlagNoteOutput
} from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';

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
  @Output() public action: EventEmitter<void>;

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

    this.action = new EventEmitter<void>();

    this.hasActions = false;
    this.columns = ['uniqueId', 'name', 'appliedDate', 'status', 'approvedDate', 'adjudicator', 'actions'];
    this.dataSource = new MatTableDataSource<HttpEnrollee>([]);

    this.showSearchFilter = false;
    this.baseRoutePath = [AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.ENROLLEES];
  }

  public onSearch(search: string | null): void {
    this.setQueryParams({ search });
  }

  public onFilter(status: EnrolmentStatus | null): void {
    this.setQueryParams({ status });
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
      data: { enrollee },
      component: ManualFlagNoteComponent
    };

    let manualFlagNote: ManualFlagNoteOutput;

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: ManualFlagNoteOutput }) => {
          if (result && result.output) {
            manualFlagNote = result.output;
            return of(noop);
          }
          return EMPTY;
        }),
        exhaustMap(() =>
          this.adjudicationResource.updateEnrolleeAlwaysManual(enrollee.id, manualFlagNote.alwaysManual)
        ),
        exhaustMap(() =>
          (manualFlagNote.note)
            ? this.adjudicationResource.createAdjudicatorNote(enrollee.id, manualFlagNote.note)
            : of(noop)
        ),
        exhaustMap(() => this.adjudicationResource.submissionAction(enrollee.id, SubmissionAction.APPROVE)),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrollee.id))
      )
      .subscribe((approvedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(approvedEnrollee);
        this.action.emit();
      });
  }

  public onDecline(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Decline Enrolment',
      message: 'Are you sure you want to decline this enrolment?',
      actionType: 'warn',
      actionText: 'Decline Enrolment',
      component: NoteComponent,
    };

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
        exhaustMap(() => this.adjudicationResource.submissionAction(enrolleeId, SubmissionAction.LOCK_PROFILE)),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
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
        exhaustMap((result: { output: string }) => {
          if (result) {
            return (result.output)
              ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, result.output)
              : of(noop);
          }
          return EMPTY;
        }),
        exhaustMap(() => this.adjudicationResource.submissionAction(enrolleeId, SubmissionAction.LOCK_PROFILE)),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
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
        exhaustMap((result: { output: string }) => {
          if (result) {
            return (result.output)
              ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, result.output)
              : of(noop);
          }
          return EMPTY;
        }),
        exhaustMap(() => this.adjudicationResource.submissionAction(enrolleeId, SubmissionAction.ENABLE_EDITING)),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
      )
      .subscribe((lockedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(lockedEnrollee);
        this.action.emit();
      });
  }

  public onDeclineEnrollee(enrolleeId: number) {
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
        exhaustMap((result: { output: string }) => {
          if (result) {
            return (result.output)
              ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, result.output)
              : of(noop);
          }
          return EMPTY;
        }),
        exhaustMap(() => this.adjudicationResource.submissionAction(enrolleeId, SubmissionAction.DECLINE_PROFILE)),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
      )
      .subscribe((declinedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(declinedEnrollee);
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
        exhaustMap((result: { output: string }) => {
          if (result) {
            return (result.output)
              ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, result.output)
              : of(noop);
          }
          return EMPTY;
        }),
        exhaustMap(() => this.adjudicationResource.submissionAction(enrolleeId, SubmissionAction.ENABLE_PROFILE)),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
      )
      .subscribe((enableEnrollee: HttpEnrollee) => {
        this.updateEnrollee(enableEnrollee);
        this.action.emit();
      });
  }

  public onDelete(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Delete Enrollee',
      message: 'Are you sure you want to delete this enrolment?',
      actionType: 'warn',
      actionText: 'Delete Enrollee',
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
        .subscribe((enrollee: HttpEnrollee) => this.routeTo(this.baseRoutePath));
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeWithin(routePath);
  }

  public ngOnInit() {
    // Use existing query params for initial search
    this.getDataset(this.route.snapshot.queryParams);

    // Update results on query param change
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

  private setQueryParams(queryParams: { search?: string, status?: number } = { search: null, status: null }) {
    // Passing `null` removes the query parameter from the URL
    queryParams = { ...this.route.snapshot.queryParams, ...queryParams };
    this.router.navigate([], { queryParams });
  }

  private resetQueryParams() {
    this.setQueryParams();
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
