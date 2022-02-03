import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { UtilsService } from '@core/services/utils.service';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

@Component({
  selector: 'app-health-auth-org-info-page',
  templateUrl: './health-auth-org-info-page.component.html',
  styleUrls: ['./health-auth-org-info-page.component.scss']
})
export class HealthAuthOrgInfoPageComponent implements OnInit {
  public busy: Subscription;
  public AdjudicationRoutes = AdjudicationRoutes;
  public healthAuthority: HealthAuthority;
  /**
   * @description
   * An almost useless parameter indicating that the health authority
   * has not yet entered their organization information.
   */
  public isInitial: boolean;
  public upload: boolean;

  public orgAgreeDocument: BaseDocument;

  private routeUtils: RouteUtils;

  constructor(
    private healthAuthResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    router: Router,
    private utilsService: UtilsService,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
    this.upload = false;
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.routeUtils.routeWithin(routePath);
  }

  public onRouteRelative(routePath: string | (string | number)[]): void {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public addOrgInfo(): void {
    this.routeUtils.routeRelativeTo(
      [AdjudicationRoutes.HEALTH_AUTH_CARE_TYPES],
      { queryParams: { initial: true } }
    );
  }

  public addOrgAgreement(): void {
    this.upload = true;
  }

  public cancelUpload(): void {
    this.upload = false;
    this.orgAgreeDocument = null;
  }

  public updateOrganizationAgreement(): void {
    this.busy = this.healthAuthResource
      .createOrganizationAgreementDocument(this.route.snapshot.params.haid, this.orgAgreeDocument.documentGuid)
      .subscribe((document: BaseDocument) => {
        this.healthAuthority.healthAuthorityOrganizationAgreementDocument = document;
        this.orgAgreeDocument = null;
        this.upload = false;
      });
  }

  public onUpload(document: BaseDocument): void {
    this.orgAgreeDocument = document;
  }

  public onRemoveDocument(_: string): void {
    this.orgAgreeDocument = null;
  }

  public downloadOrganizationAgreement(event: Event): void {
    event.preventDefault();
    this.healthAuthResource.getOrganizationAgreementDocumentToken(this.route.snapshot.params.haid)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public ngOnInit(): void {
    this.busy = this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe((healthAuthority: HealthAuthority) => {
        this.healthAuthority = healthAuthority;
        this.isInitial = !!(
          healthAuthority?.careTypes.length &&
          healthAuthority?.vendors.length &&
          // Check one of the required fields
          healthAuthority?.privacyOffice?.email &&
          healthAuthority?.technicalSupports.length &&
          healthAuthority?.pharmanetAdministrators.length
        );
      });
  }
}
