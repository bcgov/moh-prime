import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';
import { RouterEvent } from '@angular/router';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';
import { Address } from '@shared/models/address.model';

import { Party } from '@registration/shared/models/party.model';
import { Site } from '@registration/shared/models/site.model';

@Injectable({
  providedIn: 'root'
})
export class SiteFormStateServiceService {
  public siteAddressForm: FormGroup;
  public hoursOperationForm: FormGroup;
  public vendorForm: FormGroup;
  public administratorPharmaNetForm: FormGroup;
  public privacyOfficerForm: FormGroup;
  public technicalSupportForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    // Initialize and configure the forms
    this.siteAddressForm = this.buildSiteAddressForm();
    this.hoursOperationForm = this.buildHoursOperationForm();
    this.vendorForm = this.buildVendorForm();
    this.administratorPharmaNetForm = this.buildAdministratorPharmaNetForm();
    this.privacyOfficerForm = this.buildPrivacyOfficerForm();
    this.technicalSupportForm = this.buildTechnicalSupportForm();
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls.
   */
  public set site(site: Site) {
    // TODO store IDs in the form groups
    this.patchForm(site);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get site(): Site {
    const physicalAddress = this.siteAddressForm.getRawValue();
    const businessHours = this.hoursOperationForm.getRawValue().businessDays;
    const vendor = this.vendorForm.getRawValue();

    // Adapt data for backend consumption
    // TODO are these needed now?
    // if (!physicalAddress.id) {
    //   physicalAddress.id = 0;
    // }
    // if (!vendor.id) {
    //   vendor.id = 0;
    // }

    const [
      administratorPharmaNet,
      privacyOfficer,
      technicalSupport
    ] = [
      this.administratorPharmaNetForm.getRawValue(),
      this.privacyOfficerForm.getRawValue(),
      this.technicalSupportForm.getRawValue()
    ].map((party: Party) => {
      if (!party.id) {
        party.id = 0;
      }
      if (!party.firstName) {
        party = null;
      } else if (!party.physicalAddress.street) {
        party.physicalAddress = null;
      } else if (!party.physicalAddress.id) {
        party.physicalAddress.id = 0;
      }

      return party;
    });

    return {
      // id: this.siteId,
      // locationId: this.locationId,
      // location: {
      //   id: this.locationId,
      //   privacyOfficerId: privacyOfficer?.id,
      //   privacyOfficer,
      //   administratorPharmaNetId: administratorPharmaNet?.id,
      //   administratorPharmaNet,
      //   technicalSupportId: technicalSupport?.id,
      //   technicalSupport,
      //   organizationId: organizationInformation?.id,
      //   physicalAddressId: physicalAddress?.id,
      //   physicalAddress,
      //   businessHours
      // },
      // vendorId: vendor?.id,
      // vendor,
      // provisionerId: this.provisionerId
    } as Site; // TODO drop this after uncomment block
  }

  public get isValid() {
    return this.forms
      .reduce((valid: boolean, form: AbstractControl) => valid && form.valid, true);
  }

  public get isDirty(): boolean {
    return this.forms
      .reduce((dirty: boolean, form: AbstractControl) => dirty || form.dirty, false);
  }

  public markAsPristine(): void {
    this.forms
      .forEach((form: AbstractControl) => form.markAsPristine());
  }

  /**
   * @description
   * Helper for getting a list of organization forms.
   */
  private get forms(): AbstractControl[] {
    return [
      this.siteAddressForm,
      this.hoursOperationForm,
      this.vendorForm,
      this.administratorPharmaNetForm,
      this.privacyOfficerForm,
      this.technicalSupportForm
    ];
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  // TODO refactor into separate adapters
  private patchForm(site: Site): Site {
    if (!site) {
      return null;
    }

    if (site.location.physicalAddress) {
      this.siteAddressForm.patchValue(site.location.physicalAddress);
    }
    if (site.vendor) {
      this.vendorForm.patchValue(site.vendor);
    }

    if (site.location.businessHours?.length) {
      const array = this.hoursOperationForm.get('businessDays') as FormArray;
      array.clear(); // Clear out existing indices
      this.formUtilsService.formArrayPush(array, site.location.businessHours);
    }

    // TODO duplicated until services are completely split apart
    [
      [this.administratorPharmaNetForm, site.location.administratorPharmaNet],
      [this.privacyOfficerForm, site.location.privacyOfficer],
      [this.technicalSupportForm, site.location.technicalSupport]
    ]
      .filter(([formGroup, data]: [FormGroup, Party]) => data)
      .forEach(([formGroup, data]: [FormGroup, Party]) => {
        const { physicalAddress, ...party } = data;

        formGroup.patchValue(party);

        const physicalAddressFormGroup = formGroup.get('physicalAddress');
        (physicalAddress)
          ? physicalAddressFormGroup.patchValue(physicalAddress)
          : physicalAddressFormGroup.reset();
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
      businessDays: this.fb.array(
        [],
        [Validators.required])
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

  private buildAdministratorPharmaNetForm(): FormGroup {
    return this.partyFormGroup();
  }

  private buildPrivacyOfficerForm(): FormGroup {
    return this.partyFormGroup();
  }

  private buildTechnicalSupportForm(): FormGroup {
    return this.partyFormGroup();
  }

  // TODO duplicated until services are completely split apart
  private partyFormGroup(disabled: boolean = false): FormGroup {
    return this.fb.group({
      id: [
        0,
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
