import { Component, OnInit } from '@angular/core';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccessTerm } from '@shared/models/access-term.model';
import { MatTableDataSource } from '@angular/material';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-enrollee-access-terms',
  templateUrl: './enrollee-access-terms.component.html',
  styleUrls: ['./enrollee-access-terms.component.scss']
})
export class EnrolleeAccessTermsComponent implements OnInit {
  public dataSource: MatTableDataSource<AccessTerm>;
  public busy: Subscription;
  public columns: string[];

  constructor(
    protected router: Router,
    protected route: ActivatedRoute,
    private enrolmentResource: EnrolmentResource,
    private logger: LoggerService,
    private toastService: ToastService
  ) {
  }

  public ngOnInit() {
    this.getAccessTerms();
  }

  private getAccessTerms() {
    const enrolleeId = this.route.snapshot.params.id;
    this.busy = this.enrolmentResource.getAccessTerms(enrolleeId)
      .subscribe(
        (accessTerms: AccessTerm[]) => {
          this.logger.info('ACCESS TERMS', accessTerms);
          this.dataSource = new MatTableDataSource<AccessTerm>(accessTerms);
        },
        (error: any) => {
          this.toastService.openErrorToast('Access Terms could not be retrieved');
          this.logger.error('[ADJUDICATION] EnrolleeAccessTerms::getAccessTerms error has occurred: ', error);
        }
      );
  }
}
