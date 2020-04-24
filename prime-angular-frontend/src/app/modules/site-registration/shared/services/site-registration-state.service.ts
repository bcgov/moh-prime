import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';
import { RouterEvent } from '@angular/router';

import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';
import { Address } from '@shared/models/address.model';
import { FormControlValidators } from '@shared/validators/form-control.validators';

import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';
import { SiteRoutes } from '@registration/site-registration.routes';

@Injectable({
  providedIn: 'root'
})
export class SiteRegistrationStateService {
  public organizationInformationForm: FormGroup;
  public siteAddressForm: FormGroup;
  public hoursOperationForm: FormGroup;
  public vendorForm: FormGroup;
  public signingAuthorityForm: FormGroup;
  public privacyOfficerForm: FormGroup;
  public administratorPharmaNetForm: FormGroup;
  public technicalSupportForm: FormGroup;

  private patched: boolean;
  private siteId: number;
  private provisionerId: number;
  private locationId: number;

  constructor(
    private fb: FormBuilder,
    private routeStateService: RouteStateService,
    private logger: LoggerService
  ) {
    this.organizationInformationForm = this.buildOrganizationInformationForm();
    this.siteAddressForm = this.buildSiteAddressForm();
    this.hoursOperationForm = this.buildHoursOperationForm();
    this.vendorForm = this.buildVendorForm();
    this.signingAuthorityForm = this.buildSigningAuthorityForm();
    this.privacyOfficerForm = this.buildPrivacyOfficerForm();
    this.administratorPharmaNetForm = this.buildAdministratorPharmaNetForm();
    this.technicalSupportForm = this.buildTechnicalSupportForm();

    // Initial state of the form is unpatched and ready for
    // site information
    this.patched = false;

    // TODO not needed until site review expands outwards from a single site
    // Listen for a route end that is outside of site review, and
    // reset the form model
    // this.routeStateService.onNavigationEnd()
    //   .subscribe((event: RouterEvent) => {
    //     const route = event.url.slice(event.url.lastIndexOf('/') + 1);
    //     if (!SiteRoutes.registrationRoutes().includes(route)) {
    //       this.logger.info('RESET SITE FORM');
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

      this.patchSite(site);
    }
  }

  /**
   * @description
   * Get the site as JSON for submission.
   */
  // TODO use Partial<Site>
  public get site(): Site {
    const organizationInformation = this.organizationInformationForm.getRawValue();
    const physicalAddress = this.siteAddressForm.getRawValue();
    const hoursOperation = this.hoursOperationForm.getRawValue();
    const vendor = this.vendorForm.getRawValue();
    const signingAuthority = this.signingAuthorityForm.getRawValue();
    let administratorPharmaNet = this.administratorPharmaNetForm.getRawValue();
    let privacyOfficer = this.privacyOfficerForm.getRawValue();
    let technicalSupport = this.technicalSupportForm.getRawValue();

    if (!organizationInformation.id) {
      organizationInformation.id = 0;
    }
    if (!physicalAddress.id) {
      physicalAddress.id = 0;
    }
    if (!vendor.id) {
      vendor.id = 0;
    }
    if (!administratorPharmaNet.id) {
      administratorPharmaNet.id = 0;
    }
    if (!privacyOfficer.id) {
      privacyOfficer.id = 0;
    }
    if (!technicalSupport.id) {
      technicalSupport.id = 0;
    }

    if (!signingAuthority.physicalAddress.street) {
      signingAuthority.physicalAddress = null;
    } else if (!signingAuthority.physicalAddress.id) {
      signingAuthority.physicalAddress.id = 0;
    }
    if (!administratorPharmaNet.firstName) {
      administratorPharmaNet = null;
    } else if (!administratorPharmaNet.physicalAddress.street) {
      administratorPharmaNet.physicalAddress = null;
    } else if (!technicalSupport.physicalAddress.id) {
      administratorPharmaNet.physicalAddress.id = 0;
    }
    if (!privacyOfficer.firstName) {
      privacyOfficer = null;
    } else if (!privacyOfficer.physicalAddress.street) {
      privacyOfficer.physicalAddress = null;
    } else if (!technicalSupport.physicalAddress.id) {
      privacyOfficer.physicalAddress.id = 0;
    }
    if (!technicalSupport.firstName) {
      technicalSupport = null;
    } else if (!technicalSupport.physicalAddress.street) {
      technicalSupport.physicalAddress = null;
    } else if (!technicalSupport.physicalAddress.id) {
      technicalSupport.physicalAddress.id = 0;
    }

    return {
      id: this.siteId,
      locationId: this.locationId,
      location: {
        id: this.locationId,
        privacyOfficerId: privacyOfficer?.id,
        privacyOfficer,
        administratorPharmaNetId: administratorPharmaNet?.id,
        administratorPharmaNet,
        technicalSupportId: technicalSupport?.id,
        technicalSupport,
        organizationId: organizationInformation?.id,
        organization: {
          signingAuthorityId: signingAuthority?.id,
          signingAuthority,
          ...organizationInformation
        },
        physicalAddressId: physicalAddress?.id,
        physicalAddress,
        ...hoursOperation
      },
      vendorId: vendor?.id,
      vendor,
      provisionerId: this.provisionerId,
      // TODO where is PEC coming from?
      // pec
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
    return this.vendorForm.valid;
  }

  public isSigningAuthorityValid(): boolean {
    return this.signingAuthorityForm.valid;
  }

  public isPrivacyOfficerValid(): boolean {
    return this.privacyOfficerForm.valid;
  }

  public isAdministratorValid(): boolean {
    return this.administratorPharmaNetForm.valid;
  }

  public isTechnicalSupportValid(): boolean {
    return this.technicalSupportForm.valid;
  }

  private patchSite(site: Site) {
    if (site) {
      console.log('PATCH_SITE');

      this.organizationInformationForm.patchValue(site.location.organization);
      if (site.location.physicalAddress) {
        this.siteAddressForm.patchValue(site.location.physicalAddress);
      }
      if (site.vendor) {
        this.vendorForm.patchValue(site.vendor);
      }
      this.hoursOperationForm.patchValue(site.location);
      if (site.location.organization.signingAuthority) {
        // TODO ignore physical address for now
        const { physicalAddress, ...remainder } = site.location.organization.signingAuthority;
        this.signingAuthorityForm.patchValue(remainder);
      }
      if (site.location.administratorPharmaNet) {
        // TODO ignore physical address for now
        const { physicalAddress, ...remainder } = site.location.administratorPharmaNet;
        this.administratorPharmaNetForm.patchValue(remainder);
      }
      if (site.location.privacyOfficer) {
        // TODO ignore physical address for now
        const { physicalAddress, ...remainder } = site.location.privacyOfficer;
        this.privacyOfficerForm.patchValue(remainder);
      }
      if (site.location.technicalSupport) {
        console.log('PATCH');

        // TODO ignore physical address for now
        const { physicalAddress, ...remainder } = site.location.technicalSupport;
        this.technicalSupportForm.patchValue(remainder);
      }
    }
  }

  private get forms(): FormGroup[] {
    return [
      this.organizationInformationForm,
      this.siteAddressForm,
      this.hoursOperationForm,
      this.vendorForm,
      this.signingAuthorityForm,
      this.privacyOfficerForm,
      this.administratorPharmaNetForm,
      this.technicalSupportForm
    ];
  }

  private buildOrganizationInformationForm(): FormGroup {
    return this.fb.group({
      id: [
        null,
        []
      ],
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
      id: [
        null,
        []
      ],
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

  private buildVendorForm(): FormGroup {
    return this.fb.group({
      id: [
        null,
        [Validators.required]
      ]
    });
  }

  private buildSigningAuthorityForm(): FormGroup {
    return this.partyFormGroup(true);
  }

  private buildPrivacyOfficerForm(): FormGroup {
    return this.partyFormGroup();
  }

  private buildAdministratorPharmaNetForm(): FormGroup {
    return this.partyFormGroup();
  }

  private buildTechnicalSupportForm(): FormGroup {
    return this.partyFormGroup();
  }

  private partyFormGroup(disabled: boolean = false): FormGroup {
    return this.fb.group({
      id: [
        null,
        []
      ],
      firstName: [
        { value: null, disabled },
        [Validators.required]
      ],
      lastName: [
        { value: null, disabled },
        [Validators.required]
      ],
      jobRoleTitle: [
        null,
        [Validators.required]
      ],
      phone: [
        null,
        [
          Validators.required,
          FormControlValidators.phone
        ]
      ],
      fax: [
        null,
        [
          Validators.required,
          FormControlValidators.phone
        ]
      ],
      smsPhone: [
        null,
        [
          Validators.required,
          FormControlValidators.phone
        ]
      ],
      email: [
        null,
        [
          Validators.required,
          FormControlValidators.email
        ]
      ],
      physicalAddress: this.fb.group({
        // TODO should this be null or 0?
        id: [
          0,
          []
        ],
        countryCode: [
          { value: null, disabled: false },
          []
        ],
        provinceCode: [
          { value: null, disabled: false },
          []
        ],
        street: [
          { value: null, disabled: false },
          []
        ],
        street2: [
          { value: null, disabled: false },
          []
        ],
        city: [
          { value: null, disabled: false },
          []
        ],
        postal: [
          { value: null, disabled: false },
          []
        ]
      })
    });
  }
}
