import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { Role } from '@auth/shared/enum/role.enum';

@Component({
  selector: 'app-health-authority-site-summary',
  templateUrl: './health-authority-site-summary.component.html',
  styleUrls: ['./health-authority-site-summary.component.scss']
})
export class HealthAuthoritySiteSummaryComponent implements OnInit, OnChanges {
  @Input() public site: HealthAuthoritySite;
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public notify: EventEmitter<{ siteId: number }>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public siteColumns: string[];
  public dataSource: MatTableDataSource<HealthAuthoritySite>;

  public SiteStatusType = SiteStatusType;
  public AdjudicationRoutes = AdjudicationRoutes;
  public Role = Role;
  public HealthAuthorityEnum = HealthAuthorityEnum;

  constructor() {
    this.siteColumns = [
      'orgName',
      'siteName',
      'submissionDate',
      'assignedTo',
      'state',
      'siteId',
      'remoteUsers',
      'siteActions'
    ];
    this.assign = new EventEmitter<number>();
    this.reassign = new EventEmitter<number>();
    this.notify = new EventEmitter<{ siteId: number }>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.dataSource = new MatTableDataSource<HealthAuthoritySite>([]);
  }

  public ngOnInit(): void {
    this.dataSource.data = [this.site];
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

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public ngOnChanges(changes: SimpleChanges) {
    if (!changes.site.firstChange) {
      this.dataSource.data = [changes.site.currentValue];
    }
  }
}
