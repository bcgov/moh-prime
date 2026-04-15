import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatDialog } from '@angular/material/dialog';
import { Validators } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';

import { EMPTY } from 'rxjs';
import { catchError, exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { Party } from '@lib/models/party.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationClaimPageFormState } from './organization-claim-page-form-state.class';
import { Organization } from '@registration/shared/models/organization.model';

@Component({
  selector: 'app-organization-claim-page',
  templateUrl: './organization-claim-page.component.html',
  styleUrls: ['./organization-claim-page.component.scss']
})
export class OrganizationClaimPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: OrganizationClaimPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public hasOrgClaimError: boolean;
  public hasOrganization: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private organizationFormStateService: OrganizationFormStateService,
    private organizationResource: OrganizationResource,
    private authService: AuthService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onClaimOrgChange(event: MatSlideToggleChange): void {
  }

  public onBack(): void {
    if (this.hasOrganization) {
      this.routeUtils.routeRelativeTo(['../']);
    } else {
      this.routeUtils.routeRelativeTo([SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY]);
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.authService.getUser$()
      .subscribe((bcscUser: BcscUser) => {
        this.organizationResource.getSigningAuthorityOrganizationByUsername(bcscUser.username)
          .subscribe((orgs: Organization[]) => this.hasOrganization = orgs ? orgs.length > 0 : false);
      });
  }

  protected createFormInstance(): void {
    this.formState = this.organizationFormStateService.organizationClaimPageFormState;
    this.formUtilsService.setValidators(this.formState.pec, [Validators.required]);
    this.formUtilsService.setValidators(this.formState.claimDetail, [Validators.required]);
  }

  protected patchForm(): void { }

  protected performSubmission(): NoContent {
    return this.authService.getUser$()
      .pipe(
        exhaustMap((bcscUser: BcscUser) => {
          this.organizationResource.getSigningAuthorityOrganizationByUsername(bcscUser.username)
            .subscribe((orgs: Organization[]) => this.hasOrganization = orgs ? orgs.length > 0 : false);
          return this.organizationResource.getSigningAuthorityByUsername(bcscUser.username);
        }
        ),
        exhaustMap((party: Party) =>
          this.organizationResource.claimOrganization(party.id, this.formState.json)
            .pipe(
              catchError(_ => {
                this.hasOrgClaimError = true;
                return EMPTY;
              })
            )),
        NoContentResponse
      );
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo([SiteRoutes.ORGANIZATION_CLAIM_CONFIRMATION]);
  }

}
