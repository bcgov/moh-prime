import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { Enrolment } from '@shared/models/enrolment.model';

@Injectable({
  providedIn: 'root'
})
export class PhsaLabtechFormStateService extends AbstractFormState<Enrolment>{
  public accessForm: FormGroup;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService) {
    super(fb, routeStateService, logger, []);
  }

  public get json(): Enrolment {
    throw new Error('Method not implemented.');
  }
  public get forms(): AbstractControl[] {
    throw new Error('Method not implemented.');
  }

  protected buildForms(): void {
    this.accessForm = this.buildAccessForm();
  }

  protected patchForm(model: Enrolment): void {
    throw new Error('Method not implemented.');
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
