import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { AccessTerm } from '@enrolment/shared/models/access-term.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { Subscription } from 'rxjs';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-access-terms',
  templateUrl: './access-terms.component.html',
  styleUrls: ['./access-terms.component.scss']
})
export class AccessTermsComponent implements OnInit {
  public dataSource: MatTableDataSource<AccessTerm>;
  public busy: Subscription;
  public columns: string[];

  constructor(
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private logger: LoggerService,
    private toastService: ToastService
  ) {
    // this.columns = ['applicationDate', 'approvalDate', 'expiryDate', 'actions'];
    this.columns = ['current', 'applicationDate', 'approvalDate', 'actions'];
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
