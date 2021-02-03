import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrollee } from '@shared/models/enrollee.model';
import { Address } from '@shared/models/address.model';

export interface BcscDemographicFormModel { }

export class BcscDemographicFormState extends AbstractFormState<Enrollee> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): Enrollee {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(enrollee: Enrollee): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(enrollee);
  }

  // TODO BCSC information form reuse for sharing between enrolment and PHSA
  public buildForm(): void {
    this.formInstance = this.fb.group({
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
      phoneExtension: [null, [FormControlValidators.numeric]],
      smsPhone: [null, [FormControlValidators.phone]],
    });
  }
}
