import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { SiteService } from '@registration/shared/services/site.service';

import { HealthAuthSiteRegRoutes } from '../../health-auth-site-reg.routes';
import { HealthAuthSiteRegFormStateService } from '../../shared/services/health-auth-site-reg-form-state.service';
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
  public vendorConfig: VendorConfig[];
  public isCompleted: boolean;
  public hasNoVendorError: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private siteResource: SiteResource,
    private configService: ConfigService,
    private siteService: SiteService,
    private haSiteRegFormStateService: HealthAuthSiteRegFormStateService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.vendorConfig = this.configService.vendors.filter(v => v.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY);
    this.hasNoVendorError = false;
  }

  public onBack() {
    this.routeUtils.routeTo([HealthAuthSiteRegRoutes.MODULE_PATH, HealthAuthSiteRegRoutes.SITE_MANAGEMENT]);
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
    this.formState = this.haSiteRegFormStateService.vendorPageFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.haSiteRegFormStateService.setForm(site, true);
    this.formState.form.markAsPristine();
  }

  protected performSubmission(): NoContent {
    const payload = this.haSiteRegFormStateService.json;
    return this.siteResource.updateSite(payload);
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_SETTING;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
