import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';

import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { EnrolleeAgreement } from '@shared/models/agreement.model';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';

@Component({
  selector: 'app-access-terms',
  templateUrl: './access-terms.component.html',
  styleUrls: ['./access-terms.component.scss']
})
export class AccessTermsComponent extends BaseEnrolmentPage implements OnInit {
  public dataSource: MatTableDataSource<EnrolleeAgreement>;
  public columns: string[];

  constructor(
    protected router: Router,
    protected route: ActivatedRoute,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private logger: LoggerService,
    private toastService: ToastService
  ) {
    super(route, router);
  }

  public ngOnInit(): void {
    this.getAccessTerms();
  }

  private getAccessTerms() {
    const enrolleeId = this.enrolmentService.enrolment.id;
    this.busy = this.enrolmentResource.getAcceptedAccessTerms(enrolleeId)
      .subscribe((accessTerms: EnrolleeAgreement[]) => {
        this.logger.info('ENROLLEE_AGREEMENT', accessTerms);
        this.dataSource = new MatTableDataSource<EnrolleeAgreement>(accessTerms);
      });
  }
}
