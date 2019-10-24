import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatSelectChange } from '@angular/material';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';

import { ProvisionResource } from '@provision/shared/services/provision-resource.service';
import { EnrolmentStatus } from '@provision/shared/enums/enrolment-status.enum';

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
    private logger: LoggerService
  ) {
    this.columns = ['appliedDate', 'name', 'status', 'approvedDate', 'actions'];
    this.statuses = this.configService.statuses;
    this.filteredStatus = null;
  }

  public filterByStatus(selection: MatSelectChange) {
    const statusCode = selection.value;
    this.filteredStatus = this.statuses.find(s => s.code === statusCode);
    console.log(this.filteredStatus);

    this.getEnrolments(statusCode);
  }

  public approveEnrolment(id: number) {
    this.provisionResource.updateEnrolmentStatus(id, EnrolmentStatus.ADJUDICATED_APPROVED)
      // TODO: request the enrolment to refresh its status
      .subscribe(
        () => {
          this.toastService.openSuccessToast('Enrolment has been approved');
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be approved');
          this.logger.error('[Provision] Enrolments::approveEnrolment error has occurred: ', error);
        }
      );
  }

  public declineEnrolment(id: number) {
    this.provisionResource.updateEnrolmentStatus(id, EnrolmentStatus.DECLINED)
      // TODO: request the enrolment to refresh its status
      .subscribe(
        () => {
          this.toastService.openSuccessToast('Enrolment has been declined');
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be declined');
          this.logger.error('[Provision] Enrolments::declineEnrolment error has occurred: ', error);
        }
      );
  }

  public deleteEnrolment(id: number) {
    this.provisionResource.deleteEnrolment(id)
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been deleted');
          this.dataSource.data = this.dataSource.data.filter(e => e.id !== id);
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
}
