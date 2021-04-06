import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Site } from '@registration/shared/models/site.model';

interface SiteInformationPageDataModel extends Pick<Site, 'doingBusinessAs' | 'pec'> { }

export class SiteInformationPageFormState extends AbstractFormState<SiteInformationPageDataModel> {
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

  public patchValue(data: SiteInformationPageDataModel): void {
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
