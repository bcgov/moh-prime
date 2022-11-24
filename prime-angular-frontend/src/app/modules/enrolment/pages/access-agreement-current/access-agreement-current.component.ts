import { Component, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';

import { EnrolleeAgreement } from '@shared/models/agreement.model';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { UtilsService } from '@core/services/utils.service';

@Component({
  selector: 'app-access-agreement-current',
  templateUrl: './access-agreement-current.component.html',
  styleUrls: ['./access-agreement-current.component.scss']
})
export class AccessAgreementCurrentComponent implements OnInit {
  public busy: Subscription;
  public accessTerm: EnrolleeAgreement;

  constructor(
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private utilsService: UtilsService,
  ) { }

  public ngOnInit(): void {
    this.getAccessTermLatestSigned();
  }

  private getAccessTermLatestSigned() {
    const enrolleeId = this.enrolmentService.enrolment.id;
    this.busy = this.enrolmentResource.getLatestAccessTerm(enrolleeId, true)
      .subscribe((accessTerm: EnrolleeAgreement) => this.accessTerm = accessTerm);
  }

  public onDownload() {
    this.enrolmentResource
      .getAcceptedTermsOfAccessToken(this.enrolmentService.enrolment.id)
      .subscribe((token: string) => {
        this.utilsService.downloadToken(token);
      });
  }
}
