import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

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
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';

@Component({
  selector: 'app-site-management-page',
  templateUrl: './site-management-page.component.html',
  styleUrls: ['./site-management-page.component.scss'],
  providers: [FormatDatePipe]
})
export class SiteManagementPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public healthAuthorityId: number;
  public healthAuthoritySites: HealthAuthoritySiteList[];
  public routeUtils: RouteUtils;
  public HealthAuthorityEnum = HealthAuthorityEnum;
  public SiteStatusType = SiteStatusType;
  public vendors: Config<number>[];
  public careTypes: string[];
  public form: UntypedFormGroup;

  constructor(
    private route: ActivatedRoute,
    private fb: UntypedFormBuilder,
    private router: Router,
    private authorizedUserService: AuthorizedUserService,
    private authorizedUserResource: AuthorizedUserResource,
    private formatDatePipe: FormatDatePipe,
    private configService: ConfigService,
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);

    this.careTypes = ["All"];
    this.vendors = this.configService.vendors
      .filter(v => v.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY)
      .sort((a, b) => a.name.localeCompare(b.name));
  }

  public get vendorCode(): UntypedFormControl {
    return this.form.get('vendorCode') as UntypedFormControl;
  }

  public get careTypeCode(): UntypedFormControl {
    return this.form.get('careTypeCode') as UntypedFormControl;
  }

  public viewAuthorizedUser(healthAuthorityId: number): void {
    this.redirectTo(healthAuthorityId, null, HealthAuthSiteRegRoutes.AUTHORIZED_USER);
  }

  public addSite(): void {
    // Health authority ID and code are synonymous
    const healthAuthorityId = this.authorizedUserService.authorizedUser.healthAuthorityCode;
    // Site created on submission of first page
    this.redirectTo(healthAuthorityId, 0, HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE);
  }

  public viewSite(healthAuthorityId: number, healthAuthoritySite: HealthAuthoritySite): void {
    const pagePath = (healthAuthoritySite.completed)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE;
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
      text: 'This site requires update.',
      label: 'Update Site',
      route: () => this.viewSite(this.healthAuthorityId, healthAuthoritySite)
    };
  }

  public trackBySiteId(index: number, healthAuthoritySite: HealthAuthoritySite) {
    return healthAuthoritySite.id;
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    const authorizedUser = this.authorizedUserService.authorizedUser;
    this.healthAuthorityId = authorizedUser.healthAuthorityCode;
    this.authorizedUserResource.getAuthorizedUserSites(authorizedUser.id)
      .subscribe((sites: HealthAuthoritySiteList[]) => {
        this.healthAuthoritySites = sites.sort((a, b) => a.siteName && b.siteName ? a.siteName.toLocaleLowerCase().localeCompare(b.siteName.toLocaleLowerCase()) : 0)

        const haVendors = this.healthAuthoritySites.map((s) => {
          return s.healthAuthorityVendor.vendorCode;
        });

        const haCareTypes = this.healthAuthoritySites.map((s) => {
          return s.healthAuthorityCareType.careType;
        });

        this.careTypes = [... new Set(haCareTypes)].sort((a, b) => a.localeCompare(b));
        this.vendors = this.vendors.filter((v) => haVendors.some((hav) => hav === v.code)).sort((a, b) => a.name.localeCompare(b.name));
      });
  }

  private createFormInstance() {
    this.form = this.fb.group({
      vendorCode: ['all', []],
      careTypeCode: ['all', []],
    });
  }

  private initForm() {
    this.vendorCode.valueChanges.subscribe(() => {
      this.filterSites();
    });
    this.careTypeCode.valueChanges.subscribe(() => {
      this.filterSites();
    })
  }

  public filterSites() {
    const authorizedUser = this.authorizedUserService.authorizedUser;
    this.authorizedUserResource.getAuthorizedUserSites(authorizedUser.id)
      .subscribe((sites: HealthAuthoritySiteList[]) => {
        this.healthAuthoritySites = sites.sort((a, b) => a.siteName && b.siteName ? a.siteName.toLocaleLowerCase().localeCompare(b.siteName.toLocaleLowerCase()) : 0);
        if (this.careTypeCode.value !== "all") {
          this.healthAuthoritySites = this.healthAuthoritySites.filter((s) => {
            return s.healthAuthorityCareType.careType === this.careTypeCode.value;
          })
        }
        if (this.vendorCode.value !== "all") {
          this.healthAuthoritySites = this.healthAuthoritySites.filter((s) => {
            return s.healthAuthorityVendor.vendorCode === this.vendorCode.value;
          })
        }
      });
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

  public getLastUpdatedUser(userName: string, updatedTimeStamp: string): string {
    return `${userName} - ${this.formatDatePipe.transform(updatedTimeStamp, "DD MMM yyyy h:mm A")}`
  }
}
