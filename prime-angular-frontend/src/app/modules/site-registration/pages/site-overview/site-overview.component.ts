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
    // TODO setup guard to pull organization on each route in the loop
    // private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private dialog: MatDialog
  ) {
    this.title = 'Site Registration Review';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public onSubmit() {
    // TODO should this be Save Site instead?
    // const payload = this.siteService.site;
    // const data: DialogOptions = {
    //   title: 'Save Site',
    // TODO who are they submitting it to?
    //   message: 'When your site is saved you will be able to submit it to ________. Are you ready to save your site?',
    //   actionText: 'Save Site'
    // };
    // this.busy = this.dialog.open(ConfirmDialogComponent, { data })
    //   .afterClosed()
    //   .pipe(
    //     exhaustMap((result: boolean) =>
    //       (result)
    //         ? this.siteRegistrationResource.submitSiteRegistration(payload)
    //         : EMPTY
    //     )
    //   )
    //   .subscribe(() =>
    // TODO add some temporary messaging for users in demo
    this.routeUtils.routeRelativeTo(SiteRoutes.CONFIRMATION);
    // );
  }

  public onRoute(routePath: string) {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit() {
    // TODO temporary until spoke routing
    const siteId = this.route.snapshot.params.sid;
    this.siteResource
      .getSiteById(siteId)
      .subscribe((site: Site) => this.site = site);
  }
}
