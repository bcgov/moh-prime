import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public colleges: Config<number>[];
  public licenses: Config<number>[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentStateService: EnrolmentStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    private configService: ConfigService
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentStateService, toastService, logger);

    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
  }

  public get certifications(): FormArray {
    return this.form.get('certifications') as FormArray;
  }

  public addCertification() {
    const certification = this.enrolmentStateService.buildCollegeCertificationForm();
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

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    // TODO refactor how this occurs as it causes the view to jump on route
    this.removeIncompleteCertifications();
  }

  protected createFormInstance() {
    this.form = this.enrolmentStateService.regulatoryForm;
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
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = (!this.certifications.length)
        ? EnrolmentRoutes.JOB
        : EnrolmentRoutes.ORGANIZATION;
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
    if (!noEmptyCert) {
      if (!this.certifications.controls.length) {
        this.addCertification();
      }
    }
  }

  /**
   * @description
   * Remove jobs from the enrolment as enrollees can not have
   * certificate(s), as well as, job(s).
   */
  private removeJobs() {
    this.removeIncompleteCertifications(true);

    if (this.certifications.length) {
      const form = this.enrolmentStateService.jobsForm;
      const jobs = form.get('jobs') as FormArray;
      jobs.clear();
    }
  }
}
