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

@Component({
  selector: 'app-health-authority-table',
  templateUrl: './health-authority-table.component.html',
  styleUrls: ['./health-authority-table.component.scss']
})
export class HealthAuthorityTableComponent implements OnInit {
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public columns: string[];
  public dataSource: MatTableDataSource<HealthAuthorityList>;
  public healthAuthorityCode: HealthAuthorityEnum;
  public flaggedHealthAuthorities: HealthAuthorityEnum[];
  public Role = Role;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private configService: ConfigService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private healthAuthorityResource: HealthAuthorityResource
  ) {
    this.columns = [
      'prefixes',
      'orgName',
      'siteName',
      'submissionDate',
      'assignedTo',
      'state',
      'siteId',
      'remoteUsers',
      'actions'
    ];
    this.route = new EventEmitter<string | (string | number)[]>();
    this.dataSource = new MatTableDataSource<HealthAuthorityList>([]);
    this.healthAuthorityCode = this.activatedRoute.snapshot.params.haid;
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public ngOnInit(): void {
    this.healthAuthorityResource.getHealthAuthorities()
      .subscribe((healthAuthorities: HealthAuthorityList[]) => {
        this.flaggedHealthAuthorities = healthAuthorities.reduce((fhas: number[], ha: HealthAuthorityList) =>
          [...fhas, ...ArrayUtils.insertIf(ha.hasUnderReviewUsers, ha.id)], []
        );

        this.dataSource.data = (this.healthAuthorityCode)
          ? healthAuthorities.filter(ha => ha.id === +this.healthAuthorityCode)
          : healthAuthorities.sort((a, b) => a.id - b.id);
      });
  }
}
