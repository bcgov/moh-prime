import { UntypedFormArray, UntypedFormBuilder, UntypedFormControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationForm } from './self-declaration-form.model';

export class SelfDeclarationFormState extends AbstractFormState<SelfDeclarationForm> {
  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get hasConviction(): UntypedFormControl {
    return this.form.get('hasConviction') as UntypedFormControl;
  }

  public get hasConvictionDetails(): UntypedFormControl {
    return this.form.get('hasConvictionDetails') as UntypedFormControl;
  }

  public get hasRegistrationSuspended(): UntypedFormControl {
    return this.form.get('hasRegistrationSuspended') as UntypedFormControl;
  }

  public get hasRegistrationSuspendedDetails(): UntypedFormControl {
    return this.form.get('hasRegistrationSuspendedDetails') as UntypedFormControl;
  }

  public get hasRegistrationSuspendedDeviceProvider(): UntypedFormControl {
    return this.form.get('hasRegistrationSuspendedDeviceProvider') as UntypedFormControl;
  }

  public get hasRegistrationSuspendedDeviceProviderDetails(): UntypedFormControl {
    return this.form.get('hasRegistrationSuspendedDeviceProviderDetails') as UntypedFormControl;
  }

  public get hasDisciplinaryAction(): UntypedFormControl {
    return this.form.get('hasDisciplinaryAction') as UntypedFormControl;
  }

  public get hasDisciplinaryActionDetails(): UntypedFormControl {
    return this.form.get('hasDisciplinaryActionDetails') as UntypedFormControl;
  }

  public get hasPharmaNetSuspended(): UntypedFormControl {
    return this.form.get('hasPharmaNetSuspended') as UntypedFormControl;
  }

  public get hasPharmaNetSuspendedDetails(): UntypedFormControl {
    return this.form.get('hasPharmaNetSuspendedDetails') as UntypedFormControl;
  }

  public get json(): SelfDeclarationForm {
    if (!this.formInstance) {
      return;
    }

    const selfDeclarations = this.form.getRawValue();
    const selfDeclarationsTypes = {
      hasConviction: SelfDeclarationTypeEnum.HAS_CONVICTION,
      hasDisciplinaryAction: SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION,
      hasPharmaNetSuspended: SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED,
      hasRegistrationSuspended: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED,
      hasRegistrationSuspendedDeviceProvider: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED_DEVICE_PROVIDER
    };
    return {
      selfDeclarations: Object.keys(selfDeclarationsTypes)
        .reduce((sds: SelfDeclaration[], sd: string) => {
          if (selfDeclarations[sd]) {
            sds.push(
              new SelfDeclaration(
                selfDeclarationsTypes[sd],
                selfDeclarations[`${sd}Details`],
                selfDeclarations[`${sd}DocumentGuids`]
              )
            );
          }
          return sds;
        }, [])
    };
  }

  /**
   * @description
   * Patch the self declaration form.
   *
   * NOTE: Default value should track the completion of the enrolment which
   * indicates that this view has been submitted at least once, and the
   * questions should be marked as "No", otherwise the user should be
   * forced to answer the questions.
   */
  public patchValue(pageModel: SelfDeclarationForm, defaultValue: boolean | null): void {
    if (!this.formInstance) {
      return;
    }

    const selfDeclarationsTypes = {
      hasConviction: SelfDeclarationTypeEnum.HAS_CONVICTION,
      hasRegistrationSuspended: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED,
      hasRegistrationSuspendedDeviceProvider: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED_DEVICE_PROVIDER,
      hasDisciplinaryAction: SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION,
      hasPharmaNetSuspended: SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED
    };
    const selfDeclarations = Object.keys(selfDeclarationsTypes)
      .reduce((sds, sd) => {
        const type = selfDeclarationsTypes[sd];
        const selfDeclarationDetails = pageModel.selfDeclarations
          .find(esd => esd.selfDeclarationTypeCode === type)
          ?.selfDeclarationDetails;
        const adapted = {
          [sd]: (selfDeclarationDetails) ? true : defaultValue,
          [`${sd}Details`]: (selfDeclarationDetails) ? selfDeclarationDetails : null
        };
        return { ...sds, ...adapted };
      }, {});

    this.form.patchValue(selfDeclarations);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      hasConviction: [null, [FormControlValidators.requiredBoolean]],
      hasConvictionDetails: [null, []],
      hasConvictionDocumentGuids: this.fb.array([]),
      hasRegistrationSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasRegistrationSuspendedDetails: [null, []],
      hasRegistrationSuspendedDocumentGuids: this.fb.array([]),
      hasRegistrationSuspendedDeviceProvider: [null, [FormControlValidators.requiredBoolean]],
      hasRegistrationSuspendedDeviceProviderDetails: [null, []],
      hasRegistrationSuspendedDeviceProviderDocumentGuids: this.fb.array([]),
      hasDisciplinaryAction: [null, [FormControlValidators.requiredBoolean]],
      hasDisciplinaryActionDetails: [null, []],
      hasDisciplinaryActionDocumentGuids: this.fb.array([]),
      hasPharmaNetSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasPharmaNetSuspendedDetails: [null, []],
      hasPharmaNetSuspendedDocumentGuids: this.fb.array([])
    });
  }

  /**
   * @description
   * Add document GUIDs to the self declaration form.
   */
  public addSelfDeclarationDocumentGuid(controlName: string, value: string) {
    const control = this.formInstance.get(controlName) as UntypedFormArray;
    control.push(this.fb.control(value));
  }

  /**
   * @description
   * Remove document GUIDs to the self declaration form.
   */
  public removeSelfDeclarationDocumentGuid(controlName: string, documentGuid: string) {
    const control = this.formInstance.get(controlName) as UntypedFormArray;
    control.removeAt(control.value.findIndex((guid: string) => guid === documentGuid));
  }

  /**
   * @description
   * Clear all document GUIDs to the self declaration form.
   */
  public clearSelfDeclarationDocumentGuids() {
    [
      'hasConvictionDocumentGuids',
      'hasRegistrationSuspendedDocumentGuids',
      'hasRegistrationSuspendedDeviceProviderDocumentGuids',
      'hasDisciplinaryActionDocumentGuids',
      'hasPharmaNetSuspendedDocumentGuids'
    ]
      .map((formArrayName: string) => this.form.get(formArrayName) as UntypedFormArray)
      .forEach((formArray: UntypedFormArray) => formArray.clear());
  }
}
