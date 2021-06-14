import { Component, EventEmitter, Inject, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, noop, Observable, of, OperatorFunction, pipe, Subscription, forkJoin } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { UtilsService } from '@core/services/utils.service';
import { ToastService } from '@core/services/toast.service';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { EnrolleeStatusAction } from '@shared/enums/enrollee-status-action.enum';
import { EnrolleeListViewModel, HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolleeNavigation } from '@shared/models/enrollee-navigation-model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import {
  AssignAction,
  AssignActionEnum,
  ClaimNoteComponent,
  ClaimType
} from '@shared/components/dialogs/content/claim-note/claim-note.component';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { EnrolleeNote } from '@enrolment/shared/models/enrollee-note.model';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SendBulkEmailComponent } from '@shared/components/dialogs/content/send-bulk-email/send-bulk-email.component';
import { BulkEmailType } from '@shared/enums/bulk-email-type';

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
  public enrollees: EnrolleeListViewModel[];
  public enrolleeNavigation: EnrolleeNavigation;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  protected routeUtils: RouteUtils;

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
    protected route: ActivatedRoute,
    protected router: Router,
    protected adjudicationResource: AdjudicationResource,
    private permissionService: PermissionService,
    private dialog: MatDialog,
    private utilsService: UtilsService,
    protected toastService: ToastService,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));

    this.action = new EventEmitter<void>();

    this.hasActions = false;
    this.enrollees = [];

    this.showSearchFilter = false;
  }

  public onSearch(textSearch: string | null): void {
    this.routeUtils.updateQueryParams({ textSearch });
  }

  public onFilter(status: EnrolmentStatus | null): void {
    this.routeUtils.updateQueryParams({ status });
  }

  public onRefresh(): void {
    this.getDataset(this.route.snapshot.params.id, this.route.snapshot.queryParams);
  }

  public onNotify(enrolleeId: number) {
    this.adjudicationResource.getEnrolleeById(enrolleeId)
      .pipe(
        exhaustMap((enrollee: HttpEnrollee) => {
          if (enrollee.email) {
            return of(enrollee);
          }
          this.toastService.openErrorToast('Enrollee does not have a contact email.');
          return EMPTY;
        }),
        exhaustMap((enrollee: HttpEnrollee) =>
          this.adjudicationResource.createInitiatedEnrolleeEmailEvent(enrollee.id)
            .pipe(map(() => enrollee))
        )
      )
      .subscribe((enrollee: HttpEnrollee) => {
        this.utilsService.mailTo(enrollee.email);
      });
  }

  public onAssign(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Assign Enrolment',
      component: ManualFlagNoteComponent,
      data: {
        reassign: false,
        type: ClaimType.ENROLLEE
      }
    };
    const assignPipe = pipe(
      exhaustMap((action: AssignAction) => {
        const response = { assigneeId: action.adjudicatorId };
        return (action.note)
          ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, action.note, false)
            .pipe(map((note: EnrolleeNote) => ({ note, ...response })))
          : of(response);
      }),
      exhaustMap((response: { note: EnrolleeNote, assigneeId: number }) => {
        const request$ = (response.note)
          ? this.adjudicationResource.createEnrolleeNotification(enrolleeId, response.note.id, response.assigneeId)
          : of(null);

        return request$.pipe(map((_) => response.assigneeId));
      }),
      exhaustMap((adjudicatorId: number) => this.adjudicationResource.setEnrolleeAdjudicator(enrolleeId, adjudicatorId))
    );

    this.busy = this.alterClaim(enrolleeId, data, assignPipe).subscribe();
  }

  public onReassign(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Reassign Enrolment',
      component: ManualFlagNoteComponent,
      data: {
        reassign: true,
        type: ClaimType.ENROLLEE
      }
    };
    const reassignPipe = pipe(
      exhaustMap((action: AssignAction) => {
        const response = { action };
        return (action.note)
          ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, action.note, false)
            .pipe(map((note: EnrolleeNote) => ({ note, ...response })))
          : of(response);
      }),
      exhaustMap((response: { note: EnrolleeNote, action: AssignAction }) => {
        const request$ = (response.note)
          ? this.adjudicationResource.createEnrolleeNotification(enrolleeId, response.note.id, response.action.adjudicatorId)
          : of(null);

        return request$.pipe(map((_) => response.action));
      }),
      exhaustMap((action: AssignAction) =>
        (action.action === AssignActionEnum.Disclaim)
          ? this.adjudicationResource.removeEnrolleeAdjudicator(enrolleeId)
          : this.adjudicationResource.setEnrolleeAdjudicator(enrolleeId, action.adjudicatorId)
      )
    );

    this.busy = this.alterClaim(enrolleeId, data, reassignPipe).subscribe();
  }

  public onApprove({ enrolleeId, agreementName }: { enrolleeId: number, agreementName: string }) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: `Are you sure you want to approve this enrolment with a ${agreementName} TOA agreement?`,
      actionText: 'Approve Enrolment',
      component: NoteComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, EnrolleeStatusAction.APPROVE)
      )
      .subscribe((approvedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(approvedEnrollee);
        this.action.emit();
      });
  }

  public onDecline(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Decline Enrollee',
      message: `When declined the enrollee will not have access to PRIME, and their Terms of Access will be terminated. Are you sure you want to decline this enrollment?`,
      actionType: 'warn',
      actionText: 'Decline Enrollee',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, EnrolleeStatusAction.DECLINE_PROFILE)
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
        this.adjudicationActionPipe(enrolleeId, EnrolleeStatusAction.LOCK_PROFILE)
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
        this.adjudicationActionPipe(enrolleeId, EnrolleeStatusAction.ENABLE_EDITING)
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
        this.adjudicationActionPipe(enrolleeId, EnrolleeStatusAction.ENABLE_EDITING)
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
        this.adjudicationActionPipe(enrolleeId, EnrolleeStatusAction.ENABLE_EDITING)
      )
      .subscribe((enableEnrollee: HttpEnrollee) => {
        this.updateEnrollee(enableEnrollee);
        this.action.emit();
      });
  }

  public onCancelToa(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Cancel TOA Assignment',
      message: 'Are you sure you want to cancel this TOA assignment and move the enrollee back into Under Review?',
      actionType: 'warn',
      actionText: 'Cancel TOA Assignment',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        this.adjudicationActionPipe(enrolleeId, EnrolleeStatusAction.CANCEL_TOA)
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
        this.adjudicationActionPipe(enrolleeId, EnrolleeStatusAction.RERUN_RULES)
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

    if (this.permissionService.hasRoles(Role.SUPER_ADMIN)) {
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

  public onToggleManualAdj({ enrolleeId, alwaysManual }: { enrolleeId: number, alwaysManual: boolean }) {
    const flagText = (alwaysManual) ? 'Flag' : 'Unflag';
    const data: DialogOptions = {
      title: `${flagText} Enrollee`,
      message: `Are you sure you want to ${flagText} this enrollee for manual adjudication?`,
      actionText: `${flagText} Enrollee`
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
          this.adjudicationResource.updateEnrolleeAlwaysManual(enrolleeId, alwaysManual)
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
      )
      .subscribe((flaggedEnrollee: HttpEnrollee) => {
        this.updateEnrollee(flaggedEnrollee);
        this.action.emit();
      });
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public onAssignToa({ enrolleeId, agreementType }: { enrolleeId: number, agreementType: AgreementType }) {
    this.busy = this.adjudicationResource.assignToaAgreementType(enrolleeId, agreementType)
      .subscribe((updatedEnrollee: HttpEnrollee) => this.updateEnrollee(updatedEnrollee));
  }

  public onSendBulkEmail() {
    const data: DialogOptions = {
      title: 'Send Email - Bulk Actions'
    };
    this.busy = this.dialog.open(SendBulkEmailComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((bulkEmailType: BulkEmailType) =>
          bulkEmailType
            ? this.adjudicationResource.getEnrolleeEmails(bulkEmailType)
            : EMPTY
        )
      )
      .subscribe((emails: string[]) => {
        emails.length
          ? this.utilsService.mailTo(emails.join(';'))
          : this.toastService.openErrorToast('No enrollees found for email type.');
      });
  }

  public onNavigateEnrollee(enrolleeId: number) {
    this.onRoute([enrolleeId, RouteUtils.currentRoutePath(this.router.url)]);
  }

  public ngOnInit() {
    // Use existing query params for initial search, and
    // update results on query param change
    this.route.queryParams
      .subscribe((queryParams: { [key: string]: any }) => this.getDataset(this.route.snapshot.params.id, queryParams));
    // url params could change due to jump action, subscribe to changes
    this.route.params
      .subscribe((params) => this.getDataset(params.id, {}));
  }

  protected getDataset(enrolleeId: number, queryParams: { search?: string, status?: number }) {
    if (enrolleeId) {
      this.getEnrolleeById(enrolleeId);
    }
    else {
      this.busy = this.getEnrollees(queryParams)
        .subscribe((enrollees: EnrolleeListViewModel[]) => this.enrollees = enrollees);
    }
  }

  private getEnrolleeById(enrolleeId: number): void {
    this.busy =
      forkJoin({
        enrollee: this.adjudicationResource.getEnrolleeById(enrolleeId)
          .pipe(
            map(this.toEnrolleeListViewModel),
            tap(() => this.showSearchFilter = false)
          ),
        enrolleeNavigation: this.adjudicationResource.getAdjacentEnrolleeId(enrolleeId)
      }).subscribe(({ enrollee, enrolleeNavigation }) => {
        this.enrollees = [enrollee];
        // Set enrolleeNavigation to null to disable navigation arrows for certain routes
        // TODO: add support for enrollee event page and notes page
        this.enrolleeNavigation =
          [AdjudicationRoutes.ENROLLEE_CURRENT_ENROLMENT,
          AdjudicationRoutes.ENROLLEE_ACCESS_TERM_ENROLMENT,
          AdjudicationRoutes.EVENT_LOG,
          AdjudicationRoutes.ADJUDICATOR_NOTES]
            .includes(RouteUtils.currentRoutePath(this.router.url)) ? null : enrolleeNavigation;
      });
  }

  private getEnrollees({ search, status }: { search?: string, status?: number }) {
    return this.adjudicationResource.getEnrollees(search, status)
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
  }

  private updateEnrollee(enrollee: HttpEnrollee) {
    const index = this.enrollees.findIndex(e => e.id === enrollee.id);
    this.enrollees.splice(index, 1, this.toEnrolleeListViewModel(enrollee));
    this.enrollees = [...this.enrollees];
  }

  private adjudicationActionPipe(enrolleeId: number, action: EnrolleeStatusAction) {
    return pipe(
      exhaustMap((result: { output: string }) => (result) ? of(result.output ?? null) : EMPTY),
      exhaustMap((note: string) => this.adjudicationResource.enrolleeStatusAction(enrolleeId, action).pipe(map(() => note))),
      exhaustMap((note: string) => this.adjudicationResource.createEnrolmentReference(enrolleeId).pipe(map(() => note))),
      exhaustMap((note: string) =>
        (note)
          ? this.adjudicationResource.createAdjudicatorNote(enrolleeId, note, true)
          : of(noop)
      ),
      exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
    );
  }

  /**
   * @description
   * Common claim logic with a custom pipe to manage
   * an assignment through a claim or disclaim.
   */
  private alterClaim(enrolleeId: number, data: DialogOptions, customPipe: OperatorFunction<AssignAction, string | void>): Observable<void> {
    return this.dialog.open(ClaimNoteComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: AssignAction }) => (result) ? of(result.output ?? null) : EMPTY),
        exhaustMap((action: AssignAction) =>
          this.adjudicationResource.deleteEnrolleeNotifications(enrolleeId)
            .pipe(map(() => action))
        ),
        customPipe,
        map((idir: string) => {
          const index = this.enrollees.findIndex(e => e.id === enrolleeId);
          const updatedEnrollee = this.enrollees[index];
          updatedEnrollee.adjudicatorIdir = idir ?? null;
          this.enrollees.splice(index, 1, updatedEnrollee);
        })
      );
  }

  protected toEnrolleeListViewModel(enrollee: HttpEnrollee): EnrolleeListViewModel {
    const {
      id,
      displayId,
      firstName,
      lastName,
      givenNames,
      appliedDate,
      approvedDate,
      expiryDate,
      currentStatus,
      previousStatus,
      currentTOAStatus,
      assignedTOAType,
      hasNewestAgreement,
      adjudicator,
      alwaysManual,
      enrolleeRemoteUsers,
      enrolleeCareSettings,
    } = enrollee;

    return {
      id,
      displayId,
      firstName,
      lastName,
      givenNames,
      appliedDate,
      approvedDate,
      expiryDate,
      currentStatusCode: currentStatus?.statusCode,
      previousStatus,
      currentTOAStatus,
      assignedTOAType,
      hasNewestAgreement,
      adjudicatorIdir: adjudicator?.idir,
      alwaysManual,
      remoteAccess: !!(enrolleeRemoteUsers?.length),
      careSettingCodes: enrolleeCareSettings.map(ecs => ecs.careSettingCode),
      hasNotification: false,
      requiresConfirmation: false,
      confirmed: false
    };
  }
}
