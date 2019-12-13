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
import { ProgressStatusType } from '@enrolment/shared/enums/progress-status-type.enum';

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
    private configService: ConfigService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    super(route, router, dialog);

    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
  }

  public get certifications(): FormArray {
    return this.form.get('certifications') as FormArray;
  }

  public onSubmit() {
    if (this.form.valid) {
      // Enrollees can not have certifications and jobs
      this.removeJobs();

      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrollee(payload)
        .subscribe(
          () => {
            this.form.markAsPristine();
            this.toastService.openSuccessToast('Regulatory information has been saved');

            this.removeIncompleteCertifications(true);

            const nextRoutePath = (!this.certifications.length)
              ? EnrolmentRoutes.JOB
              : EnrolmentRoutes.SELF_DECLARATION;
            const routePath = (!this.isProfileComplete)
              ? nextRoutePath
              : EnrolmentRoutes.REVIEW;
            this.routeTo(routePath);
          },
          (error: any) => {
            this.toastService.openErrorToast('Regulatory information could not be saved');
            this.logger.error('[Enrolment] Regulatory::onSubmit error has occurred: ', error);
          }
        );
    } else {
      this.form.markAllAsTouched();
    }
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

  protected patchForm() {
    const enrolment = this.enrolmentService.enrolment;

    this.enrolmentStateService.enrolment = enrolment;
    this.isProfileComplete = enrolment.profileCompleted;
    this.isInitialEnrolment = enrolment.progressStatus !== ProgressStatusType.FINISHED;
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
