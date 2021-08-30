import { FormArray, FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormGroupValidators } from '@lib/validators/form-group.validators';
import { StringUtils } from '@lib/utils/string-utils.class';
// TODO moved to lib into /common
import { BusinessDay } from '@registration/shared/models/business-day.model';
import { BusinessDayHours } from '@registration/shared/models/business-day-hours.model';
import { HoursOperationForm } from './hours-operation-form.model';

export class HoursOperationFormState extends AbstractFormState<HoursOperationForm> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get businessDays(): FormArray {
    return this.formInstance.get('businessDays') as FormArray;
  }

  public get json(): HoursOperationForm {
    if (!this.formInstance) {
      return;
    }

    const businessHours = this.formInstance.getRawValue().businessDays
      .map((hours: BusinessDayHours, dayOfWeek: number) => {
        if (hours.startTime && hours.endTime) {
          hours.startTime = StringUtils.splice(hours.startTime, 2, ':');
          hours.endTime = StringUtils.splice(hours.endTime, 2, ':');
        }
        return new BusinessDay(dayOfWeek, hours.startTime, hours.endTime);
      })
      .filter((day: BusinessDay) => day.startTime)
      .map((businessDay: BusinessDay) => {
        businessDay.startTime = BusinessDayHours.toTimespan(businessDay.startTime);
        businessDay.endTime = BusinessDayHours.toTimespan(businessDay.endTime);
        return businessDay;
      });

    return { businessHours };
  }

  public patchValue({ businessHours }: HoursOperationForm): void {
    if (!this.formInstance || !businessHours?.length) {
      return;
    }

    businessHours = businessHours.map((businessDay: BusinessDay) => {
      businessDay.startTime = BusinessDayHours.fromTimeSpan(businessDay.startTime);
      businessDay.endTime = BusinessDayHours.fromTimeSpan(businessDay.endTime);
      return businessDay;
    });

    const businessDays = [...Array(7).keys()]
      .reduce((days: (BusinessDay | {})[], dayOfWeek: number) => {
        const day = businessHours.find(bh => bh.day === dayOfWeek);
        if (day) {
          day.startTime = day.startTime.replace(':', '');
          day.endTime = day.endTime.replace(':', '');
        }
        days.push(day ?? {});
        return days;
      }, []);

    this.formInstance.patchValue({ businessDays });
  }

  public buildForm(): void {
    const groups = [...new Array(7)].map(() =>
      this.fb.group({
        startTime: [null, []],
        endTime: [null, []],
      }, { validators: FormGroupValidators.lessThan('startTime', 'endTime') })
    );

    this.formInstance = this.fb.group({
      businessDays: this.fb.array(groups)
      // TODO at least one business hours is required
      // [FormArrayValidators.atLeast(1)]
    });
  }
}
