import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Site } from '@registration/shared/models/site.model';
import { SiteService } from '@registration/shared/services/site.service';

import { HealthAuthSiteRegRoutes } from '../../health-auth-site-reg.routes';
import { HealthAuthSiteRegFormStateService } from '../../shared/services/health-auth-site-reg-form-state.service';
import { SiteInfoPageFormState } from './site-info-page-form-state.class';

@Component({
  selector: 'app-site-info',
  templateUrl: './site-info.component.html',
  styleUrls: ['./site-info.component.scss']
})
export class SiteInfoComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: SiteInfoPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public site: Site;
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
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public onBack() {
    this.routeUtils.routeTo([HealthAuthSiteRegRoutes.MODULE_PATH, HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_SETTING]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.haSiteRegFormStateService.siteInfoPageFormState;
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.haSiteRegFormStateService.setForm(this.site, true);
    this.formState.form.markAsPristine();
  }

  protected performSubmission(): NoContent {
    const payload = this.haSiteRegFormStateService.json;
    return this.siteResource.updateSite(payload);
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_REVIEW
      : HealthAuthSiteRegRoutes.SITE_ADDRESS;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
