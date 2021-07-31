import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ArrayUtils } from '@lib/utils/array-utils.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { HealthAuthorityList } from '@shared/models/health-authority-list.model';
import { Role } from '@auth/shared/enum/role.enum';
import { exhaustMap, mergeMap } from 'rxjs/operators';
import { from } from 'rxjs';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

@Component({
  selector: 'app-health-authority-table',
  templateUrl: './health-authority-table.component.html',
  styleUrls: ['./health-authority-table.component.scss']
})
export class HealthAuthorityTableComponent implements OnInit {
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public haColumns: string[];
  public haDataSource: MatTableDataSource<HealthAuthorityList>;
  public healthAuthorityCode: HealthAuthorityEnum;
  public flaggedHealthAuthorities: HealthAuthorityEnum[];
  public Role = Role;
  public AdjudicationRoutes = AdjudicationRoutes;
  public healthAuthorityIdToSites: Map<number, HealthAuthoritySite[]>;
  public siteColumns: string[];
  public sitesDataSource: MatTableDataSource<HealthAuthoritySite>;


  constructor(
    private configService: ConfigService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private healthAuthorityResource: HealthAuthorityResource
  ) {
    this.haColumns = [
      'prefixes',
      'orgName',
      'assignedTo',
      'state',
      'remoteUsers',
      'actions'
    ];
    this.route = new EventEmitter<string | (string | number)[]>();
    this.haDataSource = new MatTableDataSource<HealthAuthorityList>([]);
    this.healthAuthorityCode = this.activatedRoute.snapshot.params.haid;
    this.healthAuthorityIdToSites = new Map<number, HealthAuthoritySite[]>();
    this.sitesDataSource = new MatTableDataSource<HealthAuthoritySite>([]);
    this.siteColumns = [
      'siteName',
      'submissionDate',
      'siteId',
    ];
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public showSites(healthAuthorityId: number) {
    this.sitesDataSource.data = this.healthAuthorityIdToSites.get(healthAuthorityId);
  }

  public ngOnInit(): void {
    this.healthAuthorityResource.getHealthAuthorities()
      .pipe(
        exhaustMap((healthAuthorities: HealthAuthorityList[]) => {
          this.flaggedHealthAuthorities = healthAuthorities.reduce((fhas: number[], ha: HealthAuthorityList) =>
            [...fhas, ...ArrayUtils.insertIf(ha.hasUnderReviewUsers, ha.id)], []
          );

          this.haDataSource.data = (this.healthAuthorityCode)
            ? healthAuthorities.filter(ha => ha.id === +this.healthAuthorityCode)
            : healthAuthorities.sort((a, b) => a.id - b.id);
          return from(healthAuthorities);
        }),
        // TODO: Rename type HealthAuthorityList to HealthAuthorityListItem?
        // As each healthAuthority is emitted, get the sites registered with that healthAuthority
        mergeMap((healthAuthority: HealthAuthorityList) => this.healthAuthorityResource.getHealthAuthoritySites(healthAuthority.id))
      )
      .subscribe((sites: HealthAuthoritySite[]) => {
        // For each list of sites in stream, store in map if non-empty list
        if (sites && sites.length > 0) {
          this.healthAuthorityIdToSites.set(sites[0].healthAuthorityOrganizationId, sites);
        }
      });
  }
}
