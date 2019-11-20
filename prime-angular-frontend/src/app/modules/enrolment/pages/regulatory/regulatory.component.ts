import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable, Subscription } from 'rxjs';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent implements OnInit, OnDestroy {
  public busy: Subscription;
  public form: FormGroup;
  public colleges: Config<number>[];
  public licenses: Config<number>[];
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
  }

  public get certifications(): FormArray {
    return this.form.get('certifications') as FormArray;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.removeJobs();
      this.removeIncompleteCertifications();
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.form.markAsPristine();
            this.toastService.openSuccessToast('Regulatory information has been saved');
            this.router.navigate([EnrolmentRoutes.DEVICE_PROVIDER], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Regulatory information could not be saved');
            this.logger.error('[Enrolment] Regulatory::onSubmit error has occurred: ', error);
          }
        );
      this.form.markAsPristine();
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

    // Add a new certification if no certifications exist
    // within the list
    if (!this.certifications.length) {
      this.addCertification();
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    this.removeIncompleteCertifications();

    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
        .pipe(
          // TODO remove values if needed
        )
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // Initialize form changes before patching
    this.initForm();
    this.enrolmentStateService.enrolment = this.enrolmentService.enrolment;
  }

  public ngOnDestroy() {
    // Remove incomplete certifications from the form model, otherwise
    // they will be persisted
    this.removeIncompleteCertifications();
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.regulatoryForm;
  }

  private initForm() {
    if (this.certifications.length === 0) {
      // Always have at least one certification ready for
      // the enrollee to fill out
      this.addCertification();
    } else {
      // After patched with existing data mark the form
      // group as pristine to avoid dirty check on route
      this.form.markAsPristine();
    }
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  private removeIncompleteCertifications() {
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        if (!control.get('collegeCode').value) {
          this.certifications.removeAt(index);
        }
      });
  }

  /**
   * @description
   * Remove chosen jobs from the enrolment as enrollees can not
   * have certificate(s), as well as, job(s).
   */
  private removeJobs() {
    if (this.enrolmentStateService.enrolment.jobs.length) {
      const jobs = this.enrolmentStateService.jobsForm.get('jobs') as FormArray;
      jobs.clear();
    }
  }
}
