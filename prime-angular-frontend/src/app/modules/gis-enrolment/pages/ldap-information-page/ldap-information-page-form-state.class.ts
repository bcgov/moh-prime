import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { LdapCredential } from '@gis/shared/models/ldap-credential.model';

interface LdapInformationPageDataModel {
  ldapUsername: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
interface LdapInformationPageFormModel extends LdapCredential {}

export class LdapInformationPageFormState extends AbstractFormState<LdapInformationPageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get ldapUsername(): FormControl {
    return this.formInstance.get('ldapUsername') as FormControl;
  }

  public get ldapPassword(): FormControl {
    return this.formInstance.get('ldapPassword') as FormControl;
  }

  /**
   * @description
   * Direct access provided to the form model for a
   * set of credentials for authentication.
   */
  public get credentials(): LdapInformationPageFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  /**
   * @description
   * Access to the username, but prevents transmission of the
   * password out of the the form state.
   */
  public get json(): LdapInformationPageDataModel {
    if (!this.formInstance) {
      return;
    }

    const { ldapUsername } = this.formInstance.getRawValue();

    return { ldapUsername };
  }

  public patchValue(model: LdapInformationPageDataModel): void {
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

  public clearPassword() {
    this.ldapPassword.reset();
  }
}
