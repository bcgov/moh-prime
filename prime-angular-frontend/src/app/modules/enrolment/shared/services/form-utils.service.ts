import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup, FormControl, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormUtilsService {

  constructor() { }

  /**
   * @description
   * Sets FormControl validators.
   */
  public setValidators(control: FormControl | FormGroup, validators: ValidatorFn | ValidatorFn[]): void {
    if (control instanceof FormGroup) {
      // Assumes that FormGroups will not be deeply nested
      Object.keys(control.controls).forEach((key: string) => this.setValidators(control.controls[key] as FormControl, validators));
    } else {
      control.setValidators(validators);
      control.updateValueAndValidity();
    }
  }

  /**
   * @description
   * Resets FormControl value(s) and clears associated validators.
   */
  public resetAndClearValidators(control: FormControl | FormGroup): void {
    if (control instanceof FormGroup) {
      // Assumes that FormGroups will not be deeply nested
      Object.keys(control.controls).forEach((key: string) => this.resetAndClearValidators(control.controls[key] as FormControl));
    } else {
      control.reset();
      control.clearValidators();
      control.updateValueAndValidity();
    }
  }

  /**
   * @description
   * Check for the required validator applied to a FormControl,
   * FormGroup, and FormArray.
   *
   * @example
   * isRequired('controlName')
   * isRequired('groupName')
   * isRequired('groupName.controlName')
   * isRequired('arrayName')
   * isRequired('arrayName[#].groupName.controlName')
   */
  public isRequired(form: FormGroup, path: string): boolean {
    const control = form.get(path);

    if (control.validator) {
      const validator = control.validator({} as AbstractControl);
      if (validator && validator.required) {
        return true;
      }
    }
    return false;
  }
}
