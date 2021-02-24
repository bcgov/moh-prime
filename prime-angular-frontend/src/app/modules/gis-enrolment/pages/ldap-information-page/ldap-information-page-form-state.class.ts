import { FormBuilder, Validators } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

export interface LdapInformationPageFormModel {
  ldapUsername: string;
  ldapPassword: string;
}

export class LdapInformationPageFormState extends AbstractFormState<LdapInformationPageFormModel> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): LdapInformationPageFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: LdapInformationPageFormModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      ldapUsername: [null, [Validators.required]],
      ldapPassword: [null, [Validators.required]]
    });
  }
}
