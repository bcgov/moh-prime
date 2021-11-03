import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { IndividualDeviceProvider } from '@registration/shared/models/individual-device-provider.model';

export class DeviceProviderPageFormState extends AbstractFormState<IndividualDeviceProvider> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get firstName(): FormControl {
    return this.formInstance.get('firstName') as FormControl;
  }

  public get middleName(): FormControl {
    return this.formInstance.get('middleName') as FormControl;
  }

  public get lastName(): FormControl {
    return this.formInstance.get('lastName') as FormControl;
  }

  public get dateOfBirth(): FormControl {
    return this.formInstance.get('dateOfBirth') as FormControl;
  }

  public get email(): FormControl {
    return this.formInstance.get('email') as FormControl;
  }

  public get json(): IndividualDeviceProvider {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: IndividualDeviceProvider): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      id: [0, []],
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
