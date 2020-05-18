import { FormArray, ValidationErrors, ValidatorFn } from '@angular/forms';

export class FormArrayValidators {

  /**
   * @description
   * Checks that at least one abstract control in a form array has
   * a truthy value. For example, a list of checkbox controls.
   */
  public static atLeast(minLength: number): ValidatorFn {
    return (array: FormArray): ValidationErrors | null => {
      const atLeastOne = array &&
        array.controls?.length &&
        array.controls.filter(c => c.value).length >= minLength;
      return (atLeastOne) ? null : { atleastone: true };
    };
  }
}
