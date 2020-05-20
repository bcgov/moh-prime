import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Site } from '@registration/shared/models/site.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-site-table',
  templateUrl: './site-table.component.html',
  styleUrls: ['./site-table.component.scss']
})
export class SiteTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<Site>;
  @Output() public delete: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public columns: string[];

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private authService: AuthService
  ) {
    this.delete = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.columns = [
      'organizationName',
      'signingAuthority',
      'submittedDate',
      'actions'
    ];
    this.dataSource = new MatTableDataSource<Site>([]);
  }

  public get canDelete(): boolean {
    return this.authService.isSuperAdmin();
  }

  public onDelete(siteId: number) {
    this.delete.emit(siteId);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public ngOnInit(): void {
  }

}
