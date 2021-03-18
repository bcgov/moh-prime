import { FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

interface LdapUserPageDataModel {
  ldapLoginSuccessDate: string;
}

export class LdapUserPageFormState extends AbstractFormState<LdapUserPageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  /**
   * @description
   * Does not provide any results.
   *
   * @throws immediately to prevent use.
   */
  public get json(): never {
    throw new Error('Method does not produce a value');
  }

  public patchValue(model: LdapUserPageDataModel): void {
    if (!this.formInstance) {
      return;
    }

    // Confirmed LDAP users automatically checked, otherwise
    // specifically used `null` to force user interaction
    const ldapUser = (!!model.ldapLoginSuccessDate) ? true : null;

    this.formInstance.patchValue({ ldapUser });
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      ldapUser: [null, [FormControlValidators.requiredTruthful]]
    });
  }
}
