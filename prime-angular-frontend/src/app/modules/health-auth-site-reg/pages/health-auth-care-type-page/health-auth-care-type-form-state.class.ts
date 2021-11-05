import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { HealthAuthorityService } from '@health-auth/shared/services/health-authority.service';
import { HealthAuthCareTypeForm } from './health-auth-care-type-form.model';

export class HealthAuthCareTypeFormState extends AbstractFormState<HealthAuthCareTypeForm> {
  public constructor(
    private fb: FormBuilder,
    private healthAuthorityService: HealthAuthorityService
  ) {
    super();

    this.buildForm();
  }

  public get healthAuthorityCareTypeId(): FormControl {
    return this.formInstance.get('healthAuthorityCareTypeId') as FormControl;
  }

  public get json(): HealthAuthCareTypeForm {
    if (!this.formInstance) {
      return;
    }

    const { healthAuthorityCareTypeId } = this.formInstance.getRawValue();
    const healthAuthorityCareType = this.healthAuthorityService.healthAuthority.careTypes
      .find(hact => hact.id === healthAuthorityCareTypeId);

    return { healthAuthorityCareType };
  }

  public patchValue(model: HealthAuthCareTypeForm): void {
    const healthAuthorityCareTypeId = model.healthAuthorityCareType?.id;
    if (!this.formInstance || !healthAuthorityCareTypeId) {
      return;
    }

    this.formInstance.patchValue({ healthAuthorityCareTypeId });
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      healthAuthorityCareTypeId: [
        0,
        [Validators.required]
      ]
    });
  }
}
