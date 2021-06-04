import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Site } from '@registration/shared/models/site.model';

// TODO should not be Site from @registration move to @lib/models if model will be shared
interface HealthAuthCareTypePageDataModel extends Pick<Site, 'careSettingCode'> {}

export class HealthAuthCareTypePageFormState extends AbstractFormState<HealthAuthCareTypePageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get careSettingCode(): FormControl {
    return this.formInstance.get('careSettingCode') as FormControl;
  }

  public get json(): any {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(data: HealthAuthCareTypePageDataModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(data);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      careSettingCode: [
        null,
        [Validators.required]
      ]
    });
  }
}
