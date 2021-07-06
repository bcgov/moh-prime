import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { AgreementVersion } from '@shared/models/agreement-version.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-organization-toa-maintenance-view-page',
  templateUrl: './organization-toa-maintenance-view-page.component.html',
  styleUrls: ['./organization-toa-maintenance-view-page.component.scss']
})
export class OrganizationToaMaintenanceViewPageComponent implements OnInit {
  public busy: Subscription;
  public orgAgreementVersion: AgreementVersion;

  public AgreementType = AgreementType;

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    private router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ORGANIZATION_INFORMATION));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['./']);
  }

  public ngOnInit(): void {
    this.getAgreementVersion();
  }

  private getAgreementVersion(): void {
    this.busy = this.adjudicationResource.getAgreementVersion(this.route.snapshot.params.aid)
      .subscribe((result: AgreementVersion) => this.orgAgreementVersion = result);
  }

}
