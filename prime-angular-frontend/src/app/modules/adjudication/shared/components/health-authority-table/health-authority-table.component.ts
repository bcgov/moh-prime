import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Role } from '@auth/shared/enum/role.enum';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';

@Component({
  selector: 'app-health-authority-table',
  templateUrl: './health-authority-table.component.html',
  styleUrls: ['./health-authority-table.component.scss']
})
export class HealthAuthorityTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<Config<number>>;

  @Output() public route: EventEmitter<string | (string | number)[]>;

  public columns: string[];

  public Role = Role;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private configService: ConfigService,
  ) {
    this.columns = [
      'referenceId',
      'name',
      'actions'
    ];
    this.route = new EventEmitter<string | (string | number)[]>();
    this.dataSource = new MatTableDataSource<Config<number>>([]);
    this.dataSource.data = this.configService.healthAuthorities;
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  ngOnInit(): void {
  }

}
