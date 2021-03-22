import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteService } from '@registration/shared/services/site.service';

import { HealthAuthSiteRegRoutes } from '../../health-auth-site-reg.routes';
import { HealthAuthSiteRegFormStateService } from '../../shared/services/health-auth-site-reg-form-state.service';
import { CareSettingPageFormState } from './care-setting-page-form-state.class';

@Component({
  selector: 'app-care-setting-page',
  templateUrl: './care-setting-page.component.html',
  styleUrls: ['./care-setting-page.component.scss']
})
export class CareSettingPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: CareSettingPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public careSettingConfig: Config<number>[];
  public isCompleted: boolean;

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
    // TODO: replace the placeholder with the real careSetting for HA from lookup
    this.careSettingConfig = [{ code: 1, name: 'Acute Care' }, { code: 2, name: 'Other' }];
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public onBack() {
    this.routeUtils.routeTo([HealthAuthSiteRegRoutes.MODULE_PATH, HealthAuthSiteRegRoutes.VENDOR]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.haSiteRegFormStateService.careSettingPageFormState;
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
      : HealthAuthSiteRegRoutes.SITE_INFORMATION;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
