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
    private healthAuthorityResource: HealthAuthorityResource,
    private healthAuthoritySiteService: HealthAuthSiteRegService
  ) {
    this.showEditRedirect = true;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.routePath(HealthAuthSiteRegRoutes.MODULE_PATH));
  }

  public onSubmit(): void {
    const { haid, sid } = this.route.snapshot.params;
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
            ? this.healthAuthorityResource.healthAuthoritySiteSubmit(haid, sid)
            : EMPTY
        )
      )
      .subscribe(() => this.nextRoute());
  }

  public onBack(): void {
    this.routeUtils.routeTo([
      HealthAuthSiteRegRoutes.MODULE_PATH,
      HealthAuthSiteRegRoutes.SITE_MANAGEMENT
    ]);
  }

  public nextRoute(): void {
    this.routeUtils.routeTo([
      HealthAuthSiteRegRoutes.MODULE_PATH,
      HealthAuthSiteRegRoutes.SITE_MANAGEMENT
    ], { queryParams: { submitted: true } });
  }

  public ngOnInit(): void {
    this.healthAuthoritySite = this.healthAuthoritySiteService.site;
  }
}
