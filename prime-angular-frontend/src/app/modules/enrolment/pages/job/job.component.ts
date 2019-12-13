import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Job } from '@enrolment/shared/models/job.model';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { ProgressStatusType } from '@enrolment/shared/enums/progress-status-type.enum';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})
export class JobComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public jobNames: Config<number>[];
  public filteredJobNames: BehaviorSubject<Config<number>[]>;
  public allowDefaultOption: boolean;
  public defaultOptionLabel: string;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    private configService: ConfigService,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    super(route, router, dialog);

    this.jobNames = this.configService.jobNames;
    this.filteredJobNames = new BehaviorSubject<Config<number>[]>(this.jobNames);
    this.allowDefaultOption = true;
    this.defaultOptionLabel = 'None';
  }

  public get jobs(): FormArray {
    return this.form.get('jobs') as FormArray;
  }

  public onSubmit() {
    if (this.form.valid) {
      // Enrollees can not have jobs and certifications
      this.removeCollegeCertifications();

      const payload = this.enrolmentStateService.enrolment;
      // Remove the default so not proliferated outside the component
      payload.jobs = payload.jobs
        .map((job: Job) => (job.title === this.defaultOptionLabel) ? { ...job, title: '' } : job);

      this.busy = this.enrolmentResource.updateEnrollee(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Job information has been saved');
            this.form.markAsPristine();

            const routePath = (!this.isProfileComplete)
              ? EnrolmentRoutes.SELF_DECLARATION
              : EnrolmentRoutes.REVIEW;
            this.routeTo(routePath);
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

  public addJob(value: string = '') {
    const defaultValue = (value)
      ? value : (this.allowDefaultOption)
        ? this.defaultOptionLabel : '';
    const job = this.enrolmentStateService.buildJobForm(defaultValue);
    this.jobs.push(job);
  }

  public removeJob(index: number) {
    this.jobs.removeAt(index);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const canDeactivate = super.canDeactivate();

    return (canDeactivate instanceof Observable)
      ? canDeactivate.pipe(tap(() => this.removeIncompleteJobs()))
      : canDeactivate;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteJobs();
  }

  protected createFormInstance() {
    this.form = this.enrolmentStateService.jobsForm;
  }

  protected initForm() {
    // Initialize listeners before patching
    this.form.valueChanges
      .subscribe(({ jobs }: { jobs: Job[] }) => this.filterJobNames(jobs));

    this.patchForm();

    // Always have at least one job ready for
    // the enrollee to fill out
    if (!this.jobs.length) {
      this.addJob();
    }
  }

  protected patchForm() {
    const enrolment = this.enrolmentService.enrolment;

    this.isProfileComplete = enrolment.profileCompleted;
    this.enrolmentStateService.enrolment = enrolment;
    this.isInitialEnrolment = enrolment.progressStatus !== ProgressStatusType.FINISHED;
  }

  private filterJobNames(jobs: Job[]) {
    // All the currently chosen jobs
    const selectedJobNames = jobs.map((j: Job) => j.title);
    // Filter the list of possible jobs using the selected jobs
    const filteredJobNames = this.jobNames
      .filter((c: Config<number>) => !selectedJobNames.includes(c.name));

    this.filteredJobNames.next(filteredJobNames);
  }

  private removeIncompleteJobs() {
    this.jobs.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('title').value;

        // Remove when empty, default option, or group is invalid
        if (!value || value === this.defaultOptionLabel || control.invalid) {
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
