import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';
import { RouterEvent } from '@angular/router';

import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';
import { Address } from '@shared/models/address.model';

import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';

@Injectable({
  providedIn: 'root'
})
export class SiteRegistrationStateService {
  public organizationInformationForm: FormGroup;
  public siteAddressForm: FormGroup;
  public hoursOperationForm: FormGroup;
  public vendorsForm: FormGroup;
  public signingAuthorityForm: FormGroup;
  public privacyOfficerForm: FormGroup;
  public administratorForm: FormGroup;
  public technicalSupportForm: FormGroup;

  // TODO move the IDs into the forms so don't need to be tracked separately
  private patched: boolean;
  private siteId: number;
  private provisionerId: number;
  private locationId: number;
  private organizationId: number;
  private signingAuthorityId: number;
  private physicalAddressId: number;
  private physicalAddress: Address;
  private vendorId: number;

  constructor(
    private fb: FormBuilder,
    private routeStateService: RouteStateService,
    private logger: LoggerService
  ) {
    this.organizationInformationForm = this.buildOrganizationInformationForm();
    this.siteAddressForm = this.buildSiteAddressForm();
    this.hoursOperationForm = this.buildHoursOperationForm();
    this.vendorsForm = this.buildVendorsForm();
    this.signingAuthorityForm = this.buildSigningAuthorityForm();
    this.privacyOfficerForm = this.buildPrivacyOfficerForm();
    this.administratorForm = this.buildAdministratorForm();
    this.technicalSupportForm = this.buildTechnicalSupportForm();

    // Initial state of the form is unpatched and ready for
    // site information
    this.patched = false;

    // Listen for a route end that is outside of site overview, and
    // reset the form model
    // this.routeStateService.onNavigationEnd()
    //   .subscribe((event: RouterEvent) => {
    //     const route = event.url.slice(event.url.lastIndexOf('/') + 1);
    //     if (!EnrolmentRoutes.enrolmentProfileRoutes().includes(route)) {
    //       this.logger.info('RESET ENROLLEE FORM');
    //       this.forms.forEach((form: FormGroup) => form.reset());
    //       this.patched = false;
    //     }
    //   });
  }

  public get isPatched() {
    return this.patched;
  }

  /**
   * @description
   * Store the site JSON and populate the site form, which can
   * only be set more than once when explicitly forced.
   */
  public setSite(site: Site, force: boolean = false) {
    if (!this.patched || force) {
      // Indicate that the form is patched, and may contain unsaved information
      this.patched = true;

      this.siteId = site.id;
      this.provisionerId = site.provisionerId;
      this.locationId = site.locationId;
      this.organizationId = site.location?.organizationId;
      this.signingAuthorityId = site.location?.organization.signingAuthorityId;
      this.physicalAddressId = site.location?.physicalAddressId;
      this.physicalAddress = site.location?.physicalAddress;
      this.vendorId = site.vendorId;

      this.patchSite(site);
    }
  }

  /**
   * @description
   * Get the site as JSON for submission.
   */
  // TODO use Partial<Site>
  public get site(): Site {
    const id = this.siteId;

    const organizationInformation = this.organizationInformationForm.getRawValue();
    const siteAddress = this.siteAddressForm.getRawValue();
    const hoursOperation = this.hoursOperationForm.getRawValue();
    const vendor = this.vendorsForm.getRawValue();
    const signingAuthority = this.signingAuthorityForm.getRawValue();
    const privacyOfficer = this.privacyOfficerForm.getRawValue();
    const administrator = this.administratorForm.getRawValue();
    const technicalSupport = this.technicalSupportForm.getRawValue();

    return {
      id,
      locationId: this.locationId,
      location: {
        ...privacyOfficer,
        ...administrator,
        ...technicalSupport,
        organizationId: this.organizationId,
        organization: {
          signingAuthorityId: this.signingAuthorityId,
          ...signingAuthority,
          ...organizationInformation
        },
        physicalAddressId: this.physicalAddressId,
        physicalAddress: siteAddress,
        ...hoursOperation
      },
      vendorId: this.vendorId,
      vendor,
      provisionerId: this.provisionerId,
      // pec
      // completed
      // approvedDate
    } as Site; // Force type definition
  }

  public get isDirty(): boolean {
    return this.forms.reduce(
      (isDirty: boolean, form: FormGroup) => isDirty || form.dirty, false
    );
  }

  public markAsPristine(): void {
    this.forms.forEach((form: FormGroup) => form.markAsPristine());
  }

  public isSiteValid(): boolean {
    return (
      this.isOrganizationInformationValid() &&
      this.isSiteAddressValid() &&
      this.isHoursOperationValid() &&
      this.isVendorValid() &&
      this.isSigningAuthorityValid() &&
      this.isPrivacyOfficerValid() &&
      this.isAdministratorValid() &&
      this.isTechnicalSupportValid()
    );
  }

  public isOrganizationInformationValid(): boolean {
    return this.organizationInformationForm.valid;
  }

  public isSiteAddressValid(): boolean {
    return this.siteAddressForm.valid;
  }

  public isHoursOperationValid(): boolean {
    return this.hoursOperationForm.valid;
  }

  public isVendorValid(): boolean {
    return this.vendorsForm.valid;
  }

  public isSigningAuthorityValid(): boolean {
    return this.signingAuthorityForm.valid;
  }

  public isPrivacyOfficerValid(): boolean {
    return this.privacyOfficerForm.valid;
  }

  public isAdministratorValid(): boolean {
    return this.administratorForm.valid;
  }

  public isTechnicalSupportValid(): boolean {
    return this.technicalSupportForm.valid;
  }

  private patchSite(site: Site) {

  }

  private get forms(): FormGroup[] {
    return [
      this.organizationInformationForm,
      this.siteAddressForm,
      this.hoursOperationForm,
      this.vendorsForm,
      this.signingAuthorityForm,
      this.privacyOfficerForm,
      this.administratorForm,
      this.technicalSupportForm
    ];
  }

  private buildOrganizationInformationForm(): FormGroup {
    return this.fb.group({
      name: [
        null,
        [Validators.required]
      ],
      doingBusinessAs: [
        null,
        []
      ]
    });
  }

  private buildSiteAddressForm(): FormGroup {
    return this.fb.group({
      street: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      city: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      provinceCode: [
        { value: Province.BRITISH_COLUMBIA, disabled: true },
        [Validators.required]
      ],
      postal: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      countryCode: [
        { value: Country.CANADA, disabled: true },
        [Validators.required]
      ],
    });
  }

  private buildHoursOperationForm(): FormGroup {
    return this.fb.group({
      hoursWeekend: [
        false,
        []
      ],
      hours24: [
        false,
        []
      ],
      hoursSpecial: [
        null,
        []
      ]
    });
  }

  private buildVendorsForm(): FormGroup {
    return this.fb.group({
      // TODO choose multiples, but schema doesn't allow for it
      // vendors: this.fb.array([])
      // TODO should send the vendor ID not name now that vendors are in the DB
      name: [
        null,
        [Validators.required]
      ]
    });
  }

  private buildSigningAuthorityForm(): FormGroup {
    return this.fb.group({});
  }

  private buildPrivacyOfficerForm(): FormGroup {
    return this.fb.group({});
  }

  private buildAdministratorForm(): FormGroup {
    return this.fb.group({});
  }

  private buildTechnicalSupportForm(): FormGroup {
    return this.fb.group({});
  }
}
