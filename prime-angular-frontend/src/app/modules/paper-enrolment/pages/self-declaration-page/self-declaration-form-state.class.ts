import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationForm } from './self-declaration-form.model';

export class SelfDeclarationFormState extends AbstractFormState<SelfDeclarationForm> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get hasConviction(): FormControl {
    return this.form.get('hasConviction') as FormControl;
  }

  public get hasConvictionDetails(): FormControl {
    return this.form.get('hasConvictionDetails') as FormControl;
  }

  public get hasRegistrationSuspended(): FormControl {
    return this.form.get('hasRegistrationSuspended') as FormControl;
  }

  public get hasRegistrationSuspendedDetails(): FormControl {
    return this.form.get('hasRegistrationSuspendedDetails') as FormControl;
  }

  public get hasDisciplinaryAction(): FormControl {
    return this.form.get('hasDisciplinaryAction') as FormControl;
  }

  public get hasDisciplinaryActionDetails(): FormControl {
    return this.form.get('hasDisciplinaryActionDetails') as FormControl;
  }

  public get hasPharmaNetSuspended(): FormControl {
    return this.form.get('hasPharmaNetSuspended') as FormControl;
  }

  public get hasPharmaNetSuspendedDetails(): FormControl {
    return this.form.get('hasPharmaNetSuspendedDetails') as FormControl;
  }

  public get json(): SelfDeclarationForm {
    throw new Error('Method not implemented.');
  }

  public convertSelfDeclarationsToJson(enrolleeId: number): SelfDeclaration[] {
    const selfDeclarations = this.form.getRawValue();
    const selfDeclarationsTypes = {
      hasConviction: SelfDeclarationTypeEnum.HAS_CONVICTION,
      hasDisciplinaryAction: SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION,
      hasPharmaNetSuspended: SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED,
      hasRegistrationSuspended: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED
    };
    return Object.keys(selfDeclarationsTypes)
      .reduce((sds: SelfDeclaration[], sd: string) => {
        if (selfDeclarations[sd]) {
          sds.push(
            new SelfDeclaration(
              selfDeclarationsTypes[sd],
              selfDeclarations[`${sd}Details`],
              selfDeclarations[`${sd}DocumentGuids`],
              enrolleeId
            )
          );
        }
        return sds;
      }, []);
  }

  public patchValue(pageModel: SelfDeclarationForm): void {
    if (!this.formInstance) {
      return;
    }

    const selfDeclarationsTypes = {
      hasConviction: SelfDeclarationTypeEnum.HAS_CONVICTION,
      hasRegistrationSuspended: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED,
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
          [sd]: (selfDeclarationDetails) ? true : null,
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
    const control = this.formInstance.get(controlName) as FormArray;
    control.push(this.fb.control(value));
  }

  /**
   * @description
   * Remove document GUIDs to the self declaration form.
   */
  public removeSelfDeclarationDocumentGuid(controlName: string, documentGuid: string) {
    const control = this.formInstance.get(controlName) as FormArray;
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
      'hasDisciplinaryActionDocumentGuids',
      'hasPharmaNetSuspendedDocumentGuids'
    ]
      .map((formArrayName: string) => this.form.get(formArrayName) as FormArray)
      .forEach((formArray: FormArray) => formArray.clear());
  }
}
