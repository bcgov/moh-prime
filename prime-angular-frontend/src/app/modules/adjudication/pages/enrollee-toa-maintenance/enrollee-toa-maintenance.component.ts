import { Component, OnInit } from '@angular/core';

import { Observable, Subscription } from 'rxjs';

import { AgreementVersion } from '@shared/models/agreement-version.model';
import { AgreementTypeNameMap } from '@shared/enums/agreement-type.enum';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-enrollee-toa-maintenance',
  templateUrl: './enrollee-toa-maintenance.component.html',
  styleUrls: ['./enrollee-toa-maintenance.component.scss']
})
export class EnrolleeToaMaintenanceComponent implements OnInit {
  public enrolleeAgreementVersions$: Observable<AgreementVersion[]>;
  public AgreementTypeNameMap = AgreementTypeNameMap;
  public previewingToa: AgreementVersion;
  public showPreview: boolean;

  constructor(
    private enrolmentResource: EnrolmentResource
  ) { }

  public previewToa(agreementVersion: AgreementVersion): void {
    this.previewingToa = agreementVersion;
  }

  public back(): void {
    this.previewingToa = null;
  }

  public ngOnInit(): void {
    this.enrolleeAgreementVersions$ = this.enrolmentResource.getLatestAgreementVersions()
  }
}
