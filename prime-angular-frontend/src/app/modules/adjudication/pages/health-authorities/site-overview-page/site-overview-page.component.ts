import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { EMPTY, forkJoin, noop, of, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteAdjudicationAction } from '@registration/shared/enum/site-adjudication-action.enum';
import { SiteActionViewModel } from '@registration/shared/models/site-registration.model';
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { ClaimType, ClaimNoteComponent } from '@shared/components/dialogs/content/claim-note/claim-note.component';
import { EscalationNoteComponent, EscalationType } from '@shared/components/dialogs/content/escalation-note/escalation-note.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EmailUtils } from '@lib/utils/email-utils.class';
import { SendEmailComponent } from '@shared/components/dialogs/content/send-email/send-email.component';

@Component({
  selector: 'app-site-overview-page',
  templateUrl: './site-overview-page.component.html',
  styleUrls: ['./site-overview-page.component.scss']
})
export class SiteOverviewPageComponent implements OnInit {

  public busy: Subscription;
  public form: FormGroup;
  public site: HealthAuthoritySite;
  public siteActinoViewModel: SiteActionViewModel;
  public healthAuthority: HealthAuthority;

  public SiteAdjudicationAction = SiteAdjudicationAction;
  public SiteStatusType = SiteStatusType;
  public AdjudicationRoutes = AdjudicationRoutes;
  public Role = Role;
  public HealthAuthorityEnum = HealthAuthorityEnum;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private healthAuthorityResource: HealthAuthorityResource,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const params = this.route.snapshot.params;
      this.busy = this.healthAuthorityResource.updateHealthAuthoritySitePec(+params.haid, +params.sid, this.form.value)
        .subscribe(() => this.getData());
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
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

    this.busy = this.dialog.open(ClaimNoteComponent, { data })
      .afterClosed()
      .pipe(
        // TODO: implement assign
        // exhaustMap((result: { output: AssignAction }) => (result) ? of(result.output ?? null) : EMPTY),
        // exhaustMap((action: AssignAction) => this.adjudicationResource.deleteSiteNotifications(siteId).pipe(map(() => action))),
        // exhaustMap((action: AssignAction) =>
        //   (action.note)
        //     ? this.siteResource.createSiteRegistrationNote(siteId, action.note)
        //       .pipe(map((note: SiteRegistrationNote) => ({ note, assigneeId: action.adjudicatorId })))
        //     : of({ assigneeId: action.adjudicatorId })
        // ),
        // exhaustMap((result: { note: SiteRegistrationNote, assigneeId: number }) =>
        //   (result.note)
        //     ? this.adjudicationResource.createSiteNotification(siteId, result.note.id, result.assigneeId).pipe(map(() => result.assigneeId))
        //     : of(noop).pipe(map(() => result.assigneeId))
        // ),
        // exhaustMap((adjudicatorId: number) => this.siteResource.setSiteAdjudicator(siteId, adjudicatorId)),
      )
      .subscribe();
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
        // TODO: implement reassign
        // exhaustMap((result: { output: AssignAction }) => (result) ? of(result.output ?? null) : EMPTY),
        // exhaustMap((action: AssignAction) => this.adjudicationResource.deleteSiteNotifications(siteId).pipe(map(() => action))),
        // exhaustMap((action: AssignAction) =>
        //   (action.note)
        //     ? this.siteResource.createSiteRegistrationNote(siteId, action.note)
        //       .pipe(map((note: SiteRegistrationNote) => ({ note, action })))
        //     : of(null).pipe(map(() => ({ action })))
        // ),
        // exhaustMap((result: { note: SiteRegistrationNote, action: AssignAction }) =>
        //   (result.note)
        //     ? this.adjudicationResource.createSiteNotification(siteId, result.note.id, result.action.adjudicatorId)
        //       .pipe(map(() => result.action))
        //     : of(noop).pipe(map(() => result.action))
        // ),
        // exhaustMap((action: AssignAction) =>
        //   (action.action === AssignActionEnum.Disclaim)
        //     ? this.siteResource.removeSiteAdjudicator(siteId)
        //     : concat(
        //       this.siteResource.removeSiteAdjudicator(siteId),
        //       this.siteResource.setSiteAdjudicator(siteId, action.adjudicatorId)
        //     )
        // )
      )
      .subscribe();
  }

  public onNotify(record: { siteId: number, healthAuthorityOrganizationId: number }): void {
    this.healthAuthorityResource.getHealthAuthoritySiteContacts(record.healthAuthorityOrganizationId, record.siteId)
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

  public onEscalate(record: { siteId: number, organizationId: number }): void {
    const data: DialogOptions = {
      data: {
        id: record.siteId,
        escalationType: EscalationType.SITE_REGISTRATION
      }
    };

    this.dialog.open(EscalationNoteComponent, { data }).afterClosed()
      .subscribe((result: { reload: boolean }) => (result?.reload) ? this.getData() : noop);
  }

  public onToggleFlagSite({ siteId, flagged }: { siteId: number, flagged: boolean }) {}

  public onDelete(record: { [key: string]: number }) {
    (record.organizationId)
      ? this.deleteOrganization(record.organizationId)
      : this.deleteSite(record.siteId);
  }

  public onDecline(record: { siteId: number, organizationId: number }): void {
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
          this.healthAuthorityResource.declineHealthAuthoritySite(record.organizationId, record.siteId)
            .pipe(
              map((updatedSite: HealthAuthoritySite) => this.site = updatedSite),
              map(() => note)
            )
        ),
        exhaustMap((note: string) =>
          (note)
            ? this.healthAuthorityResource.createHealthAuthoriitySiteNote(record.organizationId, record.siteId, note)
            : of(noop)
        )
      )
      .subscribe();
  }

  public onApprove(record: { siteId: number, organizationId: number }): void {

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
          this.healthAuthorityResource.approveHealthAuthoritySite(record.organizationId, record.siteId)
            .pipe(
              map((updatedSite: HealthAuthoritySite) => this.site = updatedSite),
              map(() => note)
            )
        ),
        exhaustMap((note: string) =>
          (note)
            ? this.healthAuthorityResource.createHealthAuthoriitySiteNote(record.organizationId, record.siteId, note)
            : of(noop)
        )
      )
      .subscribe();
  }

  public onEnableEditing(record: { siteId: number, organizationId: number }) {
    this.busy = this.healthAuthorityResource.enableEditingHealthAuthoritySite(record.organizationId, record.siteId)
      .subscribe((updatedSite: HealthAuthoritySite) => this.site = updatedSite);
  }

  public onUnreject(record: { siteId: number, organizationId: number }) {
    this.busy = this.healthAuthorityResource.unrejectHealthAuthoritySite(record.organizationId, record.siteId)
      .subscribe((updatedSite: HealthAuthoritySite) => this.site = updatedSite);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getData();
  }

  private getData(): void {
    const params = this.route.snapshot.params;
    this.busy = forkJoin({
      site: this.healthAuthorityResource.getHealthAuthoritySiteById(+params.haid, +params.sid),
      healthAuthority: this.healthAuthorityResource.getHealthAuthorityById(+params.haid)
    })
      .subscribe(({ site, healthAuthority }) => {
        this.site = site;
        this.healthAuthority = healthAuthority;
        this.siteActinoViewModel = {
          siteId: site.id,
          status: site.status,
          name: healthAuthority.name,
          organizationId: healthAuthority.id,
          adjudicatorIdir: null, // TODO: to populate from HA site
          flagged: null,         // N/A
          signingAuthority: null // N/A
        };
        this.form.get('pec').setValue(site.siteId);
      });
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required],
      ]
    });
  }

  private deleteOrganization(organizationId: number): void {
    if (organizationId) {
      // TODO: delete HA org
      // const request$ = this.healthAuthorityResource.deleteOrganization(organizationId);
      // const supplementaryMessage = 'Deleting an organization also deletes all the organization\'s sites, including remote users.';
      // this.busy = this.deleteResource<Organization>(this.defaultOptions.delete('organization', supplementaryMessage), request$)
      //   .subscribe((organization: Organization) =>
      //     this.dataSource.data = MatTableDataSourceUtils
      //       .delete<SiteRegistrationListViewModel>(this.dataSource, 'organizationId', organization.id)
      //   );
    }
  }

  private deleteSite(siteId: number): void {
    if (siteId) {
      // TODO: delete HA site
      // const request$ = this.healthAuthorityResource.deleteSite(siteId);
      // this.busy = this.deleteResource<Site>(this.defaultOptions.delete('site'), request$)
      //   .subscribe((site: Site) => {
      //     this.dataSource.data = MatTableDataSourceUtils
      //       .delete<SiteRegistrationListViewModel>(this.dataSource, 'siteId', site.id);
      //   });
    }
  }
}
