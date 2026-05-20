import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, Validators } from '@angular/forms';


import { RouteUtils } from '@lib/utils/route-utils.class';
import { Address, AddressLine } from '@lib/models/address.model';
import { CareTypeConfig, Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthoritySiteFormStateService } from '@health-auth/shared/services/health-authority-site-form-state.service';
import { AbstractHealthAuthoritySiteRegistrationPage } from '@health-auth/shared/classes/abstract-health-authority-site-registration-page.class';
import { SiteInformationFormState } from './site-information-form-state.class';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { SiteResource } from '@core/resources/site-resource.service';
import { Observable, of } from 'rxjs';
import { asyncValidator } from '@lib/validators/form-async.validators';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

@Component({
  selector: 'app-site-information-page',
  templateUrl: './site-information-page.component.html',
  styleUrls: ['./site-information-page.component.scss'],
  standalone: false
})
export class SiteInformationPageComponent extends AbstractHealthAuthoritySiteRegistrationPage implements OnInit {
  public formState: SiteInformationFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public securityGroups: Config<number>[];
  public careTypes: CareTypeConfig[];
  public formControlNames: AddressLine[];
  public isCompleted: boolean;
  public isSubmitted: boolean;
  public showAddressFields: boolean;
  public site: HealthAuthoritySite;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected healthAuthoritySiteService: HealthAuthoritySiteService,
    protected healthAuthoritySiteFormStateService: HealthAuthoritySiteFormStateService,
    protected healthAuthoritySiteResource: HealthAuthoritySiteResource,
    protected siteResource: SiteResource,
    private fb: UntypedFormBuilder,
    private configService: ConfigService,
    router: Router
  ) {
    super(dialog, formUtilsService, route, healthAuthoritySiteService, healthAuthoritySiteFormStateService, healthAuthoritySiteResource);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);

    this.securityGroups = this.configService.securityGroups;
    this.careTypes = this.configService.careTypes;
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

    //add validation to PEC
    this.formState.pec.addAsyncValidators(asyncValidator(this.checkPecIsAssignable(), 'assignable'));
    if (this.isPharmacyPhoneRequired()) {
      this.formState.pharmacyPhone.setValidators(Validators.required);
    }
  }

  protected createFormInstance(): void {
    this.formState = this.healthAuthoritySiteFormStateService.siteInformationFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      throw new Error('No health authority site ID was provided');
    }

    this.site = this.healthAuthoritySiteService.site;
    if (Address.isNotEmpty(this.site.physicalAddress, ['countryCode', 'provinceCode'])) {
      this.showAddressFields = true;
    }
    this.isCompleted = this.site?.completed;
    this.isSubmitted = this.site?.submittedDate ? true : false;
    this.healthAuthoritySiteFormStateService.setForm(this.site, !this.hasBeenSubmitted);
  }

  public onToggleChange($event: MatSlideToggleChange) {
    if (!$event.checked) {
      this.formUtilsService.resetAndClearValidators(this.formState.pec);
      this.formState.patchValue({ pec: null, pharmacyPhone: this.formState.pharmacyPhone.value, siteName: this.formState.siteName.value, securityGroupCode: this.formState.securityGroupCode.value, mnemonic: this.formState.mnemonic.value, physicalAddress: this.formState.physicalAddress.value }, this.route.snapshot.params.sid);
    } else {
      this.formUtilsService.setValidators(this.formState.pec, [Validators.required]);
    }
  }

  protected afterSubmitIsSuccessful(): void {
    const nextRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HOURS_OPERATION;

    this.routeUtils.routeRelativeTo(nextRoutePath);
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Replace previous values on deactivation so updates are discarded
    this.healthAuthoritySiteFormStateService.patchSiteInformationForm(this.healthAuthoritySiteService.site);
  }

  protected onSubmitFormIsInvalid(): void {
    this.showAddressFields = true;
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => value ? this.siteResource.pecAssignable(this.route.snapshot.params.sid, value) : of(true);
  }

  public isPharmacyPhoneRequired(): boolean {
    return this.careTypes.find(ct => ct.name === this.site?.healthAuthorityCareType.careType)?.pharmacyPhoneRequired;
  }
}
