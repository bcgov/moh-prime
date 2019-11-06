import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatSelectChange, MatDialog } from '@angular/material';

import { exhaustMap, catchError, retry } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';

import { ProvisionResource } from '@provision/shared/services/provision-resource.service';

@Component({
  selector: 'app-enrolments',
  templateUrl: './enrolments.component.html',
  styleUrls: ['./enrolments.component.scss']
})
export class EnrolmentsComponent implements OnInit {
  public columns: string[];
  public statuses: Config<number>[];
  public filteredStatus: Config<number>;
  public dataSource: MatTableDataSource<Enrolment>;

  constructor(
    private configService: ConfigService,
    private provisionResource: ProvisionResource,
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

  public canApproveOrDeny(currentStatusCode: number) {
    return (currentStatusCode === EnrolmentStatus.SUBMITTED);
  }

  public reviewStatusReasons(enrolment: Enrolment) {
    const message = enrolment.currentStatus.enrolmentStatusReasons
      .reduce((result: string, enrolmentStatusReason: EnrolmentStatusReason) => {
        if (result) {
          return `${result}, ${enrolmentStatusReason.statusReason.name}`;
        } else {
          return `${enrolmentStatusReason.statusReason.name}`;
        }
      }, '');

    const data: DialogOptions = {
      title: 'Review Enrolment Status Reasons',
      icon: 'flag',
      message,
      actionText: 'Close',
      data: { enrolment },
      cancelHide: true
    };
    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe();
  }

  public approveEnrolment(id: number) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: 'Are you sure you want to approve this enrolment?',
      actionText: 'Approve Enrolment'
    };
    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.provisionResource.updateEnrolmentStatus(id, EnrolmentStatus.ADJUDICATED_APPROVED)
            : EMPTY
        ),
        // TODO: show success/error for enrolment status, and attempt replay getting enrolment for update
        // map(() => { })
        // catchError(() => { })
        exhaustMap(() => this.provisionResource.enrolment(id))
        // retry(3),
        // catchError(() => { })
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been approved');
          this.updateEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be approved');
          this.logger.error('[Provision] Enrolments::approveEnrolment error has occurred: ', error);
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
    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.provisionResource.updateEnrolmentStatus(id, EnrolmentStatus.DECLINED)
            : EMPTY
        ),
        // TODO: show success/error for enrolment status, and attempt replay getting enrolment for update
        // map(() => { })
        // catchError(() => { })
        exhaustMap(() => this.provisionResource.enrolment(id)),
        // retry(3),
        // catchError(() => { })
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been declined');
          this.updateEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be declined');
          this.logger.error('[Provision] Enrolments::declineEnrolment error has occurred: ', error);
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
    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.provisionResource.deleteEnrolment(id)
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
          this.logger.error('[Provision] Enrolments::deleteEnrolments error has occurred: ', error);
        }
      );
  }

  public ngOnInit() {
    this.getEnrolments();
  }

  private getEnrolments(statusCode?: number) {
    this.provisionResource.enrolments(statusCode)
      .subscribe(
        (enrolments: Enrolment[]) => {
          this.logger.info('ENROLMENTS', enrolments);
          this.dataSource = new MatTableDataSource<Enrolment>(enrolments);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolments could not be retrieved');
          this.logger.error('[Provision] Enrolments::getEnrolments error has occurred: ', error);
        }
      );
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
