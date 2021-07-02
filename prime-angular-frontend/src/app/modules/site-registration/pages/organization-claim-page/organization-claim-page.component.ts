import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { FormUtilsService } from '@core/services/form-utils.service';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
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
    private formUtilsService: FormUtilsService,
    private organizationFormStateService: OrganizationFormStateService,
    private route: ActivatedRoute,
    router: Router
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.isClaimExistingOrg = false;
  }

  public onBack(): void {
    this.routeUtils.routeWithin(SiteRoutes.COLLECTION_NOTICE);
  }

  public onContinue(): void {
    // Claim an existing organization
    if (this.isClaimExistingOrg && !this.formUtilsService.checkValidity(this.formState.form)) {
      return;
    }
    this.routeUtils.routeRelativeTo([SiteRoutes.SITE_MANAGEMENT], { queryParams: { claimOrg: this.isClaimExistingOrg } });
  }

  public onChange(event: MatSlideToggleChange): void {
    this.isClaimExistingOrg = event.checked;
    if (!event.checked) {
      this.formState.form.reset();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.isClaimExistingOrg = !!this.formState.json.pec && !!this.formState.json.claimDetail;
  }

  protected createFormInstance(): void {
    this.formState = this.organizationFormStateService.organizationClaimPageFormState;
  }
}
