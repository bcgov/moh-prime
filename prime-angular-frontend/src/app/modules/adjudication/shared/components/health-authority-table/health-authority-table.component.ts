import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';

import { ConfigService } from '@config/config.service';
import { ArrayUtils } from '@lib/utils/array-utils.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { HealthAuthorityListItem } from '@shared/models/health-authority-list.model';
import { Role } from '@auth/shared/enum/role.enum';
import { exhaustMap, mergeMap } from 'rxjs/operators';
import { from } from 'rxjs';
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
  public dataSource: MatTableDataSource<HealthAuthorityListItem | HealthAuthoritySite>;
  public healthAuthorityCode: HealthAuthorityEnum;
  public flaggedHealthAuthorities: HealthAuthorityEnum[];
  public Role = Role;
  public AdjudicationRoutes = AdjudicationRoutes;
  public SiteStatusType = SiteStatusType;
  public idOfExpandedHA: number;


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
    this.dataSource = new MatTableDataSource<HealthAuthorityListItem | HealthAuthoritySite>([]);
    this.healthAuthorityCode = this.activatedRoute.snapshot.params.haid;
    // No HA headers should be expanded initially
    this.idOfExpandedHA = -1;
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  // TODO: Eliminate duplication as this method copied from SiteRegistrationTableComponent
  public displayStatus(status: SiteStatusType) {
    switch (status) {
      case SiteStatusType.EDITABLE:
        return "Editable";
      case SiteStatusType.IN_REVIEW:
        return "In Review";
      case SiteStatusType.LOCKED:
        return "Locked";
      default:
        return "Editable";
    }
  }

  // TODO: Eliminate duplication as this method very similar to that from SiteRegistrationTableComponent
  public remoteUsers(siteRegistration: HealthAuthoritySite): number | 'Yes' | 'No' {
    const count = siteRegistration.remoteUsers?.length;

    return (!this.activatedRoute.snapshot.params.sid)
      ? (count) ? 'Yes' : 'No'
      : count;
  }

  public isHealthAuthorityObject(rowData): boolean {
    return rowData.hasUnderReviewUsers !== undefined;
  }

  public isGroup(index: number, rowData: HealthAuthorityListItem | HealthAuthoritySite): boolean {
    return HealthAuthorityTableComponent.isHA(rowData);
  }

  public expandHeader(item: HealthAuthorityListItem) {
    this.idOfExpandedHA =
      this.idOfExpandedHA !== item.id ? item.id : -1;
  }

  public ngOnInit(): void {
    this.healthAuthorityResource.getHealthAuthorities()
      .pipe(
        exhaustMap((healthAuthorities: HealthAuthorityListItem[]) => {
          this.flaggedHealthAuthorities = healthAuthorities.reduce((fhas: number[], ha: HealthAuthorityListItem) =>
            [...fhas, ...ArrayUtils.insertIf(ha.hasUnderReviewUsers, ha.id)], []
          );

          this.dataSource.data = (this.healthAuthorityCode)
            ? healthAuthorities.filter(ha => ha.id === +this.healthAuthorityCode)
            : healthAuthorities.sort(this.sortData);
          return from(healthAuthorities);
        }),
        // As each healthAuthority is emitted, get the sites registered with that healthAuthority
        mergeMap((healthAuthority: HealthAuthorityListItem) => this.healthAuthorityResource.getHealthAuthoritySites(healthAuthority.id))
      )
      .subscribe((sites: HealthAuthoritySite[]) => {
        // For each list of sites in stream, store in map if non-empty list
        if (sites && sites.length > 0) {
          this.dataSource.data = [...this.dataSource.data, ...sites].sort(this.sortData);
        }
      });
  }

  private static isHA(obj): boolean {
    return obj.hasUnderReviewUsers !== undefined;
  }

  /**
   * @description
   * Compare function for sorting that intends to sort Health Authorities in ascending order by their ID,
   * and group Site Registrations immediately following each related Health Authority.
   */
  private sortData(a: HealthAuthorityListItem | HealthAuthoritySite, b: HealthAuthorityListItem | HealthAuthoritySite): number {
    if (HealthAuthorityTableComponent.isHA(a) && HealthAuthorityTableComponent.isHA(b)) {
      return a.id - b.id;
    }
    else if (HealthAuthorityTableComponent.isHA(a)) {
      if ((a as HealthAuthorityListItem).id === (b as HealthAuthoritySite).healthAuthorityOrganizationId) {
        return -1;
      } else {
        return (a as HealthAuthorityListItem).id - (b as HealthAuthoritySite).healthAuthorityOrganizationId;
      }
    }
    else if (HealthAuthorityTableComponent.isHA(b)) {
      if ((b as HealthAuthorityListItem).id === (a as HealthAuthoritySite).healthAuthorityOrganizationId) {
        return 1;
      } else {
        return (a as HealthAuthoritySite).healthAuthorityOrganizationId - (b as HealthAuthorityListItem).id;
      }
    }
    else {
      return (a as HealthAuthoritySite).healthAuthorityOrganizationId - (b as HealthAuthoritySite).healthAuthorityOrganizationId;
    }
  }
}
