import { AbstractControl, FormArray, FormGroup, UntypedFormArray, ValidationErrors, ValidatorFn } from '@angular/forms';

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

  public static noDuplicateValue(
    keyString: string
  ): ValidatorFn {
    return (array: FormArray): ValidationErrors | null => {

      let setSize = (array: FormArray) => {
        const values = array.controls.filter((form: FormGroup) => {
          return form.get(keyString) && form.get(keyString).value !== null
        }).map((form: FormGroup) => {
          return form.get(keyString) ?
            form.get(keyString).value : null;
        });
        const newSet = new Set(values);
        return newSet.size;
      };

      if (array && array.controls?.length && array.controls?.length > 1) {
        const valid = setSize(array) === array.controls.filter(c => c.get(keyString) && c.get(keyString).value !== null).length;
        return (valid) ? null : { duplicate: true };
      } else {
        return null;
      }
    };
  }

}
