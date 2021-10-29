import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RemoteUser } from '@lib/models/remote-user.model';
import { RemoteUserCertification } from '@lib/models/remote-user-certification.model';
import { RemoteUsersForm } from '@health-auth/pages/remote-users-page/remote-users-form.model';

export class RemoteUsersFormState extends AbstractFormState<RemoteUsersForm> {
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

  public get remoteUserCertifications(): FormArray {
    return this.formInstance.get('remoteUserCertifications') as FormArray;
  }

  public getRemoteUsers(): RemoteUser[] {
    return this.remoteUsers.getRawValue() as RemoteUser[];
  }

  public get json(): RemoteUsersForm {
    if (!this.formInstance) {
      return;
    }

    const remoteUsers = this.formInstance.getRawValue().remoteUsers
      .map((ru: RemoteUser) => {
        // Remove the ID from the remote user to simplify updates on the server
        const { id, ...remoteUser } = ru;
        return remoteUser;
      });

    return { remoteUsers };
  }

  public patchValue({ remoteUsers }: RemoteUsersForm): void {
    if (!this.formInstance || !remoteUsers?.length) {
      return;
    }

    // const remoteUsersFormArray = this.formInstance.get('remoteUsers') as FormArray;
    // remoteUsersFormArray.clear(); // Clear out existing indices
    //
    // if (remoteUsers?.length) {
    //   // Omitted from payload, but provided in the form to allow for
    //   // validation to occur when "Have Remote Users" is toggled
    //   this.formInstance.get('hasRemoteUsers').patchValue(!!remoteUsers.length);
    //
    //   remoteUsers.forEach((remoteUser: RemoteUser) => {
    //     const group = this.createEmptyRemoteUserFormAndPatch(remoteUser);
    //     remoteUsersFormArray.push(group);
    //   });
    // }
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
      // TODO at least one remote users is required
      // [FormArrayValidators.atLeast(1)]
    });
  }

  /**
   * @description
   * Create an empty remote user form group, and patch
   * it with a remote user if provided.
   */
  public createEmptyRemoteUserFormAndPatch(remoteUser: RemoteUser = null): FormGroup {
    const group = this.remoteUserFormGroup();

    if (remoteUser) {
      const { id, firstName, lastName, email, remoteUserCertifications } = remoteUser;
      group.patchValue({ id, firstName, lastName, email });

      const certs = group.get('remoteUserCertifications') as FormArray;
      remoteUserCertifications.map((cert: RemoteUserCertification) => {
        const formGroup = this.remoteUserCertificationFormGroup();
        formGroup.patchValue(cert);
        return formGroup;
      }).forEach((remoteUserCertificationFormGroup: FormGroup) =>
        certs.push(remoteUserCertificationFormGroup)
      );
    }

    return group;
  }

  public remoteUserCertificationFormGroup(): FormGroup {
    return this.fb.group({
      // Force selection of "None" on new certifications
      collegeCode: ['', []],
      // Validators are applied at the component-level when
      // fields are made visible to allow empty submissions
      licenseNumber: [null, []],
      licenseCode: [null, []]
    });
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
      remoteUserCertifications: this.fb.array(
        [],
        { validators: FormArrayValidators.atLeast(1) }
      )
    });
  }
}
