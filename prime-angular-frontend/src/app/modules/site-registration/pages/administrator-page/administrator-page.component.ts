import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { AdministratorPageFormState } from './administrator-page-form-state.class';

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
  public showAddressFields: boolean;
  public SiteRoutes = SiteRoutes;

  private site: Site;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onSelect(contact: Contact) {
    if (!contact.physicalAddress) {
      contact.physicalAddress = new Address();
    }
    this.formState.form.patchValue(contact);
  }

  public onBack() {
    const site = this.siteService.site;
    let routePath = SiteRoutes.REMOTE_USERS;

    if (site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST) {
      routePath = SiteRoutes.HOURS_OPERATION;
    }

    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.administratorPharmaNetFormState;
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.siteFormStateService.setForm(this.site, true);
    this.formState.form.markAsPristine();
  }

  protected onSubmitFormIsInvalid(): void {
    this.showAddressFields = true;
  }

  protected performSubmission(): NoContent {
    const payload = this.siteFormStateService.json;
    return this.siteResource.updateSite(payload);
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.PRIVACY_OFFICER;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
