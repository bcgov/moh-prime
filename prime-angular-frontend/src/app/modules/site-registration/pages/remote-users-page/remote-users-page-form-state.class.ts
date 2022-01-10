import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RemoteUser } from '@lib/models/remote-user.model';

export class RemoteUsersPageFormState extends AbstractFormState<RemoteUser[]> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get remoteUsers(): FormArray {
    return this.formInstance.get('remoteUsers') as FormArray;
  }

  public get hasRemoteUsers(): FormControl {
    return this.formInstance.get('hasRemoteUsers') as FormControl;
  }

  public get remoteUserCertification(): FormGroup {
    return this.formInstance.get('remoteUserCertification') as FormGroup;
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

  public patchValue(remoteUsers: RemoteUser[]): void {
    if (!this.formInstance || !remoteUsers?.length) {
      return;
    }

    const remoteUsersFormArray = this.formInstance.get('remoteUsers') as FormArray;
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
  public createEmptyRemoteUserFormAndPatch(remoteUser: RemoteUser = null): FormGroup {
    const group = this.remoteUserFormGroup();

    if (remoteUser.remoteUserCertifications) {
      remoteUser.remoteUserCertification = remoteUser.remoteUserCertifications?.[0];
    }

    if (remoteUser) {
      const { id, firstName, lastName, email, remoteUserCertification, notified } = remoteUser;
      group.patchValue({ id, firstName, lastName, email, notified, remoteUserCertification });
    }

    return group;
  }

  private remoteUserFormGroup(): FormGroup {
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
        licenseCode: [null, []]
      }),
      notified: [
        false,
        []
      ]
    });
  }
}
