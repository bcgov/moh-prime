import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { concat, EMPTY, noop, of, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { EmailUtils } from '@lib/utils/email-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { RoutePath, RouteUtils } from '@lib/utils/route-utils.class';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { AssignAction, AssignActionEnum, ClaimNoteComponent, ClaimType } from '@shared/components/dialogs/content/claim-note/claim-note.component';
import { EscalationNoteComponent, EscalationType } from '@shared/components/dialogs/content/escalation-note/escalation-note.component';
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { SendEmailComponent } from '@shared/components/dialogs/content/send-email/send-email.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '../services/adjudication-resource.service';
import { SiteActionEnum, SiteArchiveRestoreComponent } from '@shared/components/dialogs/content/site-archive-restore/site-archive-restore.component';

export abstract class AbstractSiteAdminPage {
  public abstract busy: Subscription;

  protected routeUtils: RouteUtils;

  protected constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
    protected healthAuthSiteResource: HealthAuthoritySiteResource,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: RoutePath): void {
    this.routeUtils.routeWithin(routePath);
  }

  public onRefresh(): void {
    this.getDataset(this.route.snapshot.queryParams);
  }

  public onToggleFlagSite({ siteId, flagged }: { siteId: number, flagged: boolean }): void {
    this.busy = this.siteResource.flagSite(siteId, flagged)
      .subscribe(() => this.updateSite(siteId, { flagged }));
  }

  public onToggleIsNewSite({ siteId, isNew }: { siteId: number, isNew: boolean }): void {
    this.busy = this.siteResource.flagIsNewSite(siteId, isNew)
      .subscribe(() => this.updateSite(siteId, { isNew }));
  }

  public onAssign(siteId: number): void {
    const data: DialogOptions = {
      title: 'Assign Site',
      component: ManualFlagNoteComponent,
      data: {
        reassign: false,
        type: ClaimType.SITE
      }
    };

    // TODO refactor this so the types align properly
    this.busy = this.dialog.open(ClaimNoteComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: AssignAction }) => (result) ? of(result.output ?? null) : EMPTY),
        exhaustMap((action: AssignAction) => this.adjudicationResource.deleteSiteNotifications(siteId).pipe(map(() => action))),
        exhaustMap((action: AssignAction) =>
          (action.note)
            ? this.siteResource.createSiteRegistrationNote(siteId, action.note)
              .pipe(map((note: SiteRegistrationNote) => ({ note, assigneeId: action.adjudicatorId })))
            : of({ assigneeId: action.adjudicatorId })
        ),
        exhaustMap((result: { note: SiteRegistrationNote, assigneeId: number }) =>
          (result.note)
            ? this.adjudicationResource.createSiteNotification(siteId, result.note.id, result.assigneeId).pipe(map(() => result.assigneeId))
            : of(noop).pipe(map(() => result.assigneeId))
        ),
        exhaustMap((adjudicatorId: number) => this.siteResource.setSiteAdjudicator(siteId, adjudicatorId)),
      )
      .subscribe(() => this.onRefresh());
  }

  public onReassign(siteId: number): void {
    const data: DialogOptions = {
      title: 'Reassign Site',
      component: ManualFlagNoteComponent,
      data: {
        reassign: true,
        type: ClaimType.SITE
      }
    };

    this.busy = this.dialog.open(ClaimNoteComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: AssignAction }) => (result) ? of(result.output ?? null) : EMPTY),
        exhaustMap((action: AssignAction) => this.adjudicationResource.deleteSiteNotifications(siteId).pipe(map(() => action))),
        exhaustMap((action: AssignAction) =>
          (action.note)
            ? this.siteResource.createSiteRegistrationNote(siteId, action.note)
              .pipe(map((note: SiteRegistrationNote) => ({ note, action })))
            : of(null).pipe(map(() => ({ action })))
        ),
        exhaustMap((result: { note: SiteRegistrationNote, action: AssignAction }) =>
          (result.note)
            ? this.adjudicationResource.createSiteNotification(siteId, result.note.id, result.action.adjudicatorId)
              .pipe(map(() => result.action))
            : of(noop).pipe(map(() => result.action))
        ),
        exhaustMap((action: AssignAction) =>
          (action.action === AssignActionEnum.Disclaim)
            ? this.siteResource.removeSiteAdjudicator(siteId)
            : concat(
              this.siteResource.removeSiteAdjudicator(siteId),
              this.siteResource.setSiteAdjudicator(siteId, action.adjudicatorId)
            )
        )
      )
      .subscribe(() => this.onRefresh());
  }

  public onNotify({ siteId, healthAuthorityOrganizationId }: { siteId: number, healthAuthorityOrganizationId?: HealthAuthorityEnum }): void {
    const request$ = (healthAuthorityOrganizationId)
      ? this.healthAuthSiteResource.getHealthAuthoritySiteContacts(healthAuthorityOrganizationId, siteId)
      : this.siteResource.getSiteContacts(siteId);

    request$
      .pipe(
        map((contacts: { label: string, email: string }[]) => {
          return {
            title: 'Send Email',
            data: { contacts }
          };
        }),
        exhaustMap((data: DialogOptions) =>
          this.dialog.open(SendEmailComponent, { data }).afterClosed()
        ),
        exhaustMap((result: string) => (result) ? of(result) : EMPTY)
      )
      .subscribe((email: string) => EmailUtils.openEmailClient(email));
  }

  public onEscalate(siteId: number): void {
    const data: DialogOptions = {
      data: {
        id: siteId,
        escalationType: EscalationType.SITE_REGISTRATION
      }
    };

    this.dialog.open(EscalationNoteComponent, { data }).afterClosed()
      .subscribe((result: { reload: boolean }) => (result?.reload) ? this.getDataset(this.route.snapshot.queryParams) : noop);
  }

  public onApprove(siteId: number): void {
    const data: DialogOptions = {
      title: 'Approve Site Registration',
      message: 'Are you sure you want to approve this Registration?',
      actionText: 'Approve Site Registration',
      component: NoteComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: string }) =>
          (result)
            ? of(result.output ?? null)
            : EMPTY
        ),
        exhaustMap((note: string) =>
          this.siteResource.approveSite(siteId)
            .pipe(
              map(() => this.updateSite(siteId, { status: SiteStatusType.EDITABLE })),
              map(() => note)
            )
        ),
        exhaustMap((note: string) =>
          (note)
            ? this.siteResource.createSiteRegistrationNote(siteId, note)
            : of(noop)
        )
      )
      .subscribe(() => this.onRefresh());
  }

  public onReject(siteId: number): void {
    const data: DialogOptions = {
      title: 'Reject Site Registration',
      message: 'Are you sure you want to reject this Site Registration?',
      actionText: 'Reject Site Registration',
      actionType: 'warn',
      component: NoteComponent,
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: string }) =>
          (result)
            ? of(result.output ?? null)
            : EMPTY
        ),
        exhaustMap((note: string) =>
          this.siteResource.rejectSite(siteId)
            .pipe(
              map(() => this.updateSite(siteId, { status: SiteStatusType.LOCKED })),
              map(() => note)
            )
        ),
        exhaustMap((note: string) =>
          (note)
            ? this.siteResource.createSiteRegistrationNote(siteId, note)
            : of(noop)
        )
      )
      .subscribe(() => this.onRefresh());
  }

  public onArchive(siteId: number): void {
    const data: DialogOptions = {
      data: {
        siteId: siteId,
        action: SiteActionEnum.Archive,
      }
    };

    this.busy = this.dialog.open(SiteArchiveRestoreComponent, { data })
      .afterClosed()
      .subscribe((result: { reload: boolean }) => (result?.reload) ?
        this.getDataset(this.route.snapshot.queryParams) : noop);
  }

  public onRestore(siteId: number): void {

    this.siteResource.canRestoreSite(siteId)
      .subscribe((value: boolean) => {

        if (value) {
          const data: DialogOptions = {
            data: {
              siteId: siteId,
              action: SiteActionEnum.Restore,
            }
          };

          this.busy = this.dialog.open(SiteArchiveRestoreComponent, { data })
            .afterClosed()
            .subscribe((result: { reload: boolean }) => (result?.reload) ?
              this.getDataset(this.route.snapshot.queryParams) : noop);

        } else {
          const data: DialogOptions = {
            title: 'Restore Archived Site',
            message: 'Site ID has been used in a different site. This Site can\'t be restored.',
            cancelText: "Close",
            actionType: 'warn',
            actionHide: true
          };

          this.dialog.open(ConfirmDialogComponent, { data })
            .afterClosed()
            .subscribe();
        }
      });
  }

  public onEnableEditing(siteId: number): void {
    this.busy = this.siteResource.enableEditingSite(siteId)
      .subscribe(() => this.onRefresh());
  }

  public onUnreject(siteId: number): void {
    this.busy = this.siteResource.unrejectSite(siteId)
      .subscribe(() => this.onRefresh());
  }

  /**
   * @description
   * Get the current data set
   */
  protected abstract getDataset(queryParams?: unknown): void;

  /**
   * @description
   * Update a site in the current data set
   */
  protected abstract updateSite(siteId: number, updatedSiteFields: {}): void;
}
