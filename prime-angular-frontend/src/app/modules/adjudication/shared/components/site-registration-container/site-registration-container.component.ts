import { Component, OnInit, Input, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable, EMPTY, of, noop, combineLatest } from 'rxjs';
import { exhaustMap, map, tap, take } from 'rxjs/operators';

import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { PaginatedList } from '@core/models/paginated-list.model';
import { NoContent } from '@core/resources/abstract-resource';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { MatTableDataSourceUtils } from '@lib/modules/ngx-material/mat-table-data-source-utils.class';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AbstractSiteAdminPage } from '@adjudication/shared/classes/abstract-site-admin-page.class';
import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import {
  SiteRegistrationListViewModel,
  SiteListViewModelPartial
} from '@registration/shared/models/site-registration.model';

@Component({
  selector: 'app-site-registration-container',
  templateUrl: './site-registration-container.component.html',
  styleUrls: ['./site-registration-container.component.scss']
})
export class SiteRegistrationContainerComponent extends AbstractSiteAdminPage implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public actions: TemplateRef<any>;
  @Input() public belowActions: TemplateRef<any>;
  @Input() public content: TemplateRef<any>;
  @Input() public refresh: Observable<boolean>;

  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
    protected healthAuthSiteResource: HealthAuthoritySiteResource,

    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
    protected organizationResource: OrganizationResource,
    private permissionService: PermissionService
  ) {
    super(route, router, dialog, siteResource, adjudicationResource, healthAuthSiteResource, organizationResource);

    this.hasActions = false;
    this.dataSource = new MatTableDataSource<SiteRegistrationListViewModel>([]);
  }

  public onSearch(textSearch: string | null): void {
    this.routeUtils.updateQueryParams({ textSearch });
  }

  public onFilter(status: any | null): void {
    this.routeUtils.updateQueryParams({ status });
  }

  public onDelete(record: { [key: string]: number }) {
    (record.organizationId)
      ? this.deleteOrganization(record.organizationId)
      : this.deleteSite(record.siteId);
  }

  public onArchiveOrganization(organizationId: number) {
    if (organizationId && this.permissionService.hasRoles(Role.SUPER_ADMIN)) {

      const data = {
        title: 'Archive Organization',
        message: 'Are you sure you want to archive this organization?Archiving an organization also archives all the organization\'s sites',
        actionType: 'warn',
        actionText: 'Archive Organization'
      };

      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? of(noop)
              : EMPTY
          ),
          exhaustMap(() => this.organizationResource.archiveOrganization(organizationId)),
          exhaustMap(() => {
            var org = this.dataSource.data.find((o) => o.organizationId === organizationId);
            org.isOrganizationArchived = true;
            return EMPTY;
          })
        ).subscribe();
    }
  }

  public onRestoreOrganization(organizationId: number) {
    this.organizationResource.restoreOrganization(organizationId)
      .subscribe(() => {
        var org = this.dataSource.data.find((o) => o.organizationId === organizationId)
        org.isOrganizationArchived = false;
      });
  }

  public ngOnInit(): void {
    // Use existing query params for initial search, and
    // update results on query param change
    this.route.queryParams
      .subscribe((queryParams: { [key: string]: any }) => this.getDataset(queryParams));

    this.refresh?.subscribe((shouldRefresh: boolean) => {
      if (shouldRefresh) {
        this.onRefresh();
      }
    });
  }

  protected getDataset(queryParams: { textSearch?: string }): void {
    const { oid, sid } = this.route.snapshot.params;
    // TODO: remove the joining of the org/site models to make the list view model.
    // This should be done in the backend through view model projection
    const request$ = (oid)
      ? combineLatest([
        this.getOrganizationById(oid),
        this.siteResource.getSiteById(sid)
      ])
        .pipe(
          take(1),
          map(this.toSiteRegistration())
        )
      : this.getPaginatedSites(queryParams)
        .pipe(
          map((paginatedList: PaginatedList<SiteRegistrationListViewModel>) => paginatedList.results)
        );

    this.busy = request$
      .subscribe((siteRegistrations: SiteRegistrationListViewModel[]) => this.dataSource.data = siteRegistrations);
  }

  protected updateSite(siteId: number, updatedSiteFields: {}): void {
    const siteRegistration = this.dataSource.data.find((siteReg: SiteRegistrationListViewModel) => siteReg.id === siteId);
    const updatedSiteRegistration = {
      ...siteRegistration,
      ...updatedSiteFields
    };
    this.dataSource.data = MatTableDataSourceUtils
      .update<SiteRegistrationListViewModel>(this.dataSource, 'siteId', updatedSiteRegistration);
  }

  private getPaginatedSites(queryParams: { textSearch?: string }): Observable<PaginatedList<SiteRegistrationListViewModel>> {
    return this.siteResource.getPaginatedSites(queryParams)
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
  }

  private getOrganizationById(organizationId: number): Observable<Organization> {
    return this.organizationResource.getOrganizationById(organizationId)
      .pipe(
        map((organization: Organization) => organization),
        tap(() => this.showSearchFilter = false)
      );
  }

  private deleteOrganization(organizationId: number) {
    if (organizationId) {
      const request$ = this.organizationResource.deleteOrganization(organizationId);
      const supplementaryMessage = 'Deleting an organization also deletes all the organization\'s sites, including remote users.';
      this.busy = this.deleteResource<Organization>(this.defaultOptions.delete('organization', supplementaryMessage), request$)
        .subscribe((organization: Organization) =>
          this.dataSource.data = MatTableDataSourceUtils
            .delete<SiteRegistrationListViewModel>(this.dataSource, 'organizationId', organizationId)
        );
    }
  }

  private deleteSite(siteId: number) {
    if (siteId) {
      const request$ = this.siteResource.deleteSite(siteId);
      this.busy = this.deleteResource<Site>(this.defaultOptions.delete('site'), request$)
        .subscribe((site: Site) => {
          this.dataSource.data = MatTableDataSourceUtils
            .delete<SiteRegistrationListViewModel>(this.dataSource, 'siteId', siteId);
        });
    }
  }

  private deleteResource<T>(dialogOptions: DialogOptions, deleteRequest$: NoContent): Observable<T> {
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
          exhaustMap(() => {
            this.routeUtils.routeTo([AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.SITE_REGISTRATIONS]);
            return EMPTY;
          })
        );
    }
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
        hasClaim,
        pendingTransfer,
        hasSubmittedSite,
        isArchived
      } = organization;

      return [{
        organizationId,
        displayId,
        signingAuthorityId,
        signingAuthority,
        signingAuthorityName: `${signingAuthority.firstName ? signingAuthority.firstName : ''} ${signingAuthority.lastName}`,
        organizationName: name,
        duplicatePecSiteCount: 0,
        predecessorSite: null,
        successorSite: null,
        name,
        organizationDoingBusinessAs: doingBusinessAs,
        hasClaim,
        hasSubmittedSite,
        isOrganizationArchived: isArchived,
        ...this.toSiteViewModelPartial(site)
      }];
    };
  }

  private toSiteViewModelPartial(site: Site): SiteListViewModelPartial {
    const {
      id,
      physicalAddress,
      doingBusinessAs,
      submittedDate,
      approvedDate,
      careSettingCode,
      siteVendors,
      remoteUsers,
      adjudicator,
      pec,
      mnemonic,
      status,
      businessLicence,
      flagged,
      isNew,
      missingBusinessLicence
    } = site;

    return {
      id,
      physicalAddress,
      doingBusinessAs,
      submittedDate,
      approvedDate,
      careSettingCode,
      siteVendors,
      remoteUserCount: remoteUsers?.length,
      adjudicatorIdir: adjudicator?.idir,
      pec,
      mnemonic,
      status,
      businessLicence,
      flagged,
      isNew,
      missingBusinessLicence
    };
  }
}
