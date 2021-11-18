import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';


import { ArrayUtils } from '@lib/utils/array-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthorityRow } from '@shared/models/health-authority-row.model';
import { Role } from '@auth/shared/enum/role.enum';

import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-admin-site-list.model';

@Component({
  selector: 'app-health-authority-table',
  templateUrl: './health-authority-table.component.html',
  styleUrls: ['./health-authority-table.component.scss']
})
export class HealthAuthorityTableComponent implements OnInit, OnChanges {
  @Input() public sites: (HealthAuthorityRow | HealthAuthoritySiteAdminList)[];
  @Input() public showHealthAuthorities: boolean = true;
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public notify: EventEmitter<{ siteId: number, healthAuthorityOrganizationId: HealthAuthorityEnum }>;
  @Output() public reload: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public dataSource: MatTableDataSource<HealthAuthorityRow | HealthAuthoritySiteAdminList>;
  public healthAuthorities: HealthAuthorityRow[];

  public siteColumns: string[];
  public flaggedHealthAuthorities: HealthAuthorityEnum[];
  public Role = Role;
  public AdjudicationRoutes = AdjudicationRoutes;
  public SiteStatusType = SiteStatusType;
  public expandedHealthAuthId: number;

  constructor(
    private activatedRoute: ActivatedRoute,
    private healthAuthorityResource: HealthAuthorityResource,
  ) {
    this.siteColumns = [
      'prefixes',
      'orgName',
      'siteName',
      'authorizedUser',
      'vendor',
      'siteId',
      'submissionDate',
      'state',
      'assignedTo',
      'siteActions'
    ];
    this.expandedHealthAuthId = 0;
    this.healthAuthorities = [];

    this.assign = new EventEmitter<number>();
    this.reassign = new EventEmitter<number>();
    this.notify = new EventEmitter<{ siteId: number, healthAuthorityOrganizationId: HealthAuthorityEnum }>();
    this.reload = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();

    this.dataSource = new MatTableDataSource<HealthAuthorityRow | HealthAuthoritySiteAdminList>([]);
  }

  public onAssign(siteId: number): void {
    this.assign.emit(siteId);
  }

  public onReassign(siteId: number): void {
    this.reassign.emit(siteId);
  }

  public onNotify(siteId: number, healthAuthorityOrganizationId: HealthAuthorityEnum): void {
    this.notify.emit({ siteId, healthAuthorityOrganizationId });
  }

  public onReload(siteId: number): void {
    this.reload.emit(siteId);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public isHealthAuthority(row: HealthAuthorityRow | HealthAuthoritySiteAdminList): boolean {
    return row.hasOwnProperty('hasUnderReviewUsers');
  }

  public isGroup(): (index: number, row: HealthAuthorityRow | HealthAuthoritySiteAdminList) => boolean {
    return (index: number, row: HealthAuthorityRow | HealthAuthoritySiteAdminList): boolean =>
      this.isHealthAuthority(row);
  }

  public onExpandHeader(item: HealthAuthorityRow): void {
    this.expandedHealthAuthId = (this.expandedHealthAuthId !== item.id)
      ? item.id
      : 0;
  }

  public ngOnInit(): void {
    if (this.showHealthAuthorities) {
      this.healthAuthorityResource.getHealthAuthorities().subscribe((has: HealthAuthorityRow[]) => {
        this.flaggedHealthAuthorities = has.reduce((fhas: number[], ha: HealthAuthorityRow) =>
          [...fhas, ...ArrayUtils.insertIf(ha.hasUnderReviewUsers, ha.id)], []
        );
        this.healthAuthorities = has;
        this.dataSource.data = Object.assign([], this.sites).concat(has).sort(this.sortData());
      })
    } else {
      this.dataSource.data = Object.assign([], this.sites).sort(this.sortData());
    }
  };

  public ngOnChanges(changes: SimpleChanges): void {
    if (changes.sites.currentValue) {
      this.dataSource.data = Object.assign([], this.sites)
        .concat((this.showHealthAuthorities) ? this.healthAuthorities : [])
        .sort(this.sortData());
    }
  }

  /**
   * @description
   * Sort health authorities and their grouped sites in ascending order by ID.
   */
  private sortData(): (a: HealthAuthorityRow | HealthAuthoritySiteAdminList, b: HealthAuthorityRow | HealthAuthoritySiteAdminList) => number {
    return (a: HealthAuthorityRow | HealthAuthoritySiteAdminList, b: HealthAuthorityRow | HealthAuthoritySiteAdminList): number => {
      if (this.isHealthAuthority(a) && this.isHealthAuthority(b)) {
        return a.id - b.id;
      } else if (this.isHealthAuthority(a)) {
        return (a.id !== (b as HealthAuthoritySiteAdminList).healthAuthorityOrganizationId)
          ? a.id - (b as HealthAuthoritySiteAdminList).healthAuthorityOrganizationId
          : -1;
      } else if (this.isHealthAuthority(b)) {
        return (b.id !== (a as HealthAuthoritySiteAdminList).healthAuthorityOrganizationId)
          ? (a as HealthAuthoritySiteAdminList).healthAuthorityOrganizationId - b.id
          : 1;
      } else {
        return (a as HealthAuthoritySiteAdminList).healthAuthorityOrganizationId - (b as HealthAuthoritySiteAdminList).healthAuthorityOrganizationId;
      }
    };
  }
}
