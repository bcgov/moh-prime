import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService
    );
  }

  public get certifications(): FormArray {
    return this.form.get('certifications') as FormArray;
  }

  public get selectedCollegeCodes(): number[] {
    return this.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  public addCertification() {
    const certification = this.enrolmentFormStateService.buildCollegeCertificationForm();
    this.certifications.push(certification);
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
    this.routeTo(EnrolmentRoutes.CARE_SETTING);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteCertifications(true);
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.regulatoryForm;
  }

  protected initForm() {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.certifications.length) {
      this.addCertification();
    }
  }

  protected onSubmitFormIsValid() {
    // Enrollees can not have certifications and jobs
    this.removeJobs();
  }

  protected afterSubmitIsSuccessful() {
    this.removeIncompleteCertifications(true);
  }

  protected nextRouteAfterSubmit() {
    const certifications = this.enrolmentFormStateService.regulatoryForm
      .get('certifications').value as CollegeCertification[];
    const careSettings = this.enrolmentFormStateService.careSettingsForm
      .get('careSettings').value as CareSetting[];

    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = (!this.certifications.length)
        ? EnrolmentRoutes.JOB
        : (this.enrolmentService.canRequestRemoteAccess(certifications, careSettings))
          ? EnrolmentRoutes.REMOTE_ACCESS
          : EnrolmentRoutes.SELF_DECLARATION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
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
      this.addCertification();
    }
  }

  /**
   * @description
   * Remove jobs and obo sites from the enrolment as enrollees can not have
   * certificate(s), as well as, job(s).
   */
  private removeJobs() {
    this.removeIncompleteCertifications(true);

    if (this.certifications.length) {
      const form = this.enrolmentFormStateService.jobsForm;
      const jobs = form.get('jobs') as FormArray;
      const oboSites = form.get('oboSites') as FormArray;
      jobs.clear();
      oboSites.clear();
    }
  }
}
