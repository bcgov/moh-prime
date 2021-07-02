import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { BehaviorSubject, Subject } from 'rxjs';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { HealthAuthSiteRegFormStateService } from '@health-auth/shared/services/health-auth-site-reg-form-state.service';
import { AdministratorPageFormState } from './administrator-page-form-state.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

@Component({
  selector: 'app-administrator-page',
  templateUrl: './administrator-page.component.html',
  styleUrls: ['./administrator-page.component.scss']
})
export class AdministratorPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: AdministratorPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public administrators: BehaviorSubject<Contact[]>;
  public SiteRoutes = HealthAuthSiteRegRoutes;

  private site: HealthAuthoritySite;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private siteResource: HealthAuthorityResource,
    private siteService: HealthAuthSiteRegService,
    private formStateService: HealthAuthSiteRegFormStateService,
    route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
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

  public onSelect(contact: Contact) {
    if (!contact.physicalAddress) {
      contact.physicalAddress = new Address();
    }
    this.formState.form.patchValue(contact);
  }

  public onBack() {
    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.REMOTE_USERS;

    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.formStateService.administratorPageFormState;
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.formStateService.setForm(this.site, true);
    this.formState.form.markAsPristine();
  }

  protected onSubmitFormIsInvalid(): void {

  }

  protected performSubmission(): NoContent {
    const payload = this.formStateService.json;
    // return this.siteResource.updateSite(payload);
    return void 0;
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_OVERVIEW);
  }
}
