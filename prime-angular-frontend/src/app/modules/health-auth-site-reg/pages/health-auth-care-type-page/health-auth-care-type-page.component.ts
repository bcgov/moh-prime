import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';

import { EMPTY, Observable } from 'rxjs';
import { exhaustMap, tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthFormStateService } from '@health-auth/shared/services/health-auth-form-state.service';
import { HealthAuthCareTypeFormState } from './health-auth-care-type-form-state.class';

@Component({
  selector: 'app-health-auth-care-type-page',
  templateUrl: './health-auth-care-type-page.component.html',
  styleUrls: ['./health-auth-care-type-page.component.scss']
})
export class HealthAuthCareTypePageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: HealthAuthCareTypeFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public careTypes: string[];
  public isCompleted: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private configService: ConfigService,
    private healthAuthorityResource: HealthAuthorityResource,
    private formStateService: HealthAuthFormStateService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public onBack(): void {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.SITE_INFORMATION;

    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.formStateService.healthAuthCareTypeFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      throw new Error('No health authority site ID was provided');
    }

    this.busy = this.healthAuthorityResource.getHealthAuthorityById(healthAuthId)
      .pipe(
        tap(({ careTypes }: HealthAuthority) => this.careTypes = careTypes),
        exhaustMap((_: HealthAuthority) =>
          (healthAuthSiteId)
            ? this.healthAuthorityResource.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
            : EMPTY
        )
      )
      .subscribe(({ healthAuthorityCareTypeCode, completed }: HealthAuthoritySite) => {
        this.isCompleted = completed;
        this.formState.patchValue({ healthAuthorityCareTypeCode });
      });
  }

  protected performSubmission(): NoContent {
    const { haid, sid } = this.route.snapshot.params;

    return this.healthAuthorityResource.updateHealthAuthoritySite(haid, sid, this.formStateService.json);
  }

  protected afterSubmitIsSuccessful(): void {
    const nextRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.SITE_ADDRESS;

    this.routeUtils.routeRelativeTo(nextRoutePath);
  }
}
