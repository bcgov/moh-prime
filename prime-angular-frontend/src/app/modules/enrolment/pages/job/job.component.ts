import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable, Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Job } from '@enrolment/shared/models/job.model';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})
export class JobComponent implements OnInit, OnDestroy {
  public busy: Subscription;
  public form: FormGroup;
  public jobNames: Config<number>[];
  public filteredJobNames: Observable<Config<number>[]>;
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
    this.jobNames = this.configService.jobNames;
  }

  public get jobs(): FormArray {
    return this.form.get('jobs') as FormArray;
  }

  public onSubmit() {
    if (this.form.valid) {
      // Enrollees can not have jobs and certifications
      this.removeCollegeCertifications();

      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Job information has been saved');
            this.form.markAsPristine();
            this.router.navigate([EnrolmentRoutes.SELF_DECLARATION], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Job information could not be saved');
            this.logger.error('[Enrolment] Job::onSubmit error has occurred: ', error);
          }
        );
    } else {
      this.form.markAllAsTouched();
    }
  }

  public addJob(value: string = 'None') {
    const job = this.enrolmentStateService.buildJobForm(value);
    this.jobs.push(job);
  }

  public removeJob(index: number) {
    this.jobs.removeAt(index);
  }

  public filterJobs(job: FormGroup) {
    // Create a list of filtered job names
    if (this.jobs.length) {
      // All the currently chosen jobs
      const selectedJobNames = this.jobs.value
        .map((j: Job) => j.title);
      // Current job name selected
      const currentJob = this.jobNames
        .find(j => j.name === job.get('title').value);
      // Filter the list of possible jobs using the selected jobs
      const filteredJobNames = this.jobNames
        .filter((c: Config<number>) => !selectedJobNames.includes(c.name));

      if (currentJob) {
        // Add the current job to the list of filtered
        // jobs so it remains visible
        filteredJobNames.unshift(currentJob);
      }

      return filteredJobNames;
    }

    // Otherwise, provide the entire list of job names
    return this.jobNames;
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
        .pipe(
          tap(() => this.removeIncompleteJobs())
        )
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // Initialize form changes before patching
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteJobs();
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.jobsForm;
  }

  private initForm() {
    this.enrolmentStateService.enrolment = this.enrolmentService.enrolment;

    // Always have at least one job ready for
    // the enrollee to fill out
    if (!this.jobs.length) {
      this.addJob();
    }
  }

  private removeIncompleteJobs() {
    this.jobs.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('title').value;

        // Remove if job is "None" or the group is invalid
        if (!value || value === 'None' || control.invalid) {
          this.removeJob(index);
        }
      });

    // Always have a single job available, and it prevents
    // the page from jumping too much when routing
    if (!this.jobs.controls.length) {
      this.addJob();
    }
  }

  /**
   * @description
   * Remove college certifications from the enrolment as enrollees can not have
   * job(s), as well as, college certification(s).
   */
  private removeCollegeCertifications() {
    const form = this.enrolmentStateService.regulatoryForm;
    const certifications = form.get('certifications') as FormArray;
    certifications.clear();
  }
}
