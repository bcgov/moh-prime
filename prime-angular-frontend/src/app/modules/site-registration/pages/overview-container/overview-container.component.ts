import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, EMPTY } from 'rxjs';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { Site } from '@registration/shared/models/site.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { UtilsService } from '@core/services/utils.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { exhaustMap } from 'rxjs/operators';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { MatDialog } from '@angular/material/dialog';
import { SiteService } from '@registration/shared/services/site.service';


@Component({
  selector: 'app-overview-container',
  templateUrl: './overview-container.component.html',
  styleUrls: ['./overview-container.component.scss']
})
export class OverviewContainerComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public site: Site;
  public organization: Organization;
  public hasActions: boolean;
  @Input() public test: string;
  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private siteResource: SiteResource,
    private organizationResource: OrganizationResource,
    private utilsService: UtilsService,
    private dialog: MatDialog,
    private siteService: SiteService,
  ) {
    super(route, router);
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onRoute(routePath: string) {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public getBusinessLicence() {
    this.siteResource.getBusinessLicenceDownloadToken(this.site.id)
      .subscribe((token: string) => {
        this.utilsService.downloadToken(token);
      });
  }

  public onSubmit() {
    const payload = this.siteService.site;
    const data: DialogOptions = {
      title: 'Save Site',
      message: 'When your site is saved it will be submitted for review. Are you ready to save your site?',
      actionText: 'Save Site'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.siteResource.submitSite(payload)
            : EMPTY
        )
      )
      .subscribe(() => this.nextRoute());
  }

  public onBack() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT]);
  }

  public nextRoute() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT], {
      queryParams: { submitted: true }
    });
  }

  ngOnInit(): void {
    if (this.route.snapshot.routeConfig.path === 'site-review') {
      this.getSite(this.route.snapshot.params.sid);
    } else if (this.route.snapshot.routeConfig.path === 'organization-review') {
      this.getOrganization(this.route.snapshot.params.oid);
    } else {
      this.getOrganization(this.route.snapshot.params.oid);
      this.getSite(this.route.snapshot.params.sid);
    }
  }

  private getSite(siteId: number): void {
    this.busy = this.siteResource.getSiteById(siteId)
      .subscribe((site: Site) => this.site = site);
  }

  private getOrganization(organizationId: number): void {
    this.busy = this.organizationResource.getOrganizationById(organizationId)
      .subscribe((organization: Organization) => this.organization = organization);
  }

}
