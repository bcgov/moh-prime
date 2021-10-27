import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthorityFormStateService } from '@health-auth/shared/services/health-authority-form-state.service';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { AbstractHealthAuthoritySiteRegistrationPage } from '@health-auth/shared/classes/abstract-health-authority-site-registration-page.class';
import { VendorFormState } from './vendor-form-state.class';

@Component({
  selector: 'app-vendor-page',
  templateUrl: './vendor-page.component.html',
  styleUrls: ['./vendor-page.component.scss']
})
export class VendorPageComponent extends AbstractHealthAuthoritySiteRegistrationPage implements OnInit {
  public formState: VendorFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public vendors: HealthAuthorityVendor[];
  public isCompleted: boolean;
  public hasNoVendorError: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected healthAuthoritySiteService: HealthAuthoritySiteService,
    protected healthAuthorityFormStateService: HealthAuthorityFormStateService,
    protected healthAuthorityResource: HealthAuthorityResource,
    private fb: FormBuilder,
    private location: Location,
    private configService: ConfigService,
    private authorizedUserService: AuthorizedUserService,
    router: Router
  ) {
    super(dialog, formUtilsService, route, healthAuthoritySiteService, healthAuthorityFormStateService, healthAuthorityResource);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.hasNoVendorError = false;
  }

  public onBack(): void {
    (this.isCompleted)
      ? this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_OVERVIEW)
      : this.routeUtils.routeTo(HealthAuthSiteRegRoutes.routePath(HealthAuthSiteRegRoutes.SITE_MANAGEMENT));
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = this.healthAuthorityFormStateService.vendorFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    if (!healthAuthId) {
      // Don't throw an error as new registrations are created in this view
      return;
    }

    const site = this.healthAuthoritySiteService.site;
    this.vendors = this.route.snapshot.data.healthAuthority?.vendors ?? [];
    this.isCompleted = site?.completed;
    this.healthAuthorityFormStateService.setForm(site, !this.hasBeenSubmitted);
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoVendorError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.formState.healthAuthorityVendorId.value) {
      this.hasNoVendorError = true;
    }
  }

  protected submissionRequest(): Observable<unknown> {
    const { haid, sid } = this.route.snapshot.params;
    const healthAuthoritySite = this.healthAuthorityFormStateService.json;

    return (+sid)
      ? this.healthAuthorityResource
        .updateHealthAuthoritySite(+haid, +sid, healthAuthoritySite.forUpdate())
        .pipe(map(() => +sid))
      : this.healthAuthorityResource
        .createHealthAuthoritySite(+haid, healthAuthoritySite.forCreate(this.authorizedUserService.authorizedUser.id))
        .pipe(
          map((site: HealthAuthoritySite) => {
            this.routeUtils.replaceState([
              HealthAuthSiteRegRoutes.MODULE_PATH,
              HealthAuthSiteRegRoutes.HEALTH_AUTHORITIES,
              +haid,
              HealthAuthSiteRegRoutes.SITES,
              site.id,
              HealthAuthSiteRegRoutes.VENDOR
            ]);
            return site.id;
          })
        );
  }

  protected afterSubmitIsSuccessful(healthAuthSiteId: number): void {
    const nextRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      // Must go up a route-level and down with newly minted site ID
      // to override the replaced route state during submission
      : ['../', healthAuthSiteId, HealthAuthSiteRegRoutes.SITE_INFORMATION];

    this.routeUtils.routeRelativeTo(nextRoutePath);
  }
}
