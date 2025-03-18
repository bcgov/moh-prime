import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Address, AddressType, addressTypes } from '@lib/models/address.model';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { DemographicForm } from '@sat/pages/demographic-page/demographic-form.model';

export class DemographicFormState extends AbstractFormState<DemographicForm> {
  public constructor(
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get preferredFirstName(): UntypedFormControl {
    return this.formInstance.get('preferredFirstName') as UntypedFormControl;
  }

  public get preferredMiddleName(): UntypedFormControl {
    return this.formInstance.get('preferredMiddleName') as UntypedFormControl;
  }

  public get preferredLastName(): UntypedFormControl {
    return this.formInstance.get('preferredLastName') as UntypedFormControl;
  }

  public get verifiedAddress(): UntypedFormGroup {
    return this.formInstance.get('verifiedAddress') as UntypedFormGroup;
  }

  public get physicalAddress(): UntypedFormGroup {
    return this.formInstance.get('physicalAddress') as UntypedFormGroup;
  }

  public get phone(): UntypedFormControl {
    return this.formInstance.get('phone') as UntypedFormControl;
  }

  public get smsPhone(): UntypedFormControl {
    return this.formInstance.get('smsPhone') as UntypedFormControl;
  }

  public get email(): UntypedFormControl {
    return this.formInstance.get('email') as UntypedFormControl;
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

  public buildForm(): void {
    this.formInstance = this.fb.group({
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
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
  private jsonToForm(formGroup: UntypedFormGroup, data: DemographicForm): void {
    if (data) {
      const { physicalAddress, ...remainder } = data;
      const addresses = { physicalAddress };

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
