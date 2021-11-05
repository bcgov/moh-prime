import { Component, OnInit, Input, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatTabChangeEvent } from '@angular/material/tabs';

import { Subscription, Observable, EMPTY, of, noop, concat } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { EmailUtils } from '@lib/utils/email-utils.class';
import { RoutePath, RouteUtils } from '@lib/utils/route-utils.class';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { MatTableDataSourceUtils } from '@lib/modules/ngx-material/mat-table-data-source-utils.class';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import {
  AssignAction,
  AssignActionEnum,
  ClaimNoteComponent,
  ClaimType
} from '@shared/components/dialogs/content/claim-note/claim-note.component';
import { SendEmailComponent } from '@shared/components/dialogs/content/send-email/send-email.component';
import { PermissionService } from '@auth/shared/services/permission.service';
import { Role } from '@auth/shared/enum/role.enum';
import { Organization } from '@registration/shared/models/organization.model';
import { Site } from '@registration/shared/models/site.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import {
  SiteRegistrationListViewModel,
  SiteListViewModelPartial,
  OrganizationSearchListViewModel
} from '@registration/shared/models/site-registration.model';
import { EscalationNoteComponent, EscalationType } from '@shared/components/dialogs/content/escalation-note/escalation-note.component';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

@Component({
  selector: 'app-site-registration-tabs',
  templateUrl: './site-registration-tabs.component.html',
  styleUrls: ['./site-registration-tabs.component.scss']
})
export class SiteRegistrationTabsComponent implements OnInit {
  public busy: Subscription;
  @Input() public refresh: Observable<boolean>;

  public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;
  public CareSettingEnum = CareSettingEnum;

  public communityPracticeColumns: string[];
  public communityPharmacyColumns: string[];
  public siteTabIndex: number;

  private routeUtils: RouteUtils;
  private tabIndexToCareSettingMap: Record<number, CareSettingEnum>;
  private careSettingToTabIndexMap: { [key in CareSettingEnum]?: number };

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
    private route: ActivatedRoute,
    private router: Router,
    private organizationResource: OrganizationResource,
    private siteResource: SiteResource,
    private healthAuthResource: HealthAuthorityResource,
    private adjudicationResource: AdjudicationResource,
    private permissionService: PermissionService,
    private utilResource: UtilsService,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
    this.dataSource = new MatTableDataSource<SiteRegistrationListViewModel>([]);

    const commonColumns = [
      'prefixes',
      'displayId',
      'organizationName',
      'signingAuthority',
      'siteDoingBusinessAs',
      'submissionDate',
      'assignedTo',
      'state',
      'siteId'
    ];

    this.communityPracticeColumns = [
      ...commonColumns,
      'remoteUsers',
      'actions'
    ];
    this.communityPharmacyColumns = [
      ...commonColumns,
      'missingBusinessLicence',
      'actions'
    ];
    this.tabIndexToCareSettingMap = {
      0: null, // map to null to remove queryString
      1: CareSettingEnum.COMMUNITY_PHARMACIST,
      2: CareSettingEnum.HEALTH_AUTHORITY
    };
    this.careSettingToTabIndexMap = {
      [CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE]: 0,
      [CareSettingEnum.COMMUNITY_PHARMACIST]: 1,
      [CareSettingEnum.HEALTH_AUTHORITY]: 2
    };
  }

  public onSearch(textSearch: string | null): void {
    this.routeUtils.updateQueryParams({ textSearch });
  }

  public onFilter(status: any | null): void {
    this.routeUtils.updateQueryParams({ status });
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

  public onRoute(routePath: RoutePath) {
    this.routeUtils.routeWithin(routePath);
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

  public onDelete(record: { [key: string]: number }) {
    (record.organizationId)
      ? this.deleteOrganization(record.organizationId)
      : this.deleteSite(record.siteId);
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

  public onTabChange(tabChangeEvent: MatTabChangeEvent): void {
    this.routeUtils.updateQueryParams({ careSetting: this.tabIndexToCareSettingMap[tabChangeEvent.index] });
  }

  public ngOnInit(): void {
    // Use existing query params for initial search, and
    // update results on query param change
    this.route.queryParams
      .subscribe((queryParams: { [key: string]: any }) => {
        this.siteTabIndex = this.careSettingToTabIndexMap[+queryParams.careSetting];
        this.getDataset(queryParams);
      });

    // Listen for requests to refresh the data layer
    if (this.refresh instanceof Observable) {
      this.refresh.subscribe((shouldRefresh: boolean) => {
        if (shouldRefresh) {
          this.onRefresh();
        }
      });
    }
  }

  private getDataset(queryParams: { careSetting?: CareSettingEnum, textSearch?: string }): void {
    let careSettingCode = +queryParams?.careSetting ?? CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;
    if (!(careSettingCode in this.careSettingToTabIndexMap)) {
      careSettingCode = CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;
    }
    this.busy = this.getOrganizations({ careSettingCode, ...queryParams })
      .pipe(
        map(this.toSiteRegistrations)
      )
      .subscribe((siteRegistrations: SiteRegistrationListViewModel[]) => this.dataSource.data = siteRegistrations);
  }

  private getOrganizations(
    queryParam: { textSearch?: string, careSettingCode?: CareSettingEnum }
  ): Observable<OrganizationSearchListViewModel[]> {
    return this.organizationResource.getOrganizations(queryParam)
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
  }

  private updateSite(updatedSite: Site) {
    const siteRegistration = this.dataSource.data.find((siteReg: SiteRegistrationListViewModel) => siteReg.siteId === updatedSite.id);
    const updatedSiteRegistration = {
      ...siteRegistration,
      ...this.toSiteViewModelPartial(updatedSite)
    };
    this.dataSource.data = MatTableDataSourceUtils
      .update<SiteRegistrationListViewModel>(this.dataSource, 'siteId', updatedSiteRegistration);
  }

  private deleteOrganization(organizationId: number) {
    if (organizationId) {
      const request$ = this.organizationResource.deleteOrganization(organizationId);
      const supplementaryMessage = 'Deleting an organization also deletes all the organization\'s sites, including remote users.';
      this.busy = this.deleteResource<Organization>(this.defaultOptions.delete('organization', supplementaryMessage), request$)
        .subscribe((organization: Organization) =>
          this.dataSource.data = MatTableDataSourceUtils
            .delete<SiteRegistrationListViewModel>(this.dataSource, 'organizationId', organization.id)
        );
    }
  }

  private deleteSite(siteId: number) {
    if (siteId) {
      const request$ = this.siteResource.deleteSite(siteId);
      this.busy = this.deleteResource<Site>(this.defaultOptions.delete('site'), request$)
        .subscribe((site: Site) => {
          this.dataSource.data = MatTableDataSourceUtils
            .delete<SiteRegistrationListViewModel>(this.dataSource, 'siteId', site.id);
        });
    }
  }

  private deleteResource<T>(dialogOptions: DialogOptions, deleteRequest$: Observable<T>): Observable<T> {
    if (this.permissionService.hasRoles(Role.SUPER_ADMIN)) {
      return this.dialog.open(ConfirmDialogComponent, { data: dialogOptions })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? of(noop)
              : EMPTY
          ),
          exhaustMap(() => deleteRequest$),
          exhaustMap((resource: T) => {
            // Route on singular resource views after deletion to refresh results
            if (this.route.snapshot.data.oid) {
              this.routeUtils.routeTo(AdjudicationRoutes.SITE_REGISTRATIONS);
              return EMPTY;
            }
            // Otherwise, stay on the list resource view and remove locally
            return of(resource);
          })
        );
    }
  }

  private toSiteRegistrations(results: OrganizationSearchListViewModel[]): SiteRegistrationListViewModel[] {
    const siteRegistrations = results.reduce((registrations, result) => {
      const { matchOn, organization: ovm } = result;
      const { id: organizationId, sites, ...organization } = ovm;
      const registration = sites.map((svm: Site, index: number) => {
        const { id: siteId, doingBusinessAs, ...site } = svm;
        return (!index)
          ? { organizationId, ...organization, siteId, siteDoingBusinessAs: doingBusinessAs, ...site, matchOn }
          : { organizationId, siteId, siteDoingBusinessAs: doingBusinessAs, ...site, matchOn };
      });
      registrations.push(registration);
      return registrations;
    }, []);

    return [].concat(...siteRegistrations);
  }

  private toSiteRegistration(): ([organization, site]: [Organization, Site]) => SiteRegistrationListViewModel[] {
    return ([organization, site]: [Organization, Site]) => {
      const {
        id: organizationId,
        displayId,
        signingAuthorityId,
        signingAuthority,
        name,
        doingBusinessAs,
        hasClaim
      } = organization;

      return [{
        organizationId,
        displayId,
        signingAuthorityId,
        signingAuthority,
        name,
        organizationDoingBusinessAs: doingBusinessAs,
        hasClaim,
        ...this.toSiteViewModelPartial(site)
      }];
    };
  }

  private toSiteViewModelPartial(site: Site): SiteListViewModelPartial {
    const {
      id: siteId,
      physicalAddress,
      doingBusinessAs,
      submittedDate,
      approvedDate,
      careSettingCode,
      siteVendors,
      remoteUsers,
      adjudicator,
      pec,
      status,
      businessLicence,
      flagged
    } = site;

    return {
      siteId,
      physicalAddress,
      siteDoingBusinessAs: doingBusinessAs,
      submittedDate,
      approvedDate,
      careSettingCode,
      siteVendors,
      remoteUserCount: remoteUsers.length,
      adjudicatorIdir: adjudicator?.idir,
      pec,
      status,
      businessLicence,
      flagged
    };
  }

}
