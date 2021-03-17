import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { FormUtilsService } from '@core/services/form-utils.service';

import { ConfigService } from '@config/config.service';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { VendorConfig } from '@config/config.model';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { VendorPageFormState } from './vendor-page-form-state.class';
import { NoContent } from '@core/resources/abstract-resource';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { RouteUtils } from '@lib/utils/route-utils.class';

import { HealthAuthSiteRegFormStateService } from './../../shared/services/health-auth-site-reg-form-state.service';
import { HealthAuthSiteRegRoutes } from '../../health-auth-site-reg.routes';


@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.scss']
})
export class VendorComponent extends AbstractEnrolmentPage implements OnInit {
  public isCompleted: boolean;
  public routeUtils: RouteUtils;
  public formState: VendorPageFormState;
  public vendorConfig: VendorConfig[];
  public hasNoVendorError: boolean;

  public get vendorCode(): FormControl {
    return this.form.get('vendorCode') as FormControl;
  }

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

    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.vendorConfig = this.configService.vendors.filter(v => v.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY);
    this.hasNoVendorError = false;
  }

  public onBack() {
    this.routeUtils.routeTo([HealthAuthSiteRegRoutes.MODULE_PATH, HealthAuthSiteRegRoutes.SITE_MANAGEMENT]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoVendorError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.vendorCode.value) {
      this.hasNoVendorError = true;
    }
  }

  protected createFormInstance(): void {
    this.formState = this.haSiteRegFormStateService.vendorPageFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.haSiteRegFormStateService.setForm(site, true);
    this.form.markAsPristine();
  }

  protected performSubmission(): NoContent {
    const payload = this.haSiteRegFormStateService.json;
    return this.siteResource.updateSite(payload);
  }

  protected afterSubmitIsSuccessful(): void {
    this.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_REVIEW
      : HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_SETTING;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
