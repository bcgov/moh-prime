import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';

import { BehaviorSubject, EMPTY } from 'rxjs';
import { exhaustMap, tap } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthorityFormStateService } from '@health-auth/shared/services/health-authority-form-state.service';
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
    protected siteService: HealthAuthoritySiteService,
    protected formStateService: HealthAuthorityFormStateService,
    protected healthAuthorityResource: HealthAuthorityResource,
    private fb: FormBuilder,
    router: Router
  ) {
    super(dialog, formUtilsService, route, siteService, formStateService, healthAuthorityResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    // TODO revisit passed subject value type
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
    this.formState = this.formStateService.administratorFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      throw new Error('No health authority site ID was provided');
    }

    // this.busy = this.healthAuthorityResource.getHealthAuthorityById(healthAuthId)
    //   .pipe(
    //     tap(({ pharmanetAdministrators }: HealthAuthority) => {
    //       const administrators = pharmanetAdministrators
    //         .map(({ id, firstName, lastName }: Contact) => ({ id, fullName: `${firstName} ${lastName}` }));
    //       this.pharmanetAdministrators.next(administrators);
    //     }),
    //     exhaustMap((_: HealthAuthority) =>
    //       (healthAuthSiteId)
    //         ? this.healthAuthorityResource.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
    //         : EMPTY
    //     )
    //   )
    //   .subscribe(({ healthAuthorityPharmanetAdministratorId, completed }: HealthAuthoritySite) => {
    //     this.isCompleted = completed;
    //     this.formState.patchValue({ healthAuthorityPharmanetAdministratorId });
    //   });

    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.formStateService.setForm(site, !this.hasBeenSubmitted);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.TECHNICAL_SUPPORT);
  }
}
