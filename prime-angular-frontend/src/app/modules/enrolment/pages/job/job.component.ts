import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatChipInputEvent, MatAutocompleteSelectedEvent } from '@angular/material';

import { Observable, Subscription } from 'rxjs';
import { startWith, map } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Job } from '../../shared/models/job.model';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';
import { EnrolmentRoutes } from '@enrolment/enrolent.routes';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})
export class JobComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public jobCtrl: FormControl;
  @ViewChild('jobInput', { static: false }) jobInput: ElementRef<HTMLInputElement>;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];
  public jobNames: Config<number>[];
  public filteredJobNames: Observable<Config<number>[]>;
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
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
      this.clearCollegeForm();
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Job information has been saved');
            this.form.markAsPristine();
            this.router.navigate(['declaration'], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Job information could not be saved');
            this.logger.error('[Enrolment] Job::onSubmit error has occurred: ', error);
          }
        );
      this.form.markAsPristine();
    } else {
      this.form.markAllAsTouched();
    }
  }

  public clearCollegeForm() {
    if (this.enrolmentStateService.enrolment.certifications.length > 0) {
      const regulatoryForm = this.enrolmentStateService.regulatoryForm;
      const certs = regulatoryForm.get('certifications') as FormArray;
      certs.clear();
    }
  }

  public addJob(event: MatChipInputEvent) {
    const value = event.value;

    if ((value || '').trim()) {
      this.jobs.push(this.enrolmentStateService.buildJobForm(value.trim()));
    }

    // Remove input value after custom value added
    this.clearInputValue();
  }

  public removeJob(index: number) {
    if (index >= 0) {
      this.jobs.removeAt(index);
    }
  }

  public selectedJob(event: MatAutocompleteSelectedEvent) {
    this.jobs.push(this.enrolmentStateService.buildJobForm(event.option.viewValue));

    // Remove input value when selected from auto-complete
    this.clearInputValue();

    // Blur when selected so double click isn't required
    // to reopen the list of jobs
    this.jobInput.nativeElement.blur();
  }

  public canDeactivate(): Observable<boolean> | boolean {
    console.log('CAN DEACTIVATE', this.form)
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // Initialize form changes before patching
    this.initForm();

    // TODO: detect enrolment already exists and don't reload
    // TODO: apply guard if not enrolment is found to redirect to profile
    this.busy = this.enrolmentResource.enrolments()
      .subscribe((enrolment: Enrolment) => {
        if (enrolment) {
          this.initMultiSelect();
          this.enrolmentStateService.enrolment = enrolment;
        }
      });
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.jobsForm;
  }

  private initForm() {
    this.jobCtrl = new FormControl();
  }

  private initMultiSelect() {
    this.filteredJobNames = this.jobCtrl.valueChanges
      .pipe(
        startWith(null),
        map((jobName: string | null) => {
          const availableJobs = [...this.jobNames];
          const selectedJobs = this.jobs.value.map((j: Job) => j.title.toLowerCase());

          return (jobName)
            ? this.filterJobNames(jobName)
            : availableJobs.filter(({ name }: Config<number>) => !selectedJobs.includes(name.toLowerCase()));
        })
      );
  }

  private filterJobNames(jobName: string): Config<number>[] {
    const jobsFilter = [...this.jobs.value.map((j: Job) => j.title.toLowerCase()), jobName.toLowerCase()];

    return this.jobNames
      // Remove selected jobs from the list of available jobs
      .filter(({ name }: Config<number>) => !jobsFilter.includes(name.toLowerCase()))
      // Perform type ahead filtering for auto-complete
      .filter(({ name }: Config<number>) => name.toLowerCase().indexOf(jobName.toLowerCase()) === 0);
  }

  private clearInputValue() {
    this.jobInput.nativeElement.value = '';
    this.jobCtrl.setValue(null);
  }
}

