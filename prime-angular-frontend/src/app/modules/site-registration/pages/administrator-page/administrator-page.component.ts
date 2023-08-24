import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Address } from '@lib/models/address.model';
import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
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
export class AdministratorPageComponent extends AbstractCommunitySiteRegistrationPage implements OnInit {
  public formState: AdministratorPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public isSubmitted: boolean;
  public showAddressFields: boolean;
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
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public isCommunityPharmacy(): boolean {
    return this.site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST;
  }

  public onSelect(contact: Contact) {
    if (!contact.physicalAddress) {
      contact.physicalAddress = new Address();
    }
    this.formState.form.patchValue(contact);
    this.formState.form.markAsDirty();
  }

  public onBack() {
    let nextRoute: string;
    if (this.isCompleted) {
      nextRoute = SiteRoutes.SITE_REVIEW;
    } else {
      switch (this.siteService.site.careSettingCode) {
        case CareSettingEnum.COMMUNITY_PHARMACIST:
        case CareSettingEnum.DEVICE_PROVIDER:
          nextRoute = SiteRoutes.HOURS_OPERATION;
          break;
        default:
          nextRoute = SiteRoutes.REMOTE_USERS;
          break;
      }
    }

    this.routeUtils.routeRelativeTo(nextRoute);
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
    this.isSubmitted = this.site?.submittedDate ? true : false;
    this.siteFormStateService.setForm(this.site, !this.hasBeenSubmitted);
    this.formState.form.markAsPristine();
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.PRIVACY_OFFICER;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
