import { UntypedFormArray, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RemoteUser } from '@lib/models/remote-user.model';

export class RemoteUsersPageFormState extends AbstractFormState<RemoteUser[]> {
  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get remoteUsers(): UntypedFormArray {
    return this.formInstance.get('remoteUsers') as UntypedFormArray;
  }

  public get hasRemoteUsers(): UntypedFormControl {
    return this.formInstance.get('hasRemoteUsers') as UntypedFormControl;
  }

  public get remoteUserCertification(): UntypedFormGroup {
    return this.formInstance.get('remoteUserCertification') as UntypedFormGroup;
  }

  public getRemoteUsers(): RemoteUser[] {
    return this.remoteUsers.getRawValue() as RemoteUser[];
  }

  public get json(): RemoteUser[] {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue().remoteUsers;
  }

  public patchValue(remoteUsers: RemoteUser[], forcePatch: boolean = false): void {
    // We want to force patch if the user goes back to previous page after removing all remote users
    // that the length is now 0 for remote users.
    if (!this.formInstance || (!remoteUsers?.length && !forcePatch)) {
      return;
    }

    const remoteUsersFormArray = this.formInstance.get('remoteUsers') as UntypedFormArray;
    remoteUsersFormArray.clear(); // Clear out existing indices

    if (remoteUsers?.length) {
      // Omitted from payload, but provided in the form to allow for
      // validation to occur when "Have Remote Users" is toggled
      this.formInstance.get('hasRemoteUsers').patchValue(!!remoteUsers.length);

      remoteUsers.forEach((remoteUser: RemoteUser) => {
        const group = this.createEmptyRemoteUserFormAndPatch(remoteUser);
        remoteUsersFormArray.push(group);
      });
    }
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      // Omitted from payload, but provided in the form to allow for
      // validation to occur when "Have Remote Users" is toggled
      hasRemoteUsers: [
        false,
        []
      ],
      remoteUsers: this.fb.array(
        [],
        []
      )
    });
  }

  /**
   * @description
   * Create an empty remote user form group, and patch
   * it with a remote user if provided.
   */
  public createEmptyRemoteUserFormAndPatch(remoteUser: RemoteUser = null): UntypedFormGroup {
    const group = this.remoteUserFormGroup();

    if (remoteUser) {
      group.patchValue(remoteUser);
    }

    return group;
  }

  private remoteUserFormGroup(): UntypedFormGroup {
    return this.fb.group({
      id: [
        0,
        []
      ],
      firstName: [
        null,
        [Validators.required]
      ],
      lastName: [
        null,
        [Validators.required]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ],
      remoteUserCertification: this.fb.group({
        // Force selection of "None" on new certifications
        collegeCode: ['', []],
        // Validators are applied at the component-level when
        // fields are made visible to allow empty submissions
        licenseNumber: [null, []],
        licenseCode: [null, []],
        practitionerId: [null, []],
      }),
      notified: [
        false,
        []
      ]
    });
  }
}
