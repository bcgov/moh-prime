import { FormBuilder, FormControl, Validators, ValidatorFn, FormGroup, ValidationErrors } from '@angular/forms';

import { Observable, of } from 'rxjs';

import { asyncValidator } from '@lib/validators/form-async.validators';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { SiteResource } from '@core/resources/site-resource.service';

import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { BusinessLicenceForm } from './business-licence-form.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

export class BusinessLicenceFormState extends AbstractFormState<BusinessLicenceForm> {
  private siteId: number;
  private businessLicence: BusinessLicence;
  private businessLicenceUpdated: boolean;

  public constructor(
    private fb: FormBuilder,
    private siteResource: SiteResource,
    private formUtilsService: FormUtilsService,
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

  public get physicalAddress(): FormGroup {
    return this.formInstance.get('physicalAddress') as FormGroup;
  }

  public get json(): BusinessLicenceForm {
    if (!this.formInstance) {
      return;
    }

    const { expiryDate, deferredLicenceReason, doingBusinessAs, pec, activeBeforeRegistration, physicalAddress, isNewWithSiteId, isNewWithoutSiteId, careSettingCode } = this.formInstance.getRawValue();

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
      careSettingCode
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

    const { doingBusinessAs, pec, businessLicence, physicalAddress, isNew, careSettingCode } = model;
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
      careSettingCode
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
    });
    this.formInstance.setValidators(this.validateMinOneCheckboxChecked());
  }

  public presetCommunityPharmacySiteId(): void {
    if (!this.pec.value || this.pec.value === "") {
      this.pec.setValue("BC00000");
    }
  }

  public resetSiteId(): void {
    if (this.pec.value && this.pec.value === "BC00000") {
      this.pec.setValue("");
    }
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => value ? this.siteResource.pecAssignable(this.siteId, value) : of(true);
  }

  public validateMinOneCheckboxChecked(): ValidatorFn {
    return (form: FormGroup): ValidationErrors | null => {

      const isNewWSiteId = form.get("isNewWithSiteId");
      const isNewWOSiteId = form.get("isNewWithoutSiteId");
      const activeBeforeRegistration = form.get("activeBeforeRegistration");
      const careSettingCode = form.get("careSettingCode");

      if (careSettingCode.value === CareSettingEnum.COMMUNITY_PHARMACIST &&
        !(isNewWOSiteId.value || isNewWSiteId.value || activeBeforeRegistration.value)) {
        return { 'checkboxRequired': true };
      }

      return null;
    }
  }
}
