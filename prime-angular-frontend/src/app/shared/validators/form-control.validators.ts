import { AbstractControl, FormGroup, ValidatorFn, Validator, Validators, ValidationErrors } from '@angular/forms';

export class FormControlValidators {

  /**
   * Checks the form control value is letters.
   *
   * @static
   * @param {AbstractControl} control
   * @returns {{ [key: string]: any }}
   * @memberof FormControlValidators
   */
  static alpha(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^[a-z]+$/i;
    const valid = (control.valid && regExp.test(control.value));
    return (valid) ? null : { alpha: true };
  }

  /**
   * Checks the form control value is a currency.
   *
   * 0?               The string can start with a zero if the value is zero, OR
   * (?![0,])         The string can NOT start with zero or a comma, AND
   * (,?[\d]{1,3})+   The string must contain numbers and decimals only, commas
   *                  are prevented from being side-by-side, or coming before
   *                  a decimal
   * (\.[\d]{2})?     The string will either have a fraction with a precision
   *                  of 2, or no fraction
   *
   * @static
   * @param {AbstractControl} control
   * @returns {{ [key: string]: any }}
   * @memberof FormControlValidators
   */
  static currency(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    // Doesn't allow . or .# only .## or no decimal
    const regExp = /^(0?|(?![0,])(,?[\d]{1,3})+)(\.[\d]{2})?$/;
    const valid = (control.valid && regExp.test(control.value));
    return (valid) ? null : { currency: true };
  }

  /**
   * Checks the form control value is an email address.
   *
   * @static
   * @param {AbstractControl} control
   * @returns {{ [key: string]: any }}
   * @memberof FormControlValidators
   */
  static email(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/i;
    const valid = (control.valid && regExp.test(control.value));
    return (valid) ? null : { email: true };
  }

  /**
   * Checks the form control value is an phone number.
   *
   * @static
   * @param {AbstractControl} control
   * @returns {{ [key: string]: any }}
   * @memberof FormControlValidators
   */
  static phone(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    // Allows for () or not on area code
    // const regExp = /^((\([2-9]{1}[0-9]{2}\))|(([2-9]{1}[0-9]{2})))[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    const regExp = /^([2-9]{1}[0-9]{2})[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { phone: true };
  }

  /**
   * Checks the form control value is a float.
   *
   * @static
   * @param {AbstractControl} control
   * @param {number} [precision=2]
   * @returns {(ValidationErrors | null)}
   * @memberof FormControlValidators
   */
  static float(control: AbstractControl, precision: number = 2): ValidationErrors | null {
    if (!control.value) { return null; }
    // Doesn't allow . or .# only .##+ or no decimal
    const regExp = /^[-+]?(0?|(?![0,])(,?[\d]{1,3})+)(\.[\d]{2,})?$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { float: true };
  }

  /**
   * Checks the form control value is numeric.
   *
   * @static
   * @param {AbstractControl} control
   * @returns {{ [key: string]: any }}
   * @memberof FormControlValidators
   */
  static numeric(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^[0-9]+$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { numeric: true };
  }

  /**
   * Checks the form control value is a percentage.
   *
   * @static
   * @param {AbstractControl} control
   * @returns {{ [key: string]: any }}
   * @memberof FormControlValidators
   */
  static percent(control: AbstractControl): ValidationErrors | null {
    if (!control.value) { return null; }
    const regExp = /^([0-9]|([1-9][0-9])|100)(\.[\d]{0,2})?$/;
    const valid = (control.valid && regExp.test(control.value));
    return valid ? null : { percent: true };
  }

  /**
   * Checks a form control is non-empty or false.
   *
   * @static
   * @param {AbstractControl} control
   * @returns {(ValidationErrors | null)}
   * @memberof FormControlValidators
   */
  static requiredTruthful(control: AbstractControl): ValidationErrors | null {
    // Not checking the control value on purpose!
    return (typeof control.value === 'boolean')
      ? Validators.requiredTrue(control)
      : Validators.required(control);
  }

  /**
   * Checks a form control is a boolean.
   *
   * @static
   * @param {AbstractControl} control
   * @returns {(ValidationErrors | null)}
   * @memberof FormControlValidators
   */
  static requiredBoolean(control: AbstractControl): ValidationErrors | null {
    // Not checking the control value on purpose!
    return (typeof control.value === 'boolean')
      ? null
      : { boolean: true };
  }

  /**
   * Checks a form control is within a valid length,
   * if there is no maxLength, it will be assumed to be the same as minLength.
   *
   * @static
   * @param {number} minLength
   * @param {number} maxLength
   * @returns {ValidatorFn}
   * @memberof FormControlValidators
   */
  static requiredLength(minLength: number, maxLength?: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value || !minLength) { return null; }
      if (!maxLength) { maxLength = minLength; }
      const currentLength = control.value.length;
      const valid = (control.valid
        && currentLength >= minLength
        && currentLength <= maxLength);
      return valid ? null : { length: true };
    }
  }
}
