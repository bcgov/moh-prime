import { Component, OnInit } from '@angular/core';
import { FormArray, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { optionalAddressLineItems } from '@shared/models/address.model';
import { Enrolment } from '@shared/models/enrolment.model';
import { pipe } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';
import { RegulatoryPaperEnrolmentFormState } from './regulatory-paper-enrolment-form-state.class';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent extends AbstractEnrolmentPage implements OnInit {
  public form: FormGroup;
  public formState: RegulatoryPaperEnrolmentFormState;
  public enrolment: Enrolment;
  public routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected paperEnrolmentService: PaperEnrolmentService,
    protected paperEnrolmentResource: PaperEnrolmentResource,
    protected paperEnrolmentFormStateService: PaperEnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
  ) {
    super(dialog, formUtilsService);

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public get certifications(): FormArray {
    return this.formState.certifications as FormArray;
  }

  public get selectedCollegeCodes(): number[] {
    return this.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  public addEmptyCollegeCertification() {
    this.formState.addCollegeCertification();
  }

  /**
   * @description
   * Removes a certification from the list in response to an
   * emitted event from college certifications. Does not allow
   * the list of certifications to empty.
   *
   * @param index to be removed
   */
  public removeCertification(index: number) {
    this.certifications.removeAt(index);
  }

  public routeBackTo() {
    // this.routeTo(['../', this.enrolment.id, PaperEnrolmentRoutes.CARE_SETTING]);
    this.routeUtils.routeRelativeTo(['../', '1', PaperEnrolmentRoutes.CARE_SETTING]);
  }

  public onSubmit(): void {
    this.nextRouteAfterSubmit();

    // if (this.formUtilsService.checkValidity(this.form)) {
    //   this.performSubmission();
    // } else {
    //   this.utilService.scrollToErrorSection();
    // }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteCertifications(true);
  }

  protected createFormInstance(): void {
    this.formState = this.paperEnrolmentFormStateService.regulatoryFormState;
    this.form = this.formState.form;
  }

  protected initForm() {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.certifications.length) {
      this.addEmptyCollegeCertification();
    }
  }

  protected patchForm(): void {
    // Will be null if enrolment has not been created
    const enrolment = this.paperEnrolmentService.enrolment;
    this.paperEnrolmentFormStateService.setForm(enrolment);
  }

  protected onSubmitFormIsValid() {
    // Enrollees can not have certifications and jobs
    this.removeJobs();
  }

  protected afterSubmitIsSuccessful() {
    this.removeIncompleteCertifications(true);
  }

  protected performSubmission(): NoContent {
    // Update using the form which could contain changes, and ensure identity
    const enrolment = this.paperEnrolmentFormStateService.json;
    const enrollee = this.form.getRawValue();
    // BCeID has to match BCSC for submission, which requires givenNames
    const givenNames = `${enrollee.firstName} ${enrollee.middleName}`;

    if (!enrolment.id) {
      const payload = {
        enrollee: { ...enrollee, givenNames }
      };
      return this.paperEnrolmentResource.createEnrollee(payload)
        .pipe(
          // Merge the enrolment with generated keys
          map((newEnrolment: Enrolment) => {
            newEnrolment.enrollee = { ...newEnrolment.enrollee, ...enrolment.enrollee };
            return newEnrolment;
          }),
          // Populate generated keys within the form state
          tap((newEnrolment: Enrolment) => {
            this.paperEnrolmentFormStateService.setForm(newEnrolment, true);
            this.enrolment = newEnrolment;
          }),
          this.handleResponse()
        );
    } else {
      enrolment.enrollee.givenNames = givenNames;
      return this.paperEnrolmentResource.updateEnrollee(enrolment)
        .pipe(this.handleResponse());
    }
  }

  private handleResponse() {
    return pipe(
      map(() => {
        this.toastService.openSuccessToast('Enrolment information has been saved');
        this.form.markAsPristine();

        this.nextRouteAfterSubmit();
      }),
      catchError((error: any) => {
        this.toastService.openErrorToast('Enrolment information could not be saved');
        this.logger.error('[Enrolment] Submission error has occurred: ', error);

        throw error;
      })
    );
  }

  private nextRouteAfterSubmit(): void {
    const certifications = this.formState.collegeCertifications;
    const careSettings = this.paperEnrolmentFormStateService.careSettingFormState.careSettings.value as CareSetting[];

    let nextRoutePath = (!this.certifications.length)
      ? PaperEnrolmentRoutes.JOB
      : PaperEnrolmentRoutes.SELF_DECLARATION;

    // this.routeTo(['../', this.enrolment.id, nextRoutePath]);
    this.routeUtils.routeRelativeTo(['../', '1', nextRoutePath]);
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  private removeIncompleteCertifications(noEmptyCert: boolean = false) {
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeCode').value || control.invalid) {
          this.removeCertification(index);
        }
      });

    // Always have a single cerfication available, and it prevents
    // the page from jumping too much when routing
    if (!noEmptyCert && !this.certifications.controls.length) {
      this.addEmptyCollegeCertification();
    }
  }

  /**
   * @description
   * Remove obo sites/jobs from the enrolment as enrollees can not have
   * certificate(s), as well as, job(s).
   */
  private removeJobs() {
    this.removeIncompleteCertifications(true);

    if (this.certifications.length) {
      const form = this.paperEnrolmentFormStateService.jobsForm;
      const oboSites = form.get('oboSites') as FormArray;
      oboSites.clear();
    }
  }

}
