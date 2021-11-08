import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

import { IndividualDeviceProvider } from '@registration/shared/models/individual-device-provider.model';

export class DeviceProviderPageFormState extends AbstractFormState<IndividualDeviceProvider[]> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get individualDeviceProviders(): FormArray {
    return this.formInstance.get('individualDeviceProviders') as FormArray;
  }

  public get json(): IndividualDeviceProvider[] {
    if (!this.formInstance) {
      return;
    }

    return this.individualDeviceProviders.getRawValue();
  }

  public patchValue(providers: IndividualDeviceProvider[]): void {
    if (!this.formInstance || !Array.isArray(providers)) {
      return;
    }

    this.individualDeviceProviders.clear(); // Clear out existing indices

    providers.forEach((p: IndividualDeviceProvider) => {
      const provider = this.buildIndividualDeviceProvider();
      provider.patchValue(p);
      this.individualDeviceProviders.push(provider);
    });
  }

  public individualDeviceProviderAt(index: number): FormGroup {
    return this.individualDeviceProviders.at(index) as FormGroup;
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      individualDeviceProviders: this.fb.array([]),
    });
  }

  public buildIndividualDeviceProvider(): FormGroup {
    return this.fb.group({
      firstName: [null, [
        Validators.required
      ]],
      middleName: [null, []],
      lastName: [null, [
        Validators.required
      ]],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
      dateOfBirth: [null, [
        Validators.required
      ]]
    });
  }
}
