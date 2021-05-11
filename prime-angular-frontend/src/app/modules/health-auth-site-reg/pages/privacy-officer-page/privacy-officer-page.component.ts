import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Subject } from 'rxjs';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';

import { HealthAuthSite } from '@health-auth/shared/models/health-auth-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { HealthAuthSiteRegResource } from '@health-auth/shared/resources/health-auth-site-reg-resource.service';
import { HealthAuthSiteRegFormStateService } from '@health-auth/shared/services/health-auth-site-reg-form-state.service';
import { PrivacyOfficerPageFormState } from './privacy-officer-page-form-state.class';

@Component({
  selector: 'app-privacy-officer-page',
  templateUrl: './privacy-officer-page.component.html',
  styleUrls: ['./privacy-officer-page.component.scss']
})
export class PrivacyOfficerPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: PrivacyOfficerPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public formSubmittingEvent: Subject<void>;
  public SiteRoutes = HealthAuthSiteRegRoutes;

  private site: HealthAuthSite;

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
    this.formSubmittingEvent = new Subject<void>();
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
    const routePath = (!this.isCompleted)
      ? HealthAuthSiteRegRoutes.ADMINISTRATOR
      : HealthAuthSiteRegRoutes.SITE_OVERVIEW;

    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.formStateService.privacyOfficerPageFormState;
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.formStateService.setForm(this.site, true);
    this.formState.form.markAsPristine();
  }

  protected performSubmission(): NoContent {
    const payload = this.formStateService.json;
    return this.siteResource.updateSite(payload);
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.TECHNICAL_SUPPORT;

    this.routeUtils.routeRelativeTo(routePath);
  }

  protected onSubmitFormIsInvalid(): void {
    //emit formSubmitting event
    this.formSubmittingEvent.next();
  }
}
