import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { pipe } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';

import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { RegulatoryFormState } from './regulatory-form-state.class';
import { ConfigService } from '@config/config.service';

@Component({
  selector: 'app-regulatory-page',
  templateUrl: './regulatory-page.component.html',
  styleUrls: ['./regulatory-page.component.scss']
})
export class RegulatoryPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public formState: RegulatoryFormState;
  public routeUtils: RouteUtils;
  public enrollee: HttpEnrollee;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private fb: FormBuilder,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public routeBackTo() {
    this.routeUtils.routeRelativeTo(['./', PaperEnrolmentRoutes.CARE_SETTING]);
  }

  public onSubmit(): void {
    this.formState.removeIncompleteCertifications(true);
    super.onSubmit();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    this.formState.removeIncompleteCertifications(true);
  }

  protected createFormInstance(): void {
    this.formState = new RegulatoryFormState(this.fb);
    this.form = this.formState.form;
  }

  protected initForm() {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.formState.certifications.length) {
      this.formState.addEmptyCollegeCertification();
    }
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      return;
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        if (enrollee) {
          this.enrollee = enrollee;
          const {
            certifications
          } = enrollee;

          // Attempt to patch the form if not already patched
          this.formState.patchValue(certifications);
        }
      });
  }

  protected onSubmitFormIsValid() {
    // Enrollees can not have certifications and jobs
    this.removeJobs();
  }


  protected performSubmission(): NoContent {
    return;
    // // Update using the form which could contain changes, and ensure identity
    // const enrolment = this.paperEnrolmentFormStateService.json;
    // const enrollee = this.form.getRawValue();
    // // BCeID has to match BCSC for submission, which requires givenNames
    // const givenNames = `${enrollee.firstName} ${enrollee.middleName}`;
    //
    // if (!enrolment.id) {
    //   const payload = {
    //     enrollee: { ...enrollee, givenNames }
    //   };
    //   return this.paperEnrolmentResource.createEnrollee(payload)
    //     .pipe(
    //       // Merge the enrolment with generated keys
    //       map((newEnrolment: Enrolment) => {
    //         newEnrolment.enrollee = { ...newEnrolment.enrollee, ...enrolment.enrollee };
    //         return newEnrolment;
    //       }),
    //       // Populate generated keys within the form state
    //       tap((newEnrolment: Enrolment) => {
    //         this.paperEnrolmentFormStateService.setForm(newEnrolment, true);
    //         this.enrolment = newEnrolment;
    //       }),
    //       this.handleResponse()
    //     );
    // } else {
    //   enrolment.enrollee.givenNames = givenNames;
    //   return this.paperEnrolmentResource.updateEnrollee(enrolment)
    //     .pipe(this.handleResponse());
    // }
  }

  protected afterSubmitIsSuccessful() {
    this.formState.removeIncompleteCertifications(true);
    const certifications = this.formState.collegeCertifications;

    const nextRoutePath = (!certifications.length)
      ? PaperEnrolmentRoutes.OBO_SITES
      : PaperEnrolmentRoutes.SELF_DECLARATION;

    this.routeUtils.routeRelativeTo(['./', nextRoutePath]);
  }

  /**
   * @description
   * Remove obo sites/jobs from the enrolment as enrollees can not have
   * certificate(s), as well as, job(s).
   */
  private removeJobs() {
    this.formState.removeIncompleteCertifications(true);

    if (this.formState.certifications.length) {
      // this.paperEnrolmentFormStateService.jobsFormState.oboSites.clear();
    }
  }

}
