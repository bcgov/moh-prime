import { UntypedFormArray, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

import { IndividualDeviceProvider } from '@registration/shared/models/individual-device-provider.model';

export class DeviceProviderPageFormState extends AbstractFormState<IndividualDeviceProvider[]> {
  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get individualDeviceProviders(): UntypedFormArray {
    return this.formInstance.get('individualDeviceProviders') as UntypedFormArray;
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

    this.clearIndividualDeviceProviders();

    providers.forEach((p: IndividualDeviceProvider) => {
      const provider = this.buildIndividualDeviceProvider();
      provider.patchValue(p);
      this.individualDeviceProviders.push(provider);
    });
  }

  public individualDeviceProviderAt(index: number): UntypedFormGroup {
    return this.individualDeviceProviders.at(index) as UntypedFormGroup;
  }

  public clearIndividualDeviceProviders(): void {
    this.individualDeviceProviders.clear();
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      individualDeviceProviders: this.fb.array([]),
    });
  }

  public buildIndividualDeviceProvider(): UntypedFormGroup {
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
