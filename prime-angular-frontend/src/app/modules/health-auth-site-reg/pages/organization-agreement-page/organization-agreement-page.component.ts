import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { HealthAuthSiteRegResource } from '@health-auth/shared/resources/health-auth-site-reg-resource.service';
import { HealthAuthSiteRegFormStateService } from '@health-auth/shared/services/health-auth-site-reg-form-state.service';
import { OrganizationAgreementPageFormState } from '@health-auth/pages/organization-agreement-page/organization-agreement-page-form-state.class';

@Component({
  selector: 'app-organization-agreement-page',
  templateUrl: './organization-agreement-page.component.html',
  styleUrls: ['./organization-agreement-page.component.scss']
})
export class OrganizationAgreementPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: OrganizationAgreementPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  // TODO don't know what we're doing yet
  public organizationAgreements: { guid: string, name: string }[];
  public hasNoSelectionError: boolean;
  public isCompleted: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private siteResource: HealthAuthSiteRegResource,
    private siteService: HealthAuthSiteRegService,
    private formStateService: HealthAuthSiteRegFormStateService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    // TODO don't know what we're doing yet
    this.organizationAgreements = [
      { guid: 'af06b812-2b46-40f7-8dc3-b4b3cef9cf61', name: 'Organization Agreement #1' },
      { guid: 'af06b812-2b46-40f7-8dc3-b4b3cef9cf62', name: 'Organization Agreement #2' },
      { guid: 'af06b812-2b46-40f7-8dc3-b4b3cef9cf63', name: 'Organization Agreement #3' },
      { guid: 'af06b812-2b46-40f7-8dc3-b4b3cef9cf64', name: 'Organization Agreement #4' },
      { guid: '00000000-0000-0000-0000-000000000000', name: 'No Organization Agreement' }
    ];
  }

  // TODO remove this method add to allow routing between pages
  public onSubmit() {
    this.hasAttemptedSubmission = true;

    if (this.checkValidity(this.formState.form)) {
      this.onSubmitFormIsValid();
      this.afterSubmitIsSuccessful();
    } else {
      this.onSubmitFormIsInvalid();
    }
  }

  public onBack() {
    this.routeUtils.routeTo(HealthAuthSiteRegRoutes.routePath(HealthAuthSiteRegRoutes.SITE_MANAGEMENT));
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoSelectionError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.formState.organizationAgreementGuid.value) {
      this.hasNoSelectionError = true;
    }
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.orgAgreementPageFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.formStateService.setForm(site, true);
    this.formState.form.markAsPristine();
  }

  protected performSubmission(): NoContent {
    const payload = this.formStateService.json;
    return this.siteResource.updateSite(payload);
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.VENDOR;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
