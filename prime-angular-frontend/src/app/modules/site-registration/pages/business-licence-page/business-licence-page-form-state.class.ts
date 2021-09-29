import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { Observable } from 'rxjs';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { SiteResource } from '@core/resources/site-resource.service';

import { Site } from '@registration/shared/models/site.model';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';

type BusinessLicencePageDataModel = Pick<Site, 'businessLicence' | 'doingBusinessAs' | 'pec'>

export class BusinessLicencePageFormState extends AbstractFormState<BusinessLicencePageDataModel> {
  private businessLicence: BusinessLicence;

  public constructor(
    private fb: FormBuilder,
    private siteResource: SiteResource
  ) {
    super();

    this.buildForm();
  }

  public get businessLicenceGuid(): FormControl {
    return this.formInstance.get('businessLicenceGuid') as FormControl;
  }

  public get businessLicenceExpiry(): FormControl {
    return this.formInstance.get('expiryDate') as FormControl;
  }

  public get deferredLicenceReason(): FormControl {
    return this.formInstance.get('deferredLicenceReason') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.formInstance.get('doingBusinessAs') as FormControl;
  }

  public get json(): BusinessLicencePageDataModel {
    if (!this.formInstance) {
      return;
    }

    const { expiryDate, deferredLicenceReason, doingBusinessAs, pec } = this.formInstance.getRawValue();

    return {
      businessLicence: {
        ...this.businessLicence,
        expiryDate,
        deferredLicenceReason
      },
      doingBusinessAs,
      pec
    };
  }

  public patchValue(model: BusinessLicencePageDataModel & { businessLicence: BusinessLicence; }): void {
    if (!this.formInstance) {
      return;
    }

    const { doingBusinessAs, pec, businessLicence } = model;
    this.businessLicence = businessLicence;

    this.formInstance.patchValue({
      ...businessLicence,
      doingBusinessAs,
      pec
    });
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      businessLicenceGuid: [
        // Will never be patched when the form is built, and is
        // only updated based on a document upload occurring
        '',
        []
      ],
      expiryDate: [
        '',
        [Validators.required]
      ],
      deferredLicenceReason: [
        '',
        []
      ],
      doingBusinessAs: [
        '',
        [Validators.required]
      ],
      pec: [
        null,
        [
          Validators.required,
          FormControlValidators.requiredLength(3),
          FormControlValidators.alpha
        ],
        // TODO revisit async validator
        // FormControlValidators.uniqueAsync(this.checkPecIsUnique())
      ]
    });
  }

  private checkPecIsUnique(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecExists(value);
  }
}
