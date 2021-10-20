import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

@Component({
  selector: 'app-site-management-page',
  templateUrl: './site-management-page.component.html',
  styleUrls: ['./site-management-page.component.scss']
})
export class SiteManagementPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public healthAuthority: HealthAuthority;
  public healthAuthoritySites: HealthAuthoritySite[];
  public routeUtils: RouteUtils;
  public SiteRoutes = HealthAuthSiteRegRoutes;
  public HealthAuthorityEnum = HealthAuthorityEnum;
  public SiteStatusType = SiteStatusType;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authorizedUserService: AuthorizedUserService,
    private healthAuthorityResource: HealthAuthorityResource
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

  public viewSiteRemoteUsers(healthAuthorityId: number, healthAuthoritySiteId: number): void {
    this.redirectTo(healthAuthorityId, healthAuthoritySiteId, HealthAuthSiteRegRoutes.REMOTE_USERS);
  }

  public isUnderReview(healthAuthoritySite: HealthAuthoritySite): boolean {
    // TODO what are the status types?
    // TODO move into template
    // return healthAuthoritySite.submittedDate && healthAuthoritySite.status === SiteStatusType.IN_REVIEW;
    return !!healthAuthoritySite.submittedDate;
  }

  public isApproved(healthAuthoritySite: HealthAuthoritySite): boolean {
    // TODO what are the status types?
    // TODO move into template
    // return healthAuthoritySite.status === SiteStatusType.APPROVED;
    return false;
  }

  public isDeclined(healthAuthoritySite: HealthAuthoritySite): boolean {
    // TODO what are the status types?
    // TODO move into template
    // return healthAuthoritySite.status === SiteStatusType.DECLINED;
    return false;
  }

  public ngOnInit(): void {
    this.getHealthAuthorities();
  }

  private getHealthAuthorities(): void {
    const healthAuthorityId = this.authorizedUserService.authorizedUser.healthAuthorityCode;
    this.busy = this.healthAuthorityResource.getHealthAuthorityById(healthAuthorityId)
      .pipe(
        map((healthAuthority: HealthAuthority) => this.healthAuthority = healthAuthority),
        exhaustMap((healthAuthority: HealthAuthority) => this.healthAuthorityResource.getHealthAuthoritySites(healthAuthority.id))
      )
      .subscribe((healthAuthoritySites: HealthAuthoritySite[]) => this.healthAuthoritySites = healthAuthoritySites);
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
