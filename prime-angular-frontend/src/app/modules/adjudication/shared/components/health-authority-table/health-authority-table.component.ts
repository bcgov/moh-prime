import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from '@auth/shared/enum/role.enum';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';

@Component({
  selector: 'app-health-authority-table',
  templateUrl: './health-authority-table.component.html',
  styleUrls: ['./health-authority-table.component.scss']
})
export class HealthAuthorityTableComponent implements OnInit {
  public dataSource: MatTableDataSource<Config<number>>;

  @Output() public route: EventEmitter<string | (string | number)[]>;

  public columns: string[];

  public Role = Role;
  public AdjudicationRoutes = AdjudicationRoutes;
  public haCode: HealthAuthorityEnum;

  constructor(
    protected configService: ConfigService,
    protected router: Router,
    protected activatedRoute: ActivatedRoute,
  ) {
    this.columns = [
      'referenceId',
      'name',
      'actions'
    ];
    this.route = new EventEmitter<string | (string | number)[]>();

    this.dataSource = new MatTableDataSource<Config<number>>([]);

    this.haCode = this.activatedRoute.snapshot.params.haid;
    this.dataSource.data = (this.haCode)
      ? this.configService.healthAuthorities.filter(ha => ha.code === +this.haCode)
      : this.configService.healthAuthorities.sort((a, b) => a.code - b.code);
  }

  public onRoute(haid: number) {
    const routePath = [AdjudicationRoutes.HEALTH_AUTHORITIES, haid, AdjudicationRoutes.AUTHORIZED_USERS];
    if (this.haCode) {
      // If on single ha vied go to create user
      routePath.push(AdjudicationRoutes.CREATE_USER);
    }
    this.route.emit(routePath);
  }

  ngOnInit(): void { }

}
