import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { EmailUtils } from '@lib/utils/email-utils.class';
import { RoutePath, RouteUtils } from '@lib/utils/route-utils.class';
import { Site } from '@registration/shared/models/site.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { AssignAction, AssignActionEnum, ClaimNoteComponent, ClaimType } from '@shared/components/dialogs/content/claim-note/claim-note.component';
import { EscalationNoteComponent, EscalationType } from '@shared/components/dialogs/content/escalation-note/escalation-note.component';
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { SendEmailComponent } from '@shared/components/dialogs/content/send-email/send-email.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';
import { concat, EMPTY, noop, of, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';
import { AdjudicationResource } from '../services/adjudication-resource.service';

export abstract class AbstractSiteAdminPage {
  public busy: Subscription;

  protected routeUtils: RouteUtils;

  protected constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
    protected healthAuthResource: HealthAuthorityResource
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: RoutePath) {
    this.routeUtils.routeWithin(routePath);
  }

  public onRefresh(): void {
    this.getDataset(this.route.snapshot.queryParams);
  }

  public onAssign(siteId: number) {
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
      .subscribe(() => this.getDataset(this.route.snapshot.queryParams));
  }

  public onReassign(siteId: number) {
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
      .subscribe(() => this.getDataset(this.route.snapshot.queryParams));
  }

  public onNotify({ siteId, healthAuthorityOrganizationId }: { siteId: number, healthAuthorityOrganizationId?: HealthAuthorityEnum }) {
    const request$ = (healthAuthorityOrganizationId)
      ? this.healthAuthResource.getHealthAuthoritySiteContacts(healthAuthorityOrganizationId, siteId)
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

  public onEscalate(siteId: number) {
    const data: DialogOptions = {
      data: {
        id: siteId,
        escalationType: EscalationType.SITE_REGISTRATION
      }
    };

    this.dialog.open(EscalationNoteComponent, { data }).afterClosed()
      .subscribe((result: { reload: boolean }) => (result?.reload) ? this.getDataset(this.route.snapshot.queryParams) : noop);
  }

  public onApprove(siteId: number) {
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
              map((updatedSite: Site) => this.updateSite(updatedSite)),
              map(() => note)
            )
        ),
        exhaustMap((note: string) =>
          (note)
            ? this.siteResource.createSiteRegistrationNote(siteId, note)
            : of(noop)
        )
      )
      .subscribe();
  }

  public onDecline(siteId: number) {
    const data: DialogOptions = {
      title: 'Decline Site Registration',
      message: 'Are you sure you want to Decline this Site Registration?',
      actionText: 'Decline Site Registration',
      actionType: 'warn',
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
          this.siteResource.declineSite(siteId)
            .pipe(
              map((updatedSite: Site) => this.updateSite(updatedSite)),
              map(() => note)
            )
        ),
        exhaustMap((note: string) =>
          (note)
            ? this.siteResource.createSiteRegistrationNote(siteId, note)
            : of(noop)
        )
      )
      .subscribe();
  }

  protected abstract getDataset(queryParams?: { careSetting?: CareSettingEnum, textSearch?: string }): void;

  protected abstract updateSite(updatedSite: Site): void;
}
