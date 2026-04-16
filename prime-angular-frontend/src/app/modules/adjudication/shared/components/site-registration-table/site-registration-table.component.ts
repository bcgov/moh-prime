import { Component, OnInit, Input, Output, EventEmitter, ViewChild, AfterViewInit } from '@angular/core';
import { formatDate } from "@angular/common";
import { FormControl } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Sort, MatSort } from '@angular/material/sort';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { Pagination } from '@core/models/pagination.model';

import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

class ImprovedPageEvent extends PageEvent {
  public stopPropogation: boolean;
}

@Component({
  selector: 'app-site-registration-table',
  templateUrl: './site-registration-table.component.html',
  styleUrls: ['./site-registration-table.component.scss']
})
export class SiteRegistrationTableComponent implements OnInit, AfterViewInit {
  @Input() public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;
  @Input() public columns: string[];
  @Input() public pagination: Pagination;
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public notify: EventEmitter<{ siteId: number }>;
  @Output() public reload: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public pecFilter: EventEmitter<string>;

  @ViewChild(MatPaginator, { static: true }) public paginator: MatPaginator;
  @ViewChild('secondaryPaginator', { static: true }) public secondaryPaginator: MatPaginator;

  //  @ViewChild('siteTbSort') siteTbSort = new MatSort();

  public SiteStatusType = SiteStatusType;
  public CareSettingEnum = CareSettingEnum;
  public AdjudicationRoutes = AdjudicationRoutes;
  public Role = Role;

  protected routeUtils: RouteUtils;

  constructor(
    private activatedRoute: ActivatedRoute,
    router: Router
  ) {
    this.columns = [
      'prefixes',
      'displayId',
      'organizationName',
      'signingAuthority',
      'siteDoingBusinessAs',
      'submissionDate',
      'assignedTo',
      'state',
      'siteId',
      'remoteUsers',
      'careSetting',
      'missingBusinessLicence',
      'actions'
    ];
    this.assign = new EventEmitter<number>();
    this.reassign = new EventEmitter<number>();
    this.notify = new EventEmitter<{ siteId: number }>();
    this.reload = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.routeUtils = new RouteUtils(activatedRoute, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
    this.pecFilter = new EventEmitter<string>();
  }

  public onAssign(siteId: number): void {
    this.assign.emit(siteId);
  }

  public onReassign(siteId: number): void {
    this.reassign.emit(siteId);
  }

  public onNotify(siteId: number): void {
    this.notify.emit({ siteId });
  }

  public onReload(siteId: number): void {
    this.reload.emit(siteId);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  // TODO status lookup for sites would remove the need for this method and only require pipes
  public displayStatus(status: SiteStatusType) {
    switch (status) {
      case SiteStatusType.EDITABLE:
        return 'Editable';
      case SiteStatusType.IN_REVIEW:
        return 'In Review';
      case SiteStatusType.LOCKED:
        return 'Locked';
      default:
        return 'Editable';
    }
  }

  public displayMissingBusinessLicence(row: SiteRegistrationListViewModel): string {
    if (row.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACY) {
      if (row.missingBusinessLicence === undefined) {
        row.missingBusinessLicence = row.businessLicence === null ||
          row.businessLicence.businessLicenceDocument === null
      }
      if (row.missingBusinessLicence) {
        return "Yes"
      } else {
        return "No"
      }
    } else {
      return "N/A";
    }
  }

  public remoteUsers(siteRegistration: SiteRegistrationListViewModel): number | 'Yes' | 'No' | 'N/A' {
    const count = siteRegistration.remoteUserCount;

    return (siteRegistration.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE)
      ? (!this.activatedRoute.snapshot.params.sid)
        ? (count) ? 'Yes' : 'No'
        : count
      : 'N/A';
  }

  public onPage(event: ImprovedPageEvent, top: boolean): void {
    const other = (!top) ? this.paginator : this.secondaryPaginator;
    if (event.stopPropogation) {
      return;
    }
    event.stopPropogation = true;
    this.routeUtils.updateQueryParams({ page: `${event.pageIndex + 1}` });
    other.page.emit(event);
  }

  public ngOnInit(): void {
  }

  public ngAfterViewInit(): void {
    // TODO: Fix sorting
    // this.siteTbSort.disableClear = true;
    // this.dataSource.sort = this.siteTbSort;
  }

  public sortData(sort: Sort) {
    const data = this.dataSource.data.slice();
    if (!sort.active || sort.direction === '') {
      this.dataSource.data = data;
      return;
    }

    this.dataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'state':
          return compare(a.status, b.status, isAsc, false);
        case 'submissionDate':
          return compare(a.submittedDate, b.submittedDate, isAsc, true);
        default:
          return 0;
      }
    });
  }

  public getDuplicatePecText(row: SiteRegistrationListViewModel) {
    return `${row.duplicatePecSiteCount + 1} sites share the same site ID`;
  }

  public onPecFilter(pec: string) {
    this.pecFilter.emit(pec);
  }

  public onFilterByLink(row: SiteRegistrationListViewModel) {
    var site = row.predecessorSite ?
      row.predecessorSite.site :
      row.successorSite.site;
    this.pecFilter.emit(`${site.pec}, ${row.pec}`);
  }

  public getPredecessorSiteText(row: SiteRegistrationListViewModel) {
    return `Predecessor Site: ${row.predecessorSite.site.doingBusinessAs} (${row.predecessorSite.site.pec})`
  }
  public getSuccessorSiteText(row: SiteRegistrationListViewModel) {
    return `Successor Site: ${row.successorSite.site.doingBusinessAs} (${row.successorSite.site.pec})`
  }
}


function compare(a: number | string, b: number | string, isAsc: boolean, isDate: boolean) {
  if (isDate) {
    return (formatDate(a, 'dd MMM yyyy', 'en_US') < formatDate(b, 'dd MMM yyyy', 'en_US') ? -1 : 1) * (isAsc ? 1 : -1);
  } else {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
}
