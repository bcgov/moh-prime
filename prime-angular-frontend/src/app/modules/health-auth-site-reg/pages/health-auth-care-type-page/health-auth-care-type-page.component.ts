import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthorityCareType } from '@health-auth/shared/models/health-authority-care-type.model';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthorityFormStateService } from '@health-auth/shared/services/health-authority-form-state.service';
import { AbstractHealthAuthoritySiteRegistrationPage } from '@health-auth/shared/classes/abstract-health-authority-site-registration-page.class';
import { HealthAuthCareTypeFormState } from './health-auth-care-type-form-state.class';

@Component({
  selector: 'app-health-auth-care-type-page',
  templateUrl: './health-auth-care-type-page.component.html',
  styleUrls: ['./health-auth-care-type-page.component.scss']
})
export class HealthAuthCareTypePageComponent extends AbstractHealthAuthoritySiteRegistrationPage implements OnInit {
  public formState: HealthAuthCareTypeFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public healthAuthorityCareTypes: HealthAuthorityCareType[];
  public isCompleted: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected siteService: HealthAuthoritySiteService,
    protected formStateService: HealthAuthorityFormStateService,
    protected healthAuthorityResource: HealthAuthorityResource,
    private fb: FormBuilder,
    private configService: ConfigService,
    router: Router
  ) {
    super(dialog, formUtilsService, route, siteService, formStateService, healthAuthorityResource);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public onBack(): void {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.SITE_INFORMATION;

    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.formStateService.healthAuthCareTypeFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    // if (!healthAuthId || !healthAuthSiteId) {
    //   throw new Error('No health authority site ID was provided');
    // }

    const site = this.siteService.site;
    this.healthAuthorityCareTypes = this.route.snapshot.data.healthAuthority?.careTypes ?? [];
    this.isCompleted = site?.completed;
    this.formStateService.setForm(site, !this.hasBeenSubmitted);
  }

  protected afterSubmitIsSuccessful(): void {
    const nextRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.SITE_ADDRESS;

    this.routeUtils.routeRelativeTo(nextRoutePath);
  }
}
