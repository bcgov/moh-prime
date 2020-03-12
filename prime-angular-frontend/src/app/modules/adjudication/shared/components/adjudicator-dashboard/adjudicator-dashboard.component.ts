import { Component, OnInit, Input, AfterContentInit, ContentChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatTableDataSource, MatDialog } from '@angular/material';

import { Observable, Subscription, EMPTY } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ApproveEnrolmentComponent } from '@shared/components/dialogs/content/approve-enrolment/approve-enrolment.component';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicatorActionsComponent } from '../adjudicator-actions/adjudicator-actions.component';

@Component({
  selector: 'app-adjudicator-dashboard',
  templateUrl: './adjudicator-dashboard.component.html',
  styleUrls: ['./adjudicator-dashboard.component.scss']
})
export class AdjudicatorDashboardComponent extends AbstractComponent implements OnInit, AfterContentInit {
  @ContentChild(AdjudicatorActionsComponent, { static: false }) public adjudicatorActionsComponent: AdjudicatorActionsComponent;

  public busy: Subscription;
  public enrolleeId: number | null;
  public columns: string[];
  public dataSource: MatTableDataSource<HttpEnrollee>;
  // TODO temporary to allow builds then use data source
  public enrollee: HttpEnrollee;

  public statuses: Config<number>[];
  public filteredStatus: Config<number>;
  public textSearch: string;

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private configService: ConfigService,
    private adjudicationResource: AdjudicationResource,
    private authService: AuthService,
    private toastService: ToastService,
    private dialog: MatDialog,
    private logger: LoggerService
  ) {
    super(route, router);

    this.enrolleeId = this.route.snapshot.params.id || null;
    this.statuses = this.configService.statuses;
    this.columns = ['uniqueId', 'name', 'appliedDate', 'status', 'approvedDate', 'adjudicator', 'actions'];
    this.filteredStatus = null;
    this.textSearch = null;
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public get canDelete(): boolean {
    return this.authService.isSuperAdmin();
  }

  // TODO extend authorization service using BehaviourSubject
  public canApproveOrDeny(currentStatusCode: EnrolmentStatus) {
    // Admins can only approve or deny an enrollee in a UNDER_REVIEW state
    return this.isUnderReview(currentStatusCode);
  }

  // TODO extend authorization service using BehaviourSubject
  public canAllowEditing(currentStatusCode: EnrolmentStatus) {
    // Admins can only allow re-enable editing for an enrollee in a UNDER_REVIEW state
    return (currentStatusCode === EnrolmentStatus.UNDER_REVIEW);
  }

  public isActive(currentStatusCode: EnrolmentStatus): boolean {
    return (currentStatusCode === EnrolmentStatus.ACTIVE);
  }

  public isUnderReview(currentStatusCode: EnrolmentStatus): boolean {
    return (currentStatusCode === EnrolmentStatus.UNDER_REVIEW);
  }

  public approveEnrollee(enrollee: HttpEnrollee) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: 'Are you sure you want to approve this enrolment?',
      actionText: 'Approve Enrolment',
      data: { enrolment: enrollee },
      component: ApproveEnrolmentComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      // TODO refactor this a bit to clean the logic
      .pipe(
        exhaustMap((result: { output: boolean }) => {
          if (result) {
            if (result.hasOwnProperty('output') && result.output !== undefined) {
              enrollee.alwaysManual = result.output;
              return this.adjudicationResource.updateEnrolleeAlwaysManual(enrollee.id, result.output);
            }
            // TODO seems unnecessary as result isn't used could be of(null)
            return this.adjudicationResource.getEnrolleeById(enrollee.id);
          }
          return EMPTY;
        }),
        exhaustMap(() =>
          this.adjudicationResource.createEnrolmentStatus(enrollee.id, EnrolmentStatus.REQUIRES_TOA)
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrollee.id))
      )
      .subscribe((approvedEnrollee: HttpEnrollee) => {
        this.toastService.openSuccessToast('Enrolment has been approved');
        this.updateEnrolment(approvedEnrollee);
      });
  }

  public declineEnrollee(id: number) {
    const data: DialogOptions = {
      title: 'Decline Enrolment',
      message: 'Are you sure you want to decline this enrolment?',
      actionType: 'warn',
      actionText: 'Decline Enrolment'
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.createEnrolmentStatus(id, EnrolmentStatus.LOCKED)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(id)),
      )
      .subscribe((enrollee: HttpEnrollee) => {
        this.toastService.openSuccessToast('Enrolment has been locked');
        this.updateEnrolment(enrollee);
      });
  }

  public unlockEnrollee(id: number) {
    const data: DialogOptions = {
      title: 'Enable Editing',
      message: 'When enabled the enrollee will be able to update their enrolment. Are you sure you want to enable editing?',
      actionType: 'warn',
      actionText: 'Enable Editing'
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.createEnrolmentStatus(id, EnrolmentStatus.ACTIVE)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(id))
      )
      .subscribe((enrollee: HttpEnrollee) => this.updateEnrolment(enrollee));
  }

  public deleteEnrollee(id: number) {
    const data: DialogOptions = {
      title: 'Delete Enrolment',
      message: 'Are you sure you want to delete this enrolment?',
      actionType: 'warn',
      actionText: 'Delete Enrolment'
    };

    if (this.canDelete) {
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.adjudicationResource.deleteEnrollee(id)
              : EMPTY
          )
        )
        .subscribe((enrollee: HttpEnrollee) => this.removeEnrolment(enrollee));
    }
  }

  public updateEnrolleeAdjudicator(currentEnrollee: HttpEnrollee) {
    const request$ = (!currentEnrollee.adjudicatorId)
      ? this.adjudicationResource.setEnrolleeAdjudicator(currentEnrollee.id)
      : this.adjudicationResource.removeEnrolleeAdjudicator(currentEnrollee.id);

    request$
      .subscribe((updatedEnrollee: HttpEnrollee) => this.updateEnrolment(updatedEnrollee));
  }

  // TODO hook up search using observable off URL
  // public onSearch(search: string): void {
  //   const statusCode = (this.filteredStatus) ? this.filteredStatus.code : null;
  //   this.textSearch = search;
  //   this.getEnrolments(statusCode, search);
  // }

  // TODO hook up filter using observable off URL
  // public onFilter(status: EnrolmentStatus): void {
  //   this.filteredStatus = this.statuses.find(s => s.code === status);
  //   this.getEnrolments(status, this.textSearch);
  // }

  // TODO hook up refresh using input binding
  // public onRefresh(): void {
  //   const statusCodes = (this.filteredStatus) ? this.filteredStatus.code : null;
  //   return this.getEnrolments(statusCodes, this.textSearch);
  // }

  public routeTo(routePath: string | (string | number)[]): void {
    const basePath = [AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.ENROLLEES];
    routePath = (Array.isArray(routePath)) ? routePath : [routePath];
    super.routeTo([...basePath, ...routePath], { relativeTo: null });
  }

  public ngOnInit() {
    this.getEnrollees()
      .subscribe((enrollees: HttpEnrollee[]) =>
        this.dataSource = new MatTableDataSource<HttpEnrollee>(enrollees)
      );
  }

  public ngAfterContentInit() {

  }

  // TODO set up request observable, and merge/pipe in search and filter results
  private getEnrollees(): Observable<HttpEnrollee[]> {
    const results$ = (this.enrolleeId)
      ? this.adjudicationResource.getEnrolleeById(this.enrolleeId)
        .pipe(
          map((enrollee: HttpEnrollee) => [this.enrollee = enrollee])
        )
      // TODO add search, filter, and pagination params
      : this.adjudicationResource.getEnrollees();

    return results$;
  }

  // TODO split out into service for managing data tables, and update with add row
  private updateEnrolment(updatedEnrollee: HttpEnrollee) {
    this.dataSource.data = this.dataSource.data
      .map((currentEnrollee: HttpEnrollee) =>
        (currentEnrollee.id === updatedEnrollee.id) ? updatedEnrollee : currentEnrollee
      );
  }

  // TODO split out into service for managing data tables, and update with add row
  private removeEnrolment(removedEnrollee: HttpEnrollee) {
    this.dataSource.data = this.dataSource.data
      .filter((currentEnrollee: HttpEnrollee) => (currentEnrollee.id !== removedEnrollee.id));
  }
}
