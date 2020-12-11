import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { OrganizationAgreement, OrganizationAgreementViewModel } from '@shared/models/agreement.model';
import { VendorEnum } from '@shared/enums/vendor.enum';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization, OrganizationListViewModel } from '@registration/shared/models/organization.model';
import { SiteListViewModel, Site } from '@registration/shared/models/site.model';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';

@Component({
  selector: 'app-site-management',
  templateUrl: './site-management.component.html',
  styleUrls: ['./site-management.component.scss']
})
export class SiteManagementComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public organizations: OrganizationListViewModel[];
  public organizationAgreements: OrganizationAgreementViewModel[];
  public hasSubmittedSite: boolean;
  public routeUtils: RouteUtils;
  public VendorEnum = VendorEnum;
  public AgreementType = AgreementType;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationResource: OrganizationResource,
    private siteResource: SiteResource,
    private fullnamePipe: FullnamePipe,
    private addressPipe: AddressPipe,
    private configCodePipe: ConfigCodePipe,
    private utilsService: UtilsService,
    // Temporary hack to show success message until guards can be refactored
    private organizationService: OrganizationService,
    private logger: LoggerService
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.organizations = [];
  }

  public viewOrganization(organization: OrganizationListViewModel): void {
    const routePath = (!organization.completed)
      ? [SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]
      : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organization.id, ...routePath]);
  }

  public viewAgreement(organization: Organization, organizationAgreement: OrganizationAgreementViewModel): void {
    const request$ = (organizationAgreement?.signedAgreementDocumentGuid)
      ? this.organizationResource.getSignedOrganizationAgreementToken(organization.id, organizationAgreement.id)
        .pipe(
          map((token: string) => this.utilsService.downloadToken(token))
        )
      : this.organizationResource.getOrganizationAgreement(organization.id, organizationAgreement.id, true)
        .pipe(
          map((agreement: OrganizationAgreement) => agreement.agreementContent),
          map((base64: string) => this.utilsService.base64ToBlob(base64)),
          map((blob: Blob) => this.utilsService.downloadDocument(blob, 'Organization-Agreement'))
        );

    this.busy = request$.subscribe();
  }

  public viewSite(organizationId: number, site: SiteListViewModel): void {
    const routePath = (site.completed)
      ? [organizationId, SiteRoutes.SITES, site.id] // Defaults to overview
      : [organizationId, SiteRoutes.SITES, site.id, SiteRoutes.CARE_SETTING];
    this.routeUtils.routeRelativeTo(routePath);
  }

  public viewSiteRemoteUsers(organizationId: number, site: SiteListViewModel): void {
    const routePath = [organizationId, SiteRoutes.SITES, site.id, SiteRoutes.REMOTE_USERS];
    this.routeUtils.routeRelativeTo(routePath);
  }

  public addSite(organizationId: number): void {
    this.createSite(organizationId);
  }

  public getOrganizationProperties(organization: OrganizationListViewModel): { key: string, value: string }[] {
    // TODO: Why are these methods called so often despite no user interaction?
    // this.logger.trace("getOrganizationProperties");
    return [
      { key: 'Signing Authority', value: this.fullnamePipe.transform(organization.signingAuthority) },
      { key: 'Organization Name', value: organization.name },
      ...ArrayUtils.insertIf(organization?.doingBusinessAs, { key: 'Doing Business As', value: organization.doingBusinessAs })
    ];
  }

  public getOrganizationNotificationProperties(organizationId: number, siteId: number) {
    return {
      icon: 'assignment_late',
      text: 'Signed agreement has not been updated',
      label: 'Upload Agreement',
      route: () => this.routeUtils.routeRelativeTo([organizationId, SiteRoutes.SITES, siteId, SiteRoutes.ORGANIZATION_AGREEMENT])
    };
  }

  public getSiteProperties(site: SiteListViewModel): { key: string, value: string }[] {
    return [
      ...ArrayUtils.insertIf(site.doingBusinessAs, { key: 'Doing Business As', value: site.doingBusinessAs }),
      { key: 'Care Setting', value: this.configCodePipe.transform(site.careSettingCode, 'careSettings') },
      { key: 'Site Address', value: this.addressPipe.transform(site.physicalAddress) },
      { key: 'Vendor', value: this.configCodePipe.transform(site.siteVendors[0]?.vendorCode, 'vendors') }
    ];
  }

  public getSiteNotificationProperties(organizationId: number, site: SiteListViewModel) {
    return {
      icon: 'notification_important',
      text: 'Submission not completed',
      label: 'Continue Site Submission',
      route: () => this.viewSite(organizationId, site)
    };
  }

  public isUnderReview(site: SiteListViewModel): boolean {
    this.logger.trace("Site id", site.id);
    this.logger.trace("Site status", site.status);
    return (site.status === 0 || site.status === SiteStatusType.UNDER_REVIEW);
  }

  public getUnderReviewSiteNotificationProperties() {
    return {
      icon: 'notification_important',
      text: 'This site is waiting for approval and an assigned Site ID',
    };
  }

  public isDeclined(site: SiteListViewModel): boolean {
    return (site.status === SiteStatusType.DECLINED);
  }

  public getDeclinedSiteNotificationProperties() {
    return {
      icon: 'not_interested',
      text: 'Declined',
    };
  }

  public isApproved(site: SiteListViewModel): boolean {
    return (site.status === SiteStatusType.APPROVED);
  }

  public getApprovedSiteNotificationProperties(site: SiteListViewModel) {
    return {
      icon: 'task_alt',
      text: 'Site Approved<br />Site ID: ' + site.pec
    };
  }

  public ngOnInit(): void {
    // this.checkQueryParams();
    // TODO temporary hack to show success message until guards can be refactored
    this.hasSubmittedSite = (this.organizationService.showSuccess) ? true : false;
    this.organizationService.showSuccess = false;
    this.getOrganizations();
  }

  private checkQueryParams(): void {
    this.hasSubmittedSite = this.route.snapshot.queryParams?.submitted;
    this.router.navigate([], { queryParams: { submitted: null } });
  }

  private getOrganizations(): void {
    this.busy = this.organizationResource.getOrganizations()
      .pipe(
        map((organizations: OrganizationListViewModel[]) =>
          this.organizations = organizations
        ),
        exhaustMap((organization: OrganizationListViewModel[]) =>
          this.organizationResource.getOrganizationAgreements(organization[0].id)
        )
      )
      .subscribe((agreements: OrganizationAgreementViewModel[]) =>
        this.organizationAgreements = agreements
      );
  }

  private createSite(organizationId: number): void {
    this.busy = this.siteResource.createSite(organizationId)
      .subscribe((site: Site) => this.routeUtils.routeRelativeTo([organizationId, SiteRoutes.SITES, site.id, SiteRoutes.CARE_SETTING]));
  }
}
