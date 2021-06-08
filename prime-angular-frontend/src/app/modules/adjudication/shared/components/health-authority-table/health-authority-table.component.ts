import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { Role } from '@auth/shared/enum/role.enum';

@Component({
  selector: 'app-health-authority-table',
  templateUrl: './health-authority-table.component.html',
  styleUrls: ['./health-authority-table.component.scss']
})
export class HealthAuthorityTableComponent implements OnInit {
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public dataSource: MatTableDataSource<Config<number>>;
  public columns: string[];
  public Role = Role;
  public AdjudicationRoutes = AdjudicationRoutes;
  public haCode: HealthAuthorityEnum;

  public flaggedHealthAuthorities: HealthAuthorityEnum[];

  constructor(
    private configService: ConfigService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private healthAuthorityResource: HealthAuthorityResource
  ) {
    this.columns = [
      'prefixes',
      'referenceId',
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

    this.dataSource = new MatTableDataSource<Config<number>>([]);
    this.haCode = this.activatedRoute.snapshot.params.haid;
    this.dataSource.data = (this.haCode)
      ? this.configService.healthAuthorities?.filter(ha => ha.code === +this.haCode)
      : this.configService.healthAuthorities?.sort((a, b) => a.code - b.code);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public ngOnInit(): void {
    this.healthAuthorityResource
      .getHealthAuthorityCodesWithUnderReviewUsers().subscribe(codes => this.flaggedHealthAuthorities = codes);
  }
}
