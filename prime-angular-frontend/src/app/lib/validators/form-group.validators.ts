import { FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

import * as moment from 'moment';

export class FormGroupValidators {

  /**
   * @description
   * Checks two form control values are equal within a form group.
   */
  public static match(inputKey: string, confirmInputKey: string): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      const input = group.controls[inputKey];
      const confirmInput = group.controls[confirmInputKey];
      if (!input || !confirmInput) { return null; }
      const valid = (input.value === confirmInput.value);
      return valid ? null : { nomatch: true };
    };
  }

  /**
   * @description
   * Checks that at least one field has been chosen within a form group.
   */
  public static atLeastOne(validator: ValidatorFn = Validators.required, whitelist: string[] = []): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      const atLeastOne = group && group.controls &&
        Object.keys(group.controls)
          .filter(key => whitelist.indexOf(key) !== -1)
          .some(key => validator(group.controls[key]) === null);
      return (atLeastOne) ? null : { atleastone: true };
    };
  }

  /**
   * @description
   * Checks that the start key value is less than end key value.
   */
  public static lessThan(startKey: string, endKey: string): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      const start = +group.controls[startKey].value;
      const end = +group.controls[endKey].value;
      if (!start || !end) { return null; }
      const valid = (start < end);
      return (valid) ? null : { lessthan: true };
    };
  }

  /**
   * @description
   * Checks that the start key value is less than end key value.
   */
  public static lessThanDateTime(startGroupKey: string, endGroupKey: string): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      const startGroup = group.controls[startGroupKey] as FormGroup;
      const endGroup = group.controls[endGroupKey] as FormGroup;

      const startDate = moment(startGroup.controls['date'].value);
      const endDate = moment(endGroup.controls['date'].value);

      if (startDate.isBefore(endDate)) {
        return null;
      }
      if (startDate.isAfter(endDate)) {
        return { lessthan: true }
      }

      const startTime = startGroup.controls['time'].value;
      const endTime = endGroup.controls['time'].value;

      if (!startTime || !endTime) { return null; }
      const valid = (startTime < endTime);
      return (valid) ? null : { lessthan: true };
    };
  }

  /**
   * @description
   * Compares date range start and end.
   */
  public static dateRange(rangeStartKey: string, rangeEndKey: string, rangeName: string): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      const start = group.controls[rangeStartKey];
      const end = group.controls[rangeEndKey];

      if (!start.value || !end.value) { return null; }
      const rangeStart = moment(start.value);
      const rangeEnd = moment(end.value);
      const valid = rangeEnd.isSameOrAfter(rangeStart);
      return (valid) ? null : { [rangeName]: true };
    };
  }
}
