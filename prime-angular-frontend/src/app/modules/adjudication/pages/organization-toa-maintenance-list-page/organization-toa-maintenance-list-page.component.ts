import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { AgreementType, AgreementTypeNameMap } from '@shared/enums/agreement-type.enum';
import { AgreementVersion } from '@shared/models/agreement-version.model';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';

@Component({
  selector: 'app-organization-toa-maintenance-list-page',
  templateUrl: './organization-toa-maintenance-list-page.component.html',
  styleUrls: ['./organization-toa-maintenance-list-page.component.scss'],
  providers: [FormatDatePipe]
})
export class OrganizationToaMaintenanceListPageComponent implements OnInit {
  public busy: Subscription;
  public orgAgreementVersions: AgreementVersion[];
  public AgreementTypeNameMap = AgreementTypeNameMap;
  public previewingToa: AgreementVersion;

  public AgreementType = AgreementType;

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    private router: Router,
    private adjudicationResource: AdjudicationResource,
    private formatDatePipe: FormatDatePipe
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ORGANIZATION_INFORMATION));
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
    this.busy = this.adjudicationResource.getLatestAgreementVersions(AgreementTypeGroup.ORGANIZATION)
      .subscribe((result) => this.orgAgreementVersions = result);
  }
}
