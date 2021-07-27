import { Injectable } from '@angular/core';
import { FormBuilder, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { PhsaEnrollee } from '@phsa/shared/models/phsa-enrollee.model';
import { AccessCodeFormState } from '@phsa/pages/access-code/access-code-form-state.class';
import { BcscDemographicFormState } from '@phsa/pages/bcsc-demographic/bcsc-demographic-form-state.class';
import { AvailableAccessFormState } from '@phsa/pages/available-access/available-access-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class PhsaEformsFormStateService extends AbstractFormStateService<PhsaEnrollee>{
  public accessCodeFormState: AccessCodeFormState;
  public demographicFormState: BcscDemographicFormState;
  public availableAccessFormState: AvailableAccessFormState;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: ConsoleLoggerService,
    private formUtilsService: FormUtilsService
  ) {
    super(fb, routeStateService, logger);

    this.initialize();
  }

  public get json(): PhsaEnrollee {
    // const accessCode = this.accessCodeFormState.json;
    const demographic = this.demographicFormState.json;
    const partyTypes = this.availableAccessFormState.json;

    return {
      // ...accessCode,
      ...demographic,
      ...partyTypes
    } as PhsaEnrollee;
  }

  public get forms(): AbstractControl[] {
    return [
      // this.accessCodeFormState.form,
      this.demographicFormState.form,
      this.availableAccessFormState.form
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used to
   * clear previous form data from the service.
   */
  protected buildForms(): void {
    this.accessCodeFormState = new AccessCodeFormState(this.fb);
    this.demographicFormState = new BcscDemographicFormState(this.fb, this.formUtilsService);
    this.availableAccessFormState = new AvailableAccessFormState(this.fb);
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(): void {
    // After patching the form is dirty, and needs to be pristine
    // to allow for deactivation modals to work properly
    this.markAsPristine();
  }
}
