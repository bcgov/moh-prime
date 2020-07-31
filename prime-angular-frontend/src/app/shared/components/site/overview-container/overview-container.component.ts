import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';

import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { AbstractComponent } from '@shared/classes/abstract-component';

import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteService } from '@registration/shared/services/site.service';

@Component({
  selector: 'app-overview-container',
  templateUrl: './overview-container.component.html',
  styleUrls: ['./overview-container.component.scss']
})
export class OverviewContainerComponent extends AbstractComponent implements OnInit {
  @Input() public site: Site;
  @Input() public organization: Organization;

  @Input() public showEditRedirect: boolean;

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

  public getBusinessLicence() {
    this.siteResource.getBusinessLicenceDownloadToken(this.site.id)
      .subscribe((token: string) => {
        this.utilsService.downloadToken(token);
      });
  }

  public ngOnInit(): void { }
}
