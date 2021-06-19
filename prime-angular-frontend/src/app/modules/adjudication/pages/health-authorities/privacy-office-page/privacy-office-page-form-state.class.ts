import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Contact } from '@lib/models/contact.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormUtilsService } from '@core/services/form-utils.service';
import { FormControlValidators } from '@lib/validators/form-control.validators';

export class PrivacyOfficePageFormState extends AbstractFormState<Contact> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get email(): FormControl {
    return this.formInstance.get('email') as FormControl;
  }

  public get phone(): FormControl {
    return this.formInstance.get('phone') as FormControl;
  }

  public get physicalAddress(): FormGroup {
    return this.formInstance.get('physicalAddress') as FormGroup;
  }

  public get privacyOfficer(): FormGroup {
    return this.formInstance.get('privacyOfficer') as FormGroup;
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
      phone: [
        null,
        [Validators.required, FormControlValidators.phone]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        exclude: ['street2']
      }),
      privacyOfficer: this.fb.group({
        firstName: [
          { value: null, disabled },
          [Validators.required]
        ],
        lastName: [
          { value: null, disabled },
          [Validators.required]
        ],
        phone: [
          null,
          [Validators.required, FormControlValidators.phone]
        ],
        smsPhone: [
          null,
          [FormControlValidators.phone]
        ],
        email: [
          null,
          [Validators.required, FormControlValidators.email]
        ]
      })
    });
  }
}
