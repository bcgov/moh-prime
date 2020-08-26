import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { ConfigCodePipe } from '@config/config-code.pipe';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';

import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationListViewModel } from '@registration/shared/models/organization.model';
import { SiteListViewModel, Site } from '@registration/shared/models/site.model';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';

@Component({
  selector: 'app-site-management',
  templateUrl: './site-management.component.html',
  styleUrls: ['./site-management.component.scss']
})
export class SiteManagementComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public organizations: OrganizationListViewModel[];
  public hasSubmittedSite: boolean;
  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private fullnamePipe: FullnamePipe,
    private addressPipe: AddressPipe,
    private configCodePipe: ConfigCodePipe
  ) {
    this.title = 'Site Management';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    this.organizations = [];
  }

  public viewOrganization(organization: OrganizationListViewModel) {
    const routePath = (!organization.completed)
      ? [SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]
      : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organization.id, ...routePath]);
  }

  public viewAgreement(organization: OrganizationListViewModel) {
    const routePath = (organization.acceptedAgreementDate)
      ? [SiteRoutes.ORGANIZATION_AGREEMENT]
      : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organization.id, ...routePath]);
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
      { key: 'Organization Name', value: organization.name }
    ];
  }

  public getSiteProperties(site: SiteListViewModel): { key: string, value: string }[] {
    return [
      { key: 'Care Setting', value: this.configCodePipe.transform(site.careSettingCode, 'careSettings') },
      { key: 'Site Address', value: this.addressPipe.transform(site.physicalAddress) },
      { key: 'Vendor', value: this.configCodePipe.transform(site.siteVendors[0]?.vendorCode, 'vendors') }
    ];
  }

  public ngOnInit(): void {
    this.resetFormStates();
    this.checkQueryParams();
    this.getOrganizations();
  }

  private resetFormStates(): void {
    // Clear the organization and site form states so new organizations, and
    // sites aren't filled with previous information
    this.siteFormStateService.init();
    this.organizationFormStateService.init();
  }

  private checkQueryParams(): void {
    this.hasSubmittedSite = this.route.snapshot.queryParams?.submitted;
    this.router.navigate([], { queryParams: { submitted: null } });
  }

  private getOrganizations(): void {
    this.busy = this.organizationResource.getOrganizations()
      .subscribe((organizations: OrganizationListViewModel[]) => this.organizations = organizations);
  }

  private createSite(organizationId: number): void {
    this.busy = this.siteResource.createSite(organizationId)
      .subscribe((site: Site) => this.routeUtils.routeRelativeTo([organizationId, SiteRoutes.SITES, site.id, SiteRoutes.CARE_SETTING]));
  }
}
