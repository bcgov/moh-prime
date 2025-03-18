import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { UntypedFormBuilder } from '@angular/forms';

import { BehaviorSubject } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';

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
  public isSubmitted: boolean;
  public pharmanetAdministrators: BehaviorSubject<{ id: number, fullName: string }[]>;
  public technicalSupports: BehaviorSubject<{ id: number, fullName: string }[]>;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected healthAuthoritySiteService: HealthAuthoritySiteService,
    protected healthAuthoritySiteFormStateService: HealthAuthoritySiteFormStateService,
    protected healthAuthoritySiteResource: HealthAuthoritySiteResource,
    private fb: UntypedFormBuilder,
    private healthAuthorityService: HealthAuthorityService,
    router: Router
  ) {
    super(dialog, formUtilsService, route, healthAuthoritySiteService, healthAuthoritySiteFormStateService, healthAuthoritySiteResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.pharmanetAdministrators = new BehaviorSubject<{ id: number, fullName: string }[]>([]);
    this.technicalSupports = new BehaviorSubject<{ id: number, fullName: string }[]>([]);
  }

  public onBack(): void {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HOURS_OPERATION;

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

    const technicalSupportContacts = this.healthAuthorityService.healthAuthority.technicalSupports
      .map(({ id, firstName, lastName }: Contact) => ({ id, fullName: `${firstName} ${lastName}` }));
    this.technicalSupports.next(technicalSupportContacts);

    const site = this.healthAuthoritySiteService.site;
    this.isCompleted = site?.completed;
    this.isSubmitted = site?.submittedDate ? true : false;
    this.healthAuthoritySiteFormStateService.setForm(site, !this.hasBeenSubmitted);
  }

  protected submissionRequest(): NoContent {
    const { haid, sid } = this.route.snapshot.params;

    return super.submissionRequest()
      .pipe(exhaustMap(() => this.healthAuthoritySiteResource.setHealthAuthoritySiteCompleted(haid, sid)));
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_OVERVIEW);
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Replace previous values on deactivation so updates are discarded
    this.healthAuthoritySiteFormStateService.patchAdmintratorForm(this.healthAuthoritySiteService.site);
  }
}
