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
import { SiteResource } from '@registration/shared/services/site-resource.service';

@Component({
  selector: 'app-organizations',
  templateUrl: './organizations.component.html',
  styleUrls: ['./organizations.component.scss']
})
export class OrganizationsComponent implements OnInit {
  public busy: Subscription;
  public organizations: Organization[];
  // TODO only for single organization then remove
  public sites: Site[];
  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private organizationResource: OrganizationResource,
    private siteResource: SiteResource
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public addOrganization() {
    this.createOrganization();
  }

  public viewOrganization(organizationId: number) {
    const organization = this.organizations.find(o => o.id === organizationId);
    const routePath = (organization.completed)
      ? [SiteRoutes.ORGANIZATIONS, organizationId] // Defaults to overview
      : [SiteRoutes.ORGANIZATIONS, organizationId, SiteRoutes.SIGNING_AUTHORITY];
    this.routeUtils.routeRelativeTo(routePath);
  }

  public removeOrganization(organizationId: number) {
    this.organizationResource.deleteOrganization(organizationId);
  }

  public addSite(organizationId: number) {
    this.createSite(organizationId);
  }

  // TODO only for single organization then remove
  public viewSite(siteId: number) {
    const site = this.sites.find(o => o.id === siteId);
    const routePath = (site.completed)
      ? [SiteRoutes.ORGANIZATIONS, siteId, SiteRoutes.SITES, site.id] // Defaults to overview
      : [SiteRoutes.ORGANIZATIONS, siteId, SiteRoutes.SITES, site.id, SiteRoutes.SITE_ADDRESS];
    this.routeUtils.routeRelativeTo(routePath);
  }

  // TODO only for single organization then remove
  public removeSite(siteId: number) {
    this.siteResource.deleteSite(siteId);
  }

  public ngOnInit(): void {
    this.busy = this.organizationResource.getOrganizations()
      .pipe(
        map((organizations: Organization[]) => this.organizations = organizations),
        // TODO only for single organization then remove
        exhaustMap((organizations: Organization[]) =>
          (organizations.length)
            // TODO hardcoded organization for single organization then remove
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
        map((organizationId: number) => this.routeUtils.routeRelativeTo([`${organizationId}`, SiteRoutes.SIGNING_AUTHORITY]))
      ).subscribe();
  }

  private createSite(organizationId: number) {
    this.busy = this.siteResource.createSite(organizationId)
      .subscribe((site: Site) => this.routeUtils.routeRelativeTo([SiteRoutes.ORGANIZATIONS, organizationId, SiteRoutes.SITES, site.id]));
  }
}
