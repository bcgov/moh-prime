import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';

import { DateUtils } from '@lib/utils/date-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';

import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteRoutes } from '@registration/site-registration.routes';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-overview-container',
  templateUrl: './overview-container.component.html',
  styleUrls: ['./overview-container.component.scss']
})
export class OverviewContainerComponent implements OnInit {
  @Input() public showEditRedirect: boolean;
  @Input() public organization: Organization;
  @Input() public site: Site;
  @Input() public admin: boolean;
  @Input() public siteErrors: ValidationErrors;
  @Input() public businessLicenceUpdated: boolean;
  @Input() public filename: string;

  public withinRenewalPeriod: boolean;
  public routeUtils: RouteUtils;
  public SiteStatusType = SiteStatusType;
  public SiteRoutes = SiteRoutes;
  public CareSettingEnum = CareSettingEnum;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private siteResource: SiteResource,
    private utilsService: UtilsService,
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.businessLicenceUpdated = false;
  }

  public get showRemnoteUsers(): boolean {
    return this.site.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY ||
      this.site.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;
  }

  public get showDeviceProviderId(): boolean {
    return this.site.careSettingCode === CareSettingEnum.DEVICE_PROVIDER;
  }

  public showUpdateBusinessLicence(): boolean {
    return this.showEditRedirect && this.site.status !== SiteStatusType.IN_REVIEW &&
      this.site.status !== SiteStatusType.LOCKED && this.site.approvedDate !== null;
  }

  public onRoute(routePath: string | (string | number)[]) {
    let navExtra: NavigationExtras;
    if (this.site) {
      navExtra = { queryParams: { redirect: `${SiteRoutes.SITES}/${this.site.id}` } };
    }
    this.routeUtils.routeTo(routePath, navExtra);
  }

  public onRouteRelative(routePath: string | string[]) {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public getBusinessLicence(businessLicenceId: number) {
    this.siteResource.getBusinessLicenceDocumentToken(this.site.id, businessLicenceId)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public ngOnInit(): void {
    this.withinRenewalPeriod = DateUtils.withinRenewalPeriod(Site.getExpiryDate(this.site));
  }
}
