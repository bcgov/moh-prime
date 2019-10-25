import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ProvisionResource } from '../../shared/services/provision-resource.service';

@Component({
  selector: 'app-enrolment',
  templateUrl: './enrolment.component.html',
  styleUrls: ['./enrolment.component.scss']
})
export class EnrolmentComponent implements OnInit {

  public enrolment: Enrolment;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
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

  private getEnrolment(id, statusCode?: number) {
    this.provisionResource.enrolment(id, statusCode)
    .pipe(
      map((enrolment: Enrolment) => this.enrolment = enrolment)
    ).subscribe();
  }
}
