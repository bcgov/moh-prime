import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
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
  public vendorCodes: Observable<number[]>;
  public isCompleted: boolean;
  public hasNoVendorError: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private location: Location,
    private configService: ConfigService,
    private healthAuthResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.hasNoVendorError = false;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_MANAGEMENT);
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
    this.formState = new VendorPageFormState(this.fb);
  }

  protected patchForm(): void {
    const siteId = +this.route.snapshot.params.eid;
    if (!siteId) {
      return;
    }

    this.healthAuthResource.getHealthAuthoritySiteById(+this.route.snapshot.params.haid, siteId)
      .pipe(
        map(({ vendorCode }: HealthAuthoritySite) =>
            console.log(vendorCode)
          // this.formState.patchValue({ vendorCode })
        )
      );
  }

  protected performSubmission(): Observable<number> {
    this.formState.form.markAsPristine();

    const payload = this.formState.json;
    const { haid, sid } = this.route.snapshot.params;

    return (+sid)
      ? this.healthAuthResource.updateHealthAuthoritySiteVendor(haid, sid, payload)
        .pipe(map(() => sid))
      : this.healthAuthResource.createHealthAuthoritySite(haid, payload)
        .pipe(
          map((site: HealthAuthoritySite) => {
            // Replace the URL with redirection, and prevent initial
            // ID of zero being pushed onto browser history
            this.location.replaceState([HealthAuthSiteRegRoutes.MODULE_PATH, site.id, HealthAuthSiteRegRoutes.VENDOR].join('/'));
            return site.id;
          })
        );
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.SITE_INFORMATION;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
