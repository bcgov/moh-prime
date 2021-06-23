import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';

export interface DemographicFormModel {
  firstName: string;
  middleName: string;
  lastName: string;
  dateOfBirth: string;
  physicalAddress: Address;
  email: string;
  phone: string;
  phoneExtension: string;
  smsPhone: string;
}

export class DemographicFormState extends AbstractFormState<DemographicFormModel> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get firstName(): FormControl {
    return this.form.get('firstName') as FormControl;
  }

  public get middleName(): FormControl {
    return this.form.get('middleName') as FormControl;
  }

  public get lastName(): FormControl {
    return this.form.get('lastName') as FormControl;
  }

  public get dateOfBirth(): FormControl {
    return this.form.get('dateOfBirth') as FormControl;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public get json(): DemographicFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: DemographicFormModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      firstName: [null, [Validators.required]],
      middleName: [null, []],
      lastName: [null, [Validators.required]],
      dateOfBirth: [null, [Validators.required]],
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
      smsPhone: [null, [FormControlValidators.phone]]
    });
  }
}
