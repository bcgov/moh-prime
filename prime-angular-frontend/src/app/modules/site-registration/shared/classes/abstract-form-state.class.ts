import { AbstractControl, FormBuilder, Validators, FormGroup } from '@angular/forms';

import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';

import { Party } from '../models/party.model';

export abstract class AbstractFormState<T> {
  protected patched: boolean;

  constructor(
    protected fb: FormBuilder
  ) {
    // Initial state of the form is unpatched and ready for
    // enrolment information
    this.patched = false;

    this.init();
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   */
  public setForm(model: T, forcePatch: boolean = false) {
    if (this.patched && !forcePatch) {
      return;
    }

    // Indicate that the form is patched, and may contain unsaved information
    this.patched = true;

    this.patchForm(model);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public abstract get json(): T;

  /**
   * @description
   * List of constituent model forms, which is used at minimum to
   * drive internal form helper methods.
   */
  public abstract get forms(): AbstractControl[];

  /**
   * @description
   * Check that all constituent forms are valid.
   */
  public get isValid(): boolean {
    return this.forms
      .reduce((valid: boolean, form: AbstractControl) => valid && form.valid, true);
  }

  /**
   * @description
   * Check that at least one constituent form is dirty.
   */
  public get isDirty(): boolean {
    return this.forms
      .reduce((dirty: boolean, form: AbstractControl) => dirty || form.dirty, false);
  }

  /**
   * @description
   * Mark all constituent forms as pristine.
   */
  public markAsPristine(): void {
    this.forms
      .forEach((form: AbstractControl) => form.markAsPristine());
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  public abstract init(): void;

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected abstract patchForm(model: T): T;

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
  // TODO start sliding this into form builders
  protected buildAddressForm(options: {
    areRequired?: string[],
    areDisabled?: string[],
    useDefaults?: boolean,
    exclude?: string[]
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
      // TODO not always used so should be able to omit the key
      // from the form controls
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
      .filter((key: string) => !options?.exclude?.includes(key))
      .map((key: string, index: number) => {
        const control = controlsConfig[key];
        if (options?.areDisabled?.includes(key)) {
          control[0].disabled = true;
        }
        if (options?.useDefaults) {
          if (key === 'provinceCode') {
            control[0].value = Province.BRITISH_COLUMBIA;
          } else if (key === 'countryCode') {
            control[0].value = Country.CANADA;
          }
        }
        if (options?.areRequired?.includes(key)) {
          control[1].push(Validators.required);
        }
      });

    return this.fb.group(controlsConfig);
  }

  /**
   * @description
   * Convert party JSON to form model for reactive forms.
   */
  protected toPartyFormModel([formGroup, data]: [FormGroup, Party]): void {
    const { physicalAddress, mailingAddress, ...party } = data;

    formGroup.patchValue(party);

    if (physicalAddress) {
      const physicalAddressFormGroup = formGroup.get('physicalAddress');
      (physicalAddress)
        ? physicalAddressFormGroup.patchValue(physicalAddress)
        : physicalAddressFormGroup.reset({ id: 0 });
    }

    if (mailingAddress) {
      const mailingAddressFormGroup = formGroup.get('mailingAddress');
      (mailingAddress)
        ? mailingAddressFormGroup.patchValue(mailingAddress)
        : mailingAddressFormGroup.reset({ id: 0 });
    }
  }

  /**
   * @description
   * Convert the party form model into JSON.
   */
  protected toPartyJson(party: Party, addressKey: 'physicalAddress' | 'mailingAddress' = 'physicalAddress'): Party {
    if (!party.firstName) {
      party = null;
    } else if (party[addressKey] && !party[addressKey].street) {
      party[addressKey] = null;
    } else if (party[addressKey].street && !party[addressKey].id) {
      party[addressKey].id = 0;
    }

    if (party) {
      // Add the address reference ID to the party
      party[`${addressKey}Id`] = (!!party[addressKey]?.id)
        ? party[addressKey].id
        : 0;
    }

    return party;
  }
}
