import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AddressLine } from '@lib/models/address.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { AbstractSiteRegistrationPage } from '@registration/shared/classes/abstract-site-registration-page.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteAddressPageFormState } from './site-address-page-form-state.class';

@Component({
  selector: 'app-site-address-page',
  templateUrl: './site-address-page.component.html',
  styleUrls: ['./site-address-page.component.scss']
})
export class SiteAddressPageComponent extends AbstractSiteRegistrationPage implements OnInit {
  public formState: SiteAddressPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public formControlNames: AddressLine[];
  public isCompleted: boolean;
  public showAddressFields: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected siteService: SiteService,
    protected siteFormStateService: SiteFormStateService,
    protected siteResource: SiteResource,
    route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService, siteService, siteFormStateService, siteResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);

    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public onBack() {
    const nextRoute = (!this.isCompleted)
      ? SiteRoutes.BUSINESS_LICENCE
      : SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(nextRoute);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.siteAddressPageFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, !this.hasBeenSubmitted);
    this.formState.form.markAsPristine();
  }

  protected initForm() {
    throw new Error('Not implemented');
  }

  protected onSubmitFormIsInvalid(): void {
    this.showAddressFields = true;
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.HOURS_OPERATION;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
