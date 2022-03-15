import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';

import { SiteStatusType } from '@lib/enums/site-status.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { Pagination } from '@core/models/pagination.model';

import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { RouteUtils } from '@lib/utils/route-utils.class';

class ImprovedPageEvent extends PageEvent {
  public stopPropogation: boolean;
}

@Component({
  selector: 'app-site-registration-table',
  templateUrl: './site-registration-table.component.html',
  styleUrls: ['./site-registration-table.component.scss']
})
export class SiteRegistrationTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;
  @Input() public columns: string[];
  @Input() public pagination: Pagination;
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public notify: EventEmitter<{ siteId: number }>;
  @Output() public reload: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  @ViewChild(MatPaginator, { static: true }) public paginator: MatPaginator;
  @ViewChild('secondaryPaginator', { static: true }) public secondaryPaginator: MatPaginator;


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

  public remoteUsers(siteRegistration: SiteRegistrationListViewModel): number | 'Yes' | 'No' | 'N/A' {
    const count = siteRegistration.remoteUserCount;

    return (siteRegistration.careSettingCode !== CareSettingEnum.COMMUNITY_PHARMACIST)
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

  public ngOnInit(): void { }
}
