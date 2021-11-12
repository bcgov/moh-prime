import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subscription } from 'rxjs';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { AuthorizedUserResource } from '@core/resources/authorized-user-resource.service';
// TODO move to @lib when less PRs are open
import { HealthAuthority } from '@shared/models/health-authority.model';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteList } from '@health-auth/shared/models/health-authority-site-list.model';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';

@Component({
  selector: 'app-site-management-page',
  templateUrl: './site-management-page.component.html',
  styleUrls: ['./site-management-page.component.scss']
})
export class SiteManagementPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public healthAuthorityId: number;
  public healthAuthoritySites$: Observable<HealthAuthoritySiteList[] | null>;
  public routeUtils: RouteUtils;
  public HealthAuthorityEnum = HealthAuthorityEnum;
  public SiteStatusType = SiteStatusType;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authorizedUserService: AuthorizedUserService,
    private authorizedUserResource: AuthorizedUserResource
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public viewAuthorizedUser(healthAuthorityId: number): void {
    this.redirectTo(healthAuthorityId, null, HealthAuthSiteRegRoutes.AUTHORIZED_USER);
  }

  public addSite(): void {
    // Health authority ID and code are synonymous
    const healthAuthorityId = this.authorizedUserService.authorizedUser.healthAuthorityCode;
    // Site created on submission of first page
    this.redirectTo(healthAuthorityId, 0, HealthAuthSiteRegRoutes.VENDOR);
  }

  public viewSite(healthAuthorityId: number, healthAuthoritySite: HealthAuthoritySite): void {
    const pagePath = (healthAuthoritySite.completed)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.VENDOR;
    this.redirectTo(healthAuthorityId, healthAuthoritySite.id, pagePath);
  }

  public getApprovedSiteNotificationProperties(healthAuthoritySite: HealthAuthoritySite) {
    return {
      icon: 'task_alt',
      text: `Site Approved<br>Site ID: ${healthAuthoritySite.pec}`
    };
  }

  public getWithinRenewalPeriodSiteNotificationProperties(healthAuthoritySite: HealthAuthoritySite) {
    return {
      icon: 'notification_important',
      text: 'This site requires renewal.',
      label: 'Renew Site',
      route: () => this.viewSite(this.healthAuthorityId, healthAuthoritySite)
    };
  }

  public trackBySiteId(index: number, healthAuthoritySite: HealthAuthoritySite) {
    return healthAuthoritySite.id;
  }

  public ngOnInit(): void {
    this.healthAuthorityId = this.authorizedUserService.authorizedUser.healthAuthorityCode;
    this.healthAuthoritySites$ = this.authorizedUserResource.getAuthorizedUserSites(this.healthAuthorityId);
  }

  private redirectTo(healthAuthorityId: number, healthAuthoritySiteId: number, pagePath: string): void {
    this.routeUtils.routeRelativeTo([
      HealthAuthSiteRegRoutes.HEALTH_AUTHORITIES,
      healthAuthorityId,
      ...ArrayUtils.insertIf(healthAuthoritySiteId >= 0, [
        HealthAuthSiteRegRoutes.SITES,
        healthAuthoritySiteId
      ]),
      pagePath
    ]);
  }
}
