import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationClaimPageFormState } from './organization-claim-page-form-state.class';

@Component({
  selector: 'app-organization-claim-page',
  templateUrl: './organization-claim-page.component.html',
  styleUrls: ['./organization-claim-page.component.scss']
})
export class OrganizationClaimPageComponent implements OnInit {
  public formState: OrganizationClaimPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public isClaimExistingOrg: boolean;
  public busy: Subscription;

  constructor(
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private siteResource: SiteResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY);
  }

  public onContinue(): void {
    if (!this.isClaimExistingOrg) {
      this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_NAME);
    }
  }

  public onChange(): void {
    this.isClaimExistingOrg = !this.isClaimExistingOrg;
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  protected createFormInstance(): void {
    this.formState = new OrganizationClaimPageFormState(this.fb);
  }
}
