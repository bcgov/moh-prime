import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl, FormControl, FormArray } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';

import { PhsaEnrollee } from '@phsa/shared/models/phsa-lab-tech.model';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { PartyTypeEnum } from '@phsa/shared/enums/party-type.enum';

@Injectable({
  providedIn: 'root'
})
export class PhsaFormStateService extends AbstractFormStateService<PhsaEnrollee>{
  public accessForm: FormGroup;
  public demographicsForm: FormGroup;
  public availableAccessForm: FormGroup;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService
  ) {
    super(fb, routeStateService, logger);

    this.initialize();
  }

  public get json(): PhsaEnrollee {
    const { phone, phoneExtension, email } = this.demographicsForm.getRawValue();
    const partyTypes = this.toPartyTypes(this.availableAccessForm.getRawValue().partyTypes);

    return {
      phone,
      phoneExtension,
      email,
      partyTypes
    } as PhsaEnrollee;
  }

  public get forms(): AbstractControl[] {
    return [
      this.demographicsForm,
      this.availableAccessForm
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used to
   * clear previous form data from the service.
   */
  protected buildForms(): void {
    this.accessForm = this.buildAccessForm();
    this.demographicsForm = this.buildDemographicsForm();
    this.availableAccessForm = this.buildAvailableAccessForm();
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(enrollee: PhsaEnrollee): void {
    this.demographicsForm.patchValue(enrollee);
  }

  private buildAccessForm(): FormGroup {
    return this.fb.group({
      accessCode: ['', [
        Validators.required,
        Validators.pattern(/^crabapples$/)
      ]]
    });
  }

  private buildDemographicsForm(): FormGroup {
    return this.fb.group({
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      phoneExtension: [null, [
        FormControlValidators.numeric
      ]],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]]
    });
  }

  private buildAvailableAccessForm(): FormGroup {
    return this.fb.group({
      partyTypes: this.fb.array([], [
        FormArrayValidators.atLeast(1, (control: AbstractControl) => control.value)
      ])
    });
  }

  public buildAvailableAccessFormControls(availablePartyTypes: PartyTypeEnum[]): void {
    const formArray = this.availableAccessForm.get('partyTypes') as FormArray;

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
  public toPartyTypes(availablePartyTypes: PartyTypeEnum[]): PartyTypeEnum[] {
    const formArray = this.availableAccessForm.get('partyTypes') as FormArray;

    return formArray.controls
      .map((control: AbstractControl, index: number) =>
        (control.value) ? availablePartyTypes[index] : 0
      )
      .filter(code => code);
  }
}
