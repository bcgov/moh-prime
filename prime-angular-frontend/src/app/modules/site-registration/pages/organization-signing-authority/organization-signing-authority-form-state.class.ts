import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';
import { Party } from '@registration/shared/models/party.model';

export interface OrganizationSigningAuthorityFormModel { }

export class OrganizationSigningAuthorityFormState extends AbstractFormState<Party> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): Party {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(party: Party): void {
    if (!this.formInstance) {
      return;
    }

    console.log('TEMPORARY TO ALLOW WORK!!!');
    // TODO add to adapters so backend can send null
    if (!party.verifiedAddress) {
      party.verifiedAddress = new Address();
    }

    this.formInstance.patchValue(party);
  }

  // TODO BCSC information form reuse for sharing between enrolment and PHSA
  public buildForm(): void {
    // Prevent BCSC information from being changed
    this.formInstance = this.fb.group({
      id: [0, []],
      firstName: [{ value: null, disabled: true }, [Validators.required]],
      lastName: [{ value: null, disabled: true }, [Validators.required]],
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
      dateOfBirth: [null, [Validators.required]],
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
}
