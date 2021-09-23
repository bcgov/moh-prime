import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';

import { exhaustMap } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { SatEformsEnrollee } from '@sat/shared/models/sat-enrollee.model';
import { RegulatoryFormState } from '@sat/pages/regulatory-page/regulatory-form-state';
import { SatEformsEnrolmentResource } from '@sat/shared/resource/sat-eforms-enrolment-resource.service';

@Component({
  selector: 'app-regulatory-page',
  templateUrl: './regulatory-page.component.html',
  styleUrls: ['./regulatory-page.component.scss']
})
export class RegulatoryPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public title: string;
  public formState: RegulatoryFormState;
  public routeUtils: RouteUtils;
  public enrollee: SatEformsEnrollee;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private fb: FormBuilder,
    private enrolmentResource: SatEformsEnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SatEformsRoutes.MODULE_PATH);
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(SatEformsRoutes.DEMOGRAPHIC);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  public ngOnDestroy(): void {
    this.formState.removeIncompleteCertifications(true);
  }

  protected createFormInstance(): void {
    this.formState = new RegulatoryFormState(this.fb, this.configService);
  }

  protected initForm(): void {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.formState.certifications.length) {
      this.formState.addEmptyCollegeCertification();
    }
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      throw new Error('No enrollee ID was provided');
    }

    this.enrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        if (enrollee) {
          this.enrollee = enrollee;
          this.formState.patchValue(enrollee.certifications);
        }
      });
  }

  protected performSubmission(): NoContent {
    const enrolleeId = +this.route.snapshot.params.eid;
    this.formState.removeIncompleteCertifications(true);
    return this.enrolmentResource.updateCertifications(enrolleeId, this.formState.json)
      .pipe(
        exhaustMap(() => this.enrolmentResource.finalize(enrolleeId))
      );
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(SatEformsRoutes.SUBMISSION_CONFIRMATION);
  }
}

