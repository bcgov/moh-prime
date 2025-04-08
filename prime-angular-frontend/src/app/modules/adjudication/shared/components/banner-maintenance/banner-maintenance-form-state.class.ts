import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, FormGroupDirective, ValidationErrors, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { BannerMaintenanceForm } from './banner-maintenance-form.model';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import moment from 'moment';
import { Banner } from '@shared/models/banner.model';

export class IsSameOrBeforeErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  public isErrorState(control: UntypedFormControl | null, form: FormGroupDirective | null): boolean {
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
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService,
    private locationCode: BannerLocationCode,
  ) {
    super();

    this.buildForm();
  }

  public get content(): UntypedFormControl {
    return this.form.get('content') as UntypedFormControl;
  }

  public get title(): UntypedFormControl {
    return this.form.get('title') as UntypedFormControl;
  }

  public get bannerType(): UntypedFormControl {
    return this.form.get('bannerType') as UntypedFormControl;
  }

  public get bannerLocationCode(): UntypedFormControl {
    return this.form.get('bannerLocationCode') as UntypedFormControl;
  }

  public get dateRange(): UntypedFormGroup {
    return this.form.get('dateRange') as UntypedFormGroup;
  }

  public get startDate(): UntypedFormControl {
    return this.dateRange.get('startDate') as UntypedFormControl;
  }

  public get startTime(): UntypedFormControl {
    return this.dateRange.get('startTime') as UntypedFormControl;
  }

  public get endDate(): UntypedFormControl {
    return this.dateRange.get('endDate') as UntypedFormControl;
  }

  public get endTime(): UntypedFormControl {
    return this.dateRange.get('endTime') as UntypedFormControl;
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

  private isTimeValid(group: UntypedFormGroup): ValidationErrors | null {
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
