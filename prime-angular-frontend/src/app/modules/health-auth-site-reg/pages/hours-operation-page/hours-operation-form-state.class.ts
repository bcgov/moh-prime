import { FormArray, FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormGroupValidators } from '@lib/validators/form-group.validators';
import { StringUtils } from '@lib/utils/string-utils.class';
import { BusinessDay } from '@lib/models/business-day.model';
import { BusinessDayHours } from '@lib/models/business-day-hours.model';
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

    const { businessDays } = this.formInstance.getRawValue();

    const businessHours = businessDays
      .map(({ startTime, endTime }: BusinessDayHours, day: number) => {
        if (!startTime || !endTime) {
          return null;
        }

        startTime = StringUtils.splice(startTime, 2, ':');
        endTime = StringUtils.splice(endTime, 2, ':');
        return new BusinessDay(day, startTime, endTime);
      })
      .filter((day: BusinessDay) => day instanceof BusinessDay);

    return { businessHours };
  }

  public patchValue({ businessHours }: HoursOperationForm): void {
    if (!this.formInstance || !businessHours?.length) {
      return;
    }

    // Create a business day for each day of the week, and
    // hydrate with the hours of operation where applicable
    const businessDays = [...Array(7).keys()]
      .reduce((days: (BusinessDay | {})[], dayOfWeek: number) => {
        let day = businessHours.find(bh => bh.day === dayOfWeek);
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
