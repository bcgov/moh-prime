import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { DemographicForm } from './demographic-form.model';

export class DemographicFormState extends AbstractFormState<DemographicForm> {
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

  public get additionalAddresses(): FormArray {
    return this.form.get('additionalAddresses') as FormArray;
  }

  public addAdditionalAddress() {
    const additionalAddress = this.formUtilsService.buildAddressForm({
      areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal']
    });

    this.additionalAddresses.push(additionalAddress);
  }

  public removeAdditionalAddress(index: number) {
    this.additionalAddresses.removeAt(index);
  }

  public get json(): DemographicForm {
    if (!this.formInstance) {
      return;
    }

    const { middleName, ...model } = this.formInstance.getRawValue();
    const givenNames = (middleName)
      ? `${model.firstName} ${middleName}`
      : model.firstName;

    return { ...model, givenNames };
  }

  public patchValue(model: DemographicForm): void {
    if (!this.formInstance) {
      return;
    }

    const middleName = (model.givenNames)
      ? model.givenNames.replace(model.firstName, '').trim()
      : '';

    model.additionalAddresses.map((additionalAddress) => {
      const additionalAddressFormGroup = this.formUtilsService.buildAddressForm();

      additionalAddressFormGroup.patchValue(additionalAddress)
      this.additionalAddresses.push(additionalAddressFormGroup);
    });

    this.formInstance.patchValue({ ...model, middleName });
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      firstName: [null, [Validators.required]],
      middleName: [null, []],
      lastName: [null, [Validators.required]],
      dateOfBirth: [null, [Validators.required]],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal']
      }),
      additionalAddresses: this.fb.array([]),
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
