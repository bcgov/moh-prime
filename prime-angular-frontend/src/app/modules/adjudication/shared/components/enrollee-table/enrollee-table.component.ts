import { Component, OnInit, Input, Output, EventEmitter, OnChanges, ViewChild, SimpleChanges } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { Sort } from '@angular/material/sort';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';

import { BehaviorSubject, combineLatest, Subscription } from 'rxjs';

import moment from 'moment';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { PAPER_ENROLLEE_GPID_PREFIX } from '@lib/constants';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { LocalStorageService } from '@core/services/local-storage.service';
import { Pagination } from '@core/models/pagination.model';
import { EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { EnrolleeNavigation } from '@shared/models/enrollee-navigation-model';
import { EnrolmentStatusEnum, PaperEnrolmentStatusMap } from '@shared/enums/enrolment-status.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { Admin } from '@auth/shared/models/admin.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

class ImprovedPageEvent extends PageEvent {
  public stopPropogation: boolean;
}

@UntilDestroy()
@Component({
  selector: 'app-enrollee-table',
  templateUrl: './enrollee-table.component.html',
  styleUrls: ['./enrollee-table.component.scss']
})
export class EnrolleeTableComponent implements OnInit, OnChanges {
  @Input() public enrollees: EnrolleeListViewModel[];
  @Input() public enrolleeNavigation: EnrolleeNavigation;
  @Input() public pagination: Pagination;
  @Input() public localStoragePrefix: string;
  @Output() public notify: EventEmitter<EnrolleeListViewModel>;
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public refresh: EventEmitter<number>;
  @Output() public sendBulkEmail: EventEmitter<void>;
  @Output() public maintenance: EventEmitter<void>;
  @Output() public navigateEnrollee: EventEmitter<number>;

  @ViewChild(MatPaginator, { static: true }) public paginator: MatPaginator;
  @ViewChild('secondaryPaginator', { static: true }) public secondaryPaginator: MatPaginator;

  public busy: Subscription;
  public dataSource: MatTableDataSource<EnrolleeListViewModel>;
  public form: UntypedFormGroup;
  public columns: string[];
  public hasAppliedDateRange: boolean;
  public hasRenewalDateRange: boolean;
  public hasAssignedToFilter$: BehaviorSubject<boolean>;
  public AdjudicationRoutes = AdjudicationRoutes;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  public EnrolmentStatus = EnrolmentStatusEnum;
  public Role = Role;
  public paperEnrolmentStatusMap = PaperEnrolmentStatusMap;
  public readonly PAPER_ENROLLEE_GPID_PREFIX = PAPER_ENROLLEE_GPID_PREFIX;

  private sortActiveKey: string;
  private sortDirectionKey: string;

  protected routeUtils: RouteUtils;

  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: UntypedFormBuilder,
    private authService: AuthService,
    private localStorageService: LocalStorageService,
    router: Router
  ) {
    this.notify = new EventEmitter<EnrolleeListViewModel>();
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
    this.routeUtils = new RouteUtils(activatedRoute, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
  }

  public canReviewStatusReasons(enrollee: EnrolleeListViewModel): boolean {
    return (
      enrollee.currentStatusCode === EnrolmentStatusEnum.UNDER_REVIEW ||
      enrollee.previousStatus?.statusCode === EnrolmentStatusEnum.UNDER_REVIEW
    );
  }

  public onNotify(enrollee: EnrolleeListViewModel): void {
    this.notify.emit(enrollee);
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

  public updateDateSortParams(sort: Sort) {
    // Do not use sorting queryParams for single row mode
    if (this.activatedRoute.snapshot.params.id) {
      return
    }

    if (!sort.active || !sort.direction) {
      this.localStorageService.removeItem(this.sortActiveKey);
      this.localStorageService.removeItem(this.sortDirectionKey);
      this.routeUtils.updateQueryParams({ sortActive: null, sortDirection: null });
    } else {
      this.localStorageService.set(this.sortActiveKey, sort.active);
      this.localStorageService.set(this.sortDirectionKey, sort.direction);
      this.routeUtils.updateQueryParams({ sortActive: sort.active, sortDirection: sort.direction });
    }
  }

  public clearAppliedDateRange() {
    this.form.get('appliedDateRangeStart').reset();
    this.form.get('appliedDateRangeEnd').reset();
    this.hasAppliedDateRange = false;

    this.routeUtils.updateQueryParams({ appliedDateRangeStart: null, appliedDateRangeEnd: null });
  }

  public clearRenewalDateRange() {
    this.form.get('renewalDateRangeStart').reset();
    this.form.get('renewalDateRangeEnd').reset();
    this.hasRenewalDateRange = false;

    this.routeUtils.updateQueryParams({ renewalDateRangeStart: null, renewalDateRangeEnd: null });
  }

  public toggleFilterAssigned() {
    this.hasAssignedToFilter$.next(!this.hasAssignedToFilter$.value);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (!changes.enrollees.firstChange) {
      this.dataSource.data = changes.enrollees.currentValue;
    }

    if (changes.sort?.firstChange === false) {
      this.updateDateSortParams(changes.sort.currentValue);
    }
  }

  public navigateToEnrollee(enrolleeId: number): void {
    this.navigateEnrollee.emit(enrolleeId);
  }

  public onPage(event: ImprovedPageEvent, top: boolean): void {
    const other = (!top) ? this.paginator : this.secondaryPaginator;
    if (event.stopPropogation) {
      return;
    }
    event.stopPropogation = true;
    this.routeUtils.updateQueryParams({ page: `${event.pageIndex + 1}` });
    other.page.emit(event);
  }

  public ngOnInit(): void {
    this.sortActiveKey = `${this.localStoragePrefix}-sort-active-key`;
    this.sortDirectionKey = `${this.localStoragePrefix}-sort-direction-key`;

    const {
      renewalDateRangeStart,
      renewalDateRangeEnd,
      appliedDateRangeStart,
      appliedDateRangeEnd,
      sortActive,
      sortDirection,
      assignedTo
    } = this.activatedRoute.snapshot.queryParams;

    this.hasAssignedToFilter$.next(!!assignedTo);

    this.createFormInstance();
    this.initForm();

    this.dataSource.data = this.enrollees;

    if (!sortActive || !sortDirection) {
      this.updateDateSortParams(<Sort>{
        active: this.localStorageService.get(this.sortActiveKey),
        direction: this.localStorageService.get(this.sortDirectionKey)
      });
    }

    this.hasRenewalDateRange = (renewalDateRangeStart && renewalDateRangeEnd);
    this.hasAppliedDateRange = (appliedDateRangeStart && appliedDateRangeEnd);
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      appliedDateRangeStart: ['', []],
      appliedDateRangeEnd: ['', []],
      renewalDateRangeStart: ['', []],
      renewalDateRangeEnd: ['', []],
    });
  }
  private initForm(): void {
    combineLatest([
      this.authService.getAdmin$(),
      this.hasAssignedToFilter$
    ])
      .pipe(untilDestroyed(this))
      .subscribe(([{ idir }, value]: [Admin, boolean]) =>
        this.routeUtils.updateQueryParams({ assignedTo: (value) ? idir : null })
      );

    this.form.get('appliedDateRangeEnd').valueChanges.pipe(untilDestroyed(this))
      .subscribe((end: moment.Moment) => {
        const start = this.form.get('appliedDateRangeStart').value as moment.Moment;

        if (!end || !start) {
          return;
        }
        this.routeUtils.updateQueryParams({ appliedDateRangeStart: start.toISOString(), appliedDateRangeEnd: end.toISOString() });
        this.hasAppliedDateRange = true;
      });

    this.form.get('renewalDateRangeEnd').valueChanges.pipe(untilDestroyed(this))
      .subscribe((end: moment.Moment) => {
        const start = this.form.get('renewalDateRangeStart').value as moment.Moment;

        if (!end || !start) {
          return;
        }
        this.routeUtils.updateQueryParams({ renewalDateRangeStart: start.toISOString(), renewalDateRangeEnd: end.toISOString() });
        this.hasRenewalDateRange = true;
      });
  }
}
