import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentResource } from '@gis/shared/resources/gis-enrolment-resource.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { OrganizationInformationPageFormState } from './organization-information-page-form-state.class';

@Component({
  selector: 'app-organization-information-page',
  templateUrl: './organization-information-page.component.html',
  styleUrls: ['./organization-information-page.component.scss']
})
export class OrganizationInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: OrganizationInformationPageFormState;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private formStateService: GisEnrolmentFormStateService,
    private gisEnrolmentResource: GisEnrolmentResource,
    route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.MODULE_PATH));
  }

  public get organization(): FormControl {
    return this.form.get('organization') as FormControl;
  }

  public get role(): FormControl {
    return this.form.get('role') as FormControl;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([`./${ GisEnrolmentRoutes.LDAP_INFO_PAGE }`]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.organizationInformationPageFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {
    throw new Error('Method not implemented.');
  }

  protected initForm(): void {
    throw new Error('Method not implemented.');
  }

  protected performSubmission(): NoContent {
    return this.gisEnrolmentResource.updateEnrolment(this.formStateService.json);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo([`./${ GisEnrolmentRoutes.ENROLLEE_INFO_PAGE }`]);
  }
}
