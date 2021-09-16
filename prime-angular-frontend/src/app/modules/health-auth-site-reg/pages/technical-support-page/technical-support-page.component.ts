import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { BehaviorSubject, EMPTY } from 'rxjs';
import { exhaustMap, tap } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { TechnicalSupportFormState } from '@health-auth/pages/technical-support-page/technical-support-form-state.class';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

@Component({
  selector: 'app-technical-support-page',
  templateUrl: './technical-support-page.component.html',
  styleUrls: ['./technical-support-page.component.scss']
})
export class TechnicalSupportPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: TechnicalSupportFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public technicalSupports: BehaviorSubject<{ id: number, fullName: string }[]>;

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
    // TODO revisit passed subject value type
    this.technicalSupports = new BehaviorSubject<{ id: number, fullName: string }[]>([]);
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
    this.formState = new TechnicalSupportFormState(this.fb);
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
          this.technicalSupports.next(administrators);
        }),
        exhaustMap((_: HealthAuthority) =>
          (healthAuthSiteId)
            ? this.healthAuthorityResource.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
            : EMPTY
        )
      )
      .subscribe(({ healthAuthorityTechnicalSupportId, completed }: HealthAuthoritySite) => {
        this.isCompleted = completed;
        this.formState.patchValue({ healthAuthorityTechnicalSupportId });
      });
  }
}
