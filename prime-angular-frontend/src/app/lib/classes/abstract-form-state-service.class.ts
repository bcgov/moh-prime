import { AbstractControl, FormBuilder, Validators, FormGroup, ValidatorFn } from '@angular/forms';
import { RouterEvent } from '@angular/router';

import { map, tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AddressLine } from '@lib/types/address-line.type';
import { RouteStateService } from '@core/services/route-state.service';
import { LoggerService } from '@core/services/logger.service';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';

import { Person } from '@registration/shared/models/person.model';

export abstract class AbstractFormStateService<T> {
  protected patched: boolean;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService
  ) { }

  /**
   * @description
   * Check whether the form has been patched.
   */
  public get isPatched() {
    return this.patched;
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   *
   * NOTE: Executed by views to populate their form models, which
   * allows for it to be used for setting required values that
   * can't be loaded during instantiation.
   */
  public setForm(model: T, forcePatch: boolean = false): void {
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
   * Reset all the forms.
   */
  public reset(): void {
    this.patched = false;

    // Recreate the forms to enforce that the expected
    // defaults are maintained, which doesn't occur
    // if AbstractControl::reset() is used
    this.buildForms();
  }

  /**
   * @description
   * Build and configure the forms for patching.
   */
  protected abstract buildForms(): void;

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected abstract patchForm(model: T): void;

  /**
   * @description
   * Determine whether the form should be reset based
   * on the current route path.
   */
  protected checkResetRoutes(currentRoutePath: string, resetRoutes: string[]): boolean {
    return resetRoutes?.includes(currentRoutePath);
  }

  /**
   * @description
   * Listen for a route that is outside of a registration route path
   * to trigger a form reset.
   */
  protected routeStateResetListener(resetRoutes: string[]): void {
    if (!resetRoutes?.length) {
      return;
    }

    this.routeStateService.onNavigationEnd()
      .pipe(
        map((event: RouterEvent) => RouteUtils.currentRoutePath(event.url)),
        tap((routePath: string) => this.logger.info('CURRENT_ROUTE', routePath)),
        map((currentRoutePath: string) => this.checkResetRoutes(currentRoutePath, resetRoutes))
      )
      .subscribe((shouldReset: boolean) => {
        if (shouldReset) {
          this.logger.info('RESET_FORM_STATE');
          this.reset();
        }
      });
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
   *
   * @deprecated moved into FormUtilsService
   */
  protected buildAddressForm(options: {
    areRequired?: AddressLine[],
    areDisabled?: AddressLine[],
    useDefaults?: Extract<AddressLine, 'provinceCode' | 'countryCode'>[],
    exclude?: AddressLine[]
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
        const controlProps = control[0] as { value: any, disabled: boolean };
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
   *
   * @deprecated moved into FormUtilsService
   */
  protected toPersonFormModel<P extends Person>([formGroup, data]: [FormGroup, P]): void {
    if (data) {
      const { physicalAddress, mailingAddress, ...person } = data;

      formGroup.patchValue(person);

      if (physicalAddress) {
        const physicalAddressFormGroup = formGroup.get('physicalAddress');
        (physicalAddress)
          ? physicalAddressFormGroup.patchValue(physicalAddress)
          : physicalAddressFormGroup.reset({ id: 0 });
      }

      // Parties don't always have a mailing address section in the form
      if (formGroup.get('mailingAddress') && mailingAddress) {
        const mailingAddressFormGroup = formGroup.get('mailingAddress');
        (mailingAddress)
          ? mailingAddressFormGroup.patchValue(mailingAddress)
          : mailingAddressFormGroup.reset({ id: 0 });
      }
    }
  }

  /**
   * @description
   * Convert the party form model into JSON.
   *
   * @deprecated moved into FormUtilsService
   */
  protected toPersonJson<P extends Person>(person: P, addressKey: 'physicalAddress' | 'mailingAddress' = 'physicalAddress'): P {
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

  /**
   * @description
   * Initialize the form state service for use by building the required
   * forms and setting up the route state listener.
   *
   * NOTE: Must be invoked by the inheriting class!
   */
  protected initialize(resetRoutes: string[] = []) {
    // Initial state of the form is unpatched and ready for
    // enrolment information to be populated
    this.patched = false;

    this.buildForms();

    if (resetRoutes?.length) {
      this.routeStateResetListener(resetRoutes);
    }
  }
}
