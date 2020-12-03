import { AbstractControl, FormArray, ValidationErrors, ValidatorFn } from '@angular/forms';

export class FormArrayValidators {
  /**
   * @description
   * Checks that at least # of abstract control(s) in a form array exists
   * based on a predicate. For example, useful for a list of checkbox form
   * controls, but can be extended using a custom predicate.
   */
  public static atLeast(
    minLength: number,
    predicate: (control: AbstractControl) => boolean = (control: AbstractControl) => !!control
  ): ValidatorFn {
    return (array: FormArray): ValidationErrors | null => {
      const atLeast = array &&
        array.controls?.length &&
        array.controls.filter(predicate).length >= minLength;
      return (atLeast) ? null : { atleast: true };
    };
  }
}
