import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { AgreementVersion } from '@shared/models/agreement-version.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-enrollee-toa-maintenance-view-page',
  templateUrl: './enrollee-toa-maintenance-view-page.component.html',
  styleUrls: ['./enrollee-toa-maintenance-view-page.component.scss']
})
export class EnrolleeToaMaintenanceViewPageComponent implements OnInit {
  public busy: Subscription;
  public enrolleeAgreementVersion: AgreementVersion;

  public AgreementType = AgreementType;

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    private router: Router,
    private enrolmentResource: EnrolmentResource
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['./']);
  }

  public ngOnInit(): void {
    this.getAgreementVersion();
  }

  private getAgreementVersion(): void {
    this.busy = this.enrolmentResource.getAgreementVersion(this.route.snapshot.params.aid)
      .subscribe((result: AgreementVersion) => this.enrolleeAgreementVersion = result);
  }

}
