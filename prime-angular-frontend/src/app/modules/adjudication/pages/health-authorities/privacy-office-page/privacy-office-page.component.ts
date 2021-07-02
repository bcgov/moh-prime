import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import {
  PrivacyOfficePageFormState
} from '@adjudication/pages/health-authorities/privacy-office-page/privacy-office-page-form-state.class';
import { NoContent } from '@core/resources/abstract-resource';

@Component({
  selector: 'app-privacy-officer-page',
  templateUrl: './privacy-office-page.component.html',
  styleUrls: ['./privacy-office-page.component.scss']
})
export class PrivacyOfficePageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: PrivacyOfficePageFormState;
  public isInitialEntry: boolean;
  public showAddressFields: boolean;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private healthAuthResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.isInitialEntry = !!route.snapshot.queryParams.initial;
    this.routeUtils = new RouteUtils(route, router, [
      AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS),
      AdjudicationRoutes.SITE_REGISTRATIONS,
      AdjudicationRoutes.HEALTH_AUTHORITIES,
      this.route.snapshot.params.haid
    ]);
  }

  public onBack(): void {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_VENDORS);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = new PrivacyOfficePageFormState(this.fb, this.formUtilsService);
  }

  protected patchForm(): void {
    this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe(({ privacyOffice }: HealthAuthority) => {
        if (privacyOffice) {
          this.formState.patchValue(privacyOffice);
          this.showAddressFields = true;
        }
      });
  }

  protected performSubmission(): NoContent {
    return this.healthAuthResource.updateHealthAuthorityPrivacyOffice(this.route.snapshot.params.haid, this.formState.json);
  }

  protected onSubmitFormIsInvalid(): void {
    this.showAddressFields = true;
  }

  protected afterSubmitIsSuccessful(_): void {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_TECHNICAL_SUPPORTS);
  }

  private routeTo(routeSegment?: string) {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
