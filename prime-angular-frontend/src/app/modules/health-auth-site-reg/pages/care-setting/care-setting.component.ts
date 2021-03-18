import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteService } from '@registration/shared/services/site.service';
import { HealthAuthSiteRegRoutes } from '../../health-auth-site-reg.routes';
import { HealthAuthSiteRegFormStateService } from '../../shared/services/health-auth-site-reg-form-state.service';
import { CareSettingPageFormState } from './care-setting-page-form-state.class';

@Component({
  selector: 'app-care-setting',
  templateUrl: './care-setting.component.html',
  styleUrls: ['./care-setting.component.scss']
})
export class CareSettingComponent extends AbstractEnrolmentPage implements OnInit {
  public isCompleted: boolean;
  public routeUtils: RouteUtils;
  public careSettingConfig: Config<number>[];
  public formState: CareSettingPageFormState;
  public title: string;

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

  public get careSettingCode(): FormControl {
    return this.form.get('careSettingCode') as FormControl;
  }

  public onBack() {
    this.routeUtils.routeTo([HealthAuthSiteRegRoutes.MODULE_PATH, HealthAuthSiteRegRoutes.VENDOR]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.formState = this.haSiteRegFormStateService.careSettingPageFormState;
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
      : HealthAuthSiteRegRoutes.SITE_INFORMATION;

    this.routeUtils.routeRelativeTo(routePath);
  }

}
