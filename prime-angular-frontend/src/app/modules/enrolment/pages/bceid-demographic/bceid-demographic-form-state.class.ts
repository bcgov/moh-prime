import { UntypedFormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrollee } from '@shared/models/enrollee.model';

export class BceidDemographicFormState extends AbstractFormState<Enrollee> {
  public constructor(
    private fb: UntypedFormBuilder,
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

  public buildForm(): void {
    this.formInstance = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      mailingAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        useDefaults: ['countryCode']
      }),
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      phoneExtension: [null, [FormControlValidators.numeric]],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
      smsPhone: [null, [FormControlValidators.phone]],
      // Disabled by default, but enabled for enrollee creation
      dateOfBirth: [{ value: null, disabled: true }, [Validators.required]]
    });
  }
}
