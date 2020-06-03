import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state-service.service';
import { SiteService } from '@registration/shared/services/site.service';

@Component({
  selector: 'app-site-overview',
  templateUrl: './site-overview.component.html',
  styleUrls: ['./site-overview.component.scss']
})
export class SiteOverviewComponent implements OnInit, IPage {
  public busy: Subscription;
  public title: string;
  public routeUtils: RouteUtils;
  public site: Site;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private dialog: MatDialog
  ) {
    this.title = 'Site Information Review';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public onSubmit() {
    // TODO shouldn't come from service when spoking to save updates
    // const payload = this.siteService.site;
    const data: DialogOptions = {
      title: 'Save Site',
      message: 'When your site is saved it will be submitted for review. Are you ready to save your site?',
      actionText: 'Save Site'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        // TODO not needed until updates are allowed
        // TODO update only required when spoking
        // exhaustMap((result: boolean) =>
        //   (result)
        //     ? this.siteResource.submitSiteRegistration(payload)
        //     : EMPTY
        // )
      )
      .subscribe(() =>
        this.routeUtils.routeRelativeTo(SiteRoutes.CONFIRMATION, {
          queryParams: { submitted: true }
        })
      );
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.TECHNICAL_SUPPORT);
  }

  public onRoute(routePath: string) {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit() {
    this.site = this.siteService.site;
  }
}
