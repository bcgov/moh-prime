import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { Observable } from 'rxjs';

import { asyncValidator } from '@lib/validators/form-async.validators';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { SiteResource } from '@core/resources/site-resource.service';

import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { BusinessLicenceForm } from './business-licence-form.model';

export class BusinessLicenceFormState extends AbstractFormState<BusinessLicenceForm> {
  private siteId: number;
  private businessLicence: BusinessLicence;
  private businessLicenceUpdated: boolean;

  public constructor(
    private fb: FormBuilder,
    private siteResource: SiteResource
  ) {
    super();
    this.businessLicenceUpdated = false;
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

  public get pec(): FormControl {
    return this.formInstance.get('pec') as FormControl;
  }

  public get json(): BusinessLicenceForm {
    if (!this.formInstance) {
      return;
    }

    const { expiryDate, deferredLicenceReason, doingBusinessAs, pec, activeBeforeRegistration } = this.formInstance.getRawValue();

    return {
      businessLicence: {
        ...this.businessLicence,
        expiryDate,
        deferredLicenceReason
      },
      doingBusinessAs,
      pec,
      activeBeforeRegistration
    };
  }

  public get isBusinessLicenceUpdated() {
    return this.businessLicenceUpdated;
  }

  public flagBusinessLicenceUpdated(flag: boolean = true) {
    this.businessLicenceUpdated = flag;
  }

  public patchValue(model: BusinessLicenceForm, siteId: number): void {
    if (!this.formInstance) {
      return;
    }

    this.siteId = siteId;

    const { doingBusinessAs, pec, businessLicence } = model;
    // Preserve the business licence for use when
    // creating JSON format from the form
    this.businessLicence = businessLicence;

    this.formInstance.patchValue({
      ...businessLicence,
      doingBusinessAs,
      pec
    });
  }

  public patchDocument(document: BusinessLicenceDocument): void {
    if (!this.businessLicence) {
      return;
    }
    this.businessLicence.businessLicenceDocument = document;
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      businessLicenceGuid: [
        // Will never be patched when the form is built, and is
        // only updated based on a document upload occurring.
        //
        // NOTE: Direct access only through getter
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
        [Validators.required],
        asyncValidator(this.checkPecIsAssignable(), 'assignable')
      ],
      activeBeforeRegistration: [
        false,
        []
      ]
    });
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecAssignable(this.siteId, value);
  }
}
