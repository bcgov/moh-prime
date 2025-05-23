import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Address, AddressType, addressTypes } from '@lib/models/address.model';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { AuthorizedUserForm } from './authorized-user-form.model';

export class AuthorizedUserFormState extends AbstractFormState<AuthorizedUserForm> {
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

  public get healthAuthorityCode(): UntypedFormControl {
    return this.formInstance.get('healthAuthorityCode') as UntypedFormControl;
  }

  public get smsPhone(): UntypedFormControl {
    return this.formInstance.get('smsPhone') as UntypedFormControl;
  }

  public get email(): UntypedFormControl {
    return this.formInstance.get('email') as UntypedFormControl;
  }

  public get json(): AuthorizedUserForm {
    if (!this.formInstance) {
      return;
    }

    return this.formToJson(this.formInstance.getRawValue());
  }

  public patchValue(model: AuthorizedUserForm): void {
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
      ]],
      smsPhone: [null, [FormControlValidators.phone]],
      jobRoleTitle: [null, [Validators.required]],
      healthAuthorityCode: [null, [Validators.required]]
    });
  }

  /**
   * @description
   * Sanitize JSON for patching the reactive form.
   */
  private jsonToForm(formGroup: UntypedFormGroup, data: AuthorizedUserForm): void {
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
  private formToJson(authorizedUser: AuthorizedUserForm): AuthorizedUserForm {
    // Minimal check that authorized user is invalid based on
    // a field that is always required
    if (!authorizedUser.firstName) {
      return null;
    }

    addressTypes
      .forEach((addressType: AddressType) => {
        const address = authorizedUser[addressType];
        if (Address.isEmpty(address)) {
          authorizedUser[addressType] = null;
        } else if (address.street && !address.id) {
          // Added to prevent errors on submission when the
          // backend attempts to instantiate the address model
          authorizedUser.id = 0;
        }

        // Add the address reference ID to the authorizedUser
        authorizedUser[`${addressType}Id`] = (!!authorizedUser[addressType]?.id)
          ? authorizedUser[addressType].id
          : 0;
      });

    return authorizedUser;
  }
}
