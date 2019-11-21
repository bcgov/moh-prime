import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable, Subscription } from 'rxjs';
import { startWith, map } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { Enrolment } from '@shared/models/enrolment.model';
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
export class JobComponent implements OnInit {
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
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // Initialize form changes before patching
    this.initForm();
    this.enrolmentStateService.enrolment = this.enrolmentService.enrolment;
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.jobsForm;
  }

  private initForm() {
    // Always have at least one job ready for
    // the enrollee to fill out
    this.addJob();
  }

  // TODO create own job-titles component
  // private initMultiSelect() {
  //   this.filteredJobNames = this.jobCtrl.valueChanges
  //     .pipe(
  //       startWith(null),
  //       map((jobName: string | null) => {
  //         const availableJobs = [...this.jobNames];
  //         const selectedJobs = this.jobs.value.map((j: Job) => j.title.toLowerCase());

  //         return (jobName)
  //           ? this.filterJobNames(jobName)
  //           : availableJobs.filter(({ name }: Config<number>) => !selectedJobs.includes(name.toLowerCase()));
  //       })
  //     );
  // }

  // TODO filter jobs based on currently chosen title(s)
  // private filterJobNames(jobName: string): Config<number>[] {
  //   const jobsFilter = [...this.jobs.value.map((j: Job) => j.title.toLowerCase()), jobName.toLowerCase()];

  //   return this.jobNames
  //     // Remove selected jobs from the list of available jobs
  //     .filter(({ name }: Config<number>) => !jobsFilter.includes(name.toLowerCase()))
  //     // Perform type ahead filtering for auto-complete
  //     .filter(({ name }: Config<number>) => name.toLowerCase().indexOf(jobName.toLowerCase()) === 0);
  // }

  /**
   * @description
   * Remove college certifications from the enrolment as enrollees can not have
   * job(s), as well as, college certification(s).
   */
  private removeCollegeCertifications() {
    const certifications = this.enrolmentStateService.regulatoryForm.get('certifications') as FormArray;
    certifications.clear();
  }
}
