import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Site } from '@registration/shared/models/site.model';

interface SiteInfoPageDataModel extends Pick<Site, 'doingBusinessAs' | 'pec'> { }

export class SiteInfoPageFormState extends AbstractFormState<SiteInfoPageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): any {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(data: SiteInfoPageDataModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(data);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      doingBusinessAs: [
        '',
        [Validators.required]
      ],
      pec: [
        null,
        [Validators.required]
      ]
    });
  }
}
