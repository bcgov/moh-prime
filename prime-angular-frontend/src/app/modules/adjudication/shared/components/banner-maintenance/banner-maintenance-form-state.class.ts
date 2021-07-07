import { FormBuilder, FormControl, FormGroup, FormGroupDirective, ValidationErrors, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { BannerMaintenanceForm } from './banner-maintenance-form.model';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import moment from 'moment';
import { Banner } from '@shared/models/banner.model';

export class IsSameOrBeforeErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  public isErrorState(control: FormControl | null, form: FormGroupDirective | null): boolean {
    const invalidCtrl = super.isErrorState(control, form);
    // Apply custom validation from parent form group
    const dirtyOrSubmitted = (control?.dirty || form?.submitted || control?.touched);
    const requiredControl = (!!(control?.hasError('required')) && dirtyOrSubmitted);
    const invalidParent = !!(control?.parent && control?.parent.hasError('isSameOrBefore') && dirtyOrSubmitted);
    return (invalidCtrl || invalidParent || requiredControl);
  }
}

export class BannerMaintenanceFormState extends AbstractFormState<BannerMaintenanceForm> {
  public isSameOrBeforeErrorStateMatcher: IsSameOrBeforeErrorStateMatcher;

  private id: number;

  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private locationCode: BannerLocationCode,
  ) {
    super();

    this.buildForm();
  }

  public get content(): FormControl {
    return this.form.get('content') as FormControl;
  }

  public get title(): FormControl {
    return this.form.get('title') as FormControl;
  }

  public get bannerType(): FormControl {
    return this.form.get('bannerType') as FormControl;
  }

  public get bannerLocationCode(): FormControl {
    return this.form.get('bannerLocationCode') as FormControl;
  }

  public get dateRange(): FormGroup {
    return this.form.get('dateRange') as FormGroup;
  }

  public get startDate(): FormControl {
    return this.dateRange.get('startDate') as FormControl;
  }

  public get startTime(): FormControl {
    return this.dateRange.get('startTime') as FormControl;
  }

  public get endDate(): FormControl {
    return this.dateRange.get('endDate') as FormControl;
  }

  public get endTime(): FormControl {
    return this.dateRange.get('endTime') as FormControl;
  }

  public get json(): BannerMaintenanceForm {
    if (!this.formInstance) {
      return;
    }
    const banner = this.formInstance.getRawValue();
    return {
      id: this.id,
      ...banner,
      startDate: banner.dateRange.startDate,
      startTime: banner.dateRange.startTime,
      endDate: banner.dateRange.endDate,
      endTime: banner.dateRange.endTime,
    };
  }

  public patchValue(model: BannerMaintenanceForm | Banner): void {
    if (!this.formInstance) {
      return;
    }
    this.id = model.id;

    this.formInstance.patchValue(model);
    this.startDate.setValue(model.startDate);
    this.startTime.setValue(model.startTime);
    this.endDate.setValue(model.endDate);
    this.endTime.setValue(model.endTime);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      content: [
        {
          value: '',
          disabled: false
        },
        [Validators.required]
      ],
      bannerType: [
        {
          value: '',
          disabled: false
        },
        [Validators.required]
      ],
      bannerLocationCode: [
        {
          value: this.locationCode,
          disabled: true
        },
        [Validators.required]
      ],
      title: [
        {
          value: '',
          disabled: false
        },
        [Validators.required]
      ],
      dateRange: this.fb.group(
        {
          startDate: ['', [Validators.required]],
          startTime: ['', [Validators.required]],
          endDate: ['', [Validators.required]],
          endTime: ['', [Validators.required]],
        },
        { validator: this.isTimeValid })
    });
    this.isSameOrBeforeErrorStateMatcher = new IsSameOrBeforeErrorStateMatcher();
  }

  private isTimeValid(group: FormGroup): ValidationErrors | null {
    const startDate = moment(group.get('startDate').value);
    const startTime = moment(group.get('startTime').value, 'HHmm');
    const endDate = moment(group.get('endDate').value);
    const endTime = moment(group.get('endTime').value, 'HHmm');

    const start = startDate.set({
      hour: startTime.get('hour'),
      minute: startTime.get('minute')
    });
    const end = endDate.set({
      hour: endTime.get('hour'),
      minute: endTime.get('minute')
    });

    if (!start || !end) {
      return null;
    }

    return start.isSameOrBefore(end)
      ? null
      : { isSameOrBefore: true };
  }
}
