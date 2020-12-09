import { AbstractControl, FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { HealthAuthority } from '@shared/models/health-authority.model';

export interface HealthAuthorityFormModel {
  healthAuthorityCode: HealthAuthorityEnum;
  facilityCodes: number[];
}

export class HealthAuthorityFormState extends AbstractFormState<HealthAuthority[]> {
  public constructor(
    private fb: FormBuilder,
    private configService: ConfigService
  ) {
    super();

    this.buildForm();
  }

  public get json(): HealthAuthority[] {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue()
      .enrolleeHealthAuthorities
      .filter((ha: HealthAuthorityFormModel) => ha.facilityCodes?.length)
      .flatMap((ha: HealthAuthorityFormModel) =>
        ha.facilityCodes.map(f => ({
          healthAuthorityCode: ha.healthAuthorityCode,
          facilityCode: f
        }))
      );
  }

  public patchValue(healthAuthorities: HealthAuthority[]): void {
    if (!this.formInstance || !Array.isArray(healthAuthorities) || !healthAuthorities.length) {
      return;
    }

    // Ensure consistent order of health authorities by always
    // using the config service sort order
    const enrolleeHealthAuthorities = this.configService.healthAuthorities
      .map(c => c.code)
      .map(code => ({
        healthAuthorityCode: code,
        facilityCodes: this.getHealthAuthorityFacilities(healthAuthorities, code)
      }));

    this.formInstance.patchValue({ enrolleeHealthAuthorities });
  }

  public buildForm(): void {
    const healthAuthorities = this.configService.healthAuthorities
      .map(h => this.fb.group({
        healthAuthorityCode: [h.code],
        facilityCodes: [[]]
      }));

    this.formInstance = this.fb.group({
      enrolleeHealthAuthorities: this.fb.array(
        healthAuthorities,
        {
          validators: FormArrayValidators
            .atLeast(1, (control: AbstractControl) => control.get('facilityCodes').value?.length)
        }
      )
    });
  }

  private getHealthAuthorityFacilities(healthAutorities: HealthAuthority[], healthAuthorityCode: HealthAuthorityEnum) {
    return healthAutorities
      .filter(ha => ha.healthAuthorityCode === healthAuthorityCode)
      .map(ha => ha.facilityCode);
  }
}
