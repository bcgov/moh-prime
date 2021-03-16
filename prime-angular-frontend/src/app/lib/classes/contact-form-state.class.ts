import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Contact } from '@lib/models/contact.model';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { AbstractFormState } from './abstract-form-state.class';
import { Person } from '@lib/models/person.model';

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

    return this.formUtilsService.toPersonJson(this.formInstance.getRawValue());
  }

  public patchValue(model: Contact): void {
    if (!model) {
      return;
    }

    this.formUtilsService.toPersonFormModel([this.formInstance, model]);
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
