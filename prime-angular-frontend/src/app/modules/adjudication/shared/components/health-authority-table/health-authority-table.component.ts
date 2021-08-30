import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { HealthAuthorityRow } from '@shared/models/health-authority-row.model';
import { Role } from '@auth/shared/enum/role.enum';
import { forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';

@Component({
  selector: 'app-health-authority-table',
  templateUrl: './health-authority-table.component.html',
  styleUrls: ['./health-authority-table.component.scss']
})
export class HealthAuthorityTableComponent implements OnInit {
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public siteColumns: string[];
  public dataSource: MatTableDataSource<HealthAuthorityRow | HealthAuthoritySite>;
  public flaggedHealthAuthorities: HealthAuthorityEnum[];
  public Role = Role;
  public AdjudicationRoutes = AdjudicationRoutes;
  public SiteStatusType = SiteStatusType;
  public expandedHealthAuthId: number;


  constructor(
    private activatedRoute: ActivatedRoute,
    private healthAuthorityResource: HealthAuthorityResource
  ) {
    this.siteColumns = [
      'prefixes',
      'orgName',
      'siteName',
      'submissionDate',
      'assignedTo',
      'state',
      'siteId',
      'remoteUsers',
      'siteActions'
    ];
    this.route = new EventEmitter<string | (string | number)[]>();
    this.dataSource = new MatTableDataSource<HealthAuthorityRow | HealthAuthoritySite>([]);
    // No HA headers should be expanded initially
    this.expandedHealthAuthId = 0;
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public isHealthAuthorityObject(rowData): boolean {
    return rowData.hasOwnProperty('hasUnderReviewUsers');
  }

  public isGroup(): (index: number, rowData: HealthAuthorityRow | HealthAuthoritySite) => boolean {
    // Return ES6 Arrow Function to avoid use of `.bind(this)` or static functions
    return (index: number, rowData: HealthAuthorityRow | HealthAuthoritySite): boolean => {
      return this.isHealthAuthorityObject(rowData);
    }
  }

  public onExpandHeader(item: HealthAuthorityRow): void {
    this.expandedHealthAuthId = (this.expandedHealthAuthId !== item.id ? item.id : 0);
  }

  public ngOnInit(): void {
    forkJoin({
      healthAuthorities: this.healthAuthorityResource.getHealthAuthorities(),
      healthAuthoritySites: this.healthAuthorityResource.getAllHealthAuthoritySites()
    }).pipe(
      map(({ healthAuthorities, healthAuthoritySites }) => {
        this.flaggedHealthAuthorities = healthAuthorities.reduce((fhas: number[], ha: HealthAuthorityRow) =>
          [...fhas, ...ArrayUtils.insertIf(ha.hasUnderReviewUsers, ha.id)], []
        );
        // Sort HAs together with HA Site Registrations
        this.dataSource.data = [...healthAuthorities, ...healthAuthoritySites].sort(this.sortData());
      })
    ).subscribe();
  }

  /**
   * @description
   * Compare function for sorting that intends to sort Health Authorities in ascending order by their ID,
   * and group Site Registrations immediately following each related Health Authority.
   */
  private sortData(): (a: HealthAuthorityRow | HealthAuthoritySite, b: HealthAuthorityRow | HealthAuthoritySite) => number {
    // Return ES6 Arrow Function to avoid use of `.bind(this)` or static functions
    return (a: HealthAuthorityRow | HealthAuthoritySite, b: HealthAuthorityRow | HealthAuthoritySite): number => {
      if (this.isHealthAuthorityObject(a) && this.isHealthAuthorityObject(b)) {
        return a.id - b.id;
      }
      else if (this.isHealthAuthorityObject(a)) {
        if ((a as HealthAuthorityRow).id === (b as HealthAuthoritySite).healthAuthorityOrganizationId) {
          return -1;
        } else {
          return (a as HealthAuthorityRow).id - (b as HealthAuthoritySite).healthAuthorityOrganizationId;
        }
      }
      else if (this.isHealthAuthorityObject(b)) {
        if ((b as HealthAuthorityRow).id === (a as HealthAuthoritySite).healthAuthorityOrganizationId) {
          return 1;
        } else {
          return (a as HealthAuthoritySite).healthAuthorityOrganizationId - (b as HealthAuthorityRow).id;
        }
      }
      else {
        return (a as HealthAuthoritySite).healthAuthorityOrganizationId - (b as HealthAuthoritySite).healthAuthorityOrganizationId;
      }
    }
  }
}
