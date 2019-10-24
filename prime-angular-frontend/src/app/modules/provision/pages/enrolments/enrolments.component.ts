import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@enrolment/shared/models/enrolment.model';

import { ProvisionResource } from '@provision/shared/services/provision-resource.service';

@Component({
  selector: 'app-enrolments',
  templateUrl: './enrolments.component.html',
  styleUrls: ['./enrolments.component.scss']
})
export class EnrolmentsComponent implements OnInit {
  public columns: string[];
  public dataSource: MatTableDataSource<Enrolment>;

  constructor(
    private provisionResource: ProvisionResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.columns = ['appliedDate', 'name', 'status', 'approvedDate', 'actions'];
  }

  public approveEnrolment(id: number) {

  }

  public declineEnrolment(id: number) {

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

  private getEnrolments() {
    this.provisionResource.enrolments()
      .subscribe(
        (enrolments: Enrolment[]) => {
          this.logger.info('ENROLMENTS', enrolments);
          this.dataSource = new MatTableDataSource<Enrolment>(enrolments);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be retrieved');
          this.logger.error('[Provision] Enrolments::getEnrolments error has occurred: ', error);
        }
      );
  }
}
