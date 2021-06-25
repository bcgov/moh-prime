import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AgreementType, AgreementTypeNameMap } from '@shared/enums/agreement-type.enum';
import { AgreementVersion } from '@shared/models/agreement-version.model';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-enrollee-toa-maintenance-list-page',
  templateUrl: './enrollee-toa-maintenance-list-page.component.html',
  styleUrls: ['./enrollee-toa-maintenance-list-page.component.scss'],
  providers: [FormatDatePipe]
})
export class EnrolleeToaMaintenanceListPageComponent implements OnInit {
  public busy: Subscription;
  public enrolleeAgreementVersions: AgreementVersion[];
  public AgreementTypeNameMap = AgreementTypeNameMap;
  public previewingToa: AgreementVersion;

  public AgreementType = AgreementType;

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    private router: Router,
    private enrolmentResource: EnrolmentResource,
    private formatDatePipe: FormatDatePipe
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['../']);
  }

  public onView(id: number): void {
    this.routeUtils.routeRelativeTo([id]);
  }

  public getToaCardProperties(agreementVersion: AgreementVersion) {
    return [
      {
        key: 'Last Modified',
        value: this.formatDatePipe.transform(agreementVersion.effectiveDate)
      }
    ];
  }

  public ngOnInit(): void {
    this.busy = this.enrolmentResource.getLatestAgreementVersions()
      .subscribe((result) => this.enrolleeAgreementVersions = result);
  }
}
