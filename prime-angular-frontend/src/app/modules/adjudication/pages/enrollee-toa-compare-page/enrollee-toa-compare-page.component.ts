import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { AgreementVersion } from '@shared/models/agreement-version.model';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';

@Component({
  selector: 'app-enrollee-toa-compare-page',
  templateUrl: './enrollee-toa-compare-page.component.html',
  styleUrls: ['./enrollee-toa-compare-page.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EnrolleeToaComparePageComponent implements OnInit {
  public busy: Subscription;
  public agreementVersions: AgreementVersion[];
  public agreementType: AgreementType;
  public initialVersionId: number;
  public finalVersionId: number;
  public diffHtml: string;

  public AgreementType = AgreementType;

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    private router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['./']);
  }

  public ngOnInit(): void {
    this.agreementType = this.route.snapshot.params.tid;
    this.getAgreementVersions();
  }

  private getAgreementVersions(): void {
    this.busy = this.adjudicationResource.getAgreementVersions(false, null, this.agreementType)
      .subscribe((result: AgreementVersion[]) => this.agreementVersions = result);
  }

  public getDiffHtml(): void {
    if (!this.initialVersionId || !this.finalVersionId) {
      return;
    }

    this.busy = this.adjudicationResource.compareAgreementVersions(this.initialVersionId, this.finalVersionId)
      .subscribe((result: string) => this.diffHtml = result);
  }
}
