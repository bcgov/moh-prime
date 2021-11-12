import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-site-list.model';

@Component({
  selector: 'app-site-management-page',
  templateUrl: './site-management-page.component.html',
  styleUrls: ['./site-management-page.component.scss']
})
export class SiteManagementPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public healthAuthority: HealthAuthority;
  public healthAuthoritySites: HealthAuthoritySiteAdminList[];
  public routeUtils: RouteUtils;
  public HealthAuthorityEnum = HealthAuthorityEnum;
  public SiteStatusType = SiteStatusType;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authorizedUserService: AuthorizedUserService,
    private healthAuthorityResource: HealthAuthorityResource,
    private healthAuthoritySiteResource: HealthAuthoritySiteResource
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

  public isInComplete(healthAuthoritySite: HealthAuthoritySite): boolean {
    return !healthAuthoritySite.submittedDate || (
      healthAuthoritySite.submittedDate &&
      !healthAuthoritySite.approvedDate &&
      healthAuthoritySite.status === SiteStatusType.EDITABLE
    );
  }

  public isInReview(healthAuthoritySite: HealthAuthoritySite): boolean {
    return healthAuthoritySite.submittedDate && healthAuthoritySite.status === SiteStatusType.IN_REVIEW;
  }

  public isLocked(healthAuthoritySite: HealthAuthoritySite): boolean {
    return healthAuthoritySite.status === SiteStatusType.LOCKED;
  }

  public isApproved(healthAuthoritySite: HealthAuthoritySite): boolean {
    return healthAuthoritySite.status === SiteStatusType.EDITABLE && !!healthAuthoritySite.approvedDate;
  }

  public requiresRenewal(healthAuthoritySite: HealthAuthoritySite): boolean {
    return healthAuthoritySite.withinRenewalPeriod();
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
      route: () => this.viewSite(this.healthAuthority.id, healthAuthoritySite)
    };
  }

  public trackBySiteId(index: number, healthAuthoritySite: HealthAuthoritySite) {
    return healthAuthoritySite.id;
  }

  public ngOnInit(): void {
    this.getHealthAuthorities();
  }

  private getHealthAuthorities(): void {
    const healthAuthorityId = this.authorizedUserService.authorizedUser.healthAuthorityCode;
    this.busy = this.healthAuthorityResource.getHealthAuthorityById(healthAuthorityId)
      .pipe(
        map((healthAuthority: HealthAuthority) => this.healthAuthority = healthAuthority),
        exhaustMap((healthAuthority: HealthAuthority) => this.healthAuthoritySiteResource.getHealthAuthorityAdminSites(healthAuthority.id))
      )
      .subscribe((healthAuthoritySites: HealthAuthoritySiteAdminList[]) => this.healthAuthoritySites = healthAuthoritySites);
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
