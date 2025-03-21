import { AbstractControl, UntypedFormArray, ValidationErrors, ValidatorFn } from '@angular/forms';

export class FormArrayValidators {
  /**
   * @description
   * Checks that at least # of abstract control(s) in a form array exist
   * based on a predicate. For example, useful for a list of checkbox form
   * controls, but can be extended using a custom predicate.
   */
  public static atLeast(
    minNumber: number,
    predicate: (control: AbstractControl) => boolean = (control: AbstractControl) => !!control
  ): ValidatorFn {
    return (array: UntypedFormArray): ValidationErrors | null => {

      const atLeast = array &&
        array.controls?.length &&
        array.controls.filter(predicate).length >= minNumber;
      return (atLeast) ? null : { atleast: true };
    };
  }
}
