import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { Address, AddressLine } from '@lib/models/address.model';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthorityFormStateService } from '@health-auth/shared/services/health-authority-form-state.service';
import { AbstractHealthAuthoritySiteRegistrationPage } from '@health-auth/shared/classes/abstract-health-authority-site-registration-page.class';
import { SiteAddressFormState } from './site-address-form-state.class';

@Component({
  selector: 'app-site-address-page',
  templateUrl: './site-address-page.component.html',
  styleUrls: ['./site-address-page.component.scss']
})
export class SiteAddressPageComponent extends AbstractHealthAuthoritySiteRegistrationPage implements OnInit {
  public formState: SiteAddressFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public formControlNames: AddressLine[];
  public isCompleted: boolean;
  public showAddressFields: boolean;
  public SiteRoutes = HealthAuthSiteRegRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected siteService: HealthAuthoritySiteService,
    protected formStateService: HealthAuthorityFormStateService,
    protected healthAuthorityResource: HealthAuthorityResource,
    private fb: FormBuilder,
    router: Router
  ) {
    super(dialog, formUtilsService, route, siteService, formStateService, healthAuthorityResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);

    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public onBack(): void {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE;

    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.siteAddressFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      throw new Error('No health authority site ID was provided');
    }

    const site = this.siteService.site;

    if (Address.isNotEmpty(site.physicalAddress, ['countryCode', 'provinceCode'])) {
      this.showAddressFields = true;
    }

    this.isCompleted = site?.completed;
    this.formStateService.setForm(site, !this.hasBeenSubmitted);
  }

  protected onSubmitFormIsInvalid(): void {
    this.showAddressFields = true;
  }

  protected afterSubmitIsSuccessful(): void {
    const nextRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HOURS_OPERATION;

    this.routeUtils.routeRelativeTo(nextRoutePath);
  }
}
