import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, Observable } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { VendorFormState } from './vendor-form-state.class';
import { HealthAuthority } from '@shared/models/health-authority.model';

@Component({
  selector: 'app-vendor-page',
  templateUrl: './vendor-page.component.html',
  styleUrls: ['./vendor-page.component.scss']
})
export class VendorPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: VendorFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public vendorCodes: number[];
  public isCompleted: boolean;
  public hasNoVendorError: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private location: Location,
    private configService: ConfigService,
    private healthAuthorityResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

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
    this.formState = new VendorFormState(this.fb);
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    if (!healthAuthId) {
      return;
    }

    const healthAuthSiteId = +this.route.snapshot.params.sid;

    this.busy = this.healthAuthorityResource.getHealthAuthorityById(healthAuthId)
      .pipe(
        tap(({ vendorCodes }: HealthAuthority) => this.vendorCodes = vendorCodes),
        exhaustMap((_: HealthAuthority) =>
          (healthAuthSiteId)
            ? this.healthAuthorityResource.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
            : EMPTY
        )
      )
      .subscribe(({ vendorCode, completed }: HealthAuthoritySite) => {
        this.isCompleted = completed;
        this.formState.patchValue({ vendorCode });
      });
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoVendorError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.formState.vendorCode.value) {
      this.hasNoVendorError = true;
    }
  }

  protected performSubmission(): Observable<number> {
    const payload = this.formState.json;
    const { haid, sid } = this.route.snapshot.params;

    return (+sid)
      ? this.healthAuthorityResource.updateHealthAuthoritySiteVendor(haid, sid, payload)
        .pipe(map(() => sid))
      : this.healthAuthorityResource.createHealthAuthoritySite(haid, payload)
        .pipe(
          map((site: HealthAuthoritySite) => {
            // Replace the URL with redirection, and prevent initial
            // ID of zero being pushed onto browser history
            this.location.replaceState([
              HealthAuthSiteRegRoutes.MODULE_PATH,
              HealthAuthSiteRegRoutes.HEALTH_AUTHORITIES,
              +haid,
              HealthAuthSiteRegRoutes.SITES,
              site.id,
              HealthAuthSiteRegRoutes.VENDOR
            ].join('/'));
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
