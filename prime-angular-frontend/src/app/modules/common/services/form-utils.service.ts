import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup, FormControl, ValidatorFn, FormArray } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormUtilsService {
  /**
   * @description
   * Checks the validity of a form, and triggers validation messages when invalid.
   */
  public checkValidity(form: FormGroup): boolean {
    if (form.valid) {
      return true;
    } else {
      form.markAllAsTouched();
      return false;
    }
  }

  /**
   * @description
   * Sets FormControl validators.
   */
  public setValidators(control: FormControl | FormGroup, validators: ValidatorFn | ValidatorFn[], blacklist: string[] = []): void {
    if (control instanceof FormGroup) {
      // Assumes that FormGroups will not be deeply nested
      Object.keys(control.controls).forEach((key: string) => {
        // Skip blacklisted keys from having validators updated
        if (!blacklist.includes(key)) {
          this.setValidators(control.controls[key] as FormControl, validators, blacklist);
        }
      });
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
   * FormGroup, or FormArray.
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

  /**
   * @description
   * Get all the errors contained within a form.
   */
  public getFormErrors(form: FormGroup | FormArray): { [key: string]: any; } | null {
    let hasError = false;
    const result = Object.keys(form?.controls).reduce((acc, key) => {
      const control = form.get(key);
      const errors = (control instanceof FormGroup || control instanceof FormArray)
        ? this.getFormErrors(control)
        : control.errors;
      if (errors) {
        acc[key] = errors;
        hasError = true;
      }
      return acc;
    }, {} as { [key: string]: any; });
    return hasError ? result : null;
  }
}
