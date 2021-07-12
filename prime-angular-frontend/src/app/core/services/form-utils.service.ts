import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup, FormControl, ValidatorFn, FormArray, FormBuilder, Validators } from '@angular/forms';

import { Person } from '@lib/models/person.model';
import { LoggerService } from '@core/services/logger.service';
import { Country } from '@shared/enums/country.enum'; // TODO move into @lib
import { Province } from '@shared/enums/province.enum'; // TODO move into @lib
import { AddressLine } from '@shared/models/address.model';

@Injectable({
  providedIn: 'root'
})
export class FormUtilsService {
  constructor(
    private fb: FormBuilder,
    private logger: LoggerService
  ) { }

  /**
   * @description
   * Checks the validity of a form, and triggers validation messages when invalid.
   */
  public checkValidity(form: FormGroup | FormArray): boolean {
    if (form.valid) {
      return true;
    } else {
      this.logFormErrors(form);

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
  public resetAndClearValidators(control: FormControl | FormGroup, blacklist: string[] = []): void {
    if (control instanceof FormGroup) {
      // Assumes that FormGroups will not be deeply nested
      Object.keys(control.controls).forEach((key: string) => {
        if (!blacklist.includes(key)) {
          this.resetAndClearValidators(control.controls[key] as FormControl);
        }
      });
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
   * Push JSON model(s) as abstract controls onto a form array.
   *
   * NOTE: Helper method for quickly adding FormControls or FormGroups to a
   * FormArray that are mapped directly from JSON. This does not take into
   * account application of validators, or other AbstractControl properties.
   */
  // TODO rename to formArrayPushFromJson, and add below TODOs in separate method(s)
  // TODO allow for push of FormControls onto the FormArray
  // TODO allow for push of FormGroups onto the FormArray
  // TODO allow for child JSON arrays to be created recursively
  // TODO allow for configuration of JSON with validators
  public formArrayPush(array: FormArray, models: any | any[], type: 'group' | 'control' = 'group') {
    const push = (control: AbstractControl) => array.push(control);
    (Array.isArray(models))
      ? models.forEach(m => push(this.fb[type](m)))
      : push(this.fb[type](models));
  }

  /**
   * @description
   * Get all the errors contained within a form.
   */
  public getFormErrors(form: FormGroup | FormArray): { [key: string]: any; } | null {
    if (!form) {
      return null;
    }

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
    return (hasError) ? result : null;
  }

  /**
   * @description
   * Helper for quickly logging form errors.
   */
  public logFormErrors(form: FormGroup | FormArray) {
    const formErrors = this.getFormErrors(form);
    if (formErrors) {
      // Do we need to log every INVALID FROM error?
      this.logger.warn('FORM_INVALID', formErrors);
    }
  }

  /**
   * @description
   * Provide an address form group.
   *
   * @param options available for manipulating the form group
   *  areRequired control names that are required
   *  areDisabled control names that are disabled
   *  useDefaults for province and country, otherwise empty
   *  exclude control names that are not needed
   */
  public buildAddressForm(options: {
    areRequired?: AddressLine[],
    areDisabled?: AddressLine[],
    useDefaults?: Extract<AddressLine, 'provinceCode' | 'countryCode'>[],
    exclude?: AddressLine[];
  } = null): FormGroup {
    const controlsConfig = {
      id: [
        0,
        []
      ],
      street: [
        { value: null, disabled: false },
        []
      ],
      street2: [
        { value: null, disabled: false },
        []
      ],
      city: [
        { value: null, disabled: false },
        []
      ],
      provinceCode: [
        { value: null, disabled: false },
        []
      ],
      countryCode: [
        { value: null, disabled: false },
        []
      ],
      postal: [
        { value: null, disabled: false },
        []
      ]
    };

    Object.keys(controlsConfig)
      .filter((key: AddressLine) => !options?.exclude?.includes(key))
      .forEach((key: AddressLine, index: number) => {
        const control = controlsConfig[key];
        const controlProps = control[0] as { value: any, disabled: boolean; };
        const controlValidators = control[1] as Array<ValidatorFn>;

        if (options?.areDisabled?.includes(key)) {
          controlProps.disabled = true;
        }

        const useDefaults = options?.useDefaults;
        if (useDefaults) {
          if (key === 'provinceCode') {
            controlProps.value = Province.BRITISH_COLUMBIA;
          } else if (key === 'countryCode') {
            controlProps.value = Country.CANADA;
          }
        }

        if (options?.areRequired?.includes(key)) {
          controlValidators.push(Validators.required);
        }
      });

    return this.fb.group(controlsConfig);
  }

  /**
   * @description
   * Convert party JSON to form model for reactive forms.
   */
  // TODO use case has changed and should be refactored
  public toPersonFormModel<P extends Person>([formGroup, data]: [FormGroup, P]): void {
    if (data) {
      const { physicalAddress, mailingAddress, ...person } = data;

      formGroup.patchValue(person);

      // Safety check first, ensure the form is not null
      const physicalAddressFormGroup = formGroup.get('physicalAddress');
      if (physicalAddressFormGroup) {
        (physicalAddress)
          ? physicalAddressFormGroup.patchValue(physicalAddress)
          : physicalAddressFormGroup.reset({ id: 0 });
      }

      // Parties don't always have a mailing address section in the form
      const mailingAddressFormGroup = formGroup.get('mailingAddress');
      if (mailingAddressFormGroup) {
        (mailingAddress)
          ? mailingAddressFormGroup.patchValue(mailingAddress)
          : mailingAddressFormGroup.reset({ id: 0 });
      }
    }
  }

  /**
   * @description
   * Convert the party form model into JSON.
   */
  public toPersonJson<P extends Person>(person: P, addressKey: 'physicalAddress' | 'mailingAddress' = 'physicalAddress'): P {
    if (!person.firstName) {
      person = null;
    } else if (person[addressKey] && !person[addressKey].street) {
      person[addressKey] = null;
    } else if (person[addressKey].street && !person[addressKey].id) {
      person[addressKey].id = 0;
    }

    if (person) {
      // Add the address reference ID to the party
      person[`${addressKey}Id`] = (!!person[addressKey]?.id)
        ? person[addressKey].id
        : 0;
    }

    return person;
  }
}
