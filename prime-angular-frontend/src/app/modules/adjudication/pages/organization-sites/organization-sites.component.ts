import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginatedList } from '@core/models/paginated-list.model';
import { SiteResource } from '@core/resources/site-resource.service';
import { EmailUtils } from '@lib/utils/email-utils.class';
import { RoutePath, RouteUtils } from '@lib/utils/route-utils.class';
import { OrganizationAdminView } from '@registration/shared/models/organization.model';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { AssignAction, AssignActionEnum, ClaimNoteComponent, ClaimType } from '@shared/components/dialogs/content/claim-note/claim-note.component';
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { SendEmailComponent } from '@shared/components/dialogs/content/send-email/send-email.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';
import { concat, EMPTY, exhaustMap, forkJoin, map, noop, of, Subscription } from 'rxjs';

@Component({
  selector: 'app-organization-sites',
  templateUrl: './organization-sites.component.html',
  styleUrls: ['./organization-sites.component.scss']
})
export class OrganizationSitesComponent implements OnInit {

  protected routeUtils: RouteUtils;
  protected busy: Subscription;

  public organization: OrganizationAdminView;
  public communityPharmacies: MatTableDataSource<SiteRegistrationListViewModel>;
  public privateCommunityHealthPractices: MatTableDataSource<SiteRegistrationListViewModel>;
  public deviceProviders: MatTableDataSource<SiteRegistrationListViewModel>;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));

    this.communityPharmacies = new MatTableDataSource<SiteRegistrationListViewModel>([]);
    this.privateCommunityHealthPractices = new MatTableDataSource<SiteRegistrationListViewModel>([]);
    this.deviceProviders = new MatTableDataSource<SiteRegistrationListViewModel>([]);
  }

  public ngOnInit(): void {
    this.route.params
      .subscribe((params) => {
        if (params.id) {
          this.getDataset(params.id);
        }
      });
  }

  protected getDataset(organizationId: number) {
    this.busy = forkJoin([
      this.adjudicationResource.getOrganizationById(organizationId),
      this.siteResource.getPaginatedSites({ organizationId: organizationId })
    ]).subscribe(([organization, sites]: [OrganizationAdminView, PaginatedList<SiteRegistrationListViewModel>]) => {
      this.organization = organization;

      this.communityPharmacies.data = sites.results.filter((s) => s.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACY);
      this.privateCommunityHealthPractices.data = sites.results.filter((s) => s.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE);
      this.deviceProviders.data = sites.results.filter((s) => s.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);

      this.initializeFirstSite();
    });
  }
  public onRoute(routePath: RoutePath): void {
    this.routeUtils.routeWithin(routePath);
  }

  public onReload() {
    this.route.params
      .subscribe((params) => {
        if (params.id) {
          this.getDataset(params.id);
        }
      });
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
      .subscribe(() => this.onReload());
  }

  public onNotify({ siteId }: { siteId: number }): void {
    const request$ = this.siteResource.getSiteContacts(siteId);

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
      .subscribe(() => this.onReload());
  }

  private initializeFirstSite() {
    if (this.communityPharmacies && this.communityPharmacies.data.length > 0) {
      this.setOrganizationInfo(this.communityPharmacies.data[0]);
    }
    if (this.privateCommunityHealthPractices && this.privateCommunityHealthPractices.data.length > 0) {
      this.setOrganizationInfo(this.privateCommunityHealthPractices.data[0]);
    }
    if (this.deviceProviders && this.deviceProviders.data.length > 0) {
      this.setOrganizationInfo(this.deviceProviders.data[0]);
    }
  }

  private setOrganizationInfo(site: SiteRegistrationListViewModel) {
    site.organizationName = this.organization.name;
    site.signingAuthorityName = this.organization.signingAuthorityName;
  }
}
