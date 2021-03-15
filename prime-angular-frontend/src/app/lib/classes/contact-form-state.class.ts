import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';
import { FormControlValidators } from '@lib/validators/form-control.validators';

import { AbstractFormState } from './abstract-form-state.class';
import { Contact } from '@registration/shared/models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactFormState extends AbstractFormState<Contact> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): Contact {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(data: Contact): void {
    if (!data) {
      return;
    }

    this.formInstance.patchValue(data);
  }

  public buildForm(disabled: boolean = false): void {
    this.formInstance = this.fb.group({
      id: [
        0,
        []
      ],
      firstName: [
        { value: null, disabled },
        [Validators.required]
      ],
      lastName: [
        { value: null, disabled },
        [Validators.required]
      ],
      jobRoleTitle: [
        null,
        [Validators.required]
      ],
      phone: [
        null,
        [Validators.required, FormControlValidators.phone]
      ],
      fax: [
        null,
        [FormControlValidators.phone]
      ],
      smsPhone: [
        null,
        [FormControlValidators.phone]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ],
      physicalAddress: this.formUtilsService.buildAddressForm({
        exclude: ['street2']
      })
    });
  }
}
