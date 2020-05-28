import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, from } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { FormUtilsService } from '@core/services/form-utils.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { User } from '@auth/shared/models/user.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Party } from '@registration/shared/models/party.model';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationResource } from '@registration/shared/services/organization-resource.service';

@Component({
  selector: 'app-organizations',
  templateUrl: './organizations.component.html',
  styleUrls: ['./organizations.component.scss']
})
export class OrganizationsComponent implements OnInit {
  public busy: Subscription;
  public organizations: Organization[];
  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formUtilsService: FormUtilsService,
    private authService: AuthService,
    private organizationResource: OrganizationResource
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public addOrganization() {
    this.createOrganization();
  }

  public addSite(organizationId: number) {
    this.createSite(organizationId);
  }

  public ngOnInit(): void {
    this.busy = this.organizationResource.getOrganizations()
      .subscribe((organizations: Organization[]) => this.organizations = organizations);
  }

  private createOrganization() {
    // TODO if not completed go signing authority otherwise got to overview
    this.busy = from(this.authService.getUser())
      .pipe(
        map((user: User) => new Party(user)),
        exhaustMap((party: Party) => this.organizationResource.createOrganization(party)),
        map((organization: Organization) => organization.id),
        map((organizationId: number) => this.routeUtils.routeRelativeTo(`${organizationId}`))
      ).subscribe();
  }

  private createSite(organizationId: number) {
    // TODO if not completed go signing authority otherwise got to overview
    // this.busy = from(this.authService.getUser())
    //   .pipe(
    //     map((user: User) => new Party(user)),
    //     exhaustMap((party: Party) => this.organizationResource.createOrganization(party)),
    //     map((organization: Organization) => organization.id),
    //     map((organizationId: number) => this.routeUtils.routeRelativeTo(`${organizationId}`))
    //   ).subscribe();
  }
}
