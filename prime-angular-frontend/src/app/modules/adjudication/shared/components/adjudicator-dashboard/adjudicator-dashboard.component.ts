import { Component, OnInit, ContentChild, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicatorActionsComponent } from '@adjudication/shared/components/adjudicator-actions/adjudicator-actions.component';

@Component({
  selector: 'app-adjudicator-dashboard',
  templateUrl: './adjudicator-dashboard.component.html',
  styleUrls: ['./adjudicator-dashboard.component.scss']
})
export class AdjudicatorDashboardComponent implements OnInit, OnChanges {
  @Input() public enrollees: HttpEnrollee | HttpEnrollee[];
  @Output() public claim: EventEmitter<number>;
  @Output() public disclaim: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  @ContentChild(AdjudicatorActionsComponent, { static: false }) public adjudicatorActionsComponent: AdjudicatorActionsComponent;

  public statuses: Config<number>[];
  public columns: string[];
  public dataSource: MatTableDataSource<HttpEnrollee>;

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private configService: ConfigService,
    private authService: AuthService
  ) {
    this.claim = new EventEmitter<number>();
    this.disclaim = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.statuses = this.configService.statuses;
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

  public ngOnChanges(changes: SimpleChanges) {
    const enrollees = changes.enrollees.currentValue;

    if (enrollees) {
      this.dataSource.data = (Array.isArray(enrollees)) ? enrollees : [enrollees];
    }
  }
}
