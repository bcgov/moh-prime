import { Component, OnInit, Input, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatTabChangeEvent } from '@angular/material/tabs';

import { Subscription, Observable, EMPTY, of, noop } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { RoutePath, RouteUtils } from '@lib/utils/route-utils.class';
import { MatTableDataSourceUtils } from '@lib/modules/ngx-material/mat-table-data-source-utils.class';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
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
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-site-list.model';
import { AbstractSiteAdminPage } from '@adjudication/shared/classes/abstract-site-admin-page.class';

@Component({
  selector: 'app-site-registration-tabs',
  templateUrl: './site-registration-tabs.component.html',
  styleUrls: ['./site-registration-tabs.component.scss']
})
export class SiteRegistrationTabsComponent extends AbstractSiteAdminPage implements OnInit {
  public busy: Subscription;
  @Input() public refresh: Observable<boolean>;

  public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;
  public healthAuthoritySites: HealthAuthoritySiteAdminList[];

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;
  public CareSettingEnum = CareSettingEnum;

  public communityPracticeColumns: string[];
  public communityPharmacyColumns: string[];
  public siteTabIndex: number;

  private tabIndexToCareSettingMap: Record<number, CareSettingEnum>;
  private careSettingToTabIndexMap: { [key in CareSettingEnum]?: number };

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
    protected healthAuthResource: HealthAuthorityResource,
    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
    private organizationResource: OrganizationResource,
    private permissionService: PermissionService,
    private utilResource: UtilsService,
  ) {
    super(route, router, dialog, siteResource, adjudicationResource, healthAuthResource);

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
    this.healthAuthoritySites = [];
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

  protected getDataset(queryParams: { careSetting?: CareSettingEnum, textSearch?: string }): void {
    let careSettingCode = +queryParams?.careSetting ?? CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;
    if (!(careSettingCode in this.careSettingToTabIndexMap)) {
      careSettingCode = CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;
    }

    if (careSettingCode === CareSettingEnum.HEALTH_AUTHORITY) {
      this.healthAuthResource.getAllHealthAuthoritySites()
        .subscribe((sites: HealthAuthoritySiteAdminList[]) => this.healthAuthoritySites = sites)
    } else {
      this.busy = this.getOrganizations({ careSettingCode, ...queryParams })
        .pipe(
          map(this.toSiteRegistrations),
        )
        .subscribe((siteRegistrations: SiteRegistrationListViewModel[]) => this.dataSource.data = siteRegistrations);
    }
  }

  protected updateSite(updatedSite: Site) {
    const siteRegistration = this.dataSource.data.find((siteReg: SiteRegistrationListViewModel) => siteReg.id === updatedSite.id);
    const updatedSiteRegistration = {
      ...siteRegistration,
      ...this.toSiteViewModelPartial(updatedSite)
    };
    this.dataSource.data = MatTableDataSourceUtils
      .update<SiteRegistrationListViewModel>(this.dataSource, 'siteId', updatedSiteRegistration);
  }

  private getOrganizations(
    queryParam: { textSearch?: string, careSettingCode?: CareSettingEnum }
  ): Observable<OrganizationSearchListViewModel[]> {
    return this.organizationResource.getOrganizations(queryParam)
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
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
        const { id, doingBusinessAs, ...site } = svm;
        return (!index)
          ? { organizationId, ...organization, id, siteDoingBusinessAs: doingBusinessAs, ...site, matchOn }
          : { organizationId, id, siteDoingBusinessAs: doingBusinessAs, ...site, matchOn };
      });
      registrations.push(registration);
      return registrations;
    }, []);

    return [].concat(...siteRegistrations);
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
      status,
      businessLicence,
      flagged
    } = site;

    return {
      id,
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
