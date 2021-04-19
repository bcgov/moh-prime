import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormUtilsService } from '@core/services/form-utils.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { HAAuthorizedUser } from '@shared/models/ha-authorized-user.model';

export class HaAuthorizedUserEntryFormState extends AbstractFormState<HAAuthorizedUser> {
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

  public get json(): HAAuthorizedUser {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(user: HAAuthorizedUser): void {
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
