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
  public enrolments: Enrolment[];
  public dataSource: MatTableDataSource<Enrolment>;

  // @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private provisionResource: ProvisionResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.columns = ['appliedDate', 'name', 'status', 'approvedDate', 'actions'];
  }

  public ngOnInit() {
    this.getEnrolments();

    // this.dataSource.paginator = this.paginator;
  }

  private getEnrolments() {
    this.provisionResource.enrolments()
      .subscribe(
        (enrolments: Enrolment[]) => {
          this.logger.info('ENROLMENTS', enrolments);
          this.dataSource = new MatTableDataSource<Enrolment>(enrolments);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be accessed');
          this.logger.error('[Provision] Enrolments::getEnrolments error has occurred: ', error);
        });
  }
}
