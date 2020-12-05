import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AbstractControl, FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { Sort } from '@angular/material/sort';
import moment from 'moment';

import { UtilsService } from '@core/services/utils.service';

import { EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-enrollee-table',
  templateUrl: './enrollee-table.component.html',
  styleUrls: ['./enrollee-table.component.scss']
})
export class EnrolleeTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<EnrolleeListViewModel>;
  @Output() public notify: EventEmitter<number>;
  @Output() public claim: EventEmitter<number>;
  @Output() public disclaim: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public columns: string[];

  public AdjudicationRoutes = AdjudicationRoutes;

  public hasAppliedDateRange = false;
  public hasRenewalDateRange = false;

  readonly filterFormControl: AbstractControl;

  constructor(
    private authService: AuthService,
    private utilsService: UtilsService,
    fb: FormBuilder
  ) {
    this.notify = new EventEmitter<number>();
    this.claim = new EventEmitter<number>();
    this.disclaim = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.columns = [
      'displayId',
      'name',
      'givenNames',
      'appliedDate',
      'status',
      'remoteAccess',
      'renewalDate',
      'currentTOA',
      'adjudicator',
      'actions'
    ];

    this.filterFormControl = fb.group({
      appliedDateRangeStart: '',
      appliedDateRangeEnd: '',
      renewalDateRangeStart: '',
      renewalDateRangeEnd: '',
    });
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public canReviewStatusReasons(enrollee: EnrolleeListViewModel): boolean {
    return (
      enrollee.currentStatusCode === EnrolmentStatus.UNDER_REVIEW ||
      enrollee.previousStatus?.statusCode === EnrolmentStatus.UNDER_REVIEW
    );
  }

  public onNotify(enrolleeId: number): void {
    this.notify.emit(enrolleeId);
  }

  public onClaim(enrolleeId: number): void {
    this.claim.emit(enrolleeId);
  }

  public onDisclaim(enrolleeId: number): void {
    this.disclaim.emit(enrolleeId);
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.route.emit(routePath);
  }

  public sortData(sort: Sort) {
    if (!sort.active || !sort.direction) {
      return;
    }

    this.dataSource.data = [...this.dataSource.data].sort((a, b) => {
      switch (sort.active) {
        case 'displayId': return this.utilsService.sortByDirection(a.id, b.id, sort.direction);
        case 'appliedDate': return this.utilsService.sortByDirection(a.appliedDate, b.appliedDate, sort.direction);
        case 'renewalDate': return this.utilsService.sortByDirection(a.expiryDate, b.expiryDate, sort.direction);
        default: return 0;
      }
    });
  }

  public clearAppliedDateRange() {
    this.filterFormControl.get('appliedDateRangeStart').reset();
    this.filterFormControl.get('appliedDateRangeEnd').reset();
    this.hasAppliedDateRange = false;
  }

  public clearRenewalDateRange() {
    this.filterFormControl.get('renewalDateRangeStart').reset();
    this.filterFormControl.get('renewalDateRangeEnd').reset();
    this.hasRenewalDateRange = false;
  }

  public ngOnInit(): void {
    this.dataSource.filterPredicate = this.getFilterPredicate();

    this.filterFormControl.valueChanges.subscribe(value => {
      const filter = { ...value, name: value.name } as string;
      this.dataSource.filter = filter;
    });

    for (const name of ['appliedDateRangeStart', 'appliedDateRangeEnd']) {
      this.filterFormControl.get(name).valueChanges.subscribe(value => {
        this.hasAppliedDateRange = value || this.hasAppliedDateRange;
      });
    }

    for (const name of ['renewalDateRangeStart', 'renewalDateRangeEnd']) {
      this.filterFormControl.get(name).valueChanges.subscribe(value => {
        this.hasRenewalDateRange = value || this.hasRenewalDateRange;
      });
    }
  }

  private getFilterPredicate() {
    return (row: EnrolleeListViewModel, filter) => {
      const appliedDate = moment.utc(row.appliedDate);
      const renewalDate = moment.utc(row.expiryDate);
      // add 1 day to range end date for inclusive check
      const searchByAppliedDate =
        (!filter.appliedDateRangeStart || moment(filter.appliedDateRangeStart) <= appliedDate)
        && (!filter.appliedDateRangeEnd || appliedDate <= moment(filter.appliedDateRangeEnd).add(1, 'd'));
      const searchByRenewalDate =
        (!filter.renewalDateRangeStart || moment(filter.renewalDateRangeStart) <= renewalDate)
        && (!filter.renewalDateRangeEnd || renewalDate <= moment(filter.renewalDateRangeEnd).add(1, 'd'));
      const matchFilter = [];
      matchFilter.push(searchByAppliedDate);
      matchFilter.push(searchByRenewalDate);
      return matchFilter.every(Boolean);
    };
  }
}
