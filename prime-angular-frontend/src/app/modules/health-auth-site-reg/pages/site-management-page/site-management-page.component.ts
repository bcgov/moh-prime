import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { OrganizationAgreementViewModel } from '@shared/models/agreement.model';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { SiteResource } from '@core/resources/site-resource.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { LoggerService } from '@core/services/logger.service';

import { VendorEnum } from '@shared/enums/vendor.enum';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { optionalAddressLineItems } from '@shared/models/address.model';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';

// TODO if these actually are used in this module move to @lib
import { Organization } from '@registration/shared/models/organization.model';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';
import { SiteListViewModel } from '@registration/shared/models/site.model';
import { OrganizationService } from '@registration/shared/services/organization.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthority } from '@shared/models/health-authority.model';

@Component({
  selector: 'app-site-management-page',
  templateUrl: './site-management-page.component.html',
  styleUrls: ['./site-management-page.component.scss']
})
export class SiteManagementPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  // public organizations: Organization[];
  // public organizationAgreements: OrganizationAgreementViewModel[];
  // public hasSubmittedSite: boolean;
  public routeUtils: RouteUtils;
  // public VendorEnum = VendorEnum;
  // public AgreementType = AgreementType;
  // public CareSettingEnum = CareSettingEnum;
  // public SiteRoutes = HealthAuthSiteRegRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    // private organizationResource: OrganizationResource,
    // private siteResource: SiteResource,
    // private fullnamePipe: FullnamePipe,
    // private addressPipe: AddressPipe,
    // private configCodePipe: ConfigCodePipe,
    // private utilsService: UtilsService
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    // this.organizations = [];
  }

  // public viewOrganization(organization: Organization): void {
  //   const routePath = (!organization.completed)
  //     ? [SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]
  //     : []; // Defaults to overview
  //   this.routeUtils.routeRelativeTo([organization.id, ...routePath]);
  // }

  // public viewAgreement(organization: Organization, organizationAgreement: OrganizationAgreementViewModel): void {
  //   const request$ = (organizationAgreement?.signedAgreementDocumentGuid)
  //     ? this.organizationResource.getSignedOrganizationAgreementToken(organization.id, organizationAgreement.id)
  //       .pipe(
  //         map((token: string) => this.utilsService.downloadToken(token))
  //       )
  //     : this.organizationResource.getOrganizationAgreement(organization.id, organizationAgreement.id, true)
  //       .pipe(
  //         map((agreement: OrganizationAgreement) => agreement.agreementContent),
  //         map((base64: string) => this.utilsService.base64ToBlob(base64)),
  //         map((blob: Blob) => this.utilsService.downloadDocument(blob, 'Organization-Agreement'))
  //       );
  //
  //   this.busy = request$.subscribe();
  // }

  // public viewSite(organizationId: number, site: SiteListViewModel): void {
  //   const routePath = (site.completed)
  //     // ? [organizationId, HealthAuthSiteRegRoutes.MODULE_PATH, site.id] // Defaults to overview
  //     // : [organizationId, HealthAuthSiteRegRoutes.MODULE_PATH, site.id, HealthAuthSiteRegRoutes.VENDOR];
  //     ? HealthAuthSiteRegRoutes.VENDOR
  //     : HealthAuthSiteRegRoutes.VENDOR;
  //   this.routeUtils.routeRelativeTo(routePath);
  // }

  // public viewSiteRemoteUsers(organizationId: number, site: SiteListViewModel): void {
  //   const routePath = [organizationId, SiteRoutes.SITES, site.id, SiteRoutes.REMOTE_USERS];
  //   this.routeUtils.routeRelativeTo(routePath);
  // }

  public addSite(healthAuthorityId: number): void {
    // Site created on submission of first page
    this.redirectToSite(healthAuthorityId, 0);
  }

  // public getOrganizationProperties(organization: Organization): { key: string, value: string; }[] {
  //   return [
  //     { key: 'Signing Authority', value: this.fullnamePipe.transform(organization.signingAuthority) },
  //     { key: 'Organization Name', value: organization.name },
  //     ...ArrayUtils.insertIf(organization?.doingBusinessAs, { key: 'Doing Business As', value: organization.doingBusinessAs })
  //   ];
  // }
  //
  // public getSiteProperties(site: SiteListViewModel): { key: string, value: string; }[] {
  //   return [
  //     ...ArrayUtils.insertIf(site.doingBusinessAs, { key: 'Doing Business As', value: site.doingBusinessAs }),
  //     { key: 'Care Setting', value: this.configCodePipe.transform(site.careSettingCode, 'careSettings') },
  //     {
  //       key: 'Site Address',
  //       value: this.addressPipe.transform(site.physicalAddress, [...optionalAddressLineItems, 'provinceCode', 'countryCode'])
  //     },
  //     { key: 'Vendor', value: this.configCodePipe.transform(site.siteVendors[0]?.vendorCode, 'vendors') }
  //   ];
  // }

  // public getNotSubmittedSiteNotificationProperties(organizationId: number, site: SiteListViewModel) {
  //   return {
  //     icon: 'notification_important',
  //     text: 'Submission not completed',
  //     label: 'Continue Site Submission',
  //     route: () => this.viewSite(organizationId, site)
  //   };
  // }

  // public isUnderReview(site: SiteListViewModel): boolean {
  //   return site.submittedDate && site.status === SiteStatusType.UNDER_REVIEW;
  // }
  //
  // public getUnderReviewSiteNotificationProperties() {
  //   return {
  //     icon: 'notification_important',
  //     text: 'This site is waiting for approval and an assigned Site ID',
  //   };
  // }

  // public isDeclined(site: SiteListViewModel): boolean {
  //   return (site.status === SiteStatusType.DECLINED);
  // }
  //
  // public getDeclinedSiteNotificationProperties() {
  //   return {
  //     icon: 'not_interested',
  //     text: 'Declined',
  //   };
  // }
  //
  // public isApproved(site: SiteListViewModel): boolean {
  //   return (site.status === SiteStatusType.APPROVED);
  // }
  //
  // public getApprovedSiteNotificationProperties(site: SiteListViewModel) {
  //   return {
  //     icon: 'task_alt',
  //     text: `Site Approved<br>Site ID: ${site.pec}`
  //   };
  // }

  public ngOnInit(): void {
    // this.getHealthAuthorities();
  }

  // private getHealthAuthorities(): void {
  //   this.busy = this.healthAuthorityResource.getOrganizations()
  //     .pipe(
  //       map((healthAuthorities: HealthAuthority[]) => this.healthAuthorities = healthAuthorities)
  //     ).subscribe();
  // }

  private redirectToSite(healthAuthId: number, healthAuthSiteId: number): void {
    this.routeUtils.routeRelativeTo([
      HealthAuthSiteRegRoutes.HEALTH_AUTHORITIES,
      healthAuthId,
      HealthAuthSiteRegRoutes.SITES,
      healthAuthSiteId,
      HealthAuthSiteRegRoutes.VENDOR
    ]);
  }
}
