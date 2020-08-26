import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';

@Component({
  selector: 'app-site-registration-table',
  templateUrl: './site-registration-table.component.html',
  styleUrls: ['./site-registration-table.component.scss']
})
export class SiteRegistrationTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public delete: EventEmitter<{ [key: string]: number }>;

  public columns: string[];

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private authService: AuthService
  ) {
    this.columns = [
      'displayId',
      'organizationName',
      'signingAuthority',
      'doingBusinessAs',
      'submissionDate',
      'siteAdjudication',
      'siteId',
      'careSetting',
      'actions'
    ];
    this.route = new EventEmitter<string | (string | number)[]>();
    this.delete = new EventEmitter<{ [key: string]: number }>();
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public onDelete(record: { [key: string]: number }) {
    this.delete.emit(record);
  }

  public ngOnInit(): void { }
}
