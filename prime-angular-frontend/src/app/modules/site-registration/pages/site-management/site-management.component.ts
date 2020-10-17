import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { OrganizationResource } from '@core/resources/organization-resource.service';
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

@Component({
  selector: 'app-site-management',
  templateUrl: './site-management.component.html',
  styleUrls: ['./site-management.component.scss']
})
export class SiteManagementComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public organizations: OrganizationListViewModel[];
  public organizationAgreements: OrganizationAgreement[];
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
    private organizationService: OrganizationService
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    this.organizations = [];
  }

  public viewOrganization(organization: OrganizationListViewModel) {
    const routePath = (!organization.completed)
      ? [SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]
      : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organization.id, ...routePath]);
  }

  public viewAgreement(organization: Organization, organizationAgreement: OrganizationAgreementViewModel) {
    if (organizationAgreement?.signedAgreementDocumentGuid) {
      // TODO PRIME-1085
    } else {
      // TODO PRIME-1085
    }
  }

  public viewSite(organizationId: number, site: SiteListViewModel) {
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
    return [
      { key: 'Signing Authority', value: this.fullnamePipe.transform(organization.signingAuthority) },
      { key: 'Organization Name', value: organization.name },
      ...ArrayUtils.insertIf(organization?.doingBusinessAs, { key: 'Doing Business As', value: organization.doingBusinessAs })
    ];
  }

  public getSiteProperties(site: SiteListViewModel): { key: string, value: string }[] {
    return [
      ...ArrayUtils.insertIf(site.doingBusinessAs, { key: 'Doing Business As', value: site.doingBusinessAs }),
      { key: 'Care Setting', value: this.configCodePipe.transform(site.careSettingCode, 'careSettings') },
      { key: 'Site Address', value: this.addressPipe.transform(site.physicalAddress) },
      { key: 'Vendor', value: this.configCodePipe.transform(site.siteVendors[0]?.vendorCode, 'vendors') }
    ];
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
