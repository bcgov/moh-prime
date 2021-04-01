import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { AddressLine } from '@shared/models/address.model';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { HealthAuthSiteRegResource } from '@health-auth/shared/resources/health-auth-site-reg-resource.service';
import { HealthAuthSiteRegFormStateService } from '@health-auth/shared/services/health-auth-site-reg-form-state.service';
import { SiteAddressPageFormState } from './site-address-page-form-state.class';

@Component({
  selector: 'app-site-address-page',
  templateUrl: './site-address-page.component.html',
  styleUrls: ['./site-address-page.component.scss']
})
export class SiteAddressPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: SiteAddressPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public formControlNames: AddressLine[];
  public isCompleted: boolean;
  public showAddressFields: boolean;
  public SiteRoutes = HealthAuthSiteRegRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private siteResource: HealthAuthSiteRegResource,
    private siteService: HealthAuthSiteRegService,
    private formStateService: HealthAuthSiteRegFormStateService,
    route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);

    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  // TODO remove this method add to allow routing between pages
  public onSubmit() {
    this.hasAttemptedSubmission = true;

    if (this.checkValidity(this.formState.form)) {
      this.onSubmitFormIsValid();
      this.afterSubmitIsSuccessful();
    } else {
      this.onSubmitFormIsInvalid();
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_INFORMATION);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.formStateService.siteAddressPageFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // Force the site to be patched each time
    this.formStateService.setForm(site, true);
    this.formState.form.markAsPristine();
  }

  protected initForm() {
    throw new Error('Not implemented');
  }

  protected onSubmitFormIsInvalid(): void {
    this.showAddressFields = true;
  }

  protected performSubmission(): NoContent {
    const payload = this.formStateService.json;
    return this.siteResource.updateSite(payload)
      .pipe(tap(() => this.formState.form.markAsPristine()));
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HOURS_OPERATION;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
