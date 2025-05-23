import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { Party } from '@lib/models/party.model';
import { Address, AddressType, addressTypes } from '@lib/models/address.model';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

export class OrganizationSigningAuthorityPageFormState extends AbstractFormState<Party> {
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

  public get mailingAddress(): UntypedFormGroup {
    return this.formInstance.get('mailingAddress') as UntypedFormGroup;
  }

  public get physicalAddress(): UntypedFormGroup {
    return this.formInstance.get('physicalAddress') as UntypedFormGroup;
  }

  public get phone(): UntypedFormControl {
    return this.formInstance.get('phone') as UntypedFormControl;
  }

  public get fax(): UntypedFormControl {
    return this.formInstance.get('fax') as UntypedFormControl;
  }

  public get smsPhone(): UntypedFormControl {
    return this.formInstance.get('smsPhone') as UntypedFormControl;
  }

  public get email(): UntypedFormControl {
    return this.formInstance.get('email') as UntypedFormControl;
  }

  public get json(): Party {
    if (!this.formInstance) {
      return;
    }

    return this.toPartyJson(this.formInstance.getRawValue());
  }

  public patchValue(party: Party): void {
    if (!this.formInstance) {
      return;
    }

    this.toPartyFormModel(this.formInstance, party);
  }

  // TODO BCSC information form reuse for sharing between enrolment and PHSA
  public buildForm(): void {
    // Prevent BCSC information from being changed
    this.formInstance = this.fb.group({
      id: [0, []], // TODO do we need this?
      firstName: [{ value: null, disabled: true }, []],
      lastName: [{ value: null, disabled: true }, [Validators.required]],
      givenNames: [{ value: null, disabled: true }, []],
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
      verifiedAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['countryCode', 'provinceCode', 'city', 'street', 'postal']
      }),
      mailingAddress: this.formUtilsService.buildAddressForm(),
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
      fax: [null, [FormControlValidators.phone]],
      jobRoleTitle: [null, [Validators.required]]
    });
  }

  /**
   * @description
   * Convert party JSON to form model for reactive forms.
   */
  private toPartyFormModel(formGroup: UntypedFormGroup, data: Party): void {
    if (data) {
      const { verifiedAddress, physicalAddress, mailingAddress, ...person } = data;
      const addresses = { verifiedAddress, physicalAddress, mailingAddress };

      formGroup.patchValue(person);

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
   * Convert the party form model into JSON.
   */
  private toPartyJson(party: Party): Party {
    // Minimal check that party is invalid
    if (!party.lastName) {
      return null;
    }

    addressTypes
      .forEach((addressType: AddressType) => {
        const address = party[addressType];
        if (Address.isEmpty(address)) {
          party[addressType] = null;
        } else if (address.street && !address.id) {
          // Added to prevent errors on submission when the
          // backend attempts to instantiate the address model
          party.id = 0;
        }

        // Add the address reference ID to the party
        party[`${addressType}Id`] = (!!party[addressType]?.id)
          ? party[addressType].id
          : 0;
      });

    return party;
  }
}
