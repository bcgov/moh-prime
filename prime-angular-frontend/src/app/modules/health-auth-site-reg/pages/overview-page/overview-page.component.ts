import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';

import { Subscription, EMPTY, forkJoin } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

@UntilDestroy()
@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {
  public busy: Subscription;
  public pharmanetAdministrators: Contact[];
  public technicalSupports: Contact[];
  public healthAuthoritySite: HealthAuthoritySite;
  public showEditRedirect: boolean;
  public showSubmissionAction: boolean;
  public routeUtils: RouteUtils;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private healthAuthorityResource: HealthAuthorityResource
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
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      return;
    }

    this.busy = forkJoin({
      healthAuthority: this.healthAuthorityResource.getHealthAuthorityById(healthAuthId),
      healthAuthoritySite: this.healthAuthorityResource.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
    })
      .pipe(untilDestroyed(this))
      .subscribe(({
                    healthAuthority: { pharmanetAdministrators, technicalSupports },
                    healthAuthoritySite
                  }: { healthAuthority: HealthAuthority, healthAuthoritySite: HealthAuthoritySite }) => {
        this.pharmanetAdministrators = pharmanetAdministrators;
        this.technicalSupports = technicalSupports;
        this.healthAuthoritySite = healthAuthoritySite;
      });
  }
}
