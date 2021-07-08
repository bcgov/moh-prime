import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';

import { BehaviorSubject, EMPTY } from 'rxjs';
import { exhaustMap, tap } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { AdministratorFormState } from './administrator-form-state.class';

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
  public pharmanetAdministrators: BehaviorSubject<{ id: number, fullName: string }[]>;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private healthAuthorityResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.pharmanetAdministrators = new BehaviorSubject<{ id: number, fullName: string }[]>([]);
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
    this.initForm();
  }

  protected createFormInstance() {
    this.formState = new AdministratorFormState(this.fb);
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      return;
    }

    this.busy = this.healthAuthorityResource.getHealthAuthorityById(healthAuthId)
      .pipe(
        tap(({ pharmanetAdministrators }: HealthAuthority) => {
          const administrators = pharmanetAdministrators
            .map(({ id, firstName, lastName }: Contact) => ({ id, fullName: `${firstName} ${lastName}` }));
          this.pharmanetAdministrators.next(administrators);
        }),
        exhaustMap((_: HealthAuthority) =>
          (healthAuthSiteId)
            ? this.healthAuthorityResource.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
            : EMPTY
        )
      )
      .subscribe(({ healthAuthorityPharmanetAdministratorId, healthAuthorityPharmanetAdministrator, completed }: HealthAuthoritySite) => {
        this.isCompleted = completed;
        // TODO change this to something shorter: pharmanetAdministratorId and pharmanetAdministrator
        this.formState.patchValue({
          pharmanetAdministratorId: healthAuthorityPharmanetAdministratorId,
          pharmanetAdministrator: healthAuthorityPharmanetAdministrator
        });
      });
  }

  protected performSubmission(): NoContent {
    const payload = this.formState.json;
    const { haid, sid } = this.route.snapshot.params;

    return this.healthAuthorityResource.updateHealthAuthorityPharmanetAdministrator(haid, sid, payload)
      .pipe(exhaustMap(() => this.healthAuthorityResource.healthAuthoritySiteCompleted(haid, sid)));
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_OVERVIEW);
  }
}
