import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { Site } from '@registration/shared/models/site.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';


@Component({
  selector: 'app-site-registration-table',
  templateUrl: './site-registration-table.component.html',
  styleUrls: ['./site-registration-table.component.scss']
})
export class SiteRegistrationTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<Site>;
  @Output() public delete: EventEmitter<number>;

  public columns: string[];

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor() {
    this.columns = [
      'locationName',
      'vendor',
      'submissionDate',
      'siteAdjudication',
      'pecCode',
      'actions'
    ];
    this.dataSource = new MatTableDataSource<Site>([]);
    this.delete = new EventEmitter<number>();
  }

  public deleteSite(siteId: number) {
    this.delete.emit(siteId);
  }

  public ngOnInit(): void { }
}
