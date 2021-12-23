import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentService } from '@gis/shared/services/gis-enrolment.service';
import { GisEnrolmentResource } from '@gis/shared/resources/gis-enrolment-resource.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { EnrolleeInformationPageFormState } from './enrollee-information-page-form-state.class';

@Component({
  selector: 'app-enrollee-information-page',
  templateUrl: './enrollee-information-page.component.html',
  styleUrls: ['./enrollee-information-page.component.scss']
})
export class EnrolleeInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: EnrolleeInformationPageFormState;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private formStateService: GisEnrolmentFormStateService,
    private gisEnrolmentService: GisEnrolmentService,
    private gisEnrolmentResource: GisEnrolmentResource,
    route: ActivatedRoute,
    router: Router,
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.MODULE_PATH));
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([`./${ GisEnrolmentRoutes.LDAP_INFO_PAGE }`]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.enrolleeInformationPageFormState;
  }

  protected patchForm(): void {
    this.formStateService.setForm(this.gisEnrolmentService.enrolment);
  }

  protected initForm(): void { } // NOOP

  protected performSubmission(): NoContent {
    return this.gisEnrolmentResource.updateEnrolment(this.formStateService.json)
      .pipe(
        exhaustMap(() => this.gisEnrolmentResource.submission(this.gisEnrolmentService.enrolment.id))
      );
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo([`./${ GisEnrolmentRoutes.SUBMISSION_CONFIRMATION }`]);
  }
}
