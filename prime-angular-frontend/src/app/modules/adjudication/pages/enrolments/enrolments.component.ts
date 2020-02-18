import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatSelectChange, MatDialog } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY, Subscription } from 'rxjs';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import {
  EnrolmentStatusReasonsComponent
} from '@shared/components/dialogs/content/enrolment-status-reasons/enrolment-status-reasons.component';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { ApproveEnrolmentComponent } from '@shared/components/dialogs/content/approve-enrolment/approve-enrolment.component';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-enrolments',
  templateUrl: './enrolments.component.html',
  styleUrls: ['./enrolments.component.scss']
})
export class EnrolmentsComponent implements OnInit {
  public busy: Subscription;
  public columns: string[];
  public statuses: Config<number>[];
  public filteredStatus: Config<number>;
  public dataSource: MatTableDataSource<Enrolment>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private configService: ConfigService,
    private adjudicationResource: AdjudicationResource,
    private authService: AuthService,
    private toastService: ToastService,
    private dialog: MatDialog,
    private logger: LoggerService
  ) {
    this.columns = ['appliedDate', 'name', 'status', 'approvedDate', 'actions'];
    this.statuses = this.configService.statuses;
    this.filteredStatus = null;
  }

  public filterByStatus(selection: MatSelectChange) {
    const statusCode = selection.value;
    this.filteredStatus = this.statuses.find(s => s.code === statusCode);
    this.getEnrolments(statusCode);
  }

  public canApproveOrDeny(currentStatusCode: EnrolmentStatus) {
    // Admins can only approve or deny an enrollee in a UNDER_REVIEW state
    return (currentStatusCode === EnrolmentStatus.UNDER_REVIEW);
  }

  public canAllowEditing(currentStatusCode: EnrolmentStatus) {
    // Admins can only allow re-enable editing for an enrollee in a UNDER_REVIEW state
    return (currentStatusCode === EnrolmentStatus.UNDER_REVIEW);
  }

  public isSuperAdmin() {
    return this.authService.isSuperAdmin();
  }

  public isActive(currentStatusCode: EnrolmentStatus) {
    return (currentStatusCode === EnrolmentStatus.ACTIVE);
  }

  public isUnderReview(currentStatusCode: EnrolmentStatus) {
    return (currentStatusCode === EnrolmentStatus.UNDER_REVIEW);
  }

  public viewEnrolmentHistory(enrolmentId: number) {
    this.router.navigate([enrolmentId, AdjudicationRoutes.PROFILE_HISTORY], { relativeTo: this.route.parent });
  }

  public reviewStatusReasons(enrolment: Enrolment) {
    const data: DialogOptions = {
      title: 'Review Status Reasons',
      icon: 'flag',
      actionText: 'Close',
      data: { enrolment },
      component: EnrolmentStatusReasonsComponent,
      cancelHide: true
    };
    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe();
  }

  public approveEnrolment(enrolment: Enrolment) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: 'Are you sure you want to approve this enrolment?',
      actionText: 'Approve Enrolment',
      data: { enrolment },
      component: ApproveEnrolmentComponent
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: boolean }) => {
          if (result) {
            if (result.hasOwnProperty('output') && result.output !== undefined) {
              enrolment.alwaysManual = result.output;
              return this.adjudicationResource.updateEnrolleeAlwaysManual(enrolment.id, result.output);
            }
            return this.adjudicationResource.enrollee(enrolment.id);
          }
          return EMPTY;
        }),
        exhaustMap(() =>
          this.adjudicationResource
            .updateEnrolmentStatus(enrolment.id, EnrolmentStatus.REQUIRES_TOA)
        ),
        exhaustMap(() => this.adjudicationResource.enrollee(enrolment.id))
      )
      .subscribe(
        (approvedEnrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been approved');
          this.updateEnrolment(approvedEnrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be approved');
          this.logger.error('[Adjudication] Enrolments::approveEnrolment error has occurred: ', error);
        }
      );
  }

  public declineEnrolment(id: number) {
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
            ? this.adjudicationResource.updateEnrolmentStatus(id, EnrolmentStatus.LOCKED)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.enrollee(id)),
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been locked');
          this.updateEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be locked');
          this.logger.error('[Adjudication] Enrolments::declineEnrolment error has occurred: ', error);
        }
      );
  }

  public markForEditing(id: number) {
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
            ? this.adjudicationResource.updateEnrolmentStatus(id, EnrolmentStatus.ACTIVE)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.enrollee(id))
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment status was reverted to Active');
          this.updateEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment status could not be reverted to Active');
          this.logger.error('[Adjudication] Enrolments::markAsActive error has occurred: ', error);
        }
      );
  }

  public deleteEnrolment(id: number) {

    const data: DialogOptions = {
      title: 'Delete Enrolment',
      message: 'Are you sure you want to delete this enrolment?',
      actionType: 'warn',
      actionText: 'Delete Enrolment'
    };

    if (this.authService.isSuperAdmin()) {
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.adjudicationResource.deleteEnrolment(id)
              : EMPTY
          )
        )
        .subscribe(
          (enrolment: Enrolment) => {
            this.toastService.openSuccessToast('Enrolment has been deleted');
            this.removeEnrolment(enrolment);
          },
          (error: any) => {
            this.toastService.openErrorToast('Enrolment could not be deleted');
            this.logger.error('[Adjudication] Enrolments::deleteEnrolments error has occurred: ', error);
          }
        );
    }
  }

  public getEnrolments(statusCode?: number) {
    this.busy = this.adjudicationResource.enrollees(statusCode)
      .subscribe(
        (enrolments: Enrolment[]) => {
          this.logger.info('ENROLMENTS', enrolments);
          this.dataSource = new MatTableDataSource<Enrolment>(enrolments);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolments could not be retrieved');
          this.logger.error('[Adjudication] Enrolments::getEnrolments error has occurred: ', error);
        }
      );
  }

  public ngOnInit() {
    this.getEnrolments();
    // console.log(this.authService.isSuperAdmin());
  }

  private updateEnrolment(enrolment: Enrolment) {
    this.dataSource.data = this.dataSource.data
      .map((currentEnrolment: Enrolment) =>
        (currentEnrolment.id === enrolment.id) ? enrolment : currentEnrolment
      );
  }

  private removeEnrolment(enrolment: Enrolment) {
    this.dataSource.data = this.dataSource.data
      .filter((currentEnrolment: Enrolment) => currentEnrolment.id !== enrolment.id);
  }
}
