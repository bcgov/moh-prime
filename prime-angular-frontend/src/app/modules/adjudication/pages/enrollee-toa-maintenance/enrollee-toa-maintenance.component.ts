import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { AgreementVersion } from '@shared/models/agreement-version.model';
import { AgreementTypeNameMap } from '@shared/enums/agreement-type.enum';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

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

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    private router: Router,
    private enrolmentResource: EnrolmentResource
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
  }

  public previewToa(agreementVersion: AgreementVersion): void {
    this.previewingToa = agreementVersion;
  }

  public onBack(): void {
    this.previewingToa 
      ? this.previewingToa = null
      : this.routeUtils.routeRelativeTo(['./']);
  }

  public ngOnInit(): void {
    this.enrolleeAgreementVersions$ = this.enrolmentResource.getLatestAgreementVersions()
  }
}
