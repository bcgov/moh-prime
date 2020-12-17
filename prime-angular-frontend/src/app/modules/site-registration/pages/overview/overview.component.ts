import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';

import { Observable, Subscription, EMPTY, ObservableInput } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { SiteService } from '@registration/shared/services/site.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Site, SiteListViewModel } from '@registration/shared/models/site.model';
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

  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  public showSubmission: boolean;

  private numSameCareSetting: number = 0;


  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private siteResource: SiteResource,
    private dialog: MatDialog,
    private siteService: SiteService,
    private organizationService: OrganizationService,
    private logger: LoggerService,
  ) {
    this.routeUtils = (this.isOrganizationReview)
      ? new RouteUtils(route, router, SiteRoutes.routePath(SiteRoutes.ORGANIZATION_REVIEW))
      : new RouteUtils(route, router, SiteRoutes.routePath(SiteRoutes.SITE_REVIEW));
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

  public get isSiteReview(): boolean {
    return this.route.snapshot.routeConfig.path === SiteRoutes.SITE_REVIEW;
  }

  public get isOrganizationReview(): boolean {
    return this.route.snapshot.routeConfig.path === SiteRoutes.ORGANIZATION_REVIEW;
  }

  public onRoute(routePath: string): void {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public onBack(): void {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT]);
  }

  public nextRoute(): void {
    this.busy = this.determineOrgAgreementRequired$().subscribe(
      wasRequired => {
        if (wasRequired) {
          this.routeUtils.routeRelativeTo(SiteRoutes.NEXT_STEPS);
        } else {
          this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT]);
        }
      });
  }

  public ngOnInit(): void {
    this.organization = this.organizationService.organization;
    this.site = this.siteService.site;

    if (this.isOrganizationReview) {
      this.showSubmission = false;
    } else {
      this.site = this.siteService.site;
      this.showSubmission = !this.site.submittedDate;
    }
  }

  /**
   * @description
   * Infer whether user was required to accept an Organization Agreement.
   * User saw an Organization Agreement if the current Site has a care setting that is unique
   * for that Organization.
   * @returns
   * Was Org Agreement required to be shown (yes if there is only 1 site with current care setting)
   */
  private determineOrgAgreementRequired$(): Observable<boolean> {
    const currentCareSettingCode: CareSettingEnum = this.site.careSettingCode;

    return this.siteResource.getSites(this.organization.id).pipe(
      map(sites =>
        sites.filter(site => site.careSettingCode === currentCareSettingCode).length < 2));
  }
}
