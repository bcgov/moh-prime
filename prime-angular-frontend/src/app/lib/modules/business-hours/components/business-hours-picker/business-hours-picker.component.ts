import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray, FormGroupDirective } from '@angular/forms';
import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';

import { FormUtilsService } from '@core/services/form-utils.service';

import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormGroupValidators } from '@lib/validators/form-group.validators';

import { BusinessDay } from '../../models/business-day.model';
import { BusinessDayHours } from '../../models/business-day-hours.model';

export class BusinessDayHoursErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  public isErrorState(control: FormControl | null, form: FormGroupDirective | null): boolean {
    const invalidCtrl = super.isErrorState(control, form);
    // Apply custom validation from parent form group
    const dirtyOrSubmitted = (control?.dirty || form?.submitted);
    const invalidParent = !!(control?.parent && control?.parent.hasError('lessthan') && dirtyOrSubmitted);
    return (invalidCtrl || invalidParent);
  }
}

@Component({
  selector: 'app-business-hours-picker',
  templateUrl: './business-hours-picker.component.html',
  styleUrls: ['./business-hours-picker.component.scss']
})
export class BusinessHoursPickerComponent implements OnChanges, OnInit {
  @Input() public businessDays: BusinessDay[];
  @Output() public add: EventEmitter<BusinessDay[]>;
  public form: FormGroup;
  public days: number[];
  public hours: number[];
  public busDayHoursErrStateMatcher: BusinessDayHoursErrorStateMatcher;

  private readonly defaultBusinesDayHours: BusinessDayHours;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    this.add = new EventEmitter<BusinessDay[]>();
    this.days = [...Array(7).keys()]; // Sunday through Saturday
    this.hours = [...Array(24).keys()];
    this.defaultBusinesDayHours = new BusinessDayHours('9', '17'); // Default 9 to 5
  }

  public get weekdays(): FormArray {
    return this.form.get('weekdays') as FormArray;
  }

  public get startTime(): FormControl {
    return this.form.get('startTime') as FormControl;
  }

  public get endTime(): FormControl {
    return this.form.get('endTime') as FormControl;
  }

  public get unavailableBusinessDays(): boolean[] {
    const businessDays = this.businessDays?.map(b => b.day);
    return this.days.map((day: number) => businessDays?.includes(day));
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const { weekdays, startTime, endTime } = this.form.getRawValue();
      const newBusinessDays = weekdays
        .map((weekday: boolean, weekdayIndex: number) =>
          // Isolate changes to be added to hours of operation
          (weekday !== this.unavailableBusinessDays[weekdayIndex])
            // Convert boolean to day index
            ? weekdayIndex // Sunday through Saturday
            : null
        )
        .filter((day: number) => day !== null)
        .map((day: number) => new BusinessDay(day, startTime, endTime));
      this.add.emit(newBusinessDays);
      this.resetHours();
    }
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (changes.businessDays && !changes.businessDays.firstChange) {
      this.updateAvailableBusinessDays();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      weekdays: this.fb.array(
        [],
        [FormArrayValidators.atLeast(1)]
      ),
      startTime: [
        '',
        [Validators.required]
      ],
      endTime: [
        '',
        [Validators.required]
      ],
      hours24: [
        false,
        []
      ]
    }, { validator: FormGroupValidators.lessThan('startTime', 'endTime') });

    this.busDayHoursErrStateMatcher = new BusinessDayHoursErrorStateMatcher();
  }

  private initForm() {
    this.days // Add days of the week controls
      .map((day: number) => this.fb.control(false))
      .forEach(c => this.weekdays.push(c));
    this.resetHours();
    // Disable times when 24 hours
    this.form.get('hours24').valueChanges
      .subscribe((is24Hours: boolean) => {
        const hours = (is24Hours)
          ? new BusinessDayHours(null, null) // 24 hours
          : this.defaultBusinesDayHours;

        if (is24Hours) {
          this.startTime.disable();
          this.endTime.disable();
        } else {
          this.startTime.enable();
          this.endTime.enable();
        }

        this.form.patchValue(hours);
      });
    this.updateAvailableBusinessDays();
  }

  private resetHours() {
    this.form.reset({
      weekdays: this.weekdays.getRawValue(),
      ...this.defaultBusinesDayHours,
      hours24: false
    });
  }

  private updateAvailableBusinessDays() {
    this.weekdays.patchValue(this.unavailableBusinessDays);
    this.weekdays.controls.forEach(c => (c.value) ? c.disable() : c.enable());
  }
}
