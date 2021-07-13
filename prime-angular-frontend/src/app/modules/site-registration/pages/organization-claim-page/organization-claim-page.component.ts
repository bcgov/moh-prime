import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { ToastService } from '@core/services/toast.service';

import { Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { AuthService } from '@auth/shared/services/auth.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationClaimFormModel } from '@registration/shared/models/organization-claim-form.model';
import { OrganizationClaimPageFormState } from './organization-claim-page-form-state.class';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationAgreementViewModel } from '@shared/models/agreement.model';

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
    private organizationResource: OrganizationResource,
    private toastService: ToastService,
    private authService: AuthService,
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
    if (this.isClaimExistingOrg) {
      if (this.formUtilsService.checkValidity(this.formState.form)) {
        // check if the organization w/ the PEC has already been claimed
        this.busy = this.organizationResource.getOrganizationClaim({ pec: this.formState.json.pec })
          .subscribe((result: boolean) => {
            if (result) {
              this.toastService.openErrorToast(`The organization associated the site of PEC code ${this.formState.json.pec} cannot be claimed.`);
            }
            else {
              this.routeUtils.routeRelativeTo([SiteRoutes.SITE_MANAGEMENT], { queryParams: { claimOrg: true } });
            }
          });
      }
      else {
        // invalid form
        return;
      }
    }
    else {
      this.routeUtils.routeRelativeTo([SiteRoutes.SITE_MANAGEMENT], { queryParams: { claimOrg: false } });
    }
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
