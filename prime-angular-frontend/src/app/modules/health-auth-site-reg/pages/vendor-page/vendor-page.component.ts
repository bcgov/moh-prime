import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { HealthAuthSiteRegResource } from '@health-auth/shared/resources/health-auth-site-reg-resource.service';
import { HealthAuthSiteRegFormStateService } from '@health-auth/shared/services/health-auth-site-reg-form-state.service';
import { VendorPageFormState } from './vendor-page-form-state.class';

@Component({
  selector: 'app-vendor-page',
  templateUrl: './vendor-page.component.html',
  styleUrls: ['./vendor-page.component.scss']
})
export class VendorPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: VendorPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public vendorCodes: Observable<string[]>;
  public isCompleted: boolean;
  public hasNoVendorError: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private siteResource: HealthAuthSiteRegResource,
    private healthAuthResource: HealthAuthorityResource,
    private siteService: HealthAuthSiteRegService,
    private formStateService: HealthAuthSiteRegFormStateService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.hasNoVendorError = false;
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
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.ORGANIZATION_AGREEMENT);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoVendorError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.formState.vendorCode.value) {
      this.hasNoVendorError = true;
    }
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.vendorPageFormState;
  }

  protected patchForm(): void {
    this.vendorCodes = this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .pipe(map((healthAuthority: HealthAuthority) => healthAuthority.vendorCodes));

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
      : HealthAuthSiteRegRoutes.SITE_INFORMATION;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
