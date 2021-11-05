import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Address } from '@lib/models/address.model';
import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { PrivacyOfficerPageFormState } from './privacy-officer-page-form-state.class';

@Component({
  selector: 'app-privacy-officer-page',
  templateUrl: './privacy-officer-page.component.html',
  styleUrls: ['./privacy-officer-page.component.scss']
})
export class PrivacyOfficerPageComponent extends AbstractCommunitySiteRegistrationPage implements OnInit {
  public formState: PrivacyOfficerPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  private site: Site;

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
  }

  public onSelect(contact: Contact) {
    if (!contact.physicalAddress) {
      contact.physicalAddress = new Address();
    }
    this.formState.form.patchValue(contact);
    this.formState.form.markAsDirty();
  }

  public onBack() {
    const nextRoute = (!this.isCompleted)
      ? SiteRoutes.ADMINISTRATOR
      : SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(nextRoute);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.privacyOfficerFormState;
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.siteFormStateService.setForm(this.site, !this.hasBeenSubmitted);
    this.formState.form.markAsPristine();
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.TECHNICAL_SUPPORT;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
