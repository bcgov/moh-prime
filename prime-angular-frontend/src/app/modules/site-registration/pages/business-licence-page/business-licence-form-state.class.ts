import { UntypedFormBuilder, UntypedFormControl, Validators, ValidatorFn, UntypedFormGroup, ValidationErrors } from '@angular/forms';

import { Observable, of } from 'rxjs';

import { asyncValidator } from '@lib/validators/form-async.validators';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { SiteResource } from '@core/resources/site-resource.service';

import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { BusinessLicenceForm } from './business-licence-form.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { SiteService } from '@registration/shared/services/site.service';
import { Site } from '@registration/shared/models/site.model';

export class BusinessLicenceFormState extends AbstractFormState<BusinessLicenceForm> {
  private siteId: number;
  private businessLicence: BusinessLicence;
  private businessLicenceUpdated: boolean;

  public constructor(
    private fb: UntypedFormBuilder,
    private siteResource: SiteResource,
    private formUtilsService: FormUtilsService,
    private siteService: SiteService,
  ) {
    super();
    this.businessLicenceUpdated = false;
    this.buildForm();
  }

  public get filename(): UntypedFormControl {
    return this.formInstance.get('filename') as UntypedFormControl;
  }

  public get businessLicenceGuid(): UntypedFormControl {
    return this.formInstance.get('businessLicenceGuid') as UntypedFormControl;
  }

  public get businessLicenceExpiry(): UntypedFormControl {
    return this.formInstance.get('expiryDate') as UntypedFormControl;
  }

  public get deferredLicenceReason(): UntypedFormControl {
    return this.formInstance.get('deferredLicenceReason') as UntypedFormControl;
  }

  public get doingBusinessAs(): UntypedFormControl {
    return this.formInstance.get('doingBusinessAs') as UntypedFormControl;
  }

  public get pec(): UntypedFormControl {
    return this.formInstance.get('pec') as UntypedFormControl;
  }

  public get physicalAddress(): UntypedFormGroup {
    return this.formInstance.get('physicalAddress') as UntypedFormGroup;
  }

  public get json(): BusinessLicenceForm {
    if (!this.formInstance) {
      return;
    }

    const { expiryDate, deferredLicenceReason, doingBusinessAs, pec, activeBeforeRegistration, physicalAddress, isNewWithSiteId, isNewWithoutSiteId, careSettingCode, deviceProviderId } = this.formInstance.getRawValue();

    const isNew = isNewWithSiteId || isNewWithoutSiteId;

    return {
      businessLicence: {
        ...this.businessLicence,
        expiryDate,
        deferredLicenceReason
      },
      doingBusinessAs,
      pec,
      activeBeforeRegistration,
      physicalAddress,
      isNew,
      careSettingCode,
      deviceProviderId
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

    const { doingBusinessAs, pec, businessLicence, physicalAddress, isNew, careSettingCode, activeBeforeRegistration, deviceProviderId } = model;
    // Preserve the business licence for use when
    // creating JSON format from the form
    this.businessLicence = businessLicence;

    const isNewWithSiteId = isNew && pec && pec !== '';
    const isNewWithoutSiteId = isNew && (!pec || pec === '');

    this.formInstance.patchValue({
      ...businessLicence,
      doingBusinessAs,
      pec,
      physicalAddress,
      isNewWithSiteId,
      isNewWithoutSiteId,
      careSettingCode,
      activeBeforeRegistration,
      deviceProviderId
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
      filename: [null, []],
      businessLicenceGuid: [
        // Will never be patched when the form is built, and is
        // only updated based on a document upload occurring.
        //
        // NOTE: Direct access only through getter
        null,
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
        [],
        asyncValidator(this.checkPecIsAssignable(), 'assignable')
      ],
      activeBeforeRegistration: [
        false,
        []
      ],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        areDisabled: ['provinceCode', 'countryCode'],
        useDefaults: ['provinceCode', 'countryCode'],
        exclude: ['street2']
      }),
      isNewWithSiteId: [
        false,
        []
      ],
      isNewWithoutSiteId: [
        false,
        []
      ],
      careSettingCode: [
        null,
        []
      ],
      deviceProviderId: [
        null,
        []
      ],
    });
    this.formInstance.setValidators(this.validateMinOneCheckboxChecked());
  }

  public resetSiteId(): void {
    if (this.pec.value) {
      this.pec.setValue("");
    }
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => value ? this.siteResource.pecAssignable(this.siteId, value) : of(true);
  }

  public validateMinOneCheckboxChecked(): ValidatorFn {
    return (form: UntypedFormGroup): ValidationErrors | null => {

      const isNewWSiteId = form.get("isNewWithSiteId");
      const isNewWOSiteId = form.get("isNewWithoutSiteId");
      const activeBeforeRegistration = form.get("activeBeforeRegistration");
      const careSettingCode = form.get("careSettingCode");

      if ((careSettingCode.value === CareSettingEnum.COMMUNITY_PHARMACIST || careSettingCode.value === CareSettingEnum.DEVICE_PROVIDER) &&
        !(isNewWOSiteId.value || isNewWSiteId.value || activeBeforeRegistration.value) && this.siteService.site?.approvedDate === null) {
        return { 'checkboxRequired': true };
      }

      return null;
    }
  }
}
