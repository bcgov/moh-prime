import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder } from '@angular/forms';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthorityCareType } from '@health-auth/shared/models/health-authority-care-type.model';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthoritySiteFormStateService } from '@health-auth/shared/services/health-authority-site-form-state.service';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { AbstractHealthAuthoritySiteRegistrationPage } from '@health-auth/shared/classes/abstract-health-authority-site-registration-page.class';
import { HealthAuthCareTypeFormState } from './health-auth-care-type-form-state.class';
import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';

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
  public vendors: HealthAuthorityVendor[];
  public hasNoVendorError: boolean;
  public isCompleted: boolean;
  public isSubmitted: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected healthAuthoritySiteService: HealthAuthoritySiteService,
    protected healthAuthoritySiteFormStateService: HealthAuthoritySiteFormStateService,
    protected healthAuthoritySiteResource: HealthAuthoritySiteResource,
    private fb: UntypedFormBuilder,
    private location: Location,
    private configService: ConfigService,
    private authorizedUserService: AuthorizedUserService,
    router: Router
  ) {
    super(dialog, formUtilsService, route, healthAuthoritySiteService, healthAuthoritySiteFormStateService, healthAuthoritySiteResource);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH, location);
  }

  public onBack(): void {
    (this.isCompleted)
      ? this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_OVERVIEW)
      : this.routeUtils.routeTo(HealthAuthSiteRegRoutes.routePath(HealthAuthSiteRegRoutes.SITE_MANAGEMENT));
  }

  public ngOnInit(): void {
    this.createFormInstance();

    // Add handler then ...
    this.formState.healthAuthorityCareTypeId.valueChanges
      .pipe(
        map((selectedCareType: number) =>
          this.healthAuthorityCareTypes.find((haCareType: HealthAuthorityCareType) => haCareType.id == selectedCareType).vendors
        )
      )
      .subscribe((filteredVendors: HealthAuthorityVendor[]) => {
        this.vendors = filteredVendors;
        // Clear any vendor selection since changed care type
        this.formState.healthAuthorityVendorId.patchValue(null);
      });

    // ... patch form (invoking handler)
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.healthAuthoritySiteFormStateService.healthAuthCareTypeFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    if (!healthAuthId) {
      // Don't throw an error as new registrations are created in this view
      return;
    }

    const site = this.healthAuthoritySiteService.site;
    this.healthAuthorityCareTypes = this.route.snapshot.data.healthAuthority?.careTypes ?? [];
    this.isCompleted = site?.completed;
    this.isSubmitted = site?.submittedDate ? true : false;
    this.healthAuthoritySiteFormStateService.setForm(site, !this.hasBeenSubmitted);
  }

  protected submissionRequest(): Observable<unknown> {
    const { haid, sid } = this.route.snapshot.params;
    const healthAuthoritySite = this.healthAuthoritySiteFormStateService.json;

    return (+sid)
      ? this.healthAuthoritySiteResource
        .updateHealthAuthoritySite(+haid, +sid, healthAuthoritySite.forUpdate())
        .pipe(map(() => +sid))
      : this.healthAuthoritySiteResource
        .createHealthAuthoritySite(+haid, healthAuthoritySite.forCreate(this.authorizedUserService.authorizedUser.id))
        .pipe(
          map((site: HealthAuthoritySite) => {
            this.routeUtils.replaceState([
              HealthAuthSiteRegRoutes.MODULE_PATH,
              HealthAuthSiteRegRoutes.HEALTH_AUTHORITIES,
              +haid,
              HealthAuthSiteRegRoutes.SITES,
              site.id,
              HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE
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

  protected onSubmitFormIsValid(): void {
    this.hasNoVendorError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.formState.healthAuthorityVendorId.value) {
      this.hasNoVendorError = true;
    }
  }
}
