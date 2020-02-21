import { Component, OnInit, Input } from '@angular/core';
import { AccessTerm } from '@shared/models/access-term.model';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-access-terms-table',
  templateUrl: './access-terms-table.component.html',
  styleUrls: ['./access-terms-table.component.scss']
})
export class AccessTermsTableComponent implements OnInit {
  @Input() dataSource: MatTableDataSource<AccessTerm>;
  @Input() enrolmentRoute: string;
  public columns: string[];

  constructor(
  ) {
    this.columns = ['current', 'applicationDate', 'approvalDate', 'actions'];
  }

  public ngOnInit() {
  }
}
