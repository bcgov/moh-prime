import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address, AddressType, addressTypes } from '@shared/models/address.model';

import { DemographicForm } from '@sat/pages/demographic-page/demographic-form.model';

export class DemographicFormState extends AbstractFormState<DemographicForm> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get preferredFirstName(): FormControl {
    return this.formInstance.get('preferredFirstName') as FormControl;
  }

  public get preferredLastName(): FormControl {
    return this.formInstance.get('preferredLastName') as FormControl;
  }

  public get verifiedAddress(): FormGroup {
    return this.formInstance.get('verifiedAddress') as FormGroup;
  }

  public get physicalAddress(): FormGroup {
    return this.formInstance.get('physicalAddress') as FormGroup;
  }

  public get phone(): FormControl {
    return this.formInstance.get('phone') as FormControl;
  }

  public get healthAuthorityCode(): FormControl {
    return this.formInstance.get('healthAuthorityCode') as FormControl;
  }

  public get smsPhone(): FormControl {
    return this.formInstance.get('smsPhone') as FormControl;
  }

  public get email(): FormControl {
    return this.formInstance.get('email') as FormControl;
  }

  public get json(): DemographicForm {
    if (!this.formInstance) {
      return;
    }

    return this.formToJson(this.formInstance.getRawValue());
  }

  public patchValue(model: DemographicForm): void {
    if (!this.formInstance) {
      return;
    }

    this.jsonToForm(this.formInstance, model);
  }

  // TODO BCSC information form reuse for sharing between enrolment and PHSA
  public buildForm(): void {
    // Prevent BCSC information from being changed
    this.formInstance = this.fb.group({
      firstName: [{ value: null, disabled: true }, [Validators.required]],
      lastName: [{ value: null, disabled: true }, [Validators.required]],
      givenNames: [{ value: null, disabled: true }, [Validators.required]],
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
      verifiedAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['countryCode', 'provinceCode', 'city', 'street', 'postal']
      }),
      physicalAddress: this.formUtilsService.buildAddressForm(),
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]]
    });
  }

  /**
   * @description
   * Sanitize JSON for patching the reactive form.
   */
  private jsonToForm(formGroup: FormGroup, data: DemographicForm): void {
    if (data) {
      const { verifiedAddress, physicalAddress, ...remainder } = data;
      const addresses = { verifiedAddress, physicalAddress };

      formGroup.patchValue(remainder);

      Object.keys(addresses)
        .forEach((addressType: AddressType) => {
          const address = addresses[addressType];
          const group = formGroup.get(addressType);
          (address)
            ? group.patchValue(address)
            : group.reset({ id: 0 });
        });
    }
  }

  /**
   * @description
   * Sanitize form value into JSON.
   */
  private formToJson(demographic: DemographicForm): DemographicForm {
    // Minimal check that authorized user is invalid based on
    // a field that is always required
    if (!demographic.firstName) {
      return null;
    }

    addressTypes
      .forEach((addressType: AddressType) => {
        const address = demographic[addressType];
        if (Address.isEmpty(address)) {
          demographic[addressType] = null;
        } else if (address.street && !address.id) {
          // Added to prevent errors on submission when the
          // backend attempts to instantiate the address model
          address.id = 0;
        }

        // Add the address reference ID to the demographic
        demographic[`${addressType}Id`] = (!!demographic[addressType]?.id)
          ? demographic[addressType].id
          : 0;
      });

    return demographic;
  }
}
