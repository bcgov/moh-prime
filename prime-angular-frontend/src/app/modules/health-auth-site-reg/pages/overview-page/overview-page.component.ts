import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';

import { Subscription, EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {
  public busy: Subscription;
  public healthAuthoritySite: HealthAuthoritySite;
  public showEditRedirect: boolean;
  public showSubmissionAction: boolean;
  public routeUtils: RouteUtils;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private siteResource: HealthAuthorityResource,
    private siteService: HealthAuthSiteRegService
  ) {
    this.showEditRedirect = true;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.routePath(HealthAuthSiteRegRoutes.MODULE_PATH));
  }

  public onSubmit(): void {
    // const payload = this.siteService.site;
    // const data: DialogOptions = {
    //   title: 'Save Site',
    //   message: 'When your site is saved, it will be submitted for review.',
    //   actionText: 'Save Site'
    // };
    // this.busy = this.dialog.open(ConfirmDialogComponent, { data })
    //   .afterClosed()
    //   .pipe(
    //     // TODO do whatever it is that we'll be doing with health authority submissions
    //     // exhaustMap((result: boolean) =>
    //     //   (result)
    //     //     ? this.siteResource.submitSite(payload)
    //     //     : EMPTY
    //     // )
    //   )
    //   .subscribe(() => this.nextRoute());
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo([
      HealthAuthSiteRegRoutes.MODULE_PATH,
      HealthAuthSiteRegRoutes.SITE_MANAGEMENT
    ]);
  }

  public nextRoute(): void {
    // this.routeUtils.routeTo([
    //   HealthAuthSiteRegRoutes.MODULE_PATH,
    //   HealthAuthSiteRegRoutes.SITE_MANAGEMENT
    // ], { queryParams: { submitted: true } });
  }

  public ngOnInit(): void {
    // this.site = this.siteService.site;
    // this.showSubmissionAction = !this.site?.submittedDate;
  }
}
