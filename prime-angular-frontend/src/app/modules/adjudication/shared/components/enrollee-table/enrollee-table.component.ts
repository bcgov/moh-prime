import { Component, OnInit, Input, Output, EventEmitter, OnChanges, ViewChild, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { Sort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

import { BehaviorSubject, combineLatest, Subscription } from 'rxjs';

import moment from 'moment';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { DateUtils } from '@lib/utils/date-utils.class';
import { UtilsService } from '@core/services/utils.service';
import { EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { EnrolleeNavigation } from '@shared/models/enrollee-navigation-model';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { Admin } from '@auth/shared/models/admin.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

@UntilDestroy()
@Component({
  selector: 'app-enrollee-table',
  templateUrl: './enrollee-table.component.html',
  styleUrls: ['./enrollee-table.component.scss']
})
export class EnrolleeTableComponent implements OnInit, OnChanges {
  @Input() public enrollees: EnrolleeListViewModel[];
  @Input() public enrolleeNavigation: EnrolleeNavigation;
  @Output() public notify: EventEmitter<number>;
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public refresh: EventEmitter<number>;
  @Output() public sendBulkEmail: EventEmitter<void>;
  @Output() public maintenance: EventEmitter<void>;
  @Output() public navigateEnrollee: EventEmitter<number>;

  @ViewChild(MatPaginator, { static: true }) public paginator: MatPaginator;

  public busy: Subscription;
  public dataSource: MatTableDataSource<EnrolleeListViewModel>;
  public hidePaginator: boolean;
  public form: FormGroup;
  public columns: string[];
  public hasAppliedDateRange: boolean;
  public hasRenewalDateRange: boolean;
  public hasAssignedToFilter$: BehaviorSubject<boolean>;
  public AdjudicationRoutes = AdjudicationRoutes;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  public EnrolmentStatus = EnrolmentStatusEnum;
  public Role = Role;
  public readonly paperEnrolleeGpidFilter = 'NOBCSC';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private utilsService: UtilsService,
  ) {
    this.notify = new EventEmitter<number>();
    this.assign = new EventEmitter<number>();
    this.reassign = new EventEmitter<number>();
    this.refresh = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.sendBulkEmail = new EventEmitter<void>();
    this.maintenance = new EventEmitter<void>();
    this.navigateEnrollee = new EventEmitter<number>();
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
    this.dataSource = new MatTableDataSource<EnrolleeListViewModel>([]);
    this.hasAssignedToFilter$ = new BehaviorSubject<boolean>(false);
  }

  public canReviewStatusReasons(enrollee: EnrolleeListViewModel): boolean {
    return (
      enrollee.currentStatusCode === EnrolmentStatusEnum.UNDER_REVIEW ||
      enrollee.previousStatus?.statusCode === EnrolmentStatusEnum.UNDER_REVIEW
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

  public onReload(): void {
    this.refresh.emit();
  }

  public onSendBulkEmail(): void {
    this.sendBulkEmail.emit();
  }

  public sortData(sort: Sort) {
    if (!sort.active || !sort.direction) {
      return;
    }

    this.dataSource.data = [...this.dataSource.data].sort((a, b) => {
      switch (sort.active) {
        case 'displayId':
          return this.utilsService.sortByDirection(a.id, b.id, sort.direction);
        case 'appliedDate':
          return this.utilsService.sortByDirection(a.appliedDate, b.appliedDate, sort.direction);
        case 'renewalDate':
          return this.utilsService.sortByDirection(a.expiryDate, b.expiryDate, sort.direction);
        default:
          return 0;
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

  public toggleFilterAssigned() {
    this.hasAssignedToFilter$.next(!this.hasAssignedToFilter$.value);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (!changes.enrollees.firstChange) {
      this.dataSource.data = changes.enrollees.currentValue;
    }
  }

  public navigateToEnrollee(enrolleeId: number): void {
    this.navigateEnrollee.emit(enrolleeId);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    this.dataSource.filterPredicate = this.getFilterPredicate();
    this.dataSource.paginator = this.paginator;
    // Paginator must exist within the DOM, but does not
    // have to be visible based on the size of the dataset
    this.hidePaginator = (this.paginator?.pageSize ?? 0) > this.enrollees.length;
    this.dataSource.data = this.enrollees;
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      appliedDateRangeStart: ['', []],
      appliedDateRangeEnd: ['', []],
      renewalDateRangeStart: ['', []],
      renewalDateRangeEnd: ['', []],
      assignedTo: ['', []]
    });
  }

  private initForm(): void {
    this.form.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(value => this.dataSource.filter = value);

    combineLatest([
      this.authService.getAdmin$(),
      this.hasAssignedToFilter$
    ])
      .pipe(untilDestroyed(this))
      .subscribe(([{ idir }, value]: [Admin, boolean]) =>
        this.form.get('assignedTo').patchValue((value) ? idir : '')
      );

    ['appliedDateRangeStart', 'appliedDateRangeEnd'].forEach(controlName =>
      this.form.get(controlName).valueChanges
        .pipe(untilDestroyed(this))
        .subscribe(value => this.hasAppliedDateRange = value ?? this.hasAppliedDateRange)
    );

    ['renewalDateRangeStart', 'renewalDateRangeEnd'].forEach(controlName =>
      this.form.get(controlName).valueChanges
        .pipe(untilDestroyed(this))
        .subscribe(value => this.hasRenewalDateRange = value ?? this.hasRenewalDateRange)
    );
  }

  private getFilterPredicate() {
    return (row: EnrolleeListViewModel, filter: any) => {
      const matchFilter = [];

      if (this.hasAppliedDateRange && filter.appliedDateRangeStart && filter.appliedDateRangeEnd) {
        const date = moment(row.appliedDate).local();
        matchFilter.push(DateUtils.isWithinDateRange(date, filter.appliedDateRangeStart, filter.appliedDateRangeEnd));
      }

      if (this.hasRenewalDateRange && filter.renewalDateRangeStart && filter.renewalDateRangeEnd) {
        const date = moment(row.expiryDate).local();
        matchFilter.push(DateUtils.isWithinDateRange(date, filter.renewalDateRangeStart, filter.renewalDateRangeEnd));
      }

      if (this.hasAssignedToFilter$.value) {
        matchFilter.push(row.adjudicatorIdir === filter.assignedTo);
      }

      return matchFilter.every(Boolean);
    };
  }
}
