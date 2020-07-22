import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, from, EMPTY } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { User } from '@auth/shared/models/user.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';
import { Site } from '@registration/shared/models/site.model';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { SiteResource } from '@registration/shared/services/site-resource.service';
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
    private siteFormStateService: SiteFormStateService
  ) {
    this.title = 'Site Management';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    this.organizations = [];
    this.sites = [];
  }

  public addOrganization() {
    this.createOrganization();
  }

  public viewOrganization(organizationId: number) {
    const organization = this.organizations.find(o => o.id === organizationId);
    const routePath = (!organization.completed)
      ? [SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]
      : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organizationId, ...routePath]);
  }

  public viewAgreement(organizationId: number) {
    const organization = this.organizations.find(o => o.id === organizationId);
    const routePath = (!organization.signedAgreementDocuments)
      ? [SiteRoutes.ORGANIZATION_AGREEMENT]
      : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organizationId, ...routePath]);
  }

  public addSite(organizationId: number) {
    this.createSite(organizationId);
  }

  public viewSite(site: Site) {
    // const routePath = (site.completed)
    //   ? [site.organizationId, SiteRoutes.SITES, site.id] // Defaults to overview
    //   : [site.organizationId, SiteRoutes.SITES, site.id, SiteRoutes.SITE_ADDRESS];
    // this.routeUtils.routeRelativeTo(routePath);
  }

  public viewSiteRemoteUsers(site: Site) {
    if (site.completed) {
      // const routePath = [site.organizationId, SiteRoutes.SITES, site.id, SiteRoutes.REMOTE_USERS];
      // this.routeUtils.routeRelativeTo(routePath);
    }
  }

  // TODO send in the organization as a param
  // TODO add default value if none provided for pipe if a global value for all isn't sufficient
  public getOrganizationProperties() {
    return [
      { key: 'Signing Authority', value: null },
      { key: 'Organization Name', value: null }
    ];
  }

  // TODO send in the site as a param
  // TODO add default value if none provided for pipe if a global value for all isn't sufficient
  public getSiteProperties() {
    return [
      { key: 'Case Setting', value: null },
      { key: 'Site Address', value: null },
      // TODO display vendor name using config
      { key: 'Vendor', value: null }
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

  // TODO remove hardcoded organization index for single organization
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

  private createOrganization() {
    this.busy = from(this.authService.getUser())
      .pipe(
        map((user: User) => new Party(user)),
        exhaustMap((party: Party) => this.organizationResource.createOrganization(party)),
        map((organization: Organization) => organization.id),
        map((organizationId: number) => this.routeUtils.routeRelativeTo([`${organizationId}`, SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]))
      ).subscribe();
  }

  private createSite(organizationId: number) {
    this.busy = this.siteResource.createSite(organizationId)
      .subscribe((site: Site) => this.routeUtils.routeRelativeTo([organizationId, SiteRoutes.SITES, site.id, SiteRoutes.SITE_ADDRESS]));
  }
}
