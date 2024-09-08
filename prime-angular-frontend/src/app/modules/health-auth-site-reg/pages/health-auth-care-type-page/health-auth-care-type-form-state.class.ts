import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { HealthAuthorityService } from '@health-auth/shared/services/health-authority.service';
import { HealthAuthCareTypeForm } from './health-auth-care-type-form.model';
import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';

export class HealthAuthCareTypeFormState extends AbstractFormState<HealthAuthCareTypeForm> {
  public constructor(
    private fb: FormBuilder,
    private healthAuthorityService: HealthAuthorityService
  ) {
    super();

    this.buildForm();
  }

  public get healthAuthorityVendorId(): FormControl {
    return this.formInstance.get('healthAuthorityVendorId') as FormControl;
  }

  public get healthAuthorityCareTypeId(): FormControl {
    return this.formInstance.get('healthAuthorityCareTypeId') as FormControl;
  }

  public get json(): HealthAuthCareTypeForm {
    if (!this.formInstance) {
      return;
    }

    const { healthAuthorityCareTypeId, healthAuthorityVendorId } = this.formInstance.getRawValue();
    const healthAuthorityCareType = this.healthAuthorityService.healthAuthority.careTypes
      .find(hact => hact.id === healthAuthorityCareTypeId);
    const healthAuthorityVendor: HealthAuthorityVendor = {
      healthAuthorityCareTypeId: healthAuthorityCareTypeId,
      id: healthAuthorityVendorId,
      // Not available at this point and not required downstream
      vendorCode: null
    };

    return { healthAuthorityCareType, healthAuthorityVendor };
  }

  public patchValue(model: HealthAuthCareTypeForm): void {
    if (!model) {
      return;
    }

    const healthAuthorityCareTypeId = model.healthAuthorityCareType?.id;
    const healthAuthorityVendorId = model.healthAuthorityVendor?.id;
    if (!this.formInstance || !healthAuthorityCareTypeId) {
      return;
    }

    this.formInstance.patchValue({ healthAuthorityCareTypeId, healthAuthorityVendorId });
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      healthAuthorityCareTypeId: [
        null,
        [Validators.required]
      ],
      healthAuthorityVendorId: [
        0,
        [FormControlValidators.requiredIndex]
      ],
    });
  }
}
