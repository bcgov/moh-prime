import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { NoContent } from '@core/resources/abstract-resource';
import { AddressLine } from '@shared/models/address.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
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
  public SiteRoutes = SiteRoutes;
  public showAddressFields: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);

    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public get name(): FormGroup {
    return this.form.get('name') as FormGroup;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.BUSINESS_LICENCE);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.siteAddressPageFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // Force the site to be patched each time
    this.siteFormStateService.setForm(site, true);
  }

  protected initForm() {
    throw new Error('Not implemented');
  }

  protected onSubmitFormIsInvalid(): void {
    this.showAddressFields = true;
  }

  protected performSubmission(): NoContent {
    const payload = this.siteFormStateService.json;
    return this.siteResource.updateSite(payload)
      .pipe(tap(() => this.form.markAsPristine()));
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.HOURS_OPERATION;
    this.routeUtils.routeRelativeTo(routePath);
  }
}
