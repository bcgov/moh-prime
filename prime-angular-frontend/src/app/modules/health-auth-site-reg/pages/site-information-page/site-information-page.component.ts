import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { HealthAuthSiteRegFormStateService } from '@health-auth/shared/services/health-auth-site-reg-form-state.service';
import { SiteInformationPageFormState } from './site-information-page-form-state.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

@Component({
  selector: 'app-site-information-page',
  templateUrl: './site-information-page.component.html',
  styleUrls: ['./site-information-page.component.scss']
})
export class SiteInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: SiteInformationPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public site: HealthAuthoritySite;
  public isCompleted: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private siteResource: HealthAuthorityResource,
    private siteService: HealthAuthSiteRegService,
    private formStateService: HealthAuthSiteRegFormStateService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
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
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.VENDOR);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.formStateService.siteInfoPageFormState;
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.formStateService.setForm(this.site, true);
    this.formState.form.markAsPristine();
  }

  protected performSubmission(): NoContent {
    const payload = this.formStateService.json;
    // return this.siteResource.updateSite(payload);
    return void 0;
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
