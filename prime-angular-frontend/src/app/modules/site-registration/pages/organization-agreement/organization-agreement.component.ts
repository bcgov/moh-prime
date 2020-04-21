import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { MatCheckbox } from '@angular/material/checkbox';

@Component({
  selector: 'app-organization-agreement',
  templateUrl: './organization-agreement.component.html',
  styleUrls: ['./organization-agreement.component.scss']
})
export class OrganizationAgreementComponent implements OnInit, IPage {
  public busy: Subscription;
  public routeUtils: RouteUtils;
  public organizationAgreement: string;
  public SiteRoutes = SiteRoutes;

  @ViewChild('accept') accepted: MatCheckbox;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onSubmit() {
    if (this.accepted.checked) {
      // TODO should be a different endpoint than update
      // this.siteRegistrationResource
      //   .updateSite()
      //   .subscribe(() => {
      this.routeUtils.routeRelativeTo(SiteRoutes.VENDORS);
      // });
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
  }

  public ngOnInit(): void {
    // TODO change the footer if already signed
    this.siteRegistrationResource
      .getOrganizationAgreement()
      .subscribe((organizationAgreement: string) => this.organizationAgreement = organizationAgreement);
  }
}
