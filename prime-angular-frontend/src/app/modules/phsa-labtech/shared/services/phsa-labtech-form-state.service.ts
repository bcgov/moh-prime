import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';

import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';
import { PhsaLabtech } from '@phsa/shared/models/phsa-lab-tech.model';

@Injectable({
  providedIn: 'root'
})
export class PhsaLabtechFormStateService extends AbstractFormStateService<PhsaLabtech>{
  public accessForm: FormGroup;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService
  ) {
    super(fb, routeStateService, logger);

    this.initialize([PhsaLabtechRoutes.ACCESS_CODE]);
  }

  public get json(): PhsaLabtech {
    throw new Error('Method not implemented.');
  }

  public get forms(): AbstractControl[] {
    throw new Error('Method not implemented.');
  }

  protected buildForms(): void {
    this.accessForm = this.buildAccessForm();
  }

  protected patchForm(model: PhsaLabtech): void {
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
