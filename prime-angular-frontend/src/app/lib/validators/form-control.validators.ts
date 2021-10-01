import { AbstractControl, ValidatorFn, Validators, ValidationErrors, AsyncValidatorFn } from '@angular/forms';

import { EMPTY, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, map, switchMap } from 'rxjs/operators';

export class FormControlValidators {

  /**
   * @description
   * Checks the form control value is letters.
   */
  public static alpha(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^[a-z]+$/i;
    const valid = (control.valid && regExp.test(control.value));
    return (valid) ? null : { alpha: true };
  }

  /**
   * @description
   * Checks the form control value is letters or numerals.
   */
  public static alphanumeric(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = new RegExp(/^[a-zA-Z0-9]*$/);
    const valid = (control.valid && regExp.test(control.value));
    return (valid) ? null : { alphanumeric: true };
  }

  /**
   * @description
   * Checks the form control value is a currency.
   *
   * 0?               The string can start with a zero if the value is zero, OR
   * (?![0,])         The string can NOT start with zero or a comma, AND
   * (,?[\d]{1,3})+   The string must contain numbers and decimals only, commas
   *                  are prevented from being side-by-side, or coming before
   *                  a decimal
   * (\.[\d]{2})?     The string will either have a fraction with a precision
   *                  of 2, or no fraction
   */
  public static currency(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    // Doesn't allow . or .# only .## or no decimal
    const regExp = /^(0?|(?![0,])(,?[\d]{1,3})+)(\.[\d]{2})?$/;
    const valid = (control.valid && regExp.test(control.value));
    return (valid) ? null : { currency: true };
  }

  /**
   * @description
   * Checks the form control value doesn't need to be trimmed.
   */
  public static trim(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const value = control.value;
    const length = value.length;
    const valid = (control.valid && length && length === value.trim().length);
    return (valid) ? null : { trim: true };
  }

  /**
   * @description
   * Checks the form control value is an email address.
   */
  public static email(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/i;
    // Affixed spaces does not invalidate the entry, and should
    // be sanitized by on submission and by the server
    const valid = (control.valid && regExp.test(control.value));
    return (valid) ? null : { email: true, ...FormControlValidators.trim(control) };
  }

  /**
   * @description
   * Checks the form control value is an email address, or comma-separated
   * list of email address(es).
   */
  public static multipleEmails(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^([a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,})(,(\s)?[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,})*$/i;
    const valid = (control.valid && regExp.test(control.value));
    return (valid) ? null : { emails: true, ...FormControlValidators.trim(control) };
  }

  /**
   * @description
   * Checks the form control value is an phone number.
   */
  public static phone(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    // Allows for () or not on area code
    // const regExp = /^((\([2-9]{1}[0-9]{2}\))|(([2-9]{1}[0-9]{2})))[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    const regExp = /^([2-9]{1}[0-9]{2})[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { phone: true };
  }

  /**
   * @description
   * Checks the form control value is a float.
   */
  public static float(control: AbstractControl, precision = 2): ValidationErrors | null {
    if (!control.value) { return null; }
    // Doesn't allow . or .# only .##+ or no decimal
    const regExp = /^[-+]?(0?|(?![0,])(,?[\d]{1,3})+)(\.[\d]{2,})?$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { float: true };
  }

  /**
   * @description
   * Checks the form control value is numeric.
   */
  public static numeric(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^[0-9]+$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { numeric: true };
  }

  /**
   * @description
   * Checks the form control value is a percentage.
   */
  public static percent(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^([0-9]|([1-9][0-9])|100)(\.[\d]{0,2})?$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { percent: true };
  }

  /**
   * @description
   * Checks that at least a # of values have been added to an
   * array on a form control.
   */
  public static atLeast(control: AbstractControl, minNumber: number): ValidationErrors | null {
    if (!Array.isArray(control.value)) { return null; }
    const atLeast = control.value?.length &&
      control.value.length >= minNumber;
    return (atLeast) ? null : { atLeast: true };
  }

  /**
   * @description
   * Checks a form control is a non-zero index (eg. database record ID).
   */
  public static requiredIndex(control: AbstractControl): ValidationErrors | null {
    const regExp = /^[1-9]\d*$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { index: true };
  }

  /**
   * @description
   * Checks a form control is non-empty or false.
   */
  public static requiredTruthful(control: AbstractControl): ValidationErrors | null {
    // Not checking the control value on purpose!
    return (typeof control.value === 'boolean')
      ? Validators.requiredTrue(control)
      : Validators.required(control);
  }

  /**
   * @description
   * Checks a form control is a boolean.
   */
  public static requiredBoolean(control: AbstractControl): ValidationErrors | null {
    // Not checking the control value on purpose!
    return (typeof control.value === 'boolean')
      ? null
      : { boolean: true };
  }

  /**
   * @description
   * Checks a form control is within a valid length, and
   * if no maxlength assumed to be minlength.
   */
  public static requiredLength(minLength: number, maxLength?: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) { return null; }
      if (!maxLength) { maxLength = minLength; }
      const currentLength = control.value.length;
      const valid = (control.valid
        && currentLength >= minLength
        && currentLength <= maxLength);
      return valid ? null : { length: true };
    };
  }

  /**
   * @description
   * Checks a form control is set to one of a set of
   * values.
   *
   * @example
   * FormControlValidators.requiredIn(Object.values(enum))
   */
  public static requiredIn<T>(allowedValues: T[]): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const valid = allowedValues.includes(control.value);
      return valid ? null : { requiredIn: true };
    };
  }
}
