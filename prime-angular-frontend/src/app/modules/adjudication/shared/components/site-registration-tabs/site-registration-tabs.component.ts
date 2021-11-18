import { Component, OnInit, Input, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatTabChangeEvent } from '@angular/material/tabs';

import { Subscription, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { MatTableDataSourceUtils } from '@lib/modules/ngx-material/mat-table-data-source-utils.class';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Site } from '@registration/shared/models/site.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import {
  SiteRegistrationListViewModel,
  SiteListViewModelPartial,
  OrganizationSearchListViewModel
} from '@registration/shared/models/site-registration.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-admin-site-list.model';
import { AbstractSiteAdminPage } from '@adjudication/shared/classes/abstract-site-admin-page.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

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
    protected healthAuthoritySiteResource: HealthAuthoritySiteResource,
    private organizationResource: OrganizationResource,
  ) {
    super(route, router, dialog, siteResource, adjudicationResource, healthAuthoritySiteResource);

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

  protected updateSite(siteId: number, updatedSiteFields: {}): void {
    const siteRegistration = this.dataSource.data.find((siteReg: SiteRegistrationListViewModel) => siteReg.id === siteId);
    const updatedSiteRegistration = {
      ...siteRegistration,
      ...updatedSiteFields
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
}
