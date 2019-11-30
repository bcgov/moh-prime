import { FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

import * as moment from 'moment';

export class FormGroupValidators {

  /**
   * @description
   * Checks two form control values are equal within a form group.
   */
  static match(inputKey: string, confirmInputKey: string): ValidatorFn {
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
  static atLeastOne(validator: ValidatorFn, whitelist: string[] = []): ValidatorFn {
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
   * Compares date range start and end.
   */
  static dateRange(rangeStartKey: string, rangeEndKey: string, rangeName: string): ValidatorFn {
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
