import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

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
import { TechnicalSupportFormState } from './technical-support-form-state.class';

@Component({
  selector: 'app-technical-support-page',
  templateUrl: './technical-support-page.component.html',
  styleUrls: ['./technical-support-page.component.scss']
})
export class TechnicalSupportPageComponent extends AbstractHealthAuthoritySiteRegistrationPage implements OnInit {
  public formState: TechnicalSupportFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public technicalSupports: BehaviorSubject<{ id: number, fullName: string }[]>;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected healthAuthoritySiteService: HealthAuthoritySiteService,
    protected healthAuthoritySiteFormStateService: HealthAuthoritySiteFormStateService,
    protected healthAuthoritySiteResource: HealthAuthoritySiteResource,
    private fb: FormBuilder,
    private healthAuthorityService: HealthAuthorityService,
    router: Router
  ) {
    super(dialog, formUtilsService, route, healthAuthoritySiteService, healthAuthoritySiteFormStateService, healthAuthoritySiteResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.technicalSupports = new BehaviorSubject<{ id: number, fullName: string }[]>([]);
  }

  public onBack(): void {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.ADMINISTRATOR;

    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance(): void {
    this.formState = this.healthAuthoritySiteFormStateService.technicalSupportFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      throw new Error('No health authority site ID was provided');
    }

    const technicalSupportContacts = this.healthAuthorityService.healthAuthority.technicalSupports
      .map(({ id, firstName, lastName }: Contact) => ({ id, fullName: `${firstName} ${lastName}` }));
    this.technicalSupports.next(technicalSupportContacts);

    const site = this.healthAuthoritySiteService.site;
    this.isCompleted = site?.completed;
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
}
