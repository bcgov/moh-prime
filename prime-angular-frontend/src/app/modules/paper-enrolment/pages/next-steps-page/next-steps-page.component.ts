import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { MatDialog } from '@angular/material/dialog';
import { FormUtilsService } from '@core/services/form-utils.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { NextStepsFormState } from '@paper-enrolment/pages/next-steps-page/next-steps-form-state.class';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { exhaustMap, map } from 'rxjs/operators';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { merge, Observable } from 'rxjs';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';

@Component({
  selector: 'app-next-steps',
  templateUrl: './next-steps-page.component.html',
  styleUrls: ['./next-steps-page.component.scss']
})
export class NextStepsPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: NextStepsFormState;
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

  public onSubmit() {
    this.routeUtils.routeRelativeTo(['../', 0, PaperEnrolmentRoutes.DEMOGRAPHIC]);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['/', AdjudicationRoutes.MODULE_PATH]);
  }

  public ngOnInit(): void { }

  protected createFormInstance(): void {
    this.formState = new NextStepsFormState(this.fb);
  }

  protected patchForm(): void {
    this.paperEnrolmentResource.getEnrolleeById(+this.route.snapshot.params.eid)
      .subscribe(() => {

      });
  }

  protected performSubmission(): NoContent {
    const payload = this.formState.json.emails;
    return this.paperEnrolmentResource.getEnrolleeById(+this.route.snapshot.params.eid)
      .pipe(
        map((enrollee: HttpEnrollee) => enrollee.enrolleeCareSettings.map(ecs => ecs.careSettingCode)),
        exhaustMap((enrolleeCareSettingCodes: number[]) =>
          merge(enrolleeCareSettingCodes.map(ecsc => this.paperEnrolmentResource.sendProvisionerAccessLink(payload, ecsc)))
        ),
        NoContentResponse
      );
  }
}
