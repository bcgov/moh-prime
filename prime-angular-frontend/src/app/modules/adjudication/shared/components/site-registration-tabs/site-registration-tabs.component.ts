import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatTabChangeEvent } from '@angular/material/tabs';

import { Subscription, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { PaginatedList } from '@core/models/paginated-list.model';
import { Pagination } from '@core/models/pagination.model';
import { SiteResource } from '@core/resources/site-resource.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { MatTableDataSourceUtils } from '@lib/modules/ngx-material/mat-table-data-source-utils.class';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { SearchFormStatusType } from '@adjudication/shared/enums/search-form-status-type.enum';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AbstractSiteAdminPage } from '@adjudication/shared/classes/abstract-site-admin-page.class';
import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-admin-site-list.model';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { SearchHAFormComponent } from '../search-ha-form/search-ha-form.component';
import { OrganizationResource } from '@core/resources/organization-resource.service';

@Component({
  selector: 'app-site-registration-tabs',
  templateUrl: './site-registration-tabs.component.html',
  styleUrls: ['./site-registration-tabs.component.scss']
})
export class SiteRegistrationTabsComponent extends AbstractSiteAdminPage implements OnInit {
  public busy: Subscription;
  @Input() public refresh: Observable<boolean>;
  @ViewChild('searchHaSiteForm') searchHaSiteForm: SearchHAFormComponent;
  @ViewChild('searchComSiteForm') searchComSiteForm: SearchHAFormComponent;

  public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;
  public healthAuthoritySites: HealthAuthoritySiteAdminList[];
  public pagination: Pagination;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;
  public CareSettingEnum = CareSettingEnum;
  public SearchFormStatusType = SearchFormStatusType;

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
    protected healthAuthoritySiteResource: HealthAuthoritySiteResource,
    protected organizationResource: OrganizationResource,
    private authService: AuthService,
  ) {
    super(route, router, dialog, siteResource, adjudicationResource, healthAuthoritySiteResource, organizationResource);

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
      2: CareSettingEnum.DEVICE_PROVIDER,
      3: CareSettingEnum.HEALTH_AUTHORITY
    };
    this.careSettingToTabIndexMap = {
      [CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE]: 0,
      [CareSettingEnum.COMMUNITY_PHARMACIST]: 1,
      [CareSettingEnum.DEVICE_PROVIDER]: 2,
      [CareSettingEnum.HEALTH_AUTHORITY]: 3
    };
    this.healthAuthoritySites = [];
  }

  public onSearch(textSearch: string | null): void {
    this.routeUtils.updateQueryParams({ textSearch, page: null });
  }

  public onFilter(status: any | null): void {
    this.routeUtils.updateQueryParams({ status, page: null });
  }

  public onSiteStatusChange(statusId: any | null): void {
    this.routeUtils.updateQueryParams({ statusId, page: null });
  }

  public onVendorChange(vendorId: any | null): void {
    this.routeUtils.updateQueryParams({ vendorId, page: null });
  }

  public onCareTypeChange(careType: any | null): void {
    this.routeUtils.updateQueryParams({ careType, page: null });
  }

  public onAssignToMeChange(assignToMe: any | null): void {
    this.routeUtils.updateQueryParams({ assignToMe, page: null });
  }

  public onTabChange(tabChangeEvent: MatTabChangeEvent): void {
    this.routeUtils.removeQueryParams({ careSetting: this.tabIndexToCareSettingMap[tabChangeEvent.index], page: null });
  }

  public onTextSearchHaSite(textSearch: string | null): void {
    this.routeUtils.updateQueryParams({ textSearch, page: null });
    this.searchHaSiteForm.textSearch.setValue(textSearch);
  }

  public onTextSearchComSite(textSearch: string | null): void {
    this.routeUtils.updateQueryParams({ textSearch, page: null });
    this.searchComSiteForm.textSearch.setValue(textSearch);
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

  protected getDataset(queryParams: {
    careSetting?: CareSettingEnum, textSearch?: string,
    careType?: string, statusId?: number, vendorId?: number, assignToMe?: boolean
  }): void {
    let careSettingCode = +queryParams?.careSetting ?? CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;
    if (!(careSettingCode in this.careSettingToTabIndexMap)) {
      careSettingCode = CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;
    }

    if (careSettingCode === CareSettingEnum.HEALTH_AUTHORITY) {
      const { textSearch, careType, statusId, vendorId, assignToMe } = queryParams;
      this.healthAuthResource.getHealthAuthoritySitesByQuery({ textSearch, careType, statusId, vendorId, assignToMe })
        .subscribe((sites: HealthAuthoritySiteAdminList[]) => {
          this.healthAuthoritySites = sites;
        })
    } else {
      this.busy = this.getPaginatedSites({ careSettingCode, ...queryParams })
        .subscribe((paginatedList: PaginatedList<SiteRegistrationListViewModel>) => {
          this.dataSource.data = paginatedList.results;
          this.pagination = paginatedList;
        });
    }
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

  private getPaginatedSites(
    queryParam: { textSearch?: string, careSettingCode?: CareSettingEnum }
  ): Observable<PaginatedList<SiteRegistrationListViewModel>> {
    return this.siteResource.getPaginatedSites(queryParam)
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
  }
}
