import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';

import { forkJoin, merge, of } from 'rxjs';
import { catchError, exhaustMap, map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { UtilsService } from '@core/services/utils.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

import { NextStepsFormState } from '@paper-enrolment/pages/next-steps-page/next-steps-form-state.class';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';

@Component({
  selector: 'app-next-steps',
  templateUrl: './next-steps-page.component.html',
  styleUrls: ['./next-steps-page.component.scss']
})
export class NextStepsPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: NextStepsFormState;
  public enrollee: HttpEnrollee;
  public routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private utilsService: UtilsService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onNext() {
    this.routeUtils.routeRelativeTo(['../', 0, PaperEnrolmentRoutes.DEMOGRAPHIC]);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['/', AdjudicationRoutes.MODULE_PATH]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = new NextStepsFormState(this.fb);
  }

  protected patchForm(): void {
    this.paperEnrolmentResource.getEnrolleeById(+this.route.snapshot.params.eid)
      .subscribe((enrollee: HttpEnrollee) => this.enrollee = enrollee);
  }

  protected performSubmission(): any {
    this.formState.emails.markAsPristine();

    const payload = this.formState.json.emails;
    const requests$ = this.enrollee.enrolleeCareSettings
      .map(ecs => ecs.careSettingCode)
      .map(ecsc => this.paperEnrolmentResource.sendProvisionerAccessLink(payload, this.enrollee.id, ecsc));

    return merge(...requests$);
  }

  protected afterSubmitIsSuccessful() {
    this.formState.emails.reset();
    this.formState.emails.markAsPristine();
  }
}
