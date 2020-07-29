import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, from, EMPTY } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { ConfigCodePipe } from '@config/config-code.pipe';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { User } from '@auth/shared/models/user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';
import { Site } from '@registration/shared/models/site.model';
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
  public organizations: Organization[];
  public sites: Site[];
  public hasSubmittedSite: boolean;
  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
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
    this.sites = [];
  }

  public viewOrganization(organization: Organization) {
    const routePath = (!organization.completed)
      ? [SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]
      : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organization.id, ...routePath]);
  }

  public viewAgreement(organization: Organization) {
    const routePath = (organization.signedAgreementDocuments)
      ? [SiteRoutes.ORGANIZATION_AGREEMENT]
      : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organization.id, ...routePath]);
  }

  public addSite(organizationId: number) {
    this.createSite(organizationId);
  }

  public viewSite(site: Site) {
    const routePath = (site.completed)
      ? [site.organizationId, SiteRoutes.SITES, site.id] // Defaults to overview
      : [site.organizationId, SiteRoutes.SITES, site.id, SiteRoutes.CARE_SETTING];
    this.routeUtils.routeRelativeTo(routePath);
  }

  public viewSiteRemoteUsers(site: Site) {
    const routePath = [site.organizationId, SiteRoutes.SITES, site.id, SiteRoutes.REMOTE_USERS];
    this.routeUtils.routeRelativeTo(routePath);
  }

  public getOrganizationProperties(organization: Organization) {
    return [
      { key: 'Signing Authority', value: this.fullnamePipe.transform(organization.signingAuthority) },
      { key: 'Organization Name', value: organization.name }
    ];
  }

  public getSiteProperties(site: Site) {
    return [
      { key: 'Case Setting', value: this.configCodePipe.transform(site.organizationTypeCode, 'organizationTypes') },
      { key: 'Site Address', value: this.addressPipe.transform(site.physicalAddress) },
      { key: 'Vendor', value: this.configCodePipe.transform(site.siteVendors[0]?.vendorCode, 'vendors') }
    ];
  }

  public ngOnInit(): void {
    this.resetFormStates();
    this.checkQueryParams();
    this.initOrganizationAndSites();
  }

  // TODO move into a guard or resolver to allow clearing of form states
  private resetFormStates() {
    // Clear the organization and site form states so new organizations, and
    // sites aren't filled with previous information
    this.siteFormStateService.init();
    this.organizationFormStateService.init();
  }

  private checkQueryParams() {
    this.hasSubmittedSite = this.route.snapshot.queryParams?.submitted;
    this.router.navigate([], { queryParams: { submitted: null } });
  }

  private initOrganizationAndSites() {
    this.busy = this.organizationResource.getOrganizations()
      .pipe(
        map((organizations: Organization[]) => this.organizations = organizations),
        exhaustMap((organizations: Organization[]) =>
          (organizations.length)
            ? this.siteResource.getSites(organizations[0].id)
            : EMPTY
        ),
        map((sites: Site[]) => this.sites = sites)
      ).subscribe();
  }

  private createSite(organizationId: number) {
    this.busy = this.siteResource.createSite(organizationId)
      .subscribe((site: Site) => this.routeUtils.routeRelativeTo([organizationId, SiteRoutes.SITES, site.id, SiteRoutes.CARE_SETTING]));
  }
}
