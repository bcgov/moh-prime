import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { Sort } from '@angular/material/sort';

import { Subscription } from 'rxjs';

import moment from 'moment';

import { UtilsService } from '@core/services/utils.service';
import { EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Role } from '@auth/shared/enum/role.enum';

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
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public reload: EventEmitter<number>;

  public busy: Subscription;
  public form: FormGroup;
  public columns: string[];
  public hasAppliedDateRange = false;
  public hasRenewalDateRange = false;
  public AdjudicationRoutes = AdjudicationRoutes;
  public EnrolmentStatus = EnrolmentStatus;
  public Role = Role;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private utilsService: UtilsService,
  ) {
    this.notify = new EventEmitter<number>();
    this.assign = new EventEmitter<number>();
    this.reassign = new EventEmitter<number>();
    this.reload = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.columns = [
      'prefixes',
      'displayId',
      'name',
      'givenNames',
      'appliedDate',
      'status',
      'remoteAccess',
      'renewalDate',
      'currentTOA',
      'assignedTo',
      'careSetting',
      'actions'
    ];
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

  public onAssign(enrolleeId: number): void {
    this.assign.emit(enrolleeId);
  }

  public onReassign(enrolleeId: number): void {
    this.reassign.emit(enrolleeId);
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.route.emit(routePath);
  }

  public onReload(enrolleeId: number): void {
    this.reload.emit(enrolleeId);
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
    this.form.get('appliedDateRangeStart').reset();
    this.form.get('appliedDateRangeEnd').reset();
    this.hasAppliedDateRange = false;
  }

  public clearRenewalDateRange() {
    this.form.get('renewalDateRangeStart').reset();
    this.form.get('renewalDateRangeEnd').reset();
    this.hasRenewalDateRange = false;
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      appliedDateRangeStart: '',
      appliedDateRangeEnd: '',
      renewalDateRangeStart: '',
      renewalDateRangeEnd: '',
    });
  }

  private initForm() {
    this.dataSource.filterPredicate = this.getFilterPredicate();

    this.form.valueChanges.subscribe(value => {
      const filter = { ...value, name: value.name } as string;
      this.dataSource.filter = filter;
    });

    for (const name of ['appliedDateRangeStart', 'appliedDateRangeEnd']) {
      this.form.get(name).valueChanges.subscribe(value => {
        this.hasAppliedDateRange = value || this.hasAppliedDateRange;
      });
    }

    for (const name of ['renewalDateRangeStart', 'renewalDateRangeEnd']) {
      this.form.get(name).valueChanges.subscribe(value => {
        this.hasRenewalDateRange = value || this.hasRenewalDateRange;
      });
    }
  }

  private getFilterPredicate() {
    return (row: EnrolleeListViewModel, filter) => {
      const appliedDate = moment.utc(row.appliedDate);
      const renewalDate = moment.utc(row.expiryDate);
      // Add 1 day to range end date for inclusive check
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
