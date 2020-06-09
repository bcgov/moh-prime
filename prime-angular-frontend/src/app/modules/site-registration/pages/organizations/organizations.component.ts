import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, from, EMPTY } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { AuthService } from '@auth/shared/services/auth.service';
import { User } from '@auth/shared/models/user.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';
import { Site } from '@registration/shared/models/site.model';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { OrganizationResource } from '@registration/shared/services/organization-resource.service';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state-service.service';

@Component({
  selector: 'app-organizations',
  templateUrl: './organizations.component.html',
  styleUrls: ['./organizations.component.scss']
})
export class OrganizationsComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public organizations: Organization[];
  // TODO only for single organization then remove
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
    this.title = 'Administration';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    this.organizations = [];
    this.sites = [];
  }

  public addOrganization() {
    this.createOrganization();
  }

  public viewOrganization(organizationId: number, optionalRoutePath: string = null) {
    const organization = this.organizations.find(o => o.id === organizationId);
    const routePath = (optionalRoutePath)
      ? [optionalRoutePath]
      : (!organization.completed)
        ? [SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]
        : []; // Defaults to overview
    this.routeUtils.routeRelativeTo([organizationId, ...routePath]);
  }

  public removeOrganization(organizationId: number) {
    this.organizationResource
      .deleteOrganization(organizationId)
      .subscribe(() => {
        const index = this.sites.findIndex(o => o.id === organizationId);
        this.organizations.splice(index, 1);
      });
  }

  public addSite(organizationId: number) {
    this.createSite(organizationId);
  }

  // TODO only for single organization then remove
  public viewSite(siteId: number) {
    const site = this.sites.find(o => o.id === siteId);
    const routePath = (site.completed)
      ? [siteId, SiteRoutes.SITES, site.id] // Defaults to overview
      : [siteId, SiteRoutes.SITES, site.id, SiteRoutes.SITE_ADDRESS];
    this.routeUtils.routeRelativeTo(routePath);
  }

  // TODO only for single organization then remove
  public removeSite(siteId: number) {
    this.siteResource
      .deleteSite(siteId)
      .subscribe(() => {
        const index = this.sites.findIndex(s => s.id === siteId);
        this.sites.splice(index, 1);
      });
  }

  public ngOnInit(): void {
    // TODO move into a guard after multiple organizations is in place, and routes are locked down
    // Clear the organization and site form states so new organizations, and
    // sites aren't filled with previous information
    this.siteFormStateService.init();
    this.organizationFormStateService.init();

    this.hasSubmittedSite = this.route.snapshot.queryParams?.submitted;
    this.router.navigate([], { queryParams: { submitted: null } });

    this.busy = this.organizationResource.getOrganizations()
      .pipe(
        map((organizations: Organization[]) => this.organizations = organizations),
        // TODO only for single organization then remove
        exhaustMap((organizations: Organization[]) =>
          (organizations.length)
            // TODO hardcoded organization index for single organization then remove
            ? this.siteResource.getSites(organizations[0].id)
            : EMPTY
        ),
        // TODO only for single organization then remove
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
