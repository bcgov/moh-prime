import { AbstractControl, UntypedFormArray, UntypedFormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

import { PartyTypeEnum } from '@phsa/shared/enums/party-type.enum';

export interface PartyTypeFormModel {
  partyTypes: PartyTypeEnum[];
}

export class AvailableAccessFormState extends AbstractFormState<PartyTypeFormModel> {
  private availablePartyTypes: PartyTypeEnum[];

  public constructor(
    private fb: UntypedFormBuilder
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
    const formArray = this.formInstance.get('partyTypes') as UntypedFormArray;

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
   * Convert checkbox values (booleans) into a party type.
   */
  private toPartyTypes(availablePartyTypes: PartyTypeEnum[]): PartyTypeEnum[] {
    return this.formInstance.value
      .partyTypes
      .map((selected: boolean, index: number) =>
        (selected) ? availablePartyTypes[index] : 0
      )
      .filter((code: PartyTypeEnum) => code);
  }
}
