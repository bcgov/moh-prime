import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';

import { BehaviorSubject } from 'rxjs';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthorityService } from '@health-auth/shared/services/health-authority.service';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthoritySiteFormStateService } from '@health-auth/shared/services/health-authority-site-form-state.service';
import { AbstractHealthAuthoritySiteRegistrationPage } from '@health-auth/shared/classes/abstract-health-authority-site-registration-page.class';
import { AdministratorFormState } from './administrator-form-state.class';

@Component({
  selector: 'app-administrator-page',
  templateUrl: './administrator-page.component.html',
  styleUrls: ['./administrator-page.component.scss']
})
export class AdministratorPageComponent extends AbstractHealthAuthoritySiteRegistrationPage implements OnInit {
  public formState: AdministratorFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public pharmanetAdministrators: BehaviorSubject<{ id: number, fullName: string }[]>;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected healthAuthoritySiteService: HealthAuthoritySiteService,
    protected healthAuthoritySiteFormStateService: HealthAuthoritySiteFormStateService,
    protected healthAuthorityResource: HealthAuthorityResource,
    private fb: FormBuilder,
    private healthAuthorityService: HealthAuthorityService,
    router: Router
  ) {
    super(dialog, formUtilsService, route, healthAuthoritySiteService, healthAuthoritySiteFormStateService, healthAuthorityResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.pharmanetAdministrators = new BehaviorSubject<{ id: number, fullName: string }[]>([]);
  }

  public onBack(): void {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.REMOTE_USERS;

    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance(): void {
    this.formState = this.healthAuthoritySiteFormStateService.administratorFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      throw new Error('No health authority site ID was provided');
    }

    const administrators = this.healthAuthorityService.healthAuthority.pharmanetAdministrators
      .map(({ id, firstName, lastName }: Contact) => ({ id, fullName: `${firstName} ${lastName}` }));
    this.pharmanetAdministrators.next(administrators);

    const site = this.healthAuthoritySiteService.site;
    this.isCompleted = site?.completed;
    this.healthAuthoritySiteFormStateService.setForm(site, !this.hasBeenSubmitted);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.TECHNICAL_SUPPORT);
  }
}
