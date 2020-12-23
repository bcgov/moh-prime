import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';

import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';
import { PhsaEnrollee } from '@phsa/shared/models/phsa-lab-tech.model';

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
    // TODO: Needs to get value (code) of selected checkboxes
    const { partyTypes } = this.availableAccessForm.getRawValue();
    return {
      phone,
      phoneExtension,
      email,
      partyTypes
    } as PhsaEnrollee
  }

  public get forms(): AbstractControl[] {
    return [
      this.demographicsForm,
      this.availableAccessForm];
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

  protected patchForm(enrollee: PhsaEnrollee): void {
    this.demographicsForm.patchValue(enrollee);
  }

  private buildAvailableAccessForm(): FormGroup {
    return this.fb.group({
    });
  }

  private buildDemographicsForm(): FormGroup {
    return this.fb.group({
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      phoneExtension: [null, [FormControlValidators.numeric]],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
    });
  }

  private buildAccessForm(): FormGroup {
    return this.fb.group({
      accessCode: ['', [
        Validators.required,
        Validators.pattern(/^crabapples$/)
      ]]
    });
  }
}
