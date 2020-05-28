import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';

@Component({
  selector: 'app-organizations',
  templateUrl: './organizations.component.html',
  styleUrls: ['./organizations.component.scss']
})
export class OrganizationsComponent implements OnInit {
  public busy: Subscription;
  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formUtilsService: FormUtilsService,
    private siteRegistrationResource: SiteRegistrationResource
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public ngOnInit(): void {
    // TODO check for organizations
    // TODO list the organizations
    // TODO create organization
    // TODO if not completed go signing authority otherwise got to overview
    // this.siteRegistrationResource.getOrganizations();
  }
}
