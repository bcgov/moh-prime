import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';

import { Observable, Subscription, EMPTY } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteService } from '@registration/shared/services/site.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {
  public busy: Subscription;
  public site: Site;
  public organization: Organization;
  public showSubmissionAction: boolean;
  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private siteResource: SiteResource,
    private dialog: MatDialog,
    private siteService: SiteService,
    private organizationService: OrganizationService,
    private logger: LoggerService
  ) {
    this.routeUtils = (this.isOrganizationReview)
      ? new RouteUtils(route, router, SiteRoutes.routePath(SiteRoutes.ORGANIZATION_REVIEW))
      : new RouteUtils(route, router, SiteRoutes.routePath(SiteRoutes.SITE_REVIEW));
  }

  public get isOrganizationReview(): boolean {
    return this.route.snapshot.routeConfig.path === SiteRoutes.ORGANIZATION_REVIEW;
  }

  public get isSiteReview(): boolean {
    return this.route.snapshot.routeConfig.path === SiteRoutes.SITE_REVIEW;
  }

  public onSubmit(): void {
    const payload = this.siteService.site;
    const data: DialogOptions = {
      title: 'Save Site',
      message: 'When your site is saved, it will be submitted for review.',
      actionText: 'Save Site'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.siteResource.submitSite(payload)
            : EMPTY
        ),
        // Temporary hack to show success message until guards can be refactored
        tap(() => this.organizationService.showSuccess = true)
      )
      .subscribe(() => this.nextRoute());
  }

  public onRoute(routePath: string): void {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public onBack(): void {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT]);
  }

  public nextRoute(): void {
    this.busy = this.isOrgAgreementRequired$()
      .subscribe((wasRequired: boolean) =>
        (wasRequired)
          ? this.routeUtils.routeRelativeTo(SiteRoutes.NEXT_STEPS)
          : this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT])
      );
  }

  public ngOnInit(): void {
    this.organization = this.organizationService.organization;

    if (this.isOrganizationReview) {
      this.showSubmissionAction = false;
    } else {
      this.site = this.siteService.site;
      this.showSubmissionAction = !this.site.submittedDate;
    }
  }

  /**
   * @description
   * Check whether an organization agreement is required for a site based on
   * whether a care setting already exists of the same type since each
   * unique care setting type requires an organization agreement.
   */
  private isOrgAgreementRequired$(): Observable<boolean> {
    const currentCareSettingCode = this.site.careSettingCode;
    return this.siteResource.getSites(this.organization.id)
      .pipe(
        map(sites => sites.filter(site => site.careSettingCode === currentCareSettingCode).length < 2)
      );
  }
}
