import { UntypedFormArray, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { DemographicForm } from './demographic-form.model';

export class DemographicFormState extends AbstractFormState<DemographicForm> {
  public constructor(
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get firstName(): UntypedFormControl {
    return this.form.get('firstName') as UntypedFormControl;
  }

  public get middleName(): UntypedFormControl {
    return this.form.get('middleName') as UntypedFormControl;
  }

  public get lastName(): UntypedFormControl {
    return this.form.get('lastName') as UntypedFormControl;
  }

  public get dateOfBirth(): UntypedFormControl {
    return this.form.get('dateOfBirth') as UntypedFormControl;
  }

  public get physicalAddress(): UntypedFormGroup {
    return this.form.get('physicalAddress') as UntypedFormGroup;
  }

  public get additionalAddresses(): UntypedFormArray {
    return this.form.get('additionalAddresses') as UntypedFormArray;
  }

  public addAdditionalAddress(): void {
    const additionalAddress = this.formUtilsService.buildAddressForm({
      areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal']
    });

    this.additionalAddresses.push(additionalAddress);
  }

  public removeAdditionalAddress(index: number): void {
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
