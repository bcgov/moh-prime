import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { Site } from '@registration/shared/models/site.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-site-registration-table',
  templateUrl: './site-registration-table.component.html',
  styleUrls: ['./site-registration-table.component.scss']
})
export class SiteRegistrationTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<Site>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public delete: EventEmitter<number>;

  public columns: string[];

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private authenticationService: AuthenticationService
  ) {
    this.columns = [
      'locationName',
      'vendor',
      'submissionDate',
      'siteAdjudication',
      'pecCode',
      'actions'
    ];
    this.dataSource = new MatTableDataSource<Site>([]);
    this.route = new EventEmitter<string | (string | number)[]>();
    this.delete = new EventEmitter<number>();
  }

  public get canEdit(): boolean {
    return this.authenticationService.isAdmin();
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public deleteSite(siteId: number) {
    this.delete.emit(siteId);
  }

  public ngOnInit(): void { }
}
