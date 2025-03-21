import { UntypedFormBuilder, Validators } from '@angular/forms';
import { FormUtilsService } from '@core/services/form-utils.service';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

export interface PhsaBcscDemographicFormModel {
  phone: string;
  phoneExtension: string;
  email: string;
}

export class BcscDemographicFormState extends AbstractFormState<PhsaBcscDemographicFormModel> {
  public constructor(
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): PhsaBcscDemographicFormModel {
    if (!this.formInstance) {
      return;
    }

    // TODO adapt the data after getting values, ie. address(es)

    return this.formInstance.getRawValue();
  }

  public patchValue(): void {

    // TODO adapt the data before patching values, ie. address(es)

    throw new Error('Not Implemented');
  }

  // TODO BCSC information form reuse for sharing between enrolment and PHSA
  public buildForm(): void {
    this.formInstance = this.fb.group({
      // hpdid: [{ value: null, disabled: true }, [Validators.required]],
      // dateOfBirth: [{ value: null, disabled: true }, [Validators.required]],
      // firstName: [{ value: null, disabled: true }, [Validators.required]],
      // lastName: [{ value: null, disabled: true }, [Validators.required]],
      // givenNames: [{ value: null, disabled: true }, [Validators.required]],
      // verifiedAddress: this.formUtilsService.buildAddressForm(),
      // physicalAddress: this.formUtilsService.buildAddressForm(),
      // mailingAddress: this.formUtilsService.buildAddressForm(),
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      phoneExtension: [null, [
        FormControlValidators.numeric
      ]],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]]
    });
  }
}
