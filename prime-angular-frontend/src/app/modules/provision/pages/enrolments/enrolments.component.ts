import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatSelectChange, MatDialog } from '@angular/material';

import { exhaustMap, map } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { ProvisionResource } from '@provision/shared/services/provision-resource.service';

@Component({
  selector: 'app-enrolments',
  templateUrl: './enrolments.component.html',
  styleUrls: ['./enrolments.component.scss']
})
export class EnrolmentsComponent implements OnInit {
  public columns: string[];
  public statuses: Config[];
  public filteredStatus: Config;
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

  public approveEnrolment(id: number) {
    this.provisionResource.updateEnrolmentStatus(id, EnrolmentStatus.ADJUDICATED_APPROVED)
      .pipe(
        exhaustMap(() => this.provisionResource.enrolment(id))
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
    this.provisionResource.updateEnrolmentStatus(id, EnrolmentStatus.DECLINED)
      .pipe(
        exhaustMap(() => this.provisionResource.enrolment(id))
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
