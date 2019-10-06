import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatChipInputEvent, MatAutocompleteSelectedEvent } from '@angular/material';

import { Observable } from 'rxjs';

import { ConfigKeyValue } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { startWith, map } from 'rxjs/operators';
import { filter } from 'minimatch';

@Component({
  selector: 'app-professional-info',
  templateUrl: './professional-info.component.html',
  styleUrls: ['./professional-info.component.scss']
})
export class ProfessionalInfoComponent implements OnInit, OnDestroy {
  public form: FormGroup;
  public jobCtrl: FormControl;
  @ViewChild('jobInput', { static: false }) jobInput: ElementRef<HTMLInputElement>;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];
  public colleges: ConfigKeyValue[];
  public licenses: ConfigKeyValue[];
  public advancedPractices: ConfigKeyValue[];
  public jobNames: ConfigKeyValue[];
  public filteredJobNames: Observable<ConfigKeyValue[]>;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService
  ) {
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
    this.advancedPractices = this.configService.advancedPractices;
    this.jobNames = this.configService.jobNames;
  }

  public get hasCertification(): FormGroup {
    return this.form.get('hasCertification') as FormGroup;
  }

  public get certifications(): FormArray {
    return this.form.get('certifications') as FormArray;
  }

  public get isDeviceProvider(): FormGroup {
    return this.form.get('isDeviceProvider') as FormGroup;
  }

  public get isInsulinPumpProvider(): FormGroup {
    return this.form.get('isInsulinPumpProvider') as FormGroup;
  }

  public get isAccessingPharmaNetOnBehalfOf(): FormGroup {
    return this.form.get('isAccessingPharmaNetOnBehalfOf') as FormGroup;
  }

  public get jobs(): FormArray {
    return this.form.get('jobs') as FormArray;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.form.markAsPristine();
      this.router.navigate(['declaration'], { relativeTo: this.route.parent });
    } else {
      this.form.markAllAsTouched();
    }
  }

  public addCertification() {
    const certification = this.fb.group({
      id: [null, []],
      collegeCode: [null, [Validators.required]],
      licenseNumber: [null, [Validators.required]],
      licenseCode: [null, [Validators.required]],
      renewalDate: [null, [Validators.required]],
      practiceCode: [null, []]
    });

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
      this.jobs.push(this.fb.control(value.trim()));
    }

    this.clearInputValue();
  }

  public removeJob(index: number) {
    if (index >= 0) {
      this.jobs.removeAt(index);
    }
  }

  public selectedJob(event: MatAutocompleteSelectedEvent) {
    this.jobs.push(this.fb.control(event.option.viewValue));

    // Remove input values when selected from auto-complete
    this.clearInputValue();

    // Blur when selected so double click isn't required
    // to reopen the list of jobs
    this.jobInput.nativeElement.blur();
  }

  public canDeactivate(): Observable<boolean> | boolean {
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDiscardChangesDialogComponent).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  public ngOnDestroy() {

  }

  private createFormInstance() {
    this.form = this.fb.group({
      id: [null, []],
      hasCertification: [null, [FormControlValidators.requiredBoolean]],
      certifications: this.fb.array([]),
      isDeviceProvider: [null, [FormControlValidators.requiredBoolean]],
      deviceProviderNumber: ['', []],
      isInsulinPumpProvider: [null, [FormControlValidators.requiredBoolean]],
      isAccessingPharmaNetOnBehalfOf: [null, [FormControlValidators.requiredBoolean]],
      jobs: this.fb.array([]),
    });

    this.jobCtrl = new FormControl();
    this.filteredJobNames = this.jobCtrl.valueChanges
      .pipe(
        startWith(null),
        map((jobName: string | null) => {
          const jobs = [...this.jobNames];
          const selectedJobs = this.jobs.value.map((j: string) => j.toLowerCase());

          return (jobName)
            ? this.filterJobNames(jobName)
            : jobs.filter(({ name }: ConfigKeyValue) => !selectedJobs.includes(name.toLowerCase()));
        })
      );
  }

  private filterJobNames(job: string): ConfigKeyValue[] {
    const jobsFilter = [...this.jobs.value.map((j: string) => j.toLowerCase()), job.toLowerCase()];

    return this.jobNames
      // Remove selected jobs from the list of available jobs
      .filter(({ name }: ConfigKeyValue) => !jobsFilter.includes(name.toLowerCase()))
      // Perform type ahead filtering of auto-complete
      .filter(({ name }: ConfigKeyValue) => name.toLowerCase().indexOf(job.toLowerCase()) === 0);
  }

  private clearInputValue() {
    this.jobInput.nativeElement.value = '';
    this.jobCtrl.setValue(null);
  }
}
