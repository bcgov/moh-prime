import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormUtilsService } from '@core/services/form-utils.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { AuthorizedUser } from '@shared/models/authorized-user.model';

export class HaAuthorizedUserEntryFormState extends AbstractFormState<AuthorizedUser> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get firstName(): FormGroup {
    return this.formInstance.get('firstName') as FormGroup;
  }

  public get lastName(): FormGroup {
    return this.formInstance.get('lastName') as FormGroup;
  }

  public get dateOfBirth(): FormGroup {
    return this.formInstance.get('dateOfBirth') as FormGroup;
  }

  public get email(): FormGroup {
    return this.formInstance.get('email') as FormGroup;
  }

  public get json(): AuthorizedUser {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(user: AuthorizedUser): void {
    if (!this.formInstance || !user) {
      return;
    }

    this.formInstance.patchValue(user);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      dateOfBirth: [null, [Validators.required]],
      email: [null, []],
    });
  }
}
