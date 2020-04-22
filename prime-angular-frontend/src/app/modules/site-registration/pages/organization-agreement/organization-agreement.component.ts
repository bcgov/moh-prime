import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCheckbox } from '@angular/material/checkbox';

import { Subscription } from 'rxjs';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

@Component({
  selector: 'app-organization-agreement',
  templateUrl: './organization-agreement.component.html',
  styleUrls: ['./organization-agreement.component.scss']
})
export class OrganizationAgreementComponent implements OnInit, IPage {
  public busy: Subscription;
  public routeUtils: RouteUtils;
  public organizationAgreement: string;
  public hasAcceptedAgreement: boolean;
  public SiteRoutes = SiteRoutes;

  @ViewChild('accept') accepted: MatCheckbox;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onSubmit() {
    if (this.accepted.checked) {
      // TODO put in a dialog at some point
      const siteId = this.siteRegistrationService.site.id;
      this.siteRegistrationResource
        .acceptCurrentOrganizationAgreement(siteId)
        .subscribe(() => this.nextRoute());
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
  }

  public nextRoute() {
    this.routeUtils.routeRelativeTo(SiteRoutes.VENDORS);
  }

  public ngOnInit(): void {
    this.hasAcceptedAgreement = !!this.siteRegistrationService.site.location.organization.acceptedAgreementDate;
    this.siteRegistrationResource
      .getOrganizationAgreement()
      .subscribe((organizationAgreement: string) => this.organizationAgreement = organizationAgreement);
  }
}
