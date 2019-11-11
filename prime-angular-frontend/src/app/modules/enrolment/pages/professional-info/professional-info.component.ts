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

@Component({
  selector: 'app-professional-info',
  templateUrl: './professional-info.component.html',
  styleUrls: ['./professional-info.component.scss']
})
export class ProfessionalInfoComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public jobCtrl: FormControl;
  @ViewChild('jobInput', { static: false }) jobInput: ElementRef<HTMLInputElement>;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];
  public colleges: Config<number>[];
  public licenses: Config<number>[];
  public practices: Config<number>[];
  public jobNames: Config<number>[];
  public filteredJobNames: Observable<Config<number>[]>;

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
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
    this.practices = this.configService.practices;
    this.jobNames = this.configService.jobNames;
  }

  public get hasCertification(): FormControl {
    return this.form.get('hasCertification') as FormControl;
  }

  public get certifications(): FormArray {
    return this.form.get('certifications') as FormArray;
  }

  public get isDeviceProvider(): FormControl {
    return this.form.get('isDeviceProvider') as FormControl;
  }

  public get deviceProviderNumber(): FormControl {
    return this.form.get('deviceProviderNumber') as FormControl;
  }

  public get isInsulinPumpProvider(): FormControl {
    return this.form.get('isInsulinPumpProvider') as FormControl;
  }

  public get isAccessingPharmaNetOnBehalfOf(): FormControl {
    return this.form.get('isAccessingPharmaNetOnBehalfOf') as FormControl;
  }

  public get jobs(): FormArray {
    return this.form.get('jobs') as FormArray;
  }

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Professional information has been saved');
            this.form.markAsPristine();
            this.router.navigate(['declaration'], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Professional information could not be saved');
            this.logger.error('[Enrolment] Professional::onSubmit error has occurred: ', error);
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

  public removeCertification(index: number) {
    if (index >= 0) {
      this.certifications.removeAt(index);
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
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // Initialize form changes before patching
    this.initForm();

    // TODO detect enrolment already exists and don't reload
    // TODO apply guard if not enrolment is found to redirect to profile
    this.busy = this.enrolmentResource.enrolments()
      .subscribe((enrolment: Enrolment) => {
        if (enrolment) {
          this.enrolmentStateService.enrolment = enrolment;
        }
      });

    // Initialize multi-select after patching
    this.initMultiSelect();
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.professionalInfoForm;
  }

  private initForm() {
    this.hasCertification.valueChanges.subscribe((value) => {
      if (!value) {
        this.certifications.clear();

        this.isAccessingPharmaNetOnBehalfOf.enable({ emitEvent: false });
      } else {
        if (!this.certifications.length) {
          // Add an initial empty certification when displayed
          this.addCertification();
        }

        // College certification indicates not being accessed on behalf of
        this.isAccessingPharmaNetOnBehalfOf.reset(null, { emitEvent: false });
        this.isAccessingPharmaNetOnBehalfOf.disable({ emitEvent: false });
      }
    });

    this.isDeviceProvider.valueChanges.subscribe((value) => {
      if (!value) {
        this.deviceProviderNumber.reset();

        // Device providers can be an insulin providers, otherwise disabled
        this.isInsulinPumpProvider.reset(null, { emitEvent: false });
        this.isInsulinPumpProvider.disable({ emitEvent: false });
      } else {
        this.isInsulinPumpProvider.enable({ emitEvent: false });
      }
    });

    this.isAccessingPharmaNetOnBehalfOf.valueChanges.subscribe((value) => {
      if (!value) {
        this.jobs.clear();

        this.hasCertification.enable({ emitEvent: false });
      } else {
        // Accessing on behalf of indicates no college certification
        this.hasCertification.reset(null, { emitEvent: false });
        this.hasCertification.disable({ emitEvent: false });
      }
    });
  }

  private initMultiSelect() {
    this.jobCtrl = new FormControl();
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
