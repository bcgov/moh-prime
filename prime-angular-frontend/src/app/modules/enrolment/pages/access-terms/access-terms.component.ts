import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';

import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { AccessTerm } from '@shared/models/access-term.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';

@Component({
  selector: 'app-access-terms',
  templateUrl: './access-terms.component.html',
  styleUrls: ['./access-terms.component.scss']
})
export class AccessTermsComponent extends BaseEnrolmentPage implements OnInit {
  public dataSource: MatTableDataSource<AccessTerm>;
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

  public ngOnInit() {
    this.getAccessTerms();
  }

  private getAccessTerms() {
    const enrolleeId = this.enrolmentService.enrolment.id;
    this.busy = this.enrolmentResource.getAccessTerms(enrolleeId)
      .subscribe(
        (accessTerms: AccessTerm[]) => {
          this.logger.info('ACCESS TERMS', accessTerms);
          this.dataSource = new MatTableDataSource<AccessTerm>(accessTerms);
        },
        (error: any) => {
          this.toastService.openErrorToast('Access Terms could not be retrieved');
          this.logger.error('[Enrolments] AccessTerms::getAccessTerms error has occurred: ', error);
        }
      );
  }
}
