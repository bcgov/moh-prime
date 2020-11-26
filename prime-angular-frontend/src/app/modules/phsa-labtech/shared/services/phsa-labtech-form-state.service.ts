import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ArrayUtils } from '@lib/utils/array-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Job } from '@enrolment/shared/models/job.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Site } from '@registration/shared/models/site.model';

@Injectable({
  providedIn: 'root'
})
export class PhsaLabtechFormStateService extends AbstractFormState<Enrolment>{
  public accessForm: FormGroup;

  private identityProvider: IdentityProviderEnum;
  private enrolleeId: number;
  private userId: string;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService,
    private authService: AuthService
  ) {
    super(fb, routeStateService, logger, []);
  }


  private buildAccessForm(): FormGroup {
    return this.fb.group({
      accessCode: ['', [
        Validators.required,
        Validators.pattern(/^crabapples$/)
      ]]
    });
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
}
