import { Component, OnInit, ContentChild, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicatorActionsComponent } from '@adjudication/shared/components/adjudicator-actions/adjudicator-actions.component';

@Component({
  selector: 'app-enrollee-table',
  templateUrl: './enrollee-table.component.html',
  styleUrls: ['./enrollee-table.component.scss']
})
export class EnrolleeTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<HttpEnrollee>;
  @Output() public claim: EventEmitter<number>;
  @Output() public disclaim: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public columns: string[];

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private authService: AuthService
  ) {
    this.claim = new EventEmitter<number>();
    this.disclaim = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.columns = ['uniqueId', 'name', 'appliedDate', 'status', 'approvedDate', 'adjudicator', 'actions'];
    this.dataSource = new MatTableDataSource<HttpEnrollee>([]);
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public onClaim(enrolleeId: number) {
    this.claim.emit(enrolleeId);
  }

  public onDisclaim(enrolleeId: number) {
    this.disclaim.emit(enrolleeId);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public ngOnInit() { }
}
