import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrollee } from '@shared/models/enrollee.model';

export class BcscDemographicFormState extends AbstractFormState<Enrollee> {
  public constructor(
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get preferredFirstName(): UntypedFormControl {
    return this.formInstance.get('preferredFirstName') as UntypedFormControl;
  }

  public get preferredMiddleName(): UntypedFormControl {
    return this.formInstance.get('preferredMiddleName') as UntypedFormControl;
  }

  public get preferredLastName(): UntypedFormControl {
    return this.formInstance.get('preferredLastName') as UntypedFormControl;
  }

  public get verifiedAddress(): UntypedFormGroup {
    return this.formInstance.get('verifiedAddress') as UntypedFormGroup;
  }

  public get physicalAddress(): UntypedFormGroup {
    return this.formInstance.get('physicalAddress') as UntypedFormGroup;
  }

  public get mailingAddress(): UntypedFormGroup {
    return this.formInstance.get('mailingAddress') as UntypedFormGroup;
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

  // TODO BCSC information form reuse for sharing between enrolment and PHSA
  public buildForm(): void {
    this.formInstance = this.fb.group({
      firstName: [{ value: null, disabled: true }, [Validators.required]],
      lastName: [{ value: null, disabled: true }, [Validators.required]],
      givenNames: [{ value: null, disabled: true }, [Validators.required]],
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
      // Expected in most cases provided by BCSC identity provider
      verifiedAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['countryCode', 'provinceCode', 'city', 'street', 'postal']
      }),
      physicalAddress: this.formUtilsService.buildAddressForm(),
      mailingAddress: this.formUtilsService.buildAddressForm(),
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
