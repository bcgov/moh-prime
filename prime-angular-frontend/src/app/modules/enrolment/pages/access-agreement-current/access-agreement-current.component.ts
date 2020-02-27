import { Component, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { AccessTerm } from '@shared/models/access-term.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-access-agreement-current',
  templateUrl: './access-agreement-current.component.html',
  styleUrls: ['./access-agreement-current.component.scss']
})
export class AccessAgreementCurrentComponent implements OnInit {
  public busy: Subscription;
  public accessTerm: AccessTerm;

  constructor(
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public ngOnInit() {
    this.getAccessTermLatestSigned();
  }

  private getAccessTermLatestSigned() {
    const enrolleeId = this.enrolmentService.enrolment.id;
    this.busy = this.enrolmentResource.getAccessTermLatest(enrolleeId, true)
      .subscribe(
        (accessTerm: AccessTerm) => {
          this.logger.info('ACCESS TERM', accessTerm);
          this.accessTerm = accessTerm;
        },
        (error: any) => {
          this.toastService.openErrorToast('Access Term could not be retrieved');
          this.logger.error('[Enrolments] AccessAgreementHistory::getAccessTerm error has occurred: ', error);
        }
      );
  }
}
