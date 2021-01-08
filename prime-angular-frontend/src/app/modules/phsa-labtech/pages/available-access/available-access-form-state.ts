import { AbstractControl, FormArray, FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { PartyTypeEnum } from '@phsa/shared/enums/party-type.enum';

export interface PartyTypeFormModel {
  partyTypes: PartyTypeEnum[];
}

export class AvailableAccessFormState extends AbstractFormState<PartyTypeFormModel> {
  private availablePartyTypes: PartyTypeEnum[];

  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): PartyTypeFormModel {
    if (!this.formInstance) {
      return;
    }

    const partyTypes = this.toPartyTypes(this.availablePartyTypes);

    return {
      partyTypes
    };
  }

  public patchValue(): void {
    throw new Error('Not Implemented');
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      partyTypes: this.fb.array([], [
        FormArrayValidators.atLeast(1, (control: AbstractControl) => control.value)
      ])
    });
  }

  public buildAvailableAccessFormControls(availablePartyTypes: PartyTypeEnum[]): void {
    const formArray = this.formInstance.get('partyTypes') as FormArray;

    // Store the available types for use when exporting as JSON
    this.availablePartyTypes = availablePartyTypes;

    availablePartyTypes.map((_) =>
      formArray.push(
        this.fb.control(false, [])
      )
    );
  }

  /**
   * @description
   * Convert checkbox values into a party type.
   *
   * NOTE: availablePartyTypes MUST be the exact list of party types in the same
   * order that was passed to buildAvailableAccessFormControls, otherwise you'll
   * break the internet!
   */
  private toPartyTypes(availablePartyTypes: PartyTypeEnum[]): PartyTypeEnum[] {
    // const formArray = this.formInstance.get('partyTypes') as FormArray;
    // return formArray.controls
    //   .map((control: AbstractControl, index: number) =>
    //     (control.value) ? availablePartyTypes[index] : 0
    //   )
    //   .filter(code => code);

    return this.formInstance.value
      .partyTypes
      .map((selected: boolean, index: number) =>
        (selected) ? availablePartyTypes[index] : 0
      )
      .filter((code: PartyTypeEnum) => code);
  }
}
