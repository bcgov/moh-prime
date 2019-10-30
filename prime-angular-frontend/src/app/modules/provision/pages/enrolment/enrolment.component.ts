import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';

import { ProvisionResource } from '@provision/shared/services/provision-resource.service';

@Component({
  selector: 'app-enrolment',
  templateUrl: './enrolment.component.html',
  styleUrls: ['./enrolment.component.scss']
})
export class EnrolmentComponent implements OnInit {

  public enrolment: Enrolment;

  constructor(
    private route: ActivatedRoute,
    private provisionResource: ProvisionResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public showYesNo(declared: boolean) {
    return (declared === null) ? 'N/A'
      : (declared) ? 'Yes' : 'No';
  }

  public ngOnInit() {
    this.getEnrolment(this.route.snapshot.params.id);
  }

  private getEnrolment(id: number, statusCode?: number) {
    this.provisionResource.enrolment(id, statusCode)
      .subscribe(
        (enrolment: Enrolment) => {
          this.enrolment = enrolment;
          this.toastService.openSuccessToast('');
        },
        (error: any) => {
          this.toastService.openErrorToast('');
          this.logger.error('', error);
        }
      );
  }
}
