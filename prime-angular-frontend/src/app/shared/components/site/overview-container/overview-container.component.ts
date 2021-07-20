import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { AbstractComponent } from '@shared/classes/abstract-component';

import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteRoutes } from '@registration/site-registration.routes';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';

@Component({
  selector: 'app-overview-container',
  templateUrl: './overview-container.component.html',
  styleUrls: ['./overview-container.component.scss']
})
export class OverviewContainerComponent extends AbstractComponent implements OnInit {
  @Input() public site: Site;
  @Input() public organization: Organization;
  @Input() public showEditRedirect: boolean;
  @Input() public admin: boolean;
  @Input() public businessLicences: BusinessLicence[];

  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private siteResource: SiteResource,
    private utilsService: UtilsService,
  ) {
    super(route, router);
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.admin = false;
    this.businessLicences = [];
  }

  public onRoute(routePath: string | (string | number)[]) {
    let navExtra: NavigationExtras;
    if (this.site) {
      navExtra = { queryParams: { redirect: `${SiteRoutes.SITES}/${this.site.id}` } };
    }
    this.routeUtils.routeTo(routePath, navExtra);
  }

  public onRouteRelative(routePath: string) {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public getBusinessLicence(businessLicenceId: number) {
    this.siteResource.getBusinessLicenceDocumentToken(this.site.id, businessLicenceId)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public ngOnInit(): void { }

}
