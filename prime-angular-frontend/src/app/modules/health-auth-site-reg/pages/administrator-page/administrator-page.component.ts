import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { BehaviorSubject, EMPTY } from 'rxjs';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { Address } from '@shared/models/address.model';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { AdministratorFormState } from './administrator-form-state.class';
import { exhaustMap, tap } from 'rxjs/operators';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-administrator-page',
  templateUrl: './administrator-page.component.html',
  styleUrls: ['./administrator-page.component.scss']
})
export class AdministratorPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: AdministratorFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public administrators: BehaviorSubject<Contact[]>;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private healthAuthorityResource: HealthAuthorityResource,
    private healthAuthoritySiteService: HealthAuthSiteRegService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public onSelect(contact: Contact) {
    // if (!contact.physicalAddress) {
    //   contact.physicalAddress = new Address();
    // }
    // this.formState.form.patchValue(contact);
  }

  public onBack() {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.REMOTE_USERS;

    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = new AdministratorFormState(this.fb, this.formUtilsService);
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      return;
    }

    // this.busy = this.healthAuthResource.getHealthAuthorityById(healthAuthId)
    //   .pipe(
    //     tap(({ careTypes }: HealthAuthority) => this.careTypes = careTypes),
    //     exhaustMap((_: HealthAuthority) =>
    //       (healthAuthSiteId)
    //         ? this.healthAuthResource.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
    //         : EMPTY
    //     )
    //   )
    //   .subscribe(({ careType, completed }: HealthAuthoritySite) => {
    //     this.isCompleted = completed;
    //     this.formState.patchValue({ careType });
    //   });
  }

  protected onSubmitFormIsInvalid(): void {

  }

  protected performSubmission(): NoContent {

    return void 0;
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_OVERVIEW);
  }
}
